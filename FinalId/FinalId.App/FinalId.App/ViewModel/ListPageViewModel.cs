// <copyright file="ListPageViewModel.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.App.ViewModel
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class ListPageViewModel : MVVMHelpers.ViewModelBase
    {
        private ObservableCollection<ListItemViewModel> _items = new ObservableCollection<ListItemViewModel>();

        public ObservableCollection<ListItemViewModel> Items
        {
            get
            {
                return _items;
            }

            set
            {
                if (value != _items)
                {
                    _items = value;
                    NotifyPropertyChanged("Items");
                }
            }
        }

        public ICommand SelectItemCommand
        {
            get
            {
                return new Command<ListItemViewModel>((model) =>
                {
                    model.Tapped();
                });
            }
        }
    }
}