// <copyright file="ListPageViewModel.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.App.ViewModel
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using FinalId.App.Components;
    using FinalId.App.MVVMHelpers;
    using Xamarin.Forms;

    public class ListPageViewModel : MVVMHelpers.ViewModelBase
    {
        private ObservableCollection<ListItemViewModel> _items;

        public ListPageViewModel(List<ListItemViewModel> items, ViewModelBase doneTarget)
        {
            this.Items = new ObservableCollection<ListItemViewModel>();
            items.ForEach(input => this.Items.Insert(this.Items.Count, input));
            this.DoneTarget = doneTarget;
        }

        public ICommand DoneCommand
        {
            get
            {
                return new AsyncCommand(this.Done);
            }
        }

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

        public ViewModelBase DoneTarget { get; set; }

        public async Task Done()
        {
            await NavigationMaster.Instance.NavigateTo(DoneTarget);
        }
    }
}