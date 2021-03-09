﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelManagementSystem_Module1.Domain.Models;

namespace HotelManagementSystem_Module1.DataSource
{
    public interface IAuthenticateRepository
    {

        string CheckPass(string username);
        string FindPin(string username);
        void UpdatePin(string pin);

    }
}