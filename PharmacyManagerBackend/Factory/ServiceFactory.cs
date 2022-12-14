using System;
using System.Diagnostics.CodeAnalysis;
using AuthLogic;
using BusinessLogic;
using DataAccess;
using DataAccess.Context;
using Domain;
using IAuthLogic;
using IBusinessLogic;
using IDataAccess;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Filter;

namespace Factory;

[ExcludeFromCodeCoverage]
public class ServiceFactory
{
    private readonly IServiceCollection _services;
    public ServiceFactory(IServiceCollection services)
    {
        this._services = services;
    }

    public void AddCustomServices()
    {
        _services.AddScoped<IRoleLogic, RoleLogic>();
        _services.AddScoped<IUserLogic, UserLogic>();
        _services.AddScoped<ISessionLogic, SessionLogic>();
        _services.AddScoped<IPharmacyLogic, PharmacyLogic>();
        _services.AddScoped<IInvitationLogic, InvitationLogic>();
        _services.AddScoped<IDrugLogic, DrugLogic>();
        _services.AddScoped<ISolicitudeLogic, SolicitudeLogic>();
        _services.AddScoped<DrugLogic>();
        _services.AddScoped<PharmacyLogic>();
        _services.AddScoped<IPermissionLogic, PermissionLogic>();
        _services.AddScoped<IPurchaseLogic, PurchaseLogic>();
        _services.AddScoped<IExporterManager, ExporterManager>();
        _services.AddScoped<Context>();

        _services.AddScoped<IRoleRepository, RoleRepository>();
        _services.AddScoped<IUserRepository, UserRepository>();
        _services.AddScoped<ISessionRepository, SessionRepository>();
        _services.AddScoped<IPharmacyRepository, PharmacyRepository>();
        _services.AddScoped<IInvitationRepository, InvitationRepository>();
        _services.AddScoped<IDrugRepository, DrugRepository>();
        _services.AddScoped<IDrugInfoRepository, DrugInfoRepository>();
        _services.AddScoped<ISolicitudeRepository, SolicitudeRepository>();
        _services.AddScoped<IPermissionRepository, PermissionRepository>();
        _services.AddScoped<IPurchaseRepository, PurchaseRepository>();
        
        _services.AddScoped<AuthorizationAttributeFilter>();
        _services.AddScoped<AuthorizationAttributePublicFilter>();
    }
    public void AddDbContextService()
    {
        _services.AddDbContext<DbContext, PharmacyManagerContext>();
    }
}

