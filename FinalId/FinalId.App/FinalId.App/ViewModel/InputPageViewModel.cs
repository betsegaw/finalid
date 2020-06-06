// <copyright file="InputPageViewModel.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.App.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using FinalId.App.Components;
    using FinalId.App.MVVMHelpers;

    public class InputPageViewModel : MVVMHelpers.ViewModelBase
    {
        private ViewModelBase _postInputViewModel;
        private Action<List<InputViewModel>> _submitted;

        public InputPageViewModel(List<InputViewModel> inputs, Action<List<InputViewModel>> submitted, ViewModelBase postInputViewModel)
        {
            this._postInputViewModel = postInputViewModel;
            this._submitted = submitted;

            this.Inputs = new ObservableCollection<InputViewModel>();
            inputs.ForEach(input => this.Inputs.Insert(this.Inputs.Count, input));
        }

        public ObservableCollection<InputViewModel> Inputs { get; set; }

        public ICommand SubmitCommand
        {
            get
            {
                return new AsyncCommand(this.Submit);
            }
        }

        public async Task Submit()
        {
            this._submitted(this.Inputs.ToList());
            await NavigationMaster.Instance.NavigateTo(_postInputViewModel);
        }
    }
}