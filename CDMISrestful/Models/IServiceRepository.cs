﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDMISrestful.Models
{
    public interface IServiceRepository
    {
        string sendSMS(string mobile, string smsType);
        int checkverification(string mobile, string smsType, string verification);
    }
}