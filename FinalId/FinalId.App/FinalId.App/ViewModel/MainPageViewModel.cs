// <copyright file="MainPageViewModel.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.App.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using FinalId.App.Components;
    using FinalId.App.MVVMHelpers;
    using FinalId.Core;
    using Xamarin.Forms;

    public class MainPageViewModel : ViewModelBase, IIdentityVerificationReceiver
    {
        private Identity _verifiedIdentity;
        private ObservableCollection<Identity> _endorsedIdentities = new ObservableCollection<Identity>();

        public ICommand SelectEndorsedIdentityCommand
        {
            get
            {
                return new Command<Identity>(async (model) =>
                {
                    await this.SelectEndorsedIdentity(model);
                });
            }
        }

        public ICommand VerifyIDCommand
        {
            get
            {
                return new AsyncCommand(this.VerifyID);
            }
        }

        public ICommand ShowIDCommand
        {
            get
            {
                return new AsyncCommand(this.ShowID);
            }
        }

        public ICommand AppoveNewDeviceCommand
        {
            get
            {
                return new AsyncCommand(this.AppoveNewDevice);
            }
        }

        public ICommand AcceptEndorsementCommand
        {
            get
            {
                return new AsyncCommand(this.AcceptEndorsement);
            }
        }

        public ObservableCollection<Identity> EndorsedIdentities
        {
            get
            {
                return _endorsedIdentities;
            }
        }

        public override async Task NavigatedToAsync()
        {
            if (_verifiedIdentity != null)
            {
                var verifiedIdentity = _verifiedIdentity;
                _verifiedIdentity = null;
                await NavigationMaster.Instance.NavigateTo(new VerifiedIDInfoViewModel(verifiedIdentity));
            }

            (await LocalIdentityStore.Instance.GetAllIdentities()).ForEach(identity => _endorsedIdentities.Insert(_endorsedIdentities.Count, identity));
        }

        public async Task SelectEndorsedIdentity(Identity endorsedIdentity)
        {
            await NavigationMaster.Instance.NavigateTo(new VerifiedIDInfoViewModel(endorsedIdentity));
        }

        public async Task VerifyID()
        {
            await NavigationMaster.Instance.NavigateTo(new VerifyIDViewModel(this, this));
        }

        public async Task ShowID()
        {
            await NavigationMaster.Instance.NavigateTo(new ShowIDViewModel(this));
        }

        public async Task AppoveNewDevice()
        {
        }

        public async Task AcceptEndorsement()
        {
            Endorsement endorsement;
            KeyPair publicKey = null; // assigning here to make compiler happy
            Guid guid;

            var readEndorsement = new ScanQRCodeViewModel(
                async (string result) =>
                {
                    endorsement = Endorsement.GetFromJSONString(result);
                    Identity endorserIdentity;

                    endorserIdentity = await LocalIdentityStore.Instance.GetIdentity(guid);

                    if (endorserIdentity == null)
                    {
                        endorserIdentity = new Identity(guid);
                    }

                    endorserIdentity.KeyPairsAssertingOwnership.Add(publicKey);

                    if (endorserIdentity.IsValidEndorsement(endorsement))
                    {
                        LocalIdentityStore.Instance.StoreIdentity(endorserIdentity);
                        LocalIdentityStore.Instance.AcceptEndorsement(endorsement);
                    }
                },
                new MainPageViewModel(),
                "Scan endorsement");

            var readPublicKey = new ScanQRCodeViewModel((byte[] result) => { publicKey = new KeyPair(result); }, readEndorsement, "Scan Public Key");
            var readGuid = new ScanQRCodeViewModel((string result) => { guid = Guid.Parse(result); }, readPublicKey, "Scan ID");

            await NavigationMaster.Instance.NavigateTo(readGuid);
        }

        void IIdentityVerificationReceiver.VerifiedIdImport(Identity verifiedIdentity)
        {
            _verifiedIdentity = verifiedIdentity;
        }
    }
}
