// <copyright file="Actions.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.Tests
{
    using System;
    using System.Linq;
    using FinalId.App.Components;
    using FinalId.App.ViewModel;
    using FinalId.Core;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public class Actions
    {
        public static async void NavigateToMainPageFromLaunch(string existringId = null)
        {
            string id;

            var launchPage = (FirstTimeLaunchPageViewModel)NavigationMaster.Instance.CurrentViewModel;

            if (existringId != null)
            {
                id = existringId;

                launchPage.GetExistingID().Wait();

                Assert.AreEqual(
                    typeof(ScanQRCodeViewModel),
                    NavigationMaster.Instance.CurrentViewModel.GetType(),
                    "Did not reach the active page within 10 seconds.");

                ScanQRCodeViewModel.TestStringValueToReturn = existringId;

                ((ScanQRCodeViewModel)NavigationMaster.Instance.CurrentViewModel).StartScan().Wait();
            }
            else
            {
                launchPage.RequestNewID().Wait();
                id = (await LocalIdentityStore.Instance.GetCurrentIdentity()).IdentityGUID.ToString();
            }

            Assert.AreEqual(
                typeof(MainPageViewModel),
                NavigationMaster.Instance.CurrentViewModel.GetType(),
                "Did not reach the main page.");

            Assert.AreNotEqual(
                (await LocalIdentityStore.Instance.GetCurrentIdentity()).IdentityGUID,
                Guid.Empty,
                "Current user has not been assigned a non-empty identity");

            Assert.AreEqual(
                (await LocalIdentityStore.Instance.GetCurrentIdentity()).IdentityGUID.ToString(),
                id,
                "Current user's identity is not correct");

            Assert.AreEqual(
                (await LocalIdentityStore.Instance.GetCurrentIdentity()).Endorsements.Count,
                1,
                "Current user does not have a single self-endorsement");

            Assert.AreEqual(
                (await LocalIdentityStore.Instance.GetCurrentIdentity()).Endorsements.First().EndorserGUID,
                (await LocalIdentityStore.Instance.GetCurrentIdentity()).IdentityGUID,
                "Current user has not self-endorsed");
        }

        public static async void ShowIdFromMainPage(string challenge)
        {
            var mainPageViewModel = (MainPageViewModel)NavigationMaster.Instance.CurrentViewModel;

            mainPageViewModel.ShowID().Wait();

            Assert.AreEqual(
                typeof(ScanQRCodeViewModel),
                NavigationMaster.Instance.CurrentViewModel.GetType(),
                "Showing ID did not get to challenge scanning page");

            ScanQRCodeViewModel.TestStringValueToReturn = challenge;

            ((ScanQRCodeViewModel)NavigationMaster.Instance.CurrentViewModel).StartScan().Wait();

            Assert.AreEqual(
                typeof(QRDisplayViewModel),
                NavigationMaster.Instance.CurrentViewModel.GetType(),
                "Showing ID did not reach the id display page");

            var idDisplay = (QRDisplayViewModel)NavigationMaster.Instance.CurrentViewModel;

            Assert.AreEqual(
                idDisplay.Content,
                (await LocalIdentityStore.Instance.GetCurrentIdentity()).IdentityGUID.ToString(),
                "The Identity being displayed is not correct");

            idDisplay.ScanComplete().Wait();

            Assert.AreEqual(
                typeof(QRDisplayViewModel),
                NavigationMaster.Instance.CurrentViewModel.GetType(),
                "Showing ID did not reach the public key display page");

            var publicKeyDisplay = (QRDisplayViewModel)NavigationMaster.Instance.CurrentViewModel;

            Assert.AreEqual(
                publicKeyDisplay.Content,
                BitConverter.ToString(KeyPair.TestKeyPair.PublicKeyBytes),
                "Public key being displayed is not correct");

            publicKeyDisplay.ScanComplete().Wait();

            Assert.AreEqual(
                typeof(QRDisplayViewModel),
                NavigationMaster.Instance.CurrentViewModel.GetType(),
                "Showing ID did not reach the signed challenge display page.");

            var signedChallenge = (QRDisplayViewModel)NavigationMaster.Instance.CurrentViewModel;

            Assert.AreEqual(
                signedChallenge.Content,
                BitConverter.ToString(KeyPair.TestKeyPair.Sign(challenge)),
                "Showing ID did not display the correct signed challenge.");

            signedChallenge.ScanComplete().Wait();

            Assert.AreEqual(
                typeof(MainPageViewModel),
                NavigationMaster.Instance.CurrentViewModel.GetType(),
                "Show ID did not end up back at the main page.");
        }

        public static async void VerifyIdFromMainPage(string id, KeyPair keyPair)
        {
            var mainPageViewModel = (MainPageViewModel)NavigationMaster.Instance.CurrentViewModel;

            mainPageViewModel.VerifyID().Wait();

            var challengeDisplay = (QRDisplayViewModel)NavigationMaster.Instance.CurrentViewModel;

            var challenge = challengeDisplay.Content;

            challengeDisplay.ScanComplete().Wait();

            var idScanner = (ScanQRCodeViewModel)NavigationMaster.Instance.CurrentViewModel;

            ScanQRCodeViewModel.TestStringValueToReturn = id;

            idScanner.StartScan().Wait();

            var publicKeyScanner = (ScanQRCodeViewModel)NavigationMaster.Instance.CurrentViewModel;

            ScanQRCodeViewModel.TestByteValueToReturn = keyPair.PublicKeyBytes;

            publicKeyScanner.StartScan().Wait();

            var signedSignatureScanner = (ScanQRCodeViewModel)NavigationMaster.Instance.CurrentViewModel;

            ScanQRCodeViewModel.TestByteValueToReturn = keyPair.Sign(challenge);

            signedSignatureScanner.StartScan().Wait();

            Assert.AreEqual(
                typeof(VerifiedIDInfoViewModel),
                NavigationMaster.Instance.CurrentViewModel.GetType(),
                "Verifying ID did not reach the Verified ID Display page");

            var verifiedIdPage = (VerifiedIDInfoViewModel)NavigationMaster.Instance.CurrentViewModel;

            Assert.AreEqual(
                verifiedIdPage.ID,
                id + "\n" + keyPair.PublicKeyString,
                "Current user has not self-endorsed");
        }
    }
}
