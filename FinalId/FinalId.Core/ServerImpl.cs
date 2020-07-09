// <copyright file="ServerImpl.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.Core
{
    using System;
    using System.Collections.Generic;
    using Plugin.BluetoothLE.Server;

    public class ServerImpl : IGattServer
    {
        private List<IGattService> internalServices = new List<IGattService>();

        IReadOnlyList<IGattService> IGattServer.Services => internalServices;

        public void AddService(Plugin.BluetoothLE.Server.IGattService service)
        {
            internalServices.Add(service);
        }

        public void AddService(Guid uuid, bool primary, Action<IGattService> callback)
        {
            throw new NotImplementedException();
        }

        public void ClearServices()
        {
            internalServices.Clear();
        }

        public IGattService CreateService(Guid uuid, bool primary)
        {
            IGattService newService = new ServiceImpl(this, uuid, primary);
            internalServices.Add(newService);
            return newService;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IList<IDevice> GetAllSubscribedDevices()
        {
            throw new NotImplementedException();
        }

        public void RemoveService(Guid serviceUuid)
        {
            throw new NotImplementedException();
        }

        public IObservable<CharacteristicSubscription> WhenAnyCharacteristicSubscriptionChanged()
        {
            throw new NotImplementedException();
        }
    }
}
