// <copyright file="OwnershipAssertion.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.Core.Assertions
{
    using Newtonsoft.Json;
    using static FinalId.Core.Assertions.Assertion;

    public class OwnershipAssertion : IAssertion
    {
        public OwnershipAssertion(string publicKeyFingerprint, string id)
        {
            this.PublicKeyFingerprint = publicKeyFingerprint;
            this.ID = id;
        }

        public Kind AssertionType => Kind.Ownership;

        public string PublicKeyFingerprint { get; private set; }

        public string ID { get; private set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
