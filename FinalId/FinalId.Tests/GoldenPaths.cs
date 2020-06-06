// <copyright file="GoldenPaths.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.Tests
{
    using FinalId.App.Components;
    using FinalId.App.ViewModel;
    using FinalId.Core;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GoldenPaths
    {
        [TestInitialize]
        public void Initialize()
        {
            NavigationMaster.ResetForTest();
            NavigationMaster.Instance.NavigateToAppLaunchedPage();
            KeyPair.TestKeyPair = new KeyPair("{\"Modulus\":\"xC4wh8tPJc1937gmXilWs5G/E6jpuUdHWF/Voo76R0KXPG8dUw16PE7iz+j/2nyWw4hdGIazVI7OY4Ffpy0GU8llZtIN4mu+k1LFW5cp6fqKWkoYnazMSA5BLegRs5sTfNItCKDrpUkDMCCokVXeWjbK0QeGyc1w9vDx3m0X2XxQVkNamSWVP8QDjlRiTNnE1nDRQculKrv+nJKNroUI8du8BoOfxZNMPdbvpFXFAXB5QSD+itTTdZSr684hsIhwKFIhnpkYHrshChplpiJhY1vYWxA0C10TyJULJRFv7RebWC60BCjx0Bg1g4Lm/mhDKiVZcdT0Q8+zcdvwPjgazQ==\",\"Exponent\":\"AQAB\",\"P\":\"7cRAFm71MrKqNhmbqNYdXJM07caBcQ00pwcJuUwVcp4I5E0l0oAwEU/0SM0byR8CMWlbcbsPd2ToIScruAuZAIKdk2QTN0yL214lOyKTApZOZVBIcI/PG39R3bkVQLJgLR3hQQyYheMTie9jsEKP4617X8+2bGE9IZW4VOvjI9s=\",\"Q\":\"0zmIwHx+THY2lf+CTUFqo2m/lu0wqIVy2PHSIH/8iJyJh40M+eTHO6cCGCtunLVuR613AD60wCvtTluZ1n7MpgZyw63pQGf6mUgKqIzhtU5IrmPWCHDD9WBW8XCtZcvmYdcOF7q9GLwVBSuSz24VhnAJUOzXVmbKrhKsbXeqUHc=\",\"DP\":\"CjDkMlH1LoQb6Y+1bUooHgStOxfMCoVvYU1a7FdHgA8oAGkSGvClnshXhxtLepZaqLWEVt6Wrh5kGK+uHwhYOX5ftKaQcElWoUNqwxsbQ/wfRhZn1HNFhD8zyhYZxBkhKC3krr4Ud4ChIMNi+XYQ5shTxmqHviN41EyvhuxJYws=\",\"DQ\":\"AdmZJwZwHL9UTxAD1iVih7ffY7UEHhW9IcVMb9hvH8svMS69FFs6m30k+Y031MuKAbq1Nh1We/Bmja8js8s8g5++1ZPqXRQMD9efsEY7m9jprg1VzJEgRj/nwwmXmzKj++tkRjTufw72qCrviD2QlrLYggrw/+K6l7e2JudLaJU=\",\"InverseQ\":\"i/AXtXT+DQ/yo5nFcejy+r4ea6+656o3wObgcMMDNrxQ+i7Vzem/D8VRpZcao4wwyetfwHEp849KPym+I1rtdFZWYxxlB/d7FE/PeO8jGH+AKLah9Afqa5GW8tbUHtZ/vJ4v0GC2CI0XmqcbKBAYSxGuTe+avzFXN/dAwHf/US4=\",\"D\":\"TgF7xJMf3o0uDuX/Q5O2aOJ8EsooXUatZMN+hAvMEse0sRARnfOO9in73+XqziqaHBe9xMHSzr2V7VUE/slVab17931wjeZ/ub7AwOGDhgjcOHib/x41gDVCz4kmeL8h6ZD83SB3cppsjRd/T4LQThjZXnJasyVgVKCL3ACO6ifkla7pawiT5fflFlNUhYME/eGB40LfEdKcZGaCcnmDK60YFvbwhcqhn8IsmLzeFFuatTfUht4Ozs8Hq1AAirpsZ0nWy0CAPJrDLVuS384/xithcmxODkUzEQDV/OBgEhNHlLMdiTsSKjQwfAPQE8h1W6MKUkKDt49M0M/1yKvwVQ==\"}");
            ScanQRCodeViewModel.TestStringValueToReturn = "7ba16e67-6a56-4d25-97da-ae33a87dd8b4";
        }

        [TestMethod]
        public void CanOnboardFirstTime()
        {
            Actions.NavigateToMainPageFromLaunch();
        }

        [TestMethod]
        public void CanOnboardNewDeviceForExistingID()
        {
            Actions.NavigateToMainPageFromLaunch("7ba16e67-6a56-4d25-97da-ae33a87dd8b4");
        }

        [TestMethod]
        public void CanEndorseFriend()
        {
            Actions.NavigateToMainPageFromLaunch();

            Actions.VerifyIdFromMainPage("7ba16e67-6a56-4d25-97da-ae33a87dd8b4", KeyPair.TestKeyPair);

            var verifiedIdPage = (VerifiedIDInfoViewModel)NavigationMaster.Instance.CurrentViewModel;

            verifiedIdPage.Endorse();

            Assert.AreEqual(
                typeof(MainPageViewModel),
                NavigationMaster.Instance.CurrentViewModel.GetType(),
                "Did not get back to Main page after endorsement.");
        }

        [TestMethod]
        public void CanBeEndorsedByFriend()
        {
            Actions.NavigateToMainPageFromLaunch();
            Actions.ShowIdFromMainPage("TestChallenge");
        }
    }
}
