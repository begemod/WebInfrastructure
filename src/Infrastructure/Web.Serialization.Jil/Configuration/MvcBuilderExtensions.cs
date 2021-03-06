﻿namespace Skeleton.Web.Serialization.Jil.Configuration
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Options;

    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder WithJsonFormattersBasedOnJil(
            this IMvcBuilder builder,
            global::Jil.Options serializerSettings)
        {
            if(builder == null)
                throw new ArgumentNullException(nameof(builder));


            builder.Services.AddSingleton(Options.Create(new MvcJilOptions {SerializerSettings = serializerSettings}));
            builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IConfigureOptions<MvcOptions>, MvcOptionsSetup>());
            return builder;
        }
    }
}