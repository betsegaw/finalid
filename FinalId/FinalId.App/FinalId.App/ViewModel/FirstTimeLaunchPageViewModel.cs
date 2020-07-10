// <copyright file="FirstTimeLaunchPageViewModel.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.App.ViewModel
{
    using System;
    using System.Collections.Generic;
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
            CrossBleAdapter.Current.Advertiser.Start(new AdvertisementData
            {
                LocalName = "TestServer",
                ServiceUuids = new List<Guid> { new Guid("123e4567-e89b-12d3-a456-426614174000") },
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
