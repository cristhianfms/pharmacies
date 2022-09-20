using System;
using BusinessLogic;
using DataAccess;
using IBusinessLogic;
using IDataAccess;
using Microsoft.Extensions.DependencyInjection;

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
        _services.AddScoped<IUserLogic, UserLogic>();
        _services.AddScoped<ISessionRepository, SessionRepository>();
        _services.AddScoped<IUserRepository, UserRepository>();
    }
}

