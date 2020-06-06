// <copyright file="VerifiedIDInfoViewModel.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.App.ViewModel
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;
    using FinalId.App.Components;
    using FinalId.App.MVVMHelpers;
    using FinalId.Core;

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
            // Do some endorsement

            // Navigate back to main page
            await NavigationMaster.Instance.NavigateTo(new MainPageViewModel());
        }
    }
}
