using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Services.AppAuthentication;

namespace xperters.infrastructure
{
    public interface IStartup 
    {
        void Configure(IApplicationBuilder app, AzureServiceTokenProvider azureServiceTokenProvider);
        IServiceProvider ConfigureServices(IServiceCollection services);

    }
}