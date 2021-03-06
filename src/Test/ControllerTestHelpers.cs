﻿using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Aspenlaub.Net.GitHub.CSharp.OhDataMyData.Test {
    public class ControllerTestHelpers {
        public static HttpClient CreateHttpClient() {
            var builder = new WebHostBuilder().UseStartup<Startup>();
            var server = new TestServer(builder);
            return server.CreateClient();
        }
    }
}
