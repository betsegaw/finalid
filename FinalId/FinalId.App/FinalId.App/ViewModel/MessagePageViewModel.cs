// <copyright file="MessagePageViewModel.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.App.ViewModel
{
    using System.Threading.Tasks;
    using System.Windows.Input;
    using FinalId.App.Components;
    using FinalId.App.MVVMHelpers;

    public class MessagePageViewModel : MVVMHelpers.ViewModelBase
    {
        private string _message = string.Empty;

        public MessagePageViewModel(string message, ViewModelBase doneTarget)
        {
            this.Message = message;
            this.DoneTarget = doneTarget;
        }

        public ICommand DoneCommand
        {
            get
            {
                return new AsyncCommand(this.Done);
            }
        }

        public string Message
        {
            get
            {
                return _message;
            }

            set
            {
                if (value != _message)
                {
                    _message = value;
                    NotifyPropertyChanged("Message");
                }
            }
        }

        public ViewModelBase DoneTarget { get; set; }

        public async Task Done()
        {
            await NavigationMaster.Instance.NavigateTo(DoneTarget);
        }
    }
}