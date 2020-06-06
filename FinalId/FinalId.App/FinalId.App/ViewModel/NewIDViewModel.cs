// <copyright file="NewIDViewModel.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.App.ViewModel
{
    using System.Threading.Tasks;
    using FinalId.App.Components;

    public class NewIDViewModel : MVVMHelpers.ViewModelBase
    {
        public override async Task NavigatedToAsync()
        {
            await Task.Delay(3000);
            await NavigationMaster.Instance.NavigateTo(new DeviceKeyGenerationViewModel());
        }
    }
}
