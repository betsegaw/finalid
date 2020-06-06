// <copyright file="VerifyIDViewModel.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.App.ViewModel
{
    using System;
    using System.Threading.Tasks;
    using FinalId.App.Components;
    using FinalId.App.MVVMHelpers;
    using FinalId.Core;

    public class VerifyIDViewModel : ViewModelBase
    {
        private Guid _id;
        private byte[] _publicKey;
        private byte[] _signedChallenge;
        private string _challenge;

        private IIdentityVerificationReceiver _verifiedPublicKeyReceiver;
        private ViewModelBase _postVerificationNavigationTarget;

        public VerifyIDViewModel(IIdentityVerificationReceiver verifiedPublicKeyReceiver, ViewModelBase postVerificationNavigationTarget)
        {
            _verifiedPublicKeyReceiver = verifiedPublicKeyReceiver;
            _postVerificationNavigationTarget = postVerificationNavigationTarget;
        }

        public override async Task NavigatedToAsync()
        {
            if (_id != null && _publicKey != null && _signedChallenge != null)
            {
                var friendPublicKey = new KeyPair(_publicKey);

                if (friendPublicKey.Verify(_challenge, _signedChallenge))
                {
                    var verifiedIdentity = new Identity(_id);
                    verifiedIdentity.KeyPairsAssertingOwnership.Add(friendPublicKey);
                    _verifiedPublicKeyReceiver.VerifiedIdImport(verifiedIdentity);
                }

                await NavigationMaster.Instance.NavigateTo(_postVerificationNavigationTarget);
            }
            else
            {
                _challenge = Guid.NewGuid().ToString();

                QRDisplayViewModel qrChallengeDisplay = new QRDisplayViewModel(_challenge, "Challenge");

                var hashScanner = new ScanQRCodeViewModel((signedChallenge) => { _signedChallenge = signedChallenge; }, this, "Scan your friend's signed challenge!");

                var publicKeyScanner = new ScanQRCodeViewModel((publicKey) => { _publicKey = publicKey; }, hashScanner, "Scan your friend's Public key");

                var idScanner = new ScanQRCodeViewModel((id) => { _id = Guid.Parse(id); }, publicKeyScanner, "Scan your friend's id");

                qrChallengeDisplay.PostDisplayComplete = idScanner;

                await NavigationMaster.Instance.NavigateTo(qrChallengeDisplay);
            }
        }
    }
}
