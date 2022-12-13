﻿using System.Net.Http;
using Employee.FunctionalTests;
using Xunit;

namespace Employee.FunctionalTests.Controllers
{
    public class BaseControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        public BaseControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        public HttpClient GetNewClient()
        {
            var newClient = _factory.WithWebHostBuilder(builder =>
            {
                _factory.CustomConfigureServices(builder);
            }).CreateClient();

            return newClient;
        }
    }
}