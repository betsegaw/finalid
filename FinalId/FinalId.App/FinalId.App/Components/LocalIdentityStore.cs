// <copyright file="LocalIdentityStore.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.App.Components
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FinalId.Core;

    public class LocalIdentityStore
    {
        private IdentityStore _identityStore = new IdentityStore();
        private static Identity _currentIdentity;

        static LocalIdentityStore()
        {
            Instance = new LocalIdentityStore();
        }

        private LocalIdentityStore()
        {
        }

        public static LocalIdentityStore Instance { get; set; }

        public async Task<Identity> GetCurrentIdentity()
        {
            if (_currentIdentity == null)
            {
                _currentIdentity = await SecureAppStorage.Instance.GetDeviceIdentity();
            }

            return _currentIdentity;
        }

        public void CreateNewIdentity()
        {
            _currentIdentity = _identityStore.CreateNewIdentity();
            SecureAppStorage.Instance.StoreDeviceIdentity(_currentIdentity);
        }

        public void AcceptEndorsement(Endorsement endorsement)
        {
            _currentIdentity.Endorsements.Add(endorsement);
            SecureAppStorage.Instance.StoreDeviceIdentity(_currentIdentity);
        }

        public void OnboardNewDeviceToExistingIdentity(Guid existingIdentityID)
        {
            _currentIdentity = _identityStore.AddNewKeyPairToExistingIdentity(existingIdentityID);
            SecureAppStorage.Instance.StoreDeviceIdentity(_currentIdentity);
        }

        public async Task<Identity> GetIdentity(Guid id)
        {
            return await SecureAppStorage.Instance.GetStoredPublicIdentity(id);
        }

        public async Task<List<Identity>> GetAllIdentities()
        {
            return await SecureAppStorage.Instance.GetAllStoredPublicIdentities();
        }

        public async Task StoreIdentity(Identity identity)
        {
            await SecureAppStorage.Instance.StorePublicIdentity(identity);
        }

        public Endorsement EndorseNewDevice(string targetDeviceFingerPrint)
        {
            return _identityStore.EndorseKeyPairFromNewDevice(
                _currentIdentity,
                targetDeviceFingerPrint);
        }

        public Endorsement EndorseFriendAsIdentityOwner(Guid identityGuid, string targetDeviceFingerPrint)
        {
            return _identityStore.EndorseKeyPairAsIdentityOwner(
                _currentIdentity,
                identityGuid,
                targetDeviceFingerPrint);
        }
    }
}
