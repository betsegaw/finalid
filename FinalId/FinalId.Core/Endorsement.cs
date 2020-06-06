// <copyright file="Endorsement.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.Core
{
    using System.Linq;
    using FinalId.Core.Assertions;
    using Newtonsoft.Json;

    public class Endorsement
    {
        public Endorsement()
        {
        }

        public Endorsement(
            Identity endorser,
            IAssertion assertion)
        {
            this.AssertionContent = assertion.ToString();
            this.SignedEndorsement = endorser.KeyPairsAssertingOwnership.First().Sign(assertion.ToString());
            this.EndorserGUID = endorser.IdentityGUID.ToString();
            this.EndorserPublicKeyFingerprint = endorser.KeyPairsAssertingOwnership.First().Fingerprint;
        }

        [JsonIgnore]
        public IAssertion Assertion
        {
            get
            {
                return Assertions.Assertion.GetAssertionFromString(AssertionContent);
            }
        }

        public string AssertionContent { get; set; }

        public string EndorserGUID { get; set; }

        public string EndorserPublicKeyFingerprint { get; set; }

        public byte[] SignedEndorsement { get; set; }

        public static Endorsement GetFromJSONString(string endorsement)
        {
            return JsonConvert.DeserializeObject<Endorsement>(endorsement);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
