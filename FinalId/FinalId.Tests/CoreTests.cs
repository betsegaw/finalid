// <copyright file="CoreTests.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.Tests
{
    using FinalId.Core;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;

    [TestClass]
    public class CoreTests
    {
        [TestInitialize]
        public void Initialize()
        {
            IdentityStore.IsInTestMode = true;
        }

        [TestMethod]
        public void CanSerializeIdentity()
        {
            IdentityStore store = new IdentityStore();

            Identity myIdentity = store.CreateNewIdentity();

            var serializedIdentity = myIdentity.ToString();

            System.Diagnostics.Debug.WriteLine(serializedIdentity);

            var deserializedIdentity = JsonConvert.DeserializeObject<Identity>(serializedIdentity);

            Assert.AreEqual(serializedIdentity, deserializedIdentity.ToString());
        }
    }
}
