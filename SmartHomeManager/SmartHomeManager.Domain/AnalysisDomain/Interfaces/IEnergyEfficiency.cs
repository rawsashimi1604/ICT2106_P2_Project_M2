﻿using System;
using SmartHomeManager.Domain.AnalysisDomain.Entities;

namespace SmartHomeManager.Domain.AnalysisDomain.Interfaces
{
	public interface IEnergyEfficiency
	{
        public Task<IEnumerable<EnergyEfficiency>> GetAllDeviceEnergyEfficiency(Guid accountId);

        public Task<EnergyEfficiency> GetDeviceEnergyEfficiency(Guid deviceId);

    }
}

