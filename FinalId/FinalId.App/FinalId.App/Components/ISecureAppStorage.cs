// <copyright file="ISecureAppStorage.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.App.Components
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FinalId.Core;

    public interface ISecureAppStorage
    {
        Task StoreDeviceIdentity(Identity deviceIdentity);

        Task<Identity> GetDeviceIdentity();

        Task StorePublicIdentity(Identity publicIdentity);

        Task<List<Identity>> GetAllStoredPublicIdentities();

        Task<Identity> GetStoredPublicIdentity(Guid identityGUID);
    }
}
