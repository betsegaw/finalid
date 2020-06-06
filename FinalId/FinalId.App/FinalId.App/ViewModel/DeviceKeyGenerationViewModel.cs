// <copyright file="DeviceKeyGenerationViewModel.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.App.ViewModel
{
    using System;
    using System.Threading.Tasks;
    using FinalId.App.Components;

    public class DeviceKeyGenerationViewModel : MVVMHelpers.ViewModelBase
    {
        public Guid UserID { get; set; }

        public override async Task NavigatedToAsync()
        {
            if (this.UserID == Guid.Empty)
            {
                await Task.Run(() => LocalIdentityStore.Instance.CreateNewIdentity());
            }
            else
            {
                await Task.Run(() => LocalIdentityStore.Instance.OnboardNewDeviceToExistingIdentity(this.UserID));
            }

            await NavigationMaster.Instance.NavigateTo(new MainPageViewModel());
        }
    }
}
