// <copyright file="SecureAppStorage.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.App.Components
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FinalId.Core;
    using Newtonsoft.Json;
    using Xamarin.Essentials;

    public class SecureAppStorage : ISecureAppStorage
    {
        private const string SSDeviceIdentity = "deviceIdentity";
        private const string SSListOfStoredIdentities = "allStoredIdentities";

        static SecureAppStorage()
        {
            Instance = new SecureAppStorage();
        }

        private SecureAppStorage()
        {
        }

        public static ISecureAppStorage Instance { get; set; }

        async Task<List<Identity>> ISecureAppStorage.GetAllStoredPublicIdentities()
        {
            try
            {
                var identityList = JsonConvert.DeserializeObject<HashSet<Guid>>(await SecureStorage.GetAsync(SSListOfStoredIdentities));

                List<Identity> result = new List<Identity>();

                foreach (Guid identity in identityList)
                {
                    result.Add(await this.GetStoredPublicIdentity(identity));
                }

                return result;
            }
            catch
            {
            }

            return new List<Identity>();
        }

        public async Task<Identity> GetDeviceIdentity()
        {
            try
            {
                return JsonConvert.DeserializeObject<Identity>(await SecureStorage.GetAsync(SSDeviceIdentity));
            }
            catch
            {
            }

            return null;
        }

        public async Task<Identity> GetStoredPublicIdentity(string keyPairFingerPrint)
        {
            try
            {
                var guid = Guid.Parse(await SecureStorage.GetAsync(keyPairFingerPrint));

                return await GetStoredPublicIdentity(guid);
            }
            catch
            {
            }

            return null;
        }

        public async Task<Identity> GetStoredPublicIdentity(Guid identityGUID)
        {
            try
            {
                return JsonConvert.DeserializeObject<Identity>(await SecureStorage.GetAsync(identityGUID.ToString()));
            }
            catch
            {
            }

            return null;
        }

        public async Task StoreDeviceIdentity(Identity deviceIdentity)
        {
            await SecureStorage.SetAsync(SSDeviceIdentity, deviceIdentity.ToString());
        }

        public async Task StorePublicIdentity(Identity publicIdentity)
        {
            await SecureStorage.SetAsync(publicIdentity.IdentityGUID.ToString(), publicIdentity.ToString());

            HashSet<Guid> identityList;
            try
            {
                identityList = JsonConvert.DeserializeObject<HashSet<Guid>>(await SecureStorage.GetAsync(SSListOfStoredIdentities));
            }
            catch
            {
                identityList = new HashSet<Guid>();
            }

            identityList.Add(publicIdentity.IdentityGUID);
            await SecureStorage.SetAsync(SSListOfStoredIdentities, JsonConvert.SerializeObject(identityList));

            foreach (var keyPair in publicIdentity.KeyPairsAssertingOwnership)
            {
                await SecureStorage.SetAsync(keyPair.Fingerprint, publicIdentity.IdentityGUID.ToString());
            }
        }
    }
}
