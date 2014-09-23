// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNet.Identity;
using Microsoft.Framework.ConfigurationModel;

namespace Microsoft.Framework.DependencyInjection
{
    public static class IdentityServiceCollectionExtensions
    {
        public static IdentityBuilder<IdentityUser, IdentityRole> AddIdentity(this IServiceCollection services, 
            IConfiguration identityConfig)
        {
            services.SetupOptions<IdentityOptions>(identityConfig);
            return services.AddIdentity<IdentityUser, IdentityRole>();
        }

        public static IdentityBuilder<IdentityUser, IdentityRole> AddIdentity(this IServiceCollection services)
        {
            return services.AddIdentity<IdentityUser, IdentityRole>();
        }

        public static IdentityBuilder<TUser, TRole> AddIdentity<TUser, TRole>(this IServiceCollection services, 
            IConfiguration identityConfig)
            where TUser : class
            where TRole : class
        {
            services.SetupOptions<IdentityOptions>(identityConfig);
            return services.AddIdentity<TUser, TRole>();
        }

        public static IdentityBuilder<TUser, TRole> AddIdentity<TUser, TRole>(this IServiceCollection services)
            where TUser : class
            where TRole : class
        {
            services.Add(IdentityServices.GetDefaultUserServices<TUser>());
            services.Add(IdentityServices.GetDefaultRoleServices<TRole>());
            services.AddScoped<UserManager<TUser>>();
            services.AddScoped<SignInManager<TUser>>();
            services.AddScoped<ISecurityStampValidator, SecurityStampValidator<TUser>>();
            services.AddScoped<RoleManager<TRole>>();
            services.AddScoped<IClaimsIdentityFactory<TUser>, ClaimsIdentityFactory<TUser, TRole>>();
            return new IdentityBuilder<TUser, TRole>(services);
        }

        public static IdentityBuilder<TUser, IdentityRole> AddIdentity<TUser>(this IServiceCollection services)
            where TUser : class
        {
            return services.AddIdentity<TUser, IdentityRole>();
        }

        public static IdentityBuilder<TUser, IdentityRole> AddIdentity<TUser>(this IServiceCollection services, 
            IConfiguration identityConfig)
            where TUser : class
        {
            return services.AddIdentity<TUser, IdentityRole>(identityConfig);
        }
    }
}