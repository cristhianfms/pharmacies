using System;
using BusinessLogic;
using IBusinessLogic;
using Microsoft.Extensions.DependencyInjection;

namespace Factory
{
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
        }
    }
}
