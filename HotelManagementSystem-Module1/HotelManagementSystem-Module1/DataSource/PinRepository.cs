﻿using HotelManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.DataSource
{
    public class PinRepository : IPinRepository
    {
        private readonly IAppDbContext appDbContext;

        public PinRepository(IAppDbContext appContext)
        {
            appDbContext = appContext;
        }

        public void UpdatePin(Pin modifiedPin)
        {
            if (modifiedPin != null)
            {
                appDbContext.PinDB().Update(modifiedPin);
                appDbContext.SaveChanges();
            }
        }

        public Pin ValidatePin(string pinNumber)
        {
            return appDbContext.PinDB().AsEnumerable().SingleOrDefault(entity => entity.PinNumberDetails() == pinNumber);
        }
    }
}