﻿namespace Web.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using Client;
    using Microsoft.Extensions.Options;
    using Skeleton.Web.Integration.BaseApiClient.Exceptions;
    using Skeleton.Web.Serialization.Jil.Serializer;
    using Skeleton.Web.Testing;
    using Skeleton.Web.Testing.Extensions;
    using Xunit;

    [Collection(nameof(ApiTestsCollection))]
    public class AccountApiClientTests : BaseApiClientTests<BaseApiTestsFixture<Startup>, ApiClient>
    {
        public AccountApiClientTests(BaseApiTestsFixture<Startup> fixture)
            : base(
                fixture,
                (httpClient, baseUrl, timeout) =>
                    new ApiClient(
                        httpClient,
                        Options.Create(
                            new ApiClientOptions
                            {
                                BaseUrl = baseUrl,
                                Timeout = timeout,
                                Serializer = JilSerializer.Default
                            }
                        )
                    )
            )
        {
        }

        [Fact]
        public void ShouldGetUserInfo()
        {
            // Given
            const string login = "lhp@lhp.com";
            const string password = "1234";

            // When
            ApiClient
                .Login(login, password)
                .UserInfo();

            // Then
            Fixture.MockLogger.VerifyNoErrorsWasLogged();
            Assert.Equal($"{ClaimTypes.Email}:{login}", ((IEnumerable<string>) ApiClient.CurrentState).First());
        }

        [Fact]
        public void ShouldThrowUnauthorizedExceptionWhileAccessingUserInfoWithoutToken()
        {
            Assert.Throws<UnauthorizedException>(() => ApiClient.UserInfo());
        }

        [Fact]
        public void ShouldReturnBadRequestWhenLoginNotProvided()
        {
            Assert.Throws<BadRequestException>(() => ApiClient.Login(null, "1234"));
        }

        [Fact]
        public void ShouldReturnBadRequestWhenLoginIsIncorrect()
        {
            Assert.Throws<BadRequestException>(() => ApiClient.Login("lhp1@lhp.com", "1234"));
        }

        [Fact]
        public void ShouldReturnBadRequestWhenPasswordIsIncorrect()
        {
            Assert.Throws<BadRequestException>(() => ApiClient.Login("lhp@lhp.com", "12345"));
        }

        [Fact]
        public void ShouldReturnBadRequestWhenPasswordNotProvided()
        {
            Assert.Throws<BadRequestException>(() => ApiClient.Login("lhp@lhp.com", null));
        }
    }
}