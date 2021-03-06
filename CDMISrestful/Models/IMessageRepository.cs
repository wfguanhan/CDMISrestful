﻿using CDMISrestful.CommonLibrary;
using CDMISrestful.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDMISrestful.Models
{
    public interface IMessageRepository
    {
        List<Message> GetSMSDialogue(DataConnection pclsCache, string Reciever, string SendBy);
        Message SetSMS(DataConnection pclsCache, string SendBy, string Reciever, string Content, string piUserId, string piTerminalName, string piTerminalIP, int piDeviceType);
        Message GetLatestSMS(DataConnection pclsCache, string DoctorId, string PatientId);
        int SetSMSRead(DataConnection pclsCache, string Reciever, string SendBy, string piUserId, string piTerminalName, string piTerminalIP, int piDeviceType);
        int GetSMSCountForOne(DataConnection pclsCache, string Reciever, string SendBy);
        List<Message> GetSMSList(DataConnection pclsCache, string DoctorId, string CategoryCode);
        int GetSMSCountForAll(DataConnection pclsCache, string DoctorId);
    }
}