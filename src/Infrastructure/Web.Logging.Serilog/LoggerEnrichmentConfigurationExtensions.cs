﻿namespace Skeleton.Web.Logging.Serilog
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using global::Serilog;
    using global::Serilog.Configuration;

    public static class LoggerEnrichmentConfigurationExtensions
    {
        [ExcludeFromCodeCoverage]
        public static LoggerConfiguration WithApplicationVersion(
            this LoggerEnrichmentConfiguration enrichmentConfiguration, 
            string versionString)
        {
            if(enrichmentConfiguration == null)
                throw new ArgumentNullException(nameof(enrichmentConfiguration));
            if(string.IsNullOrWhiteSpace(versionString))
                throw new ArgumentNullException(nameof(versionString));

            return enrichmentConfiguration.WithProperty("Version", versionString);
        }

        [ExcludeFromCodeCoverage]
        public static LoggerConfiguration WithApplicationInformationalVersion(
            this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            if (enrichmentConfiguration == null)
                throw new ArgumentNullException(nameof(enrichmentConfiguration));

            return enrichmentConfiguration.WithApplicationVersion(
                Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion
            );
        }

        [ExcludeFromCodeCoverage]
        public static LoggerConfiguration WithApplicationAssemblyVersion(
            this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            if (enrichmentConfiguration == null)
                throw new ArgumentNullException(nameof(enrichmentConfiguration));

            return enrichmentConfiguration.WithApplicationVersion(
                Assembly.GetEntryAssembly().GetName().Version.ToString(4)
            );
        }
    }
}