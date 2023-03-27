﻿using SmartHomeManager.Domain.DeviceLoggingDomain.Entities;
using SmartHomeManager.Domain.DeviceLoggingDomain.Entities.DTO;
using SmartHomeManager.Domain.DeviceLoggingDomain.Interfaces;
using SmartHomeManager.Domain.RoomDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.DeviceLoggingDomain.Services
{
    public class DeviceLogWriteService
    {
        private readonly IDeviceLogRepository _deviceLogRepository;
        private readonly TimeSpan _interval;
        private Timer _timer;


        public DeviceLogWriteService(TimeSpan interval, IDeviceLogRepository deviceLogRepository)
        {
            _interval = interval;
            _deviceLogRepository = deviceLogRepository;
        }
        // when add device i just need device id
        // the rest are fixed values that can be generated by system
        public async Task<GetDeviceLogWebRequest> AddDeviceLog(Guid deviceId, Guid roomId) {
            var newDeviceLog = new DeviceLog
            {
                DeviceId = deviceId,
                DateLogged = DateTime.Now,
                DeviceActivity = 0,
                DeviceState = true,
                RoomId = roomId
            };
            _deviceLogRepository.Add(newDeviceLog);
            await _deviceLogRepository.SaveChangesAsync();

            var ret = new GetDeviceLogWebRequest 
            {
                LogId = newDeviceLog.LogId,
                DeviceState = newDeviceLog.DeviceState

            };
            return ret;
        }


        // should update when devices turn off hence device state
        public async Task UpdateDeviceLog(DateTime date, Guid deviceId, double deviceActivity, double deviceUsage, DateTime endTime, bool deviceState)
        {
            var logs = await _deviceLogRepository.GetByDate(date, deviceId, true);

            
                foreach (var log in logs)
                {
                    log.DeviceActivity = deviceActivity;
                    log.DeviceEnergyUsage = deviceUsage;
                    log.EndTime = endTime;
                    log.DeviceState = deviceState;

                    _deviceLogRepository.Update(log);
                }

                await _deviceLogRepository.SaveChangesAsync();
            
           
        }

        // UpdateBy Schedular, 0000 & hourly
      
       /* public void StartUpdatingDeviceLogs()
        {
            _timer = new Timer(async _ =>
            {
                // Calculate the next update time
                var nextUpdate = DateTime.Now.Add(_interval);

                // Update the logs
                await UpdateDeviceLogs();

                // Set the timer to trigger again at the next update time
                _timer.Change(nextUpdate - DateTime.Now, Timeout.InfiniteTimeSpan);
            }, null, TimeSpan.Zero, Timeout.InfiniteTimeSpan);
        }

        public void StopUpdatingDeviceLogs()
        {
            _timer?.Dispose();
        }*/

        /*private async Task UpdateDeviceLogs()
        {
            // Get the current date
            var now = DateTime.Now;

            // Update the logs for the current hour (if hourly update is requested)
            if (_interval == TimeSpan.FromHours(1))
            {
                
                // get logs that are currently running
                var logs = await _deviceLogRepository.Get(true);

                foreach (var log in logs)
                {
                    log.EndTime = endHour;
                    _deviceLogRepository.Update(log);
                }
            }

            // Update the logs for the current day (if daily update is requested)
            if (_interval == TimeSpan.FromDays(1))
            {
                var startDate = now.Date;
                var endDate = startDate.AddDays(1);

                var log = await _deviceLogRepository.Get(startDate, endDate);

                if (log == null)
                {
                    log = new DeviceLog
                    {
                        StartTime = startDate,
                        EndTime = endDate,
                        // Set other properties as needed
                    };

                    _deviceLogRepository.Add(log);
                }
                else
                {
                    log.EndTime = endDate;
                    _deviceLogRepository.Update(log);
                }
            }

            await _deviceLogRepository.SaveChangesAsync();
        }*/
    }
}
