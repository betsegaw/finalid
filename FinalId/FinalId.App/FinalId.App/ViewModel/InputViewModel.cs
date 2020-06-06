// <copyright file="InputViewModel.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.App.ViewModel
{
    public class InputViewModel : MVVMHelpers.ViewModelBase
    {
        private string _content;

        public string Label { get; set; }

        public string Content
        {
            get
            {
                return _content;
            }

            set
            {
                if (value != _content)
                {
                    _content = value;
                    NotifyPropertyChanged("Content");
                }
            }
        }
    }
}