﻿using SmartHomeManager.Domain.DeviceLoggingDomain.Interfaces;
using SmartHomeManager.Domain.DeviceLoggingDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.DeviceLoggingDomain.Services
{
    public class GetDeviceService
    {
        private readonly iDeviceLogRepository _deviceLogRepository;

        public GetDeviceService(iDeviceLogRepository deviceLogRepository)
        {
            _deviceLogRepository = deviceLogRepository;
        }

        public IEnumerable<DeviceLog> GetAll()
        {
            return _deviceLogRepository.GetAll();
        }
    }
}