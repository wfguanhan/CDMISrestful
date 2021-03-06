﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.OData;
using CDMISrestful.CommonLibrary;
using CDMISrestful.DataModels;
using CDMISrestful.DataViewModels;
using CDMISrestful.Models;

namespace CDMISrestful.Controllers
{
     [WebApiTracker]
    //[AllowAnonymous]  
    public class UsersController : ApiController
    {
        static readonly IUsersRepository repository = new UsersRepository();
        DataConnection pclsCache = new DataConnection();
        /// <summary>
        /// 根据输入的手机号和邮箱等获取系统唯一标识符 20151023 CSQ
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/UID")]
        public HttpResponseMessage GetIDByInputPhone(string Type, string Name)
        {
            string ret = repository.GetIDByInputPhone(pclsCache, Type, Name);
            return new ExceptionHandler().Common(Request, ret);   
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="logOn"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/LogOn")]
        [ModelValidationFilter]
        public HttpResponseMessage LogOn(LogOn logOn)
        {
          
            //msg.url = "http://my.company.com/login";

            //if (SecurityManager.IsTokenValid(token))
            //{
            ForToken ret = new ForToken();
            ret = repository.LogOn(pclsCache, logOn.PwType, logOn.username, logOn.password, logOn.role);
            return new ExceptionHandler().LogOn(Request,ret);          
        }
     
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="Register"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/Register")]
        [ModelValidationFilter]
        public HttpResponseMessage Register(Register Register)
        {
            int ret = repository.Register(pclsCache, Register.PwType, Register.userId, Register.UserName, Register.Password, Register.role, Register.revUserId, Register.TerminalName, new CommonFunction().getRemoteIPAddress(), Register.DeviceType);
            return new ExceptionHandler().Register(Request, ret);
        }

        /// <summary>
        /// 激活
        /// </summary>
        /// <param name="activation"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/Activition")]
        [ModelValidationFilter]
        public HttpResponseMessage Activition(Activation activation)
        {
            int ret = repository.Activition(pclsCache, activation.UserId, activation.InviteCode, activation.role);
            return new ExceptionHandler().Activation(Request, ret);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="ChangePassword"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/ChangePassword")]
        [ModelValidationFilter]
        public HttpResponseMessage ChangePassword(ChangePassword ChangePassword)
        {
            int ret = repository.ChangePassword(pclsCache, ChangePassword.OldPassword, ChangePassword.NewPassword, ChangePassword.UserId, ChangePassword.revUserId, ChangePassword.TerminalName, new CommonFunction().getRemoteIPAddress(), ChangePassword.DeviceType);
            return new ExceptionHandler().ChangePassword(Request, ret);
        }

        #region csq 20151101 用GetPatientsPlan方法替代
        /// <summary>
        /// 获取健康专员负责的患者列表
        /// </summary>
        /// <param name="DoctorId"></param>
        /// <param name="ModuleType"></param>
        /// <param name="Plan"></param>
        /// <param name="Compliance"></param>
        /// <param name="Goal"></param>
        /// <returns></returns>
        //[Route("Api/v1/Users/GetPatientsList")]
        //[ModelValidationFilter]
        //[EnableQuery]
        //[RESTAuthorizeAttribute]
        //public PatientsDataSet GetPatientsList(string DoctorId, string ModuleType, int Plan, int Compliance,int Goal)
        //{
        //    PatientsDataSet ret = repository.GetPatientsList(DoctorId, ModuleType, Plan, Compliance, Goal);
        //    return ret;
        //}
        #endregion

        /// <summary>
        /// 验证用户名
        /// </summary>
        /// <param name="Verification"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/Verification")]
        [ModelValidationFilter]
        public HttpResponseMessage Verification(Verification Verification)
        {
            int ret = repository.Verification(pclsCache, Verification.userId, Verification.PwType);
            return new ExceptionHandler().Verification(Request, ret);
        }

        /// <summary>
        /// 获取患者基本信息、模块信息
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/{UserId}/BasicInfo")]
        [ModelValidationFilter]
        [RESTAuthorizeAttribute]
        public PatBasicInfo GetPatBasicInfo(string UserId)
        {
            PatBasicInfo ret = repository.GetPatBasicInfo(pclsCache, UserId);
            return ret;
        }

        /// <summary>
        /// 根据用户名获取用户详细信息
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/{UserId}/BasicDtlInfo")]
        [ModelValidationFilter]
        [RESTAuthorizeAttribute]
        public PatientDetailInfo GetPatientDetailInfo(string UserId)
        {
            PatientDetailInfo ret = repository.GetPatientDetailInfo(pclsCache, UserId);
            return ret;
        }
        /// <summary>
        /// 根据用户名获取医生身份信息
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/{UserId}/DoctorDtlInfo")]
        [ModelValidationFilter]
        [RESTAuthorizeAttribute]
        public DocInfoDetail GetDoctorDetailInfo(string UserId)
        {
            DocInfoDetail ret = repository.GetDoctorDetailInfo(pclsCache, UserId);
            return ret;
        }

        /// <summary>
        /// 根据用户名获取医生基本信息
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/{UserId}/DoctorInfo")]
        [ModelValidationFilter]
        [RESTAuthorizeAttribute]
        public DoctorInfo GetDoctorInfo(string UserId)
        {
            DoctorInfo ret = repository.GetDoctorInfo(pclsCache, UserId);
            return ret;
        }
        /// <summary>
        /// Ps.DoctorInfoDetail写数据
        /// </summary>
        /// <param name="SetDoctorInfoDetail"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/DoctorDtlInfo")]
        [ModelValidationFilter]
        [RESTAuthorizeAttribute]
        public HttpResponseMessage SetDoctorInfoDetail(List<SetDoctorInfoDetail> items)
        {
            int ret = 2;
            if (items != null)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    ret = repository.SetDoctorInfoDetail(pclsCache, items[i].Doctor, items[i].CategoryCode, items[i].ItemCode, items[i].ItemSeq, items[i].Value, items[i].Description, items[i].SortNo, items[i].piUserId, items[i].piTerminalName, new CommonFunction().getRemoteIPAddress(), items[i].piDeviceType);
                    if (ret != 1)
                    {
                        break;
                    }
                }
            }
            return new ExceptionHandler().SetData(Request, ret);
        }
        /// <summary>
        /// Ps.DoctorInfo写数据
        /// </summary>
        /// <param name="SetPsDoctor"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/DoctorInfo")]
        [ModelValidationFilter]
        [RESTAuthorizeAttribute]
        public HttpResponseMessage SetPsDoctor(SetPsDoctor SetPsDoctor)
        {
            int ret = repository.SetPsDoctor(pclsCache, SetPsDoctor.UserId, SetPsDoctor.UserName, SetPsDoctor.Birthday, SetPsDoctor.Gender, SetPsDoctor.IDNo, SetPsDoctor.InvalidFlag, SetPsDoctor.piUserId, SetPsDoctor.piTerminalName, new CommonFunction().getRemoteIPAddress(), SetPsDoctor.piDeviceType);
            return new ExceptionHandler().SetData(Request, ret);
        }
        
        /// <summary>
        /// 插入患者基本信息
        /// </summary>
        /// <param name="SetPatBasicInfo"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/BasicInfo")]
        [ModelValidationFilter]
        [RESTAuthorizeAttribute]
        public HttpResponseMessage SetPatBasicInfo(SetPatBasicInfo SetPatBasicInfo)
        {
            int ret = repository.SetPatBasicInfo(pclsCache, SetPatBasicInfo.UserId, SetPatBasicInfo.UserName, SetPatBasicInfo.Birthday, SetPatBasicInfo.Gender, SetPatBasicInfo.BloodType, SetPatBasicInfo.IDNo, SetPatBasicInfo.DoctorId, SetPatBasicInfo.InsuranceType, SetPatBasicInfo.InvalidFlag, SetPatBasicInfo.piUserId, SetPatBasicInfo.piTerminalName, new CommonFunction().getRemoteIPAddress(), SetPatBasicInfo.piDeviceType);
            return new ExceptionHandler().SetData(Request, ret);
        }

        /// <summary>
        /// 插入患者详细信息 LY 2015-10-14
        /// </summary>
        /// <param name="Patient"></param>
        /// <param name="CategoryCode"></param>
        /// <param name="ItemCode"></param>
        /// <param name="ItemSeq"></param>
        /// <param name="Value"></param>
        /// <param name="Description"></param>
        /// <param name="SortNo"></param>
        /// <param name="revUserId"></param>
        /// <param name="TerminalName"></param>
        /// <param name="TerminalIP"></param>
        /// <param name="DeviceType"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/BasicDtlInfo")]
        [ModelValidationFilter]
        [RESTAuthorizeAttribute]
        public HttpResponseMessage PostPatBasicInfoDetail(List<BasinInfoDetail> items)
        {
            int length = items.Count();
            int ret = 0;
            for (int i = 0; i < length;i++ )
            {
                ret = repository.SetPatBasicInfoDetail(pclsCache, items[i].Patient, items[i].CategoryCode, items[i].ItemCode, items[i].ItemSeq, items[i].Value, items[i].Description, items[i].SortNo, items[i].revUserId, items[i].TerminalName, new CommonFunction().getRemoteIPAddress(), items[i].DeviceType);
                if (ret !=1)
                    break;
            } 
            return new ExceptionHandler().SetData(Request, ret);
        }

        [Route("Api/v1/Users/GetCalendar")]
        [ModelValidationFilter]
        [RESTAuthorizeAttribute]
        public List<Calendar> GetCalendar(string DoctorId)
        {
            List<Calendar> ret = repository.GetCalendar(pclsCache, DoctorId);
            return ret;
        }

        /// <summary>
        /// GetHealthCoachList 获取所有健康专员列表 SYF
        /// </summary>
        /// <returns></returns>
        [Route("Api/v1/Users/HealthCoaches")]
        public List<HealthCoachList> GetHealthCoachList()
        {
            return repository.GetHealthCoachList(pclsCache);
        }

        /// <summary>
        /// GetHealthCoachInfo 获取某个健康专员的具体信息 SYF
        /// </summary>
        /// <param name="HealthCoachID"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/GetHealthCoachInfo")]
        public HealthCoachInfo GetHealthCoachInfo(string HealthCoachID)
        {
            return repository.GetHealthCoachInfo(pclsCache, HealthCoachID);
        }

        /// <summary>
        /// ReserveHealthCoach 预约健康专员 SYF
        /// </summary>
        /// <param name="ReserveHealthCoach"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/ReserveHealthCoach")]
        public HttpResponseMessage ReserveHealthCoach(ReserveHealthCoach ReserveHealthCoach)
        {
            int ret = repository.ReserveHealthCoach(pclsCache, ReserveHealthCoach.DoctorId, ReserveHealthCoach.PatientId, ReserveHealthCoach.Module, ReserveHealthCoach.Description, ReserveHealthCoach.Status, ReserveHealthCoach.ApplicationTime, ReserveHealthCoach.AppointmentTime, ReserveHealthCoach.AppointmentAdd, ReserveHealthCoach.Redundancy, ReserveHealthCoach.revUserId, ReserveHealthCoach.TerminalName, new CommonFunction().getRemoteIPAddress(), ReserveHealthCoach.DeviceType);
            return new ExceptionHandler().SetData(Request, ret);
        }

        /// <summary>
        /// UpdateReservation 更新预约状态 SYF
        /// </summary>
        /// <param name="UpdateReservation"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/UpdateReservation")]
        public HttpResponseMessage UpdateReservation(UpdateReservation UpdateReservation)
        {
            int ret = repository.UpdateReservation(pclsCache, UpdateReservation.DoctorId, UpdateReservation.PatientId, UpdateReservation.Status, UpdateReservation.revUserId, UpdateReservation.TerminalName, new CommonFunction().getRemoteIPAddress(), UpdateReservation.DeviceType);
            return new ExceptionHandler().SetData(Request, ret);
        }

        /// <summary>
        /// GetCommentList 获取某专员（医生）某个模块的评论列表 SYF
        /// </summary>
        /// <param name="DoctorId"></param>
        /// <param name="CategoryCode"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/GetCommentList")]
        public List<CommentList> GetCommentList(string DoctorId, string CategoryCode)
        {
            return repository.GetCommentList(pclsCache, DoctorId, CategoryCode);
        }

        /// <summary>
        /// GetHealthCoachListByPatient 获取某病人对应的专员列表 SYF
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="CategoryCode"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/GetHealthCoachListByPatient")]
        public List<HealthCoachListByPatient> GetHealthCoachListByPatient(string PatientId, string CategoryCode)
        {
            return repository.GetHealthCoachListByPatient(pclsCache, PatientId, CategoryCode);
        }

        /// <summary>
        /// RemoveHealthCoach 某病人解除某一模块的某个健康专员 SYF
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="DoctorId"></param>
        /// <param name="CategoryCode"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/RemoveHealthCoach")]
        public HttpResponseMessage RemoveHealthCoach(string PatientId, string DoctorId, string CategoryCode)
        {
            int ret = repository.RemoveHealthCoach(pclsCache, PatientId, DoctorId, CategoryCode);
            return new ExceptionHandler().DeleteData(Request, ret);
        }

        /// <summary>
        /// GetAppoitmentPatientList 获取某专员对应的病人列表 SYF
        /// </summary>
        /// <param name="healthCoachID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/GetAppoitmentPatientList")]
        public List<AppoitmentPatient> GetAppoitmentPatientList(string healthCoachID, string Status)
        {
            return repository.GetAppoitmentPatientList(pclsCache, healthCoachID, Status);
        }
        /// <summary>
        /// 替代GetPatientsList 用于pad登录后获得专员的病人列表
        /// </summary>
        /// <param name="healthCoachID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/GetPatientsPlan")]
         [EnableQuery]
        public List<PatientListTable> GetPatientsPlan(string DoctorId, string Module, string VitalType, string VitalCode)
        {
            return repository.GetPatientsPlan(pclsCache, DoctorId, Module, VitalType, VitalCode);
        }

        /// <summary>
        /// 根据UID获取用户所有角色
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/Roles")]
        public List<string> GetAllRoleMatch(string UserId)
        {
            return repository.GetAllRoleMatch(pclsCache,UserId);
        }
    }
}
