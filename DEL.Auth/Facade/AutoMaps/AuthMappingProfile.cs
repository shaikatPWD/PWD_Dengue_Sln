using AutoMapper;
using DEL.Auth.DTO;
using DEL.Auth.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEL.Auth.Facade.AutoMaps
{
    public class AuthMappingProfile : Profile
    {
        protected override void Configure()
        {
            //base.Configure();
            CreateMap<Module, ModuleDto>();
            CreateMap<ModuleDto, Module>();

            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, Role>();

            CreateMap<Infrastructure.Task, TaskDto>();
            CreateMap<TaskDto, Infrastructure.Task>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<Designation, DesignationDto>();
            CreateMap<DesignationDto, Designation>();

            CreateMap<Employee, EmployeeDto>()
                //.ForMember(d => d.OwnOfficeName, o => o.MapFrom(m => m.OwnOffice.Name))
                //.ForMember(d => d.OwnOfficeBranchName, o => o.MapFrom(m => m.OwnOfficeBranch.Name))
                .ForMember(d => d.DesignationName, o => o.MapFrom(m => m.Designation.Name));
            CreateMap<EmployeeDto, Employee>();

            CreateMap<Application, ApplicationDto>();
            CreateMap<ApplicationDto, Application>();

            CreateMap<OfficeProfile, OfficeProfileDto>()
                .ForMember(d => d.ParentName, o => o.MapFrom(m => m.Parent.Name))
                .ForMember(d => d.CompanyTypeName, o => o.MapFrom(m => Enum.GetName(typeof(CompanyType), m.CompanyType)));
            CreateMap<OfficeProfileDto, OfficeProfile>();

            CreateMap<OwnOfficeBranch, OwnOfficeBranchDto>()
                .ForMember(d => d.OwnOfficeName, o => o.MapFrom(m => m.OwnOffice.Name))
                .ForMember(d => d.BranchTypeName, o => o.MapFrom(m => Enum.GetName(typeof(BranchType), m.BranchType)));
            CreateMap<OwnOfficeBranchDto, OwnOfficeBranch>();

            CreateMap<OwnOffice, OwnOfficeDto>();
            CreateMap<OwnOfficeDto, OwnOffice>();

            CreateMap<RoleMenu, UserProxyRoleMenu>();
            CreateMap<UserProxyRoleMenu, RoleMenu>();

            CreateMap<RoleMenuTask, UserProxyRoleMenuTask>();
            CreateMap<UserProxyRoleMenuTask, RoleMenuTask>();

            // extended mappings
            CreateMap<UserOfficeApplication, UserOfficeApplicationDto>();
            CreateMap<UserOfficeApplicationDto, UserOfficeApplication>();

            CreateMap<SubModule, SubModuleDto>()
                .ForMember(d => d.ModuleId, o => o.MapFrom(m => m.ModuleId))
                .ForMember(d => d.ModuleName, o => o.MapFrom(m => m.Module.Name));
            CreateMap<SubModuleDto, SubModule>();

            CreateMap<Menu, MenuDto>()
                .ForMember(d => d.SubModuleId, o => o.MapFrom(m => m.SubModuleId))
                .ForMember(d => d.SubModuleName, o => o.MapFrom(m => m.SubModule.Name));
            CreateMap<MenuDto, Menu>();

            CreateMap<UserOfficeApplication, UserCompanyApplicationDto>()
                .ForMember(d => d.UserName, o => o.MapFrom(m => m.User.UserName))
                .ForMember(d => d.OfficeName, o => o.MapFrom(m => m.Office.Name));

            CreateMap<HrOffice, HrOfficeDto>()
                .ForMember(d => d.ZoneName, o => o.MapFrom(m => m.ZoneId !=0 ? m.Parent1.BnName:"N/A"))
                .ForMember(d => d.CircleName, o => o.MapFrom(m => m.CircleId != 0 ? m.Parent2.BnName : "N/A"))
                .ForMember(d => d.DivisionName, o => o.MapFrom(m => m.DivisionId != 0 ? m.Parent3.BnName : "N/A"))
                .ForMember(d => d.IsShowName, o => o.MapFrom(m => Enum.GetName(typeof(IsShow), m.IsShow)));
            CreateMap<HrOfficeDto, HrOffice>();

            CreateMap<District, DistrictDto>()
                .ForMember(d => d.IsShowName, o => o.MapFrom(m => Enum.GetName(typeof(IsShow), m.IsShow)));
            CreateMap<DistrictDto, District>();

            CreateMap<Area, AreaDto>()
                .ForMember(d => d.HrOfficeId, o => o.MapFrom(m => m.HrOfficeId))
                .ForMember(d => d.OfficeName, o => o.MapFrom(m => m.HrOfficeId != 0 ? m.HrOffice.BnName:"N/A"))
                .ForMember(d => d.IsShowName, o => o.MapFrom(m => Enum.GetName(typeof(IsShow), m.IsShow)));
            CreateMap<AreaDto, Area>();

            CreateMap<Information, InformationDto>()
                .ForMember(d => d.AreaID, o => o.MapFrom(m => m.AreaID))
                .ForMember(d => d.AreaName, o => o.MapFrom(m => m.AreaID != 0 ? m.Area.BnName : "N/A"))
                .ForMember(d => d.DistrictID, o => o.MapFrom(m => m.DistrictID))
                .ForMember(d => d.DistrictName, o => o.MapFrom(m => m.DistrictID != null ? m.District.BnName : "N/A"))
                .ForMember(d => d.ComplainStatus, o => o.MapFrom(m => Enum.GetName(typeof(ComplainStatus), m.ComplainStatus)));
            CreateMap<InformationDto, Information>();

            CreateMap<Resources, ResourcesDto>()
                .ForMember(d => d.HrOfficeId, o => o.MapFrom(m => m.HrOfficeId))
                .ForMember(d => d.OfficeName, o => o.MapFrom(m => m.HrOfficeId != 0 ? m.HrOffice.BnName : "N/A"));
            CreateMap<ResourcesDto, Resources>();

            CreateMap<OfficeAssets, OfficeAssetsDto>()
                .ForMember(d => d.HrOfficeId, o => o.MapFrom(m => m.HrOfficeId))
                .ForMember(d => d.OfficeName, o => o.MapFrom(m => m.HrOfficeId != 0 ? m.HrOffice.BnName : "N/A"))
                .ForMember(d => d.Activities, o => o.MapFrom(m => m.Activities.Where(c => c.Status == EntityStatus.Active)));
            CreateMap<OfficeAssetsDto, OfficeAssets>();


            CreateMap<WorkActivity, OfficeAssetsDto>()
                //.ForMember(d => d.OfficeId, o => o.MapFrom(m => m.OfficeId))
                .ForMember(d => d.Id, o => o.MapFrom(m => m.OfficeAssetId))
                .ForMember(d => d.AssetName, o => o.MapFrom(m => m.OfficeAssetId != 0 ? m.OfficeAssets.AssetName : "N/A"))
                .ForMember(d => d.IsPondsCleanUpName, o => o.MapFrom(m => Enum.GetName(typeof(IsComplete), m.IsPondsCleanUp)))
                .ForMember(d => d.IsWastageCleanUpName, o => o.MapFrom(m => Enum.GetName(typeof(IsComplete), m.IsWastageCleanUp)))
                .ForMember(d => d.IsMedicalCollegeCleanUpName, o => o.MapFrom(m => Enum.GetName(typeof(IsComplete), m.IsMedicalCollegeCleanUp)))
                .ForMember(d => d.IsOfficeAndHouseholdCleanUpName, o => o.MapFrom(m => Enum.GetName(typeof(IsComplete), m.IsOfficeAndHouseholdCleanUp)))
                .ForMember(d => d.IsStillWaterCleanUpName, o => o.MapFrom(m => Enum.GetName(typeof(IsComplete), m.IsStillWaterCleanUp)))
                .ForMember(d => d.IsUnderConstructionBuildingCleanUpName, o => o.MapFrom(m => Enum.GetName(typeof(IsComplete), m.IsUnderConstructionBuildingCleanUp)))
                .ForMember(d => d.IsCuringWaterCleanUp, o => o.MapFrom(m => Enum.GetName(typeof(IsComplete), m.IsCuringWaterCleanUp)));
            CreateMap<OfficeAssetsDto, WorkActivity>();

            CreateMap<Assets, AssetsDto>()
                .ForMember(d => d.WorkRecordDetails, o => o.MapFrom(s => s.WorkRecordDetails.Where(c => c.Status == EntityStatus.Active)));
            CreateMap<AssetsDto, Assets>()
                .ForMember(d => d.WorkRecordDetails, o => o.Ignore());

            CreateMap<WorkRecordDetails, WorkRecordDetailsDto>()
                .ForMember(d => d.OfficeId, o => o.MapFrom(m => m.OfficeId))
                .ForMember(d => d.AssetId, o => o.MapFrom(m => m.AssetId))
                .ForMember(d => d.IsCompleteName, o => o.MapFrom(m => Enum.GetName(typeof(IsComplete), m.IsComplete)));
            CreateMap<WorkRecordDetailsDto, WorkRecordDetails>();

            CreateMap<WorkActivity, WorkActivityDto>()
                //.ForMember(d => d.OfficeId, o => o.MapFrom(m => m.OfficeId))
                .ForMember(d => d.OfficeAssetId, o => o.MapFrom(m => m.OfficeAssetId))
                .ForMember(d => d.OfficeAssetName, o => o.MapFrom(m => m.OfficeAssetId != 0 ? m.OfficeAssets.AssetName : "N/A"))
                .ForMember(d => d.IsPondsCleanUpName, o => o.MapFrom(m => Enum.GetName(typeof(IsComplete), m.IsPondsCleanUp)))
                .ForMember(d => d.IsWastageCleanUpName, o => o.MapFrom(m => Enum.GetName(typeof(IsComplete), m.IsWastageCleanUp)))
                .ForMember(d => d.IsMedicalCollegeCleanUpName, o => o.MapFrom(m => Enum.GetName(typeof(IsComplete), m.IsMedicalCollegeCleanUp)))
                .ForMember(d => d.IsOfficeAndHouseholdCleanUpName, o => o.MapFrom(m => Enum.GetName(typeof(IsComplete), m.IsOfficeAndHouseholdCleanUp)))
                .ForMember(d => d.IsStillWaterCleanUpName, o => o.MapFrom(m => Enum.GetName(typeof(IsComplete), m.IsStillWaterCleanUp)))
                .ForMember(d => d.IsUnderConstructionBuildingCleanUpName, o => o.MapFrom(m => Enum.GetName(typeof(IsComplete), m.IsUnderConstructionBuildingCleanUp)))
                .ForMember(d => d.IsCuringWaterCleanUp, o => o.MapFrom(m => Enum.GetName(typeof(IsComplete), m.IsCuringWaterCleanUp)));
            CreateMap<WorkActivityDto, WorkActivity>();
        }
    }
}
