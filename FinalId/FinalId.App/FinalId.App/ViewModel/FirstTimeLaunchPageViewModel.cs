// <copyright file="FirstTimeLaunchPageViewModel.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.App.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Reactive.Threading.Tasks;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using FinalId.App.Components;
    using FinalId.App.MVVMHelpers;
    using Plugin.BluetoothLE;
    using Plugin.BluetoothLE.Server;

    public class FirstTimeLaunchPageViewModel : MVVMHelpers.ViewModelBase
    {
        private Guid guid = new Guid();

        public ICommand RequestNewIDCommand
        {
            get
            {
                return new AsyncCommand(this.RequestNewID);
            }
        }

        public ICommand GetExistingIDCommand
        {
            get
            {
                return new AsyncCommand(this.GetExistingID);
            }
        }

        public ICommand AdvertiseCommand
        {
            get
            {
                return new AsyncCommand(this.Advertise);
            }
        }

        public ICommand ScanCommand
        {
            get
            {
                return new AsyncCommand(this.Scan);
            }
        }

        public async Task RequestNewID()
        {
            await NavigationMaster.Instance.NavigateTo(new DeviceKeyGenerationViewModel());
        }

        public async Task GetExistingID()
        {
            await NavigationMaster.Instance.NavigateTo(new NewDeviceViewModel());
        }

        public async Task Advertise()
        {
            string publicKey = "ThisIsPublicKey";

            var t = (await CrossBleAdapter.Current.CreateGattServer().ToList().ToTask()).ToList();
            IGattServer server = t.First();

            server.AddService(new Guid(), true, (something) =>
            {
                something.AddCharacteristic(//difference between characterstic properties and gatt permissions?
                    Guid.NewGuid(),
                    CharacteristicProperties.Read | CharacteristicProperties.Write,
                    GattPermissions.Read | GattPermissions.Write
                );

                var characteristic = something.Characteristics.First();
                characteristic.WhenReadReceived().Subscribe(x =>
                {
                    x.Value = Encoding.UTF8.GetBytes(publicKey);

                    //x.Status = GattStatus.Success;   
                });
            });

            CrossBleAdapter.Current.Advertiser.Start(new AdvertisementData
            {
                LocalName = "TestServer",
                ServiceUuids = new List<Guid> { new Guid("1d82f959-54d3-42d5-a8cd-3c90df6b19b6") },
            });

            //CrossBleAdapter.Current.Advertiser.Stop();
        }

        public async Task StopAdvertisingCommand()
        {
            // await NavigationMaster.Instance.NavigateTo();
        }

        public async Task Scan()
        {
            await NavigationMaster.Instance.NavigateTo(new ScanViewModel());
        }

        public async Task ShowUuidCommand()
        {
            // await NavigationMaster.Instance.NavigateTo();
        }
    }
}
