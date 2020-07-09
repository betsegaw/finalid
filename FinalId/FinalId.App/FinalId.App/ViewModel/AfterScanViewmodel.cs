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
#pragma warning disable SA1401 // Fields should be private
        public static string Data;
#pragma warning restore SA1401 // Fields should be private

        public AfterScanViewmodel(string data)
        {
            Data = data;
        }

        public override async Task NavigatedToAsync()
        {
        }
    }
}