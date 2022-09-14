using System;
using BusinessLogic;
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
            _services.AddScoped<PharmacyLogic, PharmacyLogic>();
            _services.AddScoped<SessionLogic, SessionLogic>();
        }
    }
}
