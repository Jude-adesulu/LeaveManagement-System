using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            //inject all the automapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
