// <copyright file="ScanQRCodeViewModel.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.App.ViewModel
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using FinalId.App.Components;
    using FinalId.App.MVVMHelpers;

    public class ScanQRCodeViewModel : ViewModelBase
    {
        private Action<string> _stringReceiver;
        private Action<byte[]> _byteReceiver;
        private ViewModelBase _postScanTarget;
        private string _scanTitle;

        public ScanQRCodeViewModel(Action<byte[]> reciever, ViewModelBase postScanTarget, string scanTitle)
        {
            _byteReceiver = reciever;
            _postScanTarget = postScanTarget;
            _scanTitle = scanTitle;
        }

        public ScanQRCodeViewModel(Action<string> reciever, ViewModelBase postScanTarget, string scanTitle)
        {
            _stringReceiver = reciever;
            _postScanTarget = postScanTarget;
            _scanTitle = scanTitle;
        }

        public static string TestStringValueToReturn { get; set; }

        public static byte[] TestByteValueToReturn { get; set; }

        public ICommand StartScanCommand
        {
            get
            {
                return new AsyncCommand(this.StartScan);
            }
        }

        public string ScanTitle
        {
            get
            {
                return _scanTitle;
            }

            set
            {
                if (value != _scanTitle)
                {
                    _scanTitle = value;
                    NotifyPropertyChanged("ScanTitle");
                }
            }
        }

        public async Task StartScan()
        {
            if (TestByteValueToReturn == null && TestStringValueToReturn == null)
            {
#if __ANDROID__
	            // Initialize the scanner first so it can track the current context
	            MobileBarcodeScanner.Initialize (Application);
#endif

                var scanner = new ZXing.Mobile.MobileBarcodeScanner();

                var result = await scanner.Scan();

                if (result != null)
                {
                    if (_stringReceiver != null)
                    {
                        _stringReceiver(result.Text);
                    }
                    else
                    {
                        _byteReceiver(result.Text.Split('-').Select(x => byte.Parse(x, NumberStyles.HexNumber)).ToArray());
                    }
                }
            }
            else
            {
                if (_stringReceiver != null)
                {
                    _stringReceiver(TestStringValueToReturn);
                }
                else
                {
                    _byteReceiver(TestByteValueToReturn);
                }
            }

            if (_postScanTarget == null)
            {
                await NavigationMaster.Instance.Pop();
            }
            else
            {
                await NavigationMaster.Instance.NavigateTo(_postScanTarget);
            }
        }
    }
}
