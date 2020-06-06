// <copyright file="Assertion.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.Core.Assertions
{
    using System;
    using Newtonsoft.Json;

    public static class Assertion
    {
        public enum Kind
        {
            Ownership,
        }

        public static IAssertion GetAssertionFromString(string assertionAsText)
        {
            IAssertion assertion = JsonConvert.DeserializeObject<IAssertion>(assertionAsText);

            switch (assertion.AssertionType)
            {
                case Kind.Ownership:
                    return JsonConvert.DeserializeObject<OwnershipAssertion>(assertionAsText);
                default:
                    throw new Exception("Unknown Assertion type");
            }
        }
    }
}