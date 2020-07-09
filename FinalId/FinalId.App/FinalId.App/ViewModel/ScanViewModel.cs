// <copyright file="ScanViewModel.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.App.ViewModel
{
    using System;
    using System.Threading.Tasks;
    using FinalId.App.Components;
    using FinalId.App.MVVMHelpers;
    using Plugin.BluetoothLE;

    public class ScanViewModel : ViewModelBase
    {
        public override async Task NavigatedToAsync()
        {
            IAdvertisementData data = null;

            // discover some devices
            var scanner = CrossBleAdapter.Current.Scan().Subscribe(scanResult =>
            {// scan for devices
                // scanResult.Device.ConnectIf(config);//connect to that device if these configrations are present
                data = scanResult.AdvertisementData;
            });
            scanner.Dispose();
            await NavigationMaster.Instance.NavigateTo(new AfterScanViewmodel(data.ServiceData.ToString()));
        }
    }
}
