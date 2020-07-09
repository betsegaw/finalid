// <copyright file="ObserverImpl.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.Core
{
    using System;
    using Plugin.BluetoothLE.Server;

    public class ObserverImpl<T> : IObserver<ReadRequest>, IObserver<WriteRequest>
    {
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(ReadRequest value)
        {
            throw new NotImplementedException();
        }

        public void OnNext(WriteRequest value)
        {
            throw new NotImplementedException();
        }
    }
}
