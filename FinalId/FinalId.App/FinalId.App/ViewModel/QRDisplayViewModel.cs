// <copyright file="QRDisplayViewModel.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.App.ViewModel
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using FinalId.App.Components;
    using FinalId.App.MVVMHelpers;

    public class QRDisplayViewModel : ViewModelBase
    {
        private string _content;
        private string _displayTitle;

        public QRDisplayViewModel(string content, string displayTitle)
        {
            this.Content = content;
            this.DisplayTitle = displayTitle;
        }

        public QRDisplayViewModel(byte[] content, string displayTitle)
        {
            this.Content = BitConverter.ToString(content);
            this.DisplayTitle = displayTitle;
        }

        public string Content
        {
            get
            {
                return _content;
            }

            set
            {
                if (_content != value)
                {
                    _content = value;
                    NotifyPropertyChanged("Content");
                }
            }
        }

        public string DisplayTitle
        {
            get
            {
                return _displayTitle;
            }

            set
            {
                if (_displayTitle != value)
                {
                    _displayTitle = value;
                    NotifyPropertyChanged("DisplayTitle");
                }
            }
        }

        public ViewModelBase PostDisplayComplete { get; set; }

        public ICommand ScanCompleteCommand
        {
            get
            {
                return new AsyncCommand(this.ScanComplete);
            }
        }

        public async Task ScanComplete()
        {
            if (this.PostDisplayComplete == null)
            {
                await NavigationMaster.Instance.Pop();
            }
            else
            {
                await NavigationMaster.Instance.NavigateTo(this.PostDisplayComplete);
            }
        }
    }
}
