// <copyright file="ShowIDViewModel.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.App.ViewModel
{
    using System.Linq;
    using System.Threading.Tasks;
    using FinalId.App.Components;
    using FinalId.App.MVVMHelpers;
    using FinalId.Core;

    public class ShowIDViewModel : MVVMHelpers.ViewModelBase
    {
        private string _challenge;
        private bool _showingIdKeyAndSignedChallenge;
        private ViewModelBase _postShowIDTarget;

        public ShowIDViewModel(ViewModelBase postShowIDTarget)
        {
            _postShowIDTarget = postShowIDTarget;
        }

        public async override Task NavigatedToAsync()
        {
            if (_challenge == null)
            {
                var challengeScanner = new ScanQRCodeViewModel((result) => { _challenge = result; }, this, "Scan the challenge from your friend's phone");
                await NavigationMaster.Instance.NavigateTo(challengeScanner);
            }
            else if (_showingIdKeyAndSignedChallenge)
            {
                await NavigationMaster.Instance.NavigateTo(_postShowIDTarget);
            }
            else
            {
                _showingIdKeyAndSignedChallenge = true;
                KeyPair keyPair = (await LocalIdentityStore.Instance.GetCurrentIdentity()).KeyPairsAssertingOwnership.First();
                byte[] signedChallenge = keyPair.Sign(_challenge);
                byte[] publicKey = keyPair.PublicKeyBytes;

                var idDisplay = new QRDisplayViewModel((await LocalIdentityStore.Instance.GetCurrentIdentity()).IdentityGUID.ToString(), "ID");
                var publicKeyDisplay = new QRDisplayViewModel(publicKey, "Public Key");
                var signedChallengeDisplay = new QRDisplayViewModel(signedChallenge, "Signed Challenge");

                idDisplay.PostDisplayComplete = publicKeyDisplay;
                publicKeyDisplay.PostDisplayComplete = signedChallengeDisplay;
                signedChallengeDisplay.PostDisplayComplete = this;

                await NavigationMaster.Instance.NavigateTo(idDisplay);
            }
        }
    }
}
