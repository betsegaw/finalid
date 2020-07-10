// <copyright file="AfterScanViewmodel.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.App.ViewModel
{
    using System.Threading.Tasks;
    using FinalId.App.MVVMHelpers;

    public class AfterScanViewmodel : ViewModelBase
    {
        public AfterScanViewmodel(string data)
        {
            Data = data;
        }

        public string Data { get; private set; }

        public override async Task NavigatedToAsync()
        {
        }
    }
}