﻿namespace Skeleton.Web.Authentication.JwtBearer
{
    using System;
    using Microsoft.AspNetCore.Builder;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseJwtBearerAuthorizationTokens(this IApplicationBuilder applicationBuilder)
        {
            if (applicationBuilder == null)
                throw new ArgumentNullException(nameof(applicationBuilder));

            return applicationBuilder
                .UseMiddleware<TokensIssuingMiddleware>()
                .UseAuthentication()
                .UseAuthorization();
        }
    }
}