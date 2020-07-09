// <copyright file="ObservableImpl.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.Core
{
    using System;
    using Plugin.BluetoothLE.Server;

    public class ObservableImpl<T> : IObservable<ReadRequest>, IObservable<WriteRequest>
    {
        public IDisposable Subscribe(IObserver<ReadRequest> observer) //implement
        {
            throw new NotImplementedException();
        }

        public IDisposable Subscribe(IObserver<WriteRequest> observer) //implement
        {
            throw new NotImplementedException();
        }
    }
}
