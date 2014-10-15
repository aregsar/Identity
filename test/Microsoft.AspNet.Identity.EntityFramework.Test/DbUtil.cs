// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNet.Hosting;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Services;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Fallback;
using Microsoft.Framework.Logging;
using Microsoft.Framework.OptionsModel;
using Xunit;

namespace Microsoft.AspNet.Identity.EntityFramework.Test
{
    [TestCaseOrderer("Microsoft.AspNet.Identity.Test.PriorityOrderer", "Microsoft.AspNet.Identity.EntityFramework.Test")]
    public static class DbUtil
    {
        public static IServiceCollection ConfigureDbServices(string connectionString, IServiceCollection services = null)
        {
            return ConfigureDbServices<IdentityDbContext>(connectionString, services);
        }

        public static IServiceCollection ConfigureDbServices<TContext>(string connectionString, IServiceCollection services = null) where TContext : DbContext
        {
            if (services == null)
            {
                services = new ServiceCollection();
            }
            services.Add(HostingServices.GetDefaultServices());
            services.AddEntityFramework().AddSqlServer().AddDbContext<TContext>(options => options.UseSqlServer(connectionString));
            services.Add(OptionsServices.GetDefaultServices());
            services.AddInstance<ILoggerFactory>(new NullLoggerFactory());
            return services;
        }

        public static IdentityDbContext Create(string connectionString)
        {
            return Create<IdentityDbContext>(connectionString);
        }

        public static TContext Create<TContext>(string connectionString) where TContext : DbContext, new()
        {
            var serviceProvider = ConfigureDbServices<TContext>(connectionString).BuildServiceProvider();
            //public void foo(IServiceCollection services)
            //{
            //    services.AddEntityFramework().AddSqlServer().AddDbContext<IdentityDbContext>(options => options.UseSqlServer("<connection>"));
            //    var serviceProvider = services.BuildServiceProvider();
            //    var dbFactory = serviceProvider.GetService<IdentityDbContext>();
            //}
            return serviceProvider.GetService<TContext>();
        }

    }
}