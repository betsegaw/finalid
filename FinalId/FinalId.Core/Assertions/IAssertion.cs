// <copyright file="IAssertion.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.Core.Assertions
{
    using static FinalId.Core.Assertions.Assertion;

    public interface IAssertion
    {
        Kind AssertionType { get; }

        // Each type needs to override this with its own toString
        // implementation to ensure that it can be stored
        string ToString();
    }
}
