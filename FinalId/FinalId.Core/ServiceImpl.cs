// <copyright file="ServiceImpl.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.Core
{
    using System;
    using System.Collections.Generic;
    using Plugin.BluetoothLE;
    using Plugin.BluetoothLE.Server;

    public class ServiceImpl : Plugin.BluetoothLE.Server.IGattService
    {
        private IGattServer internalServer = new ServerImpl();
        private Guid internalUuid = new Guid();
        private bool internalIsPrimary;

        public IGattServer Server => internalServer;

        public Guid Uuid => internalUuid;

        public bool IsPrimary => internalIsPrimary;

#pragma warning disable SA1201 // Elements should appear in the correct order
        private List<Plugin.BluetoothLE.Server.IGattCharacteristic> internalCharacteristics = new List<Plugin.BluetoothLE.Server.IGattCharacteristic>();

#pragma warning restore SA1201 // Elements should appear in the correct order
        public IReadOnlyList<Plugin.BluetoothLE.Server.IGattCharacteristic> Characteristics => internalCharacteristics;

#pragma warning disable SA1201 // Elements should appear in the correct order
        public ServiceImpl(IGattServer server, Guid uuid, bool isPrimary)
#pragma warning restore SA1201 // Elements should appear in the correct order
        {
            this.internalServer = server;
            this.internalUuid = uuid;
            this.internalIsPrimary = isPrimary;
        }

        public Plugin.BluetoothLE.Server.IGattCharacteristic AddCharacteristic(Guid uuid, CharacteristicProperties properties, GattPermissions permissions)
        {
            Plugin.BluetoothLE.Server.IGattCharacteristic characteristic = new Characteristicimpl(uuid, properties, permissions);
            internalCharacteristics.Add(characteristic);
            return characteristic;
        }

    }
}
