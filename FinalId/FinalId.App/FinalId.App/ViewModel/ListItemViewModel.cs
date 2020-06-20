// <copyright file="ListItemViewModel.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.App.ViewModel
{
    using System;

    public class ListItemViewModel : MVVMHelpers.ViewModelBase
    {
        public ListItemViewModel(object value, string displayTitle, Action tapped)
        {
            this.Value = value;
            this.DisplayTitle = displayTitle;
            this.Tapped = tapped;
        }

        public object Value { get; private set; }

        public string DisplayTitle { get; private set; }

        public Action Tapped { get; private set; }
    }
}