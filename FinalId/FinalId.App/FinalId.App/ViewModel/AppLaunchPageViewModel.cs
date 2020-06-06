// <copyright file="AppLaunchPageViewModel.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.App.ViewModel
{
    using System.Threading.Tasks;
    using FinalId.App.Components;
    using FinalId.App.MVVMHelpers;

    public class AppLaunchPageViewModel : ViewModelBase
    {
        public override async Task NavigatedToAsync()
        {
            if (await LocalIdentityStore.Instance.GetCurrentIdentity() != null)
            {
                await NavigationMaster.Instance.NavigateTo(new MainPageViewModel());
            }
            else
            {
                await NavigationMaster.Instance.NavigateTo(new FirstTimeLaunchPageViewModel());
            }
        }
    }
}
