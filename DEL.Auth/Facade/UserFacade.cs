using AutoMapper;
using DEL.Auth.DTO;
using DEL.Auth.Infrastructure;
using DEL.Auth.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace DEL.Auth.Facade
{
    public class UserFacade : BaseFacade
    {
        //private readonly GenService _service;
        public ResponseDto UserRegistration(UserDto userDto)
        {
            userDto.Password = KeyVault.EncryptOrDecryptUserPassForDatabase(userDto.Password);
            userDto.ConfirmPassword = KeyVault.EncryptOrDecryptUserPassForDatabase(userDto.ConfirmPassword);
            ResponseDto response = new ResponseDto();
            User user = new User();
            try
            {
                if (userDto.Password != userDto.ConfirmPassword)
                {
                    response.Message = "Passwords Don't Match. Please Retype";
                    return response;
                }
                if (userDto.Id != null && userDto.Id > 0)
                    user = GenService.GetAll<User>().SingleOrDefault(x => x.Id == userDto.Id);
                if (user != null && user.Id > 0)
                {
                    user.UserName = userDto.UserName;
                    user.Password = userDto.Password;
                    user.EmployeeId = userDto.EmployeeId;
                    user.IsActive = true;
                }
                else
                {
                    user = GenService.GetAll<User>().Where(u => u.UserName.ToLower() == userDto.UserName.ToLower()).FirstOrDefault();
                    if (user == null)
                    {
                        if (userDto.EmployeeId != null && userDto.EmployeeId > 0)
                        {
                            var emp =
                                GenService.GetAll<User>()
                                    .Where(u => u.EmployeeId == userDto.EmployeeId)
                                    .FirstOrDefault();
                            if (emp != null && emp.Id > 0)
                            {
                                emp.UserName = userDto.UserName;
                                emp.Password = userDto.Password;
                                emp.EmployeeId = userDto.EmployeeId;
                                emp.IsActive = true;
                                //Mapper.Map(item, docDetail);
                                GenService.Save(emp);
                            }
                            if (emp == null)
                            {
                                user = new User
                                {
                                    UserName = userDto.UserName,
                                    Password = userDto.Password,
                                    EmployeeId = userDto.EmployeeId,
                                    IsActive = true
                                };
                                //GenService.Save(user);
                                //var companyEntry = new UserOfficeApplication
                                //{
                                //    ApplicationId = 1,
                                //    OfficeId = 1,
                                //    UserId = user.Id
                                //};
                                //GenService.Save(companyEntry);
                            }
                        }
                        else
                        {
                            
                            if (user != null && user.Id > 0)
                            {
                                user.UserName = userDto.UserName;
                                user.Password = userDto.Password;
                                user.EmployeeId = userDto.EmployeeId;
                                user.IsActive = true;
                                //Mapper.Map(item, docDetail);
                                GenService.Save(user);
                            }
                            if (user == null)
                            {
                                user = new User
                                {
                                    UserName = userDto.UserName,
                                    Password = userDto.Password,
                                    IsActive = true
                                };
                                GenService.Save(user);
                                //var companyEntry = new UserOfficeApplication
                                //{
                                //    ApplicationId = 1,
                                //    OfficeId = 1,
                                //    UserId = user.Id
                                //};
                                //GenService.Save(companyEntry);
                            }
                        }

                    }
                    else
                    {
                        response.Message = "Username Already Exist . Please Give Another Username";
                        return response;
                    }



                }
                GenService.SaveChanges();
                response.Success = true;
                response.Message = "User Saved Successfully";
                return response;
            }
            catch (Exception)
            {
                response.Message = "User Save Failed";
                return response;
            }

        }

        public List<UserDto> GetUsersByCompanyId(long officeId)//(long CompanyId, long BranchId)
        {
            var data = GenService.GetAll<UserOfficeApplication>().Where(u => u.OfficeId == officeId).Select(u => u.User).Distinct();
            //(u => u.OwnOfficeId == branchId && u.OwnOfficeBranchId == BranchId).Select(u => u.User).Distinct();
            List<UserDto> users = new List<UserDto>();
            foreach (var item in data)
            {
                users.Add(Mapper.Map<UserDto>(item));
            }
            return users;
        }

        public long GetEmployeeIdByUserId(long userId)
        {
            var employeeId = GenService.GetSingleById<User>(userId).EmployeeId;
            long empId = 0;
            if (employeeId != null)
                empId = (long)employeeId;

            return empId;
        }

        public UserDto GetUserByEmployeeId(long empId)
        {

            var user = GenService.GetAll<User>().Where(u => u.EmployeeId == empId).FirstOrDefault();
            if (user != null)
                return Mapper.Map<UserDto>(user);
            return null;
        }


        public List<UserDto> GetAllUserByEmployeeId(long empId)
        {
            var userlist = GenService.GetAll<User>().Where(u => u.EmployeeId == empId).ToList();
            if (userlist != null)
                return Mapper.Map<List<UserDto>>(userlist);
            return null;
        }

        public long GetUserIdByEmployeeId(long empId)
        {
            var user = GenService.GetAll<User>().Where(u => u.EmployeeId == empId).FirstOrDefault();
            if (user != null)
            {
                var result = Mapper.Map<UserDto>(user);
                return (long)result.Id;
            }

            return 0;
        }

        public ResponseDto SaveUserInformation(UserDto userinfo, int id)
        {
            var response = new ResponseDto();
            var user = GenService.GetAll<User>().SingleOrDefault(x => x.EmployeeId == id);
            if (user != null)
            {
                user.UserName = userinfo.UserName;
                user.Password = userinfo.Password;
                user.EmployeeId = userinfo.EmployeeId;
                user.IsActive = true;
            }
            else
            {
                GenService.Save(new User
                {
                    UserName = userinfo.UserName,
                    Password = userinfo.Password,
                    EmployeeId = userinfo.EmployeeId,
                    IsActive = true
                });
            }
            GenService.SaveChanges();
            response.Success = true;
            response.Message = "User information updted successfully.";
            return response;
        }

        public List<RoleDto> GetAllRoles()
        {
            var data = GenService.GetAll<Role>().OrderBy(r => r.Name).ToList();
            return Mapper.Map<List<RoleDto>>(data);
        }

        //public bool SaveUserDegSetId
        public List<UserEmployeeMapping> GetUserEmployeeMapping()
        {
            var result = GenService.GetAll<User>().Where(u => u.Status == EntityStatus.Active && u.EmployeeId != null).Select(u => new UserEmployeeMapping { UserId = u.Id, EmployeeId = u.EmployeeId }).Distinct().ToList();
            return result;
        }

        public List<ApplicationDto> GetAllApplications()
        {
            var data = GenService.GetAll<Application>().ToList();
            return Mapper.Map<List<ApplicationDto>>(data);
        }

        public List<OfficeProfileDto> GetAllOfficeProfiles()
        {
            var data = GenService.GetAll<OfficeProfile>().ToList();
            return Mapper.Map<List<OfficeProfileDto>>(data);
        }

        public List<UserDto> GetAllUsers()
        {
            var data = GenService.GetAll<User>().ToList();
            return Mapper.Map<List<UserDto>>(data);
        }

        public List<UserOfficeApplicationDto> getUserOfficeApplications(long? appId, long? officeId, long? userId)
        {
            var data = GenService.GetAll<UserOfficeApplication>().Where(r => r.Status == EntityStatus.Active);
            var dto = Mapper.Map<List<UserOfficeApplicationDto>>(data);
            if (appId != null && appId > 0)
            {
                dto = dto.Where(r => r.ApplicationId == appId).ToList();
            }
            if (officeId != null && officeId > 0)
            {
                dto = dto.Where(r => r.OfficeId == officeId).ToList();
            }
            if (userId != null && userId > 0)
            {
                dto = dto.Where(r => r.UserId == userId).ToList();
            }
            return dto;
        }
        public ResponseDto SaveUserOfficeApplication(UserOfficeApplicationDto dto)
        {
            var entity = new UserOfficeApplication();
            ResponseDto responce = new ResponseDto();
            //if (dto.OfficeId == null)
            //{
            //    responce.Message = "Select Office Profile Settings First";
            //    return responce;
            //}
            try
            {
                //salesLeadDto.LeadStatus = LeadStatus.Submitted;
                entity = Mapper.Map<UserOfficeApplication>(dto);
                entity.Status = EntityStatus.Active;
                //entity.LeadStatus = LeadStatus.Submitted; //todo- Lead Status Extra button To Save and Save And Draft
                GenService.Save(entity);
                responce.Success = true;
                responce.Message = "User Office Application Saved Successfully";

            }
            catch (Exception ex)
            {
                responce.Message = "User Office Application Save Failed";
            }
            GenService.SaveChanges();
            return responce;
        }

        public ResponseDto UpdateUserOfficeApplications(long id)
        {
            ResponseDto responce = new ResponseDto();
            var entity = GenService.GetById<UserOfficeApplication>(id);
            entity.Status = EntityStatus.Inactive;
            GenService.Save(entity);
            responce.Success = true;
            responce.Message = "Office Designation Area Removed Successfully";
            return responce;
        }
        public UserDto GetUserInfoById(int empid)
        {
            var user = GenService.GetAll<User>().SingleOrDefault(x => x.EmployeeId == empid);
            UserDto userdto = new UserDto();
            if (user != null)
            {
                userdto.Id = user.Id;
                userdto.UserName = user.UserName;
                userdto.Password = user.Password;
                userdto.EmployeeId = user.EmployeeId;
            }
            return userdto;
        }
        #region User Management
        public IPagedList<UserDto> UserList(int pageSize, int pageCount, string searchString)
        {
            var user = GenService.GetAll<User>().OrderByDescending(r => r.Id).ToList();
            var data = (from usr in user
                        select new UserDto()
                        {
                            UserId = usr.Id,
                            UserName = usr.UserName,
                            Password = usr.Password,
                            ConfirmPassword = usr.Password,
                            EmployeeId = usr.EmployeeId,
                            IsActive = usr.IsActive,
                        }).OrderByDescending(r => r.Id).ToList();
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                data = data.Where(a => a.UserName.ToLower().Contains(searchString)).ToList();
            }
            var temp = data.OrderByDescending(r => r.Id).ToPagedList(pageCount, pageSize);
            return temp;
        }
        public UserDto LoadUserById(long id)
        {
            var data = GenService.GetById<User>(id);
            var result = Mapper.Map<UserDto>(data);
            result.UserCompanyApplications.RemoveAll(f => f.Status != EntityStatus.Active);
            return result;
        }
        public ResponseDto ChangeUserStatus(long id, bool status)
        {
            var responce = new ResponseDto();
            var user = GenService.GetById<User>(id);
            if (user.IsActive != status)
            {
                user.IsActive = status;
                GenService.Save(user);
                responce.Id = user.Id;
                responce.Message = "User Status Change Successfully";
                return responce;
            }
            return responce;
        }
        public ResponseDto SaveUserDetails(List<UserOfficeApplicationDto> dto, List<long> RemovedUserCompanyApplications, long userId)
        {
            ResponseDto response = new ResponseDto();
            try
            {
                foreach (var item in dto)
                {
                    UserOfficeApplication officeApplication;
                    if (item.Id != null && item.Id > 0)
                    {
                        officeApplication = GenService.GetById<UserOfficeApplication>((long)item.Id);
                        item.CreateDate = officeApplication.CreateDate;
                        item.CreatedBy = officeApplication.CreatedBy;
                        item.UserId = officeApplication.UserId;
                        item.EditDate = DateTime.Now;
                        item.EditedBy = userId;
                        item.Status = EntityStatus.Active;
                        Mapper.Map(item, officeApplication);
                        GenService.Save(officeApplication);
                    }
                    else
                    {
                        officeApplication = new UserOfficeApplication();
                        item.CreateDate = DateTime.Now;
                        item.CreatedBy = userId;
                        item.Status = EntityStatus.Active;
                        item.UserId = item.UserId;
                        officeApplication = Mapper.Map<UserOfficeApplication>(item);
                        GenService.Save(officeApplication);
                    }
                }

                if (RemovedUserCompanyApplications != null)
                {
                    foreach (var item in RemovedUserCompanyApplications)
                    {
                        var text = GenService.GetById<UserOfficeApplication>(item);
                        if (text != null)
                        {
                            text.Status = EntityStatus.Inactive;
                            text.EditDate = DateTime.Now;
                            text.EditedBy = userId;
                        }
                        GenService.Save(text);
                    }
                }
                response.Id = userId;
                response.Success = true;
                response.Message = "User Office Application Saved Successfully";
            }
            catch (Exception ex)
            {
                response.Message = "Error Message : " + ex;
            }
            return response;
        }
        #endregion
    }
    public class UserEmployeeMapping
    {
        public long? UserId { get; set; }
        public long? EmployeeId { get; set; }
    }
}
