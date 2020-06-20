// <copyright file="NavigationMaster.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.App.Components
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FinalId.App.MVVMHelpers;
    using FinalId.App.View;
    using FinalId.App.ViewModel;
    using Xamarin.Forms;

    public class NavigationMaster
    {
        private Dictionary<Type, Type> viewToViewModelMapping = new Dictionary<Type, Type>()
        {
            { typeof(AppLaunchPageViewModel), typeof(LoadingPage) },
            { typeof(FirstTimeLaunchPageViewModel), typeof(LaunchPage) },
            { typeof(NewDeviceViewModel), typeof(NewDevicePage) },
            { typeof(NewIDViewModel), typeof(NewIDPage) },
            { typeof(ScanQRCodeViewModel), typeof(ScanQRCodePage) },
            { typeof(DeviceKeyGenerationViewModel), typeof(DeviceKeyGenerationPage) },
            { typeof(MainPageViewModel), typeof(MainPage) },
            { typeof(QRDisplayViewModel), typeof(QRDisplayPage) },
            { typeof(VerifiedIDInfoViewModel), typeof(VerifiedIDInfoPage) },
            { typeof(VerifyIDViewModel), typeof(LoadingPage) },
            { typeof(ShowIDViewModel), typeof(LoadingPage) },
            { typeof(InputPageViewModel), typeof(InputPage) },
            { typeof(ListPageViewModel), typeof(ListPage) },
            { typeof(MessagePageViewModel), typeof(MessagePage) },
        };

        static NavigationMaster()
        {
            Instance = new NavigationMaster();
        }

        private NavigationMaster(bool isInTestMode = false)
        {
            this.IsInTestMode = isInTestMode;

            this.UIStack = new Stack<ViewModelBase>();

            this.CurrentViewModel = new AppLaunchPageViewModel();
            this.UIStack.Push(this.CurrentViewModel);

            if (!this.IsInTestMode)
            {
                // The try catch is to account for running in test environments
                // where IsInTestMode hasn't had a chance yet to get set
                try
                {
                    this.AppView = new NavigationPage(new LoadingPage() { BindingContext = this.CurrentViewModel });
                }
                catch
                {
                }
            }
        }

        public static NavigationMaster Instance { get; set; }

        public Page AppView { get; set; }

        public ViewModelBase CurrentViewModel { get; set; }

        public Stack<ViewModelBase> UIStack { get; set; }

        public bool IsInTestMode { get; set; }

        public static void ResetForTest()
        {
            Instance = new NavigationMaster(true);
        }

        public async void NavigateToAppLaunchedPage()
        {
            await this.CurrentViewModel.NavigatedToAsync();
        }

        public async Task NavigateTo(ViewModelBase viewModelInstance)
        {
            this.UIStack.Push(viewModelInstance);
            this.CurrentViewModel = viewModelInstance;

            if (!this.IsInTestMode)
            {
                var view = (ContentPage)Activator.CreateInstance(this.viewToViewModelMapping[viewModelInstance.GetType()]);
                view.BindingContext = this.CurrentViewModel;
                await this.AppView.Navigation.PushAsync(view);
            }

            await this.CurrentViewModel.NavigatedToAsync();
        }

        public async Task Pop()
        {
            this.UIStack.Pop();
            this.CurrentViewModel = this.UIStack.Peek();

            if (!this.IsInTestMode)
            {
                await this.AppView.Navigation.PopAsync();
            }

            await this.CurrentViewModel.NavigatedToAsync();
        }
    }
}
