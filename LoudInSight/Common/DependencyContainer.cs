
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
            //var currentAssembly = Assembly.GetExecutingAssembly();
            //var businessObjectInterfaceAssembly = currentAssembly.GetReferencedAssemblies()
            //    .Where(asm => asm.Name == "LoudInSight.BusinessObject.Interfaces").FirstOrDefault();
            //var businessObjectAssembly = Assembly.Load(businessObjectInterfaceAssembly).GetReferencedAssemblies()
            //    .Where(asm => asm.Name == "LoudInSight.BusinessObject").FirstOrDefault();
            //var databaseObjectInterfaceAssembly = currentAssembly.GetReferencedAssemblies()
            //    .Where(asm => asm.Name == "LoudInSight.DataAccessObject.Interfaces").FirstOrDefault();
            //var databaseObjectAssembly = Assembly.Load(databaseObjectInterfaceAssembly).GetReferencedAssemblies()
            //    .Where(asm => asm.Name == "LoudInSight.DataAccessObject").FirstOrDefault();
            //List<Assembly> assemblies = new List<Assembly>();
            //foreach (string assemblyPath in Directory.GetFiles(System.AppDomain.CurrentDomain.BaseDirectory, "*.dll", SearchOption.AllDirectories))
            //    {
            //        var assembly = System.Runtime.Loader.AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblyPath);
            //        assemblies.Add(assembly);
            //    }
            //var assemblyScan = Assembly.GetAssembly(typeof(BusinessObject.BaseManager)); // here it gets the dll
            //var assemblyScan = Assembly.GetAssembly(typeof(BusinessObject.Interfaces.IBaseManager));
     //       var tess = Assembly.GetCallingAssembly();
     //       //var taa = Assembly.LoadFrom("LoudInSight.BusinessObject.dll");    
     //       //     services.RegisterAssemblyPublicNonGenericClasses(
     //       //Assembly.GetAssembly(typeof("")), Assembly.GetAssembly(typeof("")))
     //       var files = Directory.GetFiles(System.AppDomain.CurrentDomain.BaseDirectory, "*.dll", SearchOption.AllDirectories);
     //       var res = System.IO.Path.GetRelativePath("LounInSight", AppDomain.CurrentDomain.BaseDirectory);
     //       var r = AppDomain.CurrentDomain.BaseDirectory.Substring(0, res.Length);
     //       var rr = AppDomain.CurrentDomain.BaseDirectory.Split("\\");
     //       StringBuilder stringBuilder = new StringBuilder();
     //       foreach (var item in rr)
     //       {
     //           if (item == "LoudInSight")
     //           {
     //               stringBuilder.Append(item + "\\");
     //               break;
     //           }
     //           stringBuilder.Append(item + "\\");
     //       }
     //       //var rrr = String.Join("\\", rr.TakeWhile(a=>a == "LoudInSight"));
     //       //var ab = new int[4] {1,2,3,4 };
     //       //var tt = ab.TakeWhile(at=>at==3);
     //       var rests = stringBuilder.ToString();
     //       //var rrss = Directory.GetFiles(rests, rests+"LoundInSight.BusinessObject\\bin\\Debug\netstandard2.0\\LoudInSight.BusinessObject.dll", SearchOption.AllDirectories);
     //       //var ttaa = Assembly.LoadFrom(rests + "LoundInSight.BusinessObject\\bin\\Debug\\netstandard2.0\\LoudInSight.BusinessObject.dll");
     //       //var businessObjectAssembly = Assembly.Load(rrss.Where(asm => asm.EndsWith("LoudInSight.BusinessObject.dll")).FirstOrDefault());
     //       // var atra = ttaa.GetReferencedAssemblies();
     //       // var assemblies = AppDomain.CurrentDomain.BaseDirectory.Where(asm => asm.Equals("LoudInSight.*.dll"));
     //       //.. register
     //       // services.sc(scan => scan
     //       //.FromAssemblies(assemblies)
     //       //.AddClasses(classes => classes.AssignableTo<ILifecycle>(), publicOnly: false)
     //       //.AsImplementedInterfaces()
     //       //.WithTransientLifetime());



     //       //services.AddTransient<IAccountManager, AccountManager>();
     //       //services.AddTransient<DataAccessObject.Interfaces.IAccountDAO, DataAccessObject.AccountDAO>();

     //       //var businessObjectAssembly = Assembly.LoadFrom(rests + "LoudInSight.BusinessObject\\bin\\Debug\\netstandard2.0\\LoudInSight.BusinessObject.dll");
     //       //var businessObjectInterfaceAssembly = currentAssembly.GetReferencedAssemblies()
     //       //    .Where(asm => asm.Name == "LoudInSight.BusinessObject.Interfaces").FirstOrDefault();

     //       ////var businessObjectAssembly = Assembly.Load(businessObjectInterfaceAssembly).GetReferencedAssemblies()
     //       ////    .Where(asm => asm.Name == "LoudInSight.BusinessObject").FirstOrDefault();
     //       //var databaseObjectInterfaceAssembly = businessObjectAssembly.GetReferencedAssemblies()
     //       //    .Where(asm => asm.Name == "LoudInSight.DataAccessObject.Interfaces").FirstOrDefault();
     //       //var databaseObjectAssembly = Assembly.LoadFrom(rests + "LoudInSight.DataAccessObject\\bin\\Debug\\netstandard2.0\\LoudInSight.DataAccessObject.dll");
     //       //Assembly.Load(databaseObjectInterfaceAssembly).GetReferencedAssemblies()
     //       //    .Where(asm => asm.Name == "LoudInSight.DataAccessObject").FirstOrDefault();
     ////       object p = services.Scan(scan =>
     ////scan.FromCallingAssembly()
     ////    .AddClasses()
     ////    .AsMatchingInterface());
            var businessObjectAssembly = Assembly.GetAssembly(typeof(BusinessObject.BaseManager)); 
            //currentAssembly.GetReferencedAssemblies()
              //  .Where(asm => asm.Name == "LoudInSight.BusinessObject").FirstOrDefault();
            var databaseObjectAssembly = Assembly.GetAssembly(typeof(DataAccessObject.BaseRepository));
           //var databaseObjectAssembly = a.GetReferencedAssemblies()
           //     .Where(asm => asm.Name == "LoudInSight.DataAccessObject").FirstOrDefault();
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
                    //var isAManager = interfaceType.Name.EndsWith("Manager");

                    //if (isAManager)
                    //{
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

                            //obj.UserContext = SetUserExecutionContext(provider.GetService<IHttpContextAccessor>()?.HttpContext);

                            return obj;
                        });

                        //var serviceDescriptor = new ServiceDescriptor(interfaceType, classType, ServiceLifetime.Transient);
                        //services.Add(serviceDescriptor);
                    //}
                    //else
                    //{
                    //    var ctor = classType.GetConstructors();
                    //    services.AddTransient(interfaceType, provider =>
                    //    {
                    //        List<object> args = new List<object>();
                    //        foreach (var param in ctor[0].GetParameters())
                    //        {
                    //            args.Add(provider.GetService(param.ParameterType));
                    //        }
                    //        DataAccessObject.Interfaces.IBaseDAO obj = (DataAccessObject.Interfaces.IBaseDAO)Activator.CreateInstance(classType, args.ToArray());
                    //        //obj.UserContext = SetUserExecutionContext(provider.GetService<IHttpContextAccessor>()?.HttpContext);

                    //        return obj;
                    //    });
                    //}
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
                    //var isAManager = interfaceType.Name.EndsWith("Manager");

                    //if (isAManager)
                    //{
                    //    var ctor = classType.GetConstructors();

                    //    services.AddTransient(interfaceType, provider =>
                    //    {
                    //        List<object> args = new List<object>();

                    //        //Thread.CurrentPrincipal = RegisterThreadPrincipal(provider.GetService<IHttpContextAccessor>()?.HttpContext);

                    //        foreach (var param in ctor[0].GetParameters())
                    //        {
                    //            args.Add(provider.GetService(param.ParameterType));
                    //        }

                    //        BusinessObject.Interfaces.IBaseManager obj = (BusinessObject.Interfaces.IBaseManager)Activator.CreateInstance(classType, args.ToArray());

                    //        //obj.UserContext = SetUserExecutionContext(provider.GetService<IHttpContextAccessor>()?.HttpContext);

                    //        return obj;
                    //    });

                    //    //var serviceDescriptor = new ServiceDescriptor(interfaceType, classType, ServiceLifetime.Transient);
                    //    //services.Add(serviceDescriptor);
                    //}
                    //else
                    //{
                        var ctor = classType.GetConstructors();
                        services.AddTransient(interfaceType, provider =>
                        {
                            List<object> args = new List<object>();
                            foreach (var param in ctor[0].GetParameters())
                            {
                                args.Add(provider.GetService(param.ParameterType));
                            }
                            DataAccessObject.Interfaces.IBaseRepository obj = (DataAccessObject.Interfaces.IBaseRepository)Activator.CreateInstance(classType, args.ToArray());
                            //obj.UserContext = SetUserExecutionContext(provider.GetService<IHttpContextAccessor>()?.HttpContext);

                            return obj;
                        });
                    //}
                }
            }

            return services;
        }
        //public static UserExecutionContext SetUserExecutionContext(HttpContext context)
        //{
        //    if (context == null)
        //        throw new NullReferenceException("context");

        //    var userExecutionContext = context.Request?.Headers.FirstOrDefault(x => x.Key.ToLower() == "userexecutioncontext").Value.ToString();

        //    if (string.IsNullOrEmpty(userExecutionContext))
        //        throw new NullReferenceException("userExecutionContext");

        //    return JsonConvert.DeserializeObject<UserExecutionContext>(userExecutionContext);
        //}

        ////public static IServiceCollection RegisterHttpClient(this IServiceCollection services)
        ////{

        ////    services.AddTransient<IOrderHttpClient, OrderHttpClient>(client =>
        ////    {
        ////        var serviceProvider = services.BuildServiceProvider();

        ////        var userContextFromHeader = serviceProvider.GetService<IHttpContextAccessor>()?.HttpContext?.Request?.Headers.FirstOrDefault(x => x.Key.ToLower() == "userexecutioncontext").Value.ToString();
        ////        var jwtToken = serviceProvider.GetService<IHttpContextAccessor>()?.HttpContext?.Request?.Headers.FirstOrDefault(x => x.Key == "Authorization").Value.ToString();

        ////        Thread.CurrentPrincipal = RegisterThreadPrincipal(jwtToken);

        ////        var parseHeader = JObject.Parse(userContextFromHeader);

        ////        var userExecutionContext = parseHeader.ToObject(typeof(UserExecutionContext)) as UserExecutionContext;

        ////        string APIMSubscriptionKey = MultiRegionConfig
        ////                                                    .GetConfig(MultiRegionConfigTypes.PrimaryRegion, CloudConfig.APIMSubscriptionKey, userExecutionContext.BuyerPartnerCode)
        ////                                                    .FirstOrDefault();


        ////        client.DefaultRequestHeaders.Add("UserExecutionContext", JsonConvert.SerializeObject(userExecutionContext));

        ////        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIMSubscriptionKey);

        ////        client.DefaultRequestHeaders.Add("BPC", Convert.ToString(userExecutionContext.BuyerPartnerCode));

        ////        client.DefaultRequestHeaders.Add("RegionID", Convert.ToString(MultiRegionConfig.PrimaryRegion));

        ////        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        ////        if(!string.IsNullOrEmpty(jwtToken))
        ////            client.DefaultRequestHeaders.Add("Authorization", jwtToken);

        ////        //HttpClientHelper.SetHttpClient(client);
        ////    });
        ////    return services;
        ////}

        //public static void AddSmartHttpClient(this IServiceCollection services)
        //{
        //    services.AddTransient(typeof(ISmartHttpClient), client =>
        //    {
        //        SmartHttpClient httpClient = new SmartHttpClient();
        //        httpClient.Headers = new Dictionary<string, string>();
        //        var serviceProvider = services.BuildServiceProvider();

        //        var userContextFromHeader = serviceProvider.GetService<IHttpContextAccessor>()?.HttpContext?.Request?.Headers.FirstOrDefault(x => x.Key.ToLower() == "userexecutioncontext").Value.ToString();
        //        var jwtToken = serviceProvider.GetService<IHttpContextAccessor>()?.HttpContext?.Request?.Headers.FirstOrDefault(x => x.Key == "Authorization").Value.ToString();

        //        Thread.CurrentPrincipal = RegisterThreadPrincipal(jwtToken);

        //        var parseHeader = JObject.Parse(userContextFromHeader);

        //        var userExecutionContext = parseHeader.ToObject(typeof(UserExecutionContext)) as UserExecutionContext;

        //        string APIMSubscriptionKey = MultiRegionConfig
        //                                                    .GetConfig(MultiRegionConfigTypes.PrimaryRegion, CloudConfig.APIMSubscriptionKey, userExecutionContext.BuyerPartnerCode)
        //                                                    .FirstOrDefault();


        //        httpClient.Headers.Add("UserExecutionContext", JsonConvert.SerializeObject(userExecutionContext));
        //        httpClient.Headers.Add("Ocp-Apim-Subscription-Key", APIMSubscriptionKey);
        //        httpClient.Headers.Add("BPC", Convert.ToString(userExecutionContext.BuyerPartnerCode));
        //        httpClient.Headers.Add("RegionID", Convert.ToString(MultiRegionConfig.PrimaryRegion));

        //        if (!string.IsNullOrEmpty(jwtToken))
        //            httpClient.Headers.Add("Authorization", jwtToken);

        //        return httpClient;
        //    });
        //}

        //public static ClaimsPrincipal RegisterThreadPrincipal(HttpContext httpContext)
        //{
        //    var jwtToken = httpContext?.Request?.Headers.FirstOrDefault(x => x.Key == "Authorization").Value.ToString().Replace("Bearer ", string.Empty);

        //    return JwtTokenHelper.ValidateJwtToken(jwtToken);

        //}
        //public static ClaimsPrincipal RegisterThreadPrincipal(string jwtToken)
        //{
        //    return JwtTokenHelper.ValidateJwtToken(jwtToken.Replace("Bearer ", string.Empty));
        //}
    }
}
