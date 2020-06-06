// <copyright file="ViewModelBase.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.App.MVVMHelpers
{
    using System.Threading.Tasks;

    public class ViewModelBase : MVVMHelpers.PropertyChangedBase
    {
        public virtual Task NavigatedToAsync()
        {
            return Task.FromResult<object>(null);
        }
    }
}
