// <copyright file="IIdentityVerificationReceiver.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.App.Components
{
    using FinalId.Core;

    public interface IIdentityVerificationReceiver
    {
        void VerifiedIdImport(Identity publicKey);
    }
}
