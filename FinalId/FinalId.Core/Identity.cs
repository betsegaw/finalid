// <copyright file="Identity.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;

    public class Identity
    {
        public Identity(Guid identityGUID)
        {
            this.IdentityGUID = identityGUID;
            KeyPairsAssertingOwnership = new List<KeyPair>();
            Endorsements = new List<Endorsement>();
            FriendlyName = this.IdentityGUID.ToString();
        }

        public Guid IdentityGUID { get; private set; }

        public string FriendlyName { get; set; }

        public List<KeyPair> KeyPairsAssertingOwnership { get; private set; }

        public List<Endorsement> Endorsements { get; private set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public bool IsValidEndorsement(Endorsement endorsement)
        {
            var signingKeyPair = KeyPairsAssertingOwnership
                .Where(x => x.Fingerprint == endorsement.EndorserPublicKeyFingerprint).First();

            return signingKeyPair.Verify(endorsement.AssertionContent, endorsement.SignedEndorsement);
        }
    }
}
