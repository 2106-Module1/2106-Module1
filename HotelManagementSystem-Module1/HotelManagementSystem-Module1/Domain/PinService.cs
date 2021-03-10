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
    public class PinService : IHostedService, IPinService
    {

        private bool timeToChangePin = false;
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(SetTimer, cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Sync Task stopped");
            return null;
        }

      

        private Task SetTimer()
        {

            while (true)
            {
                //Do what ever you want to do all 4 Minutes
                timeToChangePin = true;
                Debug.WriteLine("TIME TO CHANGE PIN : " + timeToChangePin.ToString());
                //Wait 4 minutes till next execution
                DateTime nextStop = DateTime.Now.AddSeconds(5);
                var timeToWait = nextStop - DateTime.Now;
                var millisToWait = timeToWait.TotalMilliseconds;
                Thread.Sleep((int)millisToWait);
            }
        }


        public bool checkPinState()
        {
            return this.timeToChangePin;
        }

        //called by controller
        public void changePinState(bool pinState)
        {
            this.timeToChangePin = pinState;
            Debug.WriteLine("PIN STATE CHANGED FROM CONTEROLLER");
        }
    }
}
