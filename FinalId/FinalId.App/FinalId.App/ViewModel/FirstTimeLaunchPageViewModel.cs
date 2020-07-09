// <copyright file="FirstTimeLaunchPageViewModel.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.App.ViewModel
{
    using System.Threading.Tasks;
    using System.Windows.Input;
    using FinalId.App.Components;
    using FinalId.App.MVVMHelpers;

    public class FirstTimeLaunchPageViewModel : MVVMHelpers.ViewModelBase
    {
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
            await NavigationMaster.Instance.NavigateTo(new AdvertiseViewModel());
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
