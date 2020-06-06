// <copyright file="NewDeviceViewModel.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.App.ViewModel
{
    using System;
    using System.Threading.Tasks;
    using FinalId.App.Components;

    public class NewDeviceViewModel : MVVMHelpers.ViewModelBase
    {
        private bool _waitingForScannerCallback = false;

        private Guid _userID;

        public Guid UserID
        {
            get
            {
                return this._userID;
            }

            set
            {
                if (value != this._userID)
                {
                    this._userID = value;
                    this.NotifyPropertyChanged("UserID");
                }
            }
        }

        public override async Task NavigatedToAsync()
        {
            if (!_waitingForScannerCallback)
            {
                var scannerViewModel =
                    new ScanQRCodeViewModel((userId) => { UserID = Guid.Parse(userId); }, this, "Scan your existing ID");

                _waitingForScannerCallback = true;
                await NavigationMaster.Instance.NavigateTo(scannerViewModel);
            }
            else
            {
                _waitingForScannerCallback = false;
                await NavigationMaster.Instance.NavigateTo(new DeviceKeyGenerationViewModel() { UserID = this.UserID });
            }
        }
    }
}
