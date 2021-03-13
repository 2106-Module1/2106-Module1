using HotelManagementSystem.DataSource;
using HotelManagementSystem.Domain.Models;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HotelManagementSystem.Domain
{
    public class TimerService : IHostedService, ITimerService
    {

        private bool timeToChangePin = false;

        /// <summary>
        /// Task to start background timer
        /// </summary>
        /// <returns>Task</returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(SetTimer, cancellationToken);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Task to stop background timer
        /// </summary>
        /// <returns>null</returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Sync Task stopped");
            return null;
        }


        /// <summary>
        /// Set Timer to change the state of the pin 
        /// if true, means its time to change pin. 
        /// </summary>
        /// <returns>null</returns>
        private Task SetTimer()
        {

            while (true)
            {
                timeToChangePin = true;
                Debug.WriteLine("TIME TO CHANGE PIN : " + timeToChangePin.ToString());
                //Wait 2 minutes till next execution
                DateTime nextStop = DateTime.Now.AddMinutes(2);
                var timeToWait = nextStop - DateTime.Now;
                var millisToWait = timeToWait.TotalMilliseconds;
                Thread.Sleep((int)millisToWait);
            }
        }

        /// <summary>
        /// When user login it will call this method to check if the pin as expired.
        /// if true, means pin has not expired. 
        /// </summary>
        /// <returns>null</returns>
        public bool CheckPinExpired()
        {
            return this.timeToChangePin;
        }

        /// <summary>
        /// Once Authenticate controller has generate a new pin it will call this
        /// if true, mean pin is expired. 
        /// </summary>
        /// <returns>null</returns>
        public void ChangePinState(bool pinState)
        {
            this.timeToChangePin = pinState;
            Debug.WriteLine("PIN STATE CHANGED FROM CONTEROLLER");
        }
    }
}
