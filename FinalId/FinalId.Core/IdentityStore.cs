// <copyright file="IdentityStore.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.Core
{
    using System;
    using FinalId.Core.Assertions;

    public class IdentityStore
    {
        public static bool IsInTestMode { get; set; }

        public Identity CreateNewIdentity()
        {
            Identity newIdentity = new Identity(Guid.NewGuid());

            KeyPair newKeyPair = KeyPair.GenerateNewKeyPair();

            newIdentity.KeyPairsAssertingOwnership.Add(newKeyPair);

            IAssertion keyGuidOwnership =
                new OwnershipAssertion(newKeyPair.Fingerprint, newIdentity.IdentityGUID.ToString());

            Endorsement selfEndorsement = new Endorsement(
                newIdentity,
                keyGuidOwnership);

            newIdentity.Endorsements.Add(selfEndorsement);

            return newIdentity;
        }

        public Identity AddNewKeyPairToExistingIdentity(Guid existingIdentityID)
        {
            Identity existingIdentity = new Identity(existingIdentityID);

            KeyPair newKeyPair = KeyPair.GenerateNewKeyPair();

            existingIdentity.KeyPairsAssertingOwnership.Add(newKeyPair);

            IAssertion keyGuidOwnership =
                new OwnershipAssertion(newKeyPair.Fingerprint, existingIdentity.IdentityGUID.ToString());

            Endorsement selfEndorsement = new Endorsement(
                existingIdentity,
                keyGuidOwnership);

            existingIdentity.Endorsements.Add(selfEndorsement);

            return existingIdentity;
        }

        public Endorsement EndorseKeyPairFromNewDevice(Identity endorser, string targetDeviceFingerPrint)
        {
            throw new NotImplementedException();
        }

        public Endorsement EndorseKeyPairAsIdentityOwner(Identity endorser, Guid identityGUID, string targetFingerPrint)
        {
            IAssertion keyGuidOwnership =
                new OwnershipAssertion(targetFingerPrint, identityGUID.ToString());

            Endorsement endorsement = new Endorsement(endorser, keyGuidOwnership);

            return endorsement;
        }

        public Identity LookUpIdentityByFingerPrint(string targetDeviceFingerPrint)
        {
            throw new NotImplementedException();
        }
    }
}
