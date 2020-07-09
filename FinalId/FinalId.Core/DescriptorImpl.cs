// <copyright file="DescriptorImpl.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.Core
{
    using System;
    using Plugin.BluetoothLE.Server;

    public class DescriptorImpl : IGattDescriptor
    {
        private IGattCharacteristic characteristic;

        public IGattCharacteristic Characteristic => characteristic;

#pragma warning disable SA1201 // Elements should appear in the correct order
        private Guid uuid;
#pragma warning restore SA1201 // Elements should appear in the correct order

        public Guid Uuid => throw new NotImplementedException();

#pragma warning disable SA1201 // Elements should appear in the correct order
        private byte[] value;
#pragma warning restore SA1201 // Elements should appear in the correct order

        public byte[] Value => throw new NotImplementedException();

        public string Description => throw new NotImplementedException();

#pragma warning disable SA1201 // Elements should appear in the correct order
        public DescriptorImpl(IGattCharacteristic characteristic, Guid uuid, byte[] value)
#pragma warning restore SA1201 // Elements should appear in the correct order
        {
            this.characteristic = characteristic;
            this.uuid = uuid;
            this.value = value;
        }
    }
}
