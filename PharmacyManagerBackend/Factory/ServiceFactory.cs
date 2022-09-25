using System;
using AuthLogic;
using BusinessLogic;
using DataAccess;
using DataAccess.Context;
using IBusinessLogic;
using IDataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Filters;

namespace Factory;

public class ServiceFactory
{
    private readonly IServiceCollection _services;
    public ServiceFactory(IServiceCollection services)
    {
        this._services = services;
    }

    public void AddCustomServices()
    {
        _services.AddScoped<IPharmacyLogic, PharmacyLogic>();
        _services.AddScoped<ISessionLogic, SessionLogic>();
        
        _services.AddScoped<UserLogic, UserLogic>();
        _services.AddScoped<RoleLogic, RoleLogic>();
        _services.AddScoped<PharmacyLogic, PharmacyLogic>();
        _services.AddScoped<InvitationLogic, InvitationLogic>();
        _services.AddScoped<IInvitationLogic, InvitationLogic>();
        _services.AddScoped<IDrugLogic, DrugLogic>();
        _services.AddScoped<IPharmacyLogic, PharmacyLogic>();
        _services.AddScoped<IPermissionLogic, PermissionLogic>();
        _services.AddScoped<AuthorizationAttributeFilter>();
        
        _services.AddScoped<ISessionRepository, SessionRepository>();
        _services.AddScoped<IInvitationRepository, InvitationRepository>();
        _services.AddScoped<IUserRepository, UserRepository>();
        _services.AddScoped<IDrugLogic, DrugLogic>();
        _services.AddScoped<IDrugRepository, DrugRepository>();
        _services.AddScoped<IDrugInfoRepository, DrugInfoRepository>();
        _services.AddScoped<IPharmacyRepository, PharmacyRepository>();
        _services.AddScoped<IRoleRepository, RoleRepository>();
        _services.AddScoped<IPermissionRepository, PermissionRepository>();
    }
    public void AddDbContextService()
    {
        _services.AddDbContext<DbContext, PharmacyManagerContext>();
    }
}

