// <copyright file="Characteristicimpl.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.Core
{
    using System;
    using System.Collections.Generic;
    using Plugin.BluetoothLE;
    using Plugin.BluetoothLE.Server;

    public class Characteristicimpl : Plugin.BluetoothLE.Server.IGattCharacteristic
    {
        public Plugin.BluetoothLE.Server.IGattService Service => throw new NotImplementedException();

#pragma warning disable SA1201 // Elements should appear in the correct order
        private Guid uuid = new Guid();

#pragma warning restore SA1201 // Elements should appear in the correct order
        public Guid Uuid => uuid;

#pragma warning disable SA1201 // Elements should appear in the correct order
        private CharacteristicProperties properties;
#pragma warning restore SA1201 // Elements should appear in the correct order

        public CharacteristicProperties Properties => properties;

#pragma warning disable SA1201 // Elements should appear in the correct order
        private GattPermissions permissions;
#pragma warning restore SA1201 // Elements should appear in the correct order

        public GattPermissions Permissions => permissions;

#pragma warning disable SA1201 // Elements should appear in the correct order
        private List<Plugin.BluetoothLE.Server.IGattDescriptor> descriptors = new List<Plugin.BluetoothLE.Server.IGattDescriptor>();
#pragma warning restore SA1201 // Elements should appear in the correct order

        public Characteristicimpl(Guid uuid, CharacteristicProperties properties, GattPermissions permissions)
        {
            this.uuid = uuid;
            this.properties = properties;
            this.permissions = permissions;
        }

        public IReadOnlyList<Plugin.BluetoothLE.Server.IGattDescriptor> Descriptors => descriptors;

        public IReadOnlyList<Plugin.BluetoothLE.Server.IDevice> SubscribedDevices => throw new NotImplementedException();

        public Plugin.BluetoothLE.Server.IGattDescriptor AddDescriptor(Guid uuid, byte[] value)
        {
            var descriptor = new DescriptorImpl(this, uuid, value);
            descriptors.Add(descriptor);
            return descriptor;

        }

        public void Broadcast(byte[] value, params Plugin.BluetoothLE.Server.IDevice[] device)
        {
            throw new NotImplementedException();
        }

        public IObservable<CharacteristicBroadcast> BroadcastObserve(byte[] value, params Plugin.BluetoothLE.Server.IDevice[] devices)
        {
            throw new NotImplementedException();
        }

        public IObservable<DeviceSubscriptionEvent> WhenDeviceSubscriptionChanged()
        {
            throw new NotImplementedException();
        }

        public IObservable<ReadRequest> WhenReadReceived()//implement
        {
            IObservable<ReadRequest> observable = new ObservableImpl<ReadRequest>();
            observable.Subscribe(new ObserverImpl<ReadRequest>());
            return observable;
        }

        public IObservable<WriteRequest> WhenWriteReceived()//implement
        {
            IObservable<WriteRequest> observable = new ObservableImpl<WriteRequest>();
            observable.Subscribe(new ObserverImpl<WriteRequest>());
            return observable;
        }
    }
}
