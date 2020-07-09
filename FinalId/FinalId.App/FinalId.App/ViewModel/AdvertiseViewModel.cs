// <copyright file="AdvertiseViewModel.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.App.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using FinalId.App.MVVMHelpers;
    using Plugin.BluetoothLE;
    using Plugin.BluetoothLE.Server;

    public class AdvertiseViewModel : ViewModelBase
    {
        private Guid guid = new Guid();

        public override async Task NavigatedToAsync()
        {
            string publicKey = "ThisIsPublicKey";
            //var server = CrossBleAdapter.Current.CreateGattServer();
            // var service = server.AddService(Guid.NewGuid(), true);
            IGattServer server = (Core.ServerImpl)CrossBleAdapter.Current.CreateGattServer();
            Plugin.BluetoothLE.Server.IGattService service = new Core.ServiceImpl(server, guid, true);
            server.AddService(service);
            //characterstics of a service are where we are able to set our values and permissions.
            var characteristic = service.AddCharacteristic(//difference between characterstic properties and gatt permissions?
                Guid.NewGuid(),
                CharacteristicProperties.Read | CharacteristicProperties.Write,
                GattPermissions.Read | GattPermissions.Write
            );

            characteristic.WhenReadReceived().Subscribe(x =>
            {
                x.Value = Encoding.UTF8.GetBytes(publicKey);

                //x.Status = GattStatus.Success;   
            });
            characteristic.WhenWriteReceived().Subscribe(x =>
            {
                var write = Encoding.UTF8.GetString(x.Value, 0, x.Value.Length);
                // do something
            });

            //advertise the characterstic to anyone who is listening
            CrossBleAdapter.Current.Advertiser.Start(new AdvertisementData
            {
                LocalName = "Public Key",
                ServiceUuids = new List<Guid>() { guid },
            });
        }
    }
}
