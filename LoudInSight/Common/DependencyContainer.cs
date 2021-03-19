
using LoudInSight.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoudInSight.api
{
    internal static class DependencyContainer
    {
        internal static void RegisterServices(this IServiceCollection services)
        {
            
            var businessObjectAssembly = Assembly.GetAssembly(typeof(BusinessObject.BaseManager)); 
            var databaseObjectAssembly = Assembly.GetAssembly(typeof(DataAccessObject.BaseRepository));
            services.RegisterPublicNonGenericClasses_Manager(businessObjectAssembly, t => t.Name.EndsWith("Manager"));
            services.RegisterPublicNonGenericClasses_DAO(databaseObjectAssembly, t => t.Name.EndsWith("Repository"));
        }
        private static IServiceCollection RegisterPublicNonGenericClasses_Manager(this IServiceCollection services, Assembly assembly,
            Func<Type, bool> predicate = null, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            if (assembly == null)
                throw new ArgumentException("assembly");

            var classes = assembly.ExportedTypes.Where(x => x.IsClass && !x.IsAbstract && !x.IsGenericType && !x.IsNested);

            foreach (var classType in classes)
            {
                var interfaces = classType.GetTypeInfo().ImplementedInterfaces.Where(i => i != typeof(IDisposable) && (i.IsPublic)).Where(t => t.Name != "IBaseManager");

                if (predicate != null)
                    interfaces = interfaces.Where(predicate);

                foreach (var interfaceType in interfaces)
                {
                        var ctor = classType.GetConstructors();

                        services.AddTransient(interfaceType, provider =>
                        {
                            List<object> args = new List<object>();

                            //Thread.CurrentPrincipal = RegisterThreadPrincipal(provider.GetService<IHttpContextAccessor>()?.HttpContext);

                            foreach (var param in ctor[0].GetParameters())
                            {
                                args.Add(provider.GetService(param.ParameterType));
                            }
                            
                            BusinessObject.Interfaces.IBaseManager obj = (BusinessObject.Interfaces.IBaseManager)Activator.CreateInstance(classType, args.ToArray());

                            
                            return obj;
                        });
                }
            }

            return services;
        }
        private static IServiceCollection RegisterPublicNonGenericClasses_DAO(this IServiceCollection services, Assembly assembly,
           Func<Type, bool> predicate = null, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            if (assembly == null)
                throw new ArgumentException("assembly");

            var classes = assembly.ExportedTypes.Where(x => x.IsClass && !x.IsAbstract && !x.IsGenericType && !x.IsNested);

            foreach (var classType in classes)
            {
                var interfaces = classType.GetTypeInfo().ImplementedInterfaces.Where(i => i != typeof(IDisposable) && (i.IsPublic)).Where(t => t.Name != "IBaseRepository");

                if (predicate != null)
                    interfaces = interfaces.Where(predicate);

                foreach (var interfaceType in interfaces)
                {
                    
                    var ctor = classType.GetConstructors();
                    services.AddTransient(interfaceType, provider =>
                    {
                        List<object> args = new List<object>();
                        foreach (var param in ctor[0].GetParameters())
                        {
                            args.Add(provider.GetService(param.ParameterType));
                        }
                        DataAccessObject.Interfaces.IBaseRepository obj = (DataAccessObject.Interfaces.IBaseRepository)Activator.CreateInstance(classType, args.ToArray());
                        
                        return obj;
                    });
                    
                }
            }

            return services;
        }
        
    }
}
