// <copyright file="VerifiedIDInfoViewModel.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.App.ViewModel
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using FinalId.App.Components;
    using FinalId.App.MVVMHelpers;
    using FinalId.Core;
    using Newtonsoft.Json;

    public class VerifiedIDInfoViewModel : MVVMHelpers.ViewModelBase
    {
        private string _id;
        private Identity _verifiedIdentity;

        public VerifiedIDInfoViewModel(Identity identity)
        {
            this.ID = identity.IdentityGUID + "\n" + identity.KeyPairsAssertingOwnership.First().PublicKeyString;
            this._verifiedIdentity = identity;
        }

        public ICommand EndorseCommand
        {
            get
            {
                return new RelayCommand(this.Endorse) { IsEnabled = true };
            }
        }

        public ICommand VerifyEndorsementCommand
        {
            get
            {
                return new RelayCommand(this.VerifyEndorsement) { IsEnabled = true };
            }
        }

        public string ID
        {
            get
            {
                return _id;
            }

            set
            {
                if (value != _id)
                {
                    _id = value;
                    NotifyPropertyChanged("ID");
                }
            }
        }

        public async void Endorse()
        {
            // Do some endorsement
            var endorsement = LocalIdentityStore.Instance.EndorseFriendAsIdentityOwner(_verifiedIdentity.IdentityGUID, _verifiedIdentity.KeyPairsAssertingOwnership.First().Fingerprint);

            var identityFriendlyName = new InputPageViewModel(
                new List<InputViewModel>()
                {
                    new InputViewModel() { Label = "Friendly Name", Content = string.Empty },
                },
                async (inputs) =>
                {
                    _verifiedIdentity.FriendlyName = inputs.First().Content;
                    _verifiedIdentity.Endorsements.Add(endorsement);
                    await LocalIdentityStore.Instance.StoreIdentity(_verifiedIdentity);
                },
                new MainPageViewModel());

            // Share endorsement
            var currentIdentity = await LocalIdentityStore.Instance.GetCurrentIdentity();
            var idDisplay = new QRDisplayViewModel(currentIdentity.IdentityGUID.ToString(), "ID");
            var publicKeyDisplay = new QRDisplayViewModel(currentIdentity.KeyPairsAssertingOwnership.First().PublicKeyBytes, "Public Key");
            var endorsementDisplay = new QRDisplayViewModel(endorsement.ToString(), "Endorsement");

            idDisplay.PostDisplayComplete = publicKeyDisplay;
            publicKeyDisplay.PostDisplayComplete = endorsementDisplay;
            endorsementDisplay.PostDisplayComplete = identityFriendlyName;

            await NavigationMaster.Instance.NavigateTo(idDisplay);
        }

        public async void VerifyEndorsement()
        {
            ObservableCollection<ListItemViewModel> identitiesInCommon = new ObservableCollection<ListItemViewModel>();

            ListPageViewModel displayListOfCommonIdentities = new ListPageViewModel(identitiesInCommon, this);

            // Do some endorsement
            ScanQRCodeViewModel scanListOfEndorsedGuids = new ScanQRCodeViewModel(
                (string endorsementsAsString) =>
                {
                    List<string> endorsingIDs = JsonConvert.DeserializeObject<List<string>>(endorsementsAsString);

                    IEnumerable<Identity> intersectionOfIdentities = LocalIdentityStore.Instance.GetAllIdentities().Result.Where(x => endorsingIDs.Contains(x.IdentityGUID.ToString()));

                    foreach (var identity in intersectionOfIdentities)
                    {
                        identitiesInCommon.Add(new ListItemViewModel(identity, identity.FriendlyName, () =>
                        {
                            MessagePageViewModel verificationResult = new MessagePageViewModel(string.Empty, displayListOfCommonIdentities);

                            QRDisplayViewModel displaySelectedIdentityQR = new QRDisplayViewModel(
                                ((Identity)identity).IdentityGUID.ToString(),
                                "Display selected identity");

                            ScanQRCodeViewModel scanEndorsement = new ScanQRCodeViewModel(
                                (string endorsement) =>
                                {
                                    var endorsementIsValid = identity.IsValidEndorsement(Endorsement.GetFromJSONString(endorsement));
                                    if (endorsementIsValid)
                                    {
                                        verificationResult.Message = "Correct";
                                    }
                                    else
                                    {
                                        verificationResult.Message = "Wrong";
                                    }
                                },
                                verificationResult,
                                "Scan endorsement");

                            displaySelectedIdentityQR.PostDisplayComplete = scanEndorsement;

                            NavigationMaster.Instance.NavigateTo(displaySelectedIdentityQR);
                        }));
                    }
                },
                displayListOfCommonIdentities,
                "Scan list of endorsements");

            // Navigate back to main page
            await NavigationMaster.Instance.NavigateTo(scanListOfEndorsedGuids);
        }
    }
}
