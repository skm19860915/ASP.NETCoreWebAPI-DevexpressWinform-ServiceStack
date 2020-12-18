#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["api/xperters.admin.api.csproj", "api/"]
COPY ["../../xperters-libraries/src/infrastructure/xperters.infrastructure.csproj", "../../xperters-libraries/src/infrastructure/"]
COPY ["../../xperters-libraries/src/mockdata/xperters.mockdata.csproj", "../../xperters-libraries/src/mockdata/"]
COPY ["../../xperters-libraries/src/domain/xperters.domain.csproj", "../../xperters-libraries/src/domain/"]
COPY ["../../xperters-libraries/src/enums/xperters.enums.csproj", "../../xperters-libraries/src/enums/"]
COPY ["../../xperters-libraries/src/extensions/xperters.extensions.csproj", "../../xperters-libraries/src/extensions/"]
COPY ["../../xperters-libraries/src/models/xperters.models.csproj", "../../xperters-libraries/src/models/"]
COPY ["../../xperters-libraries/src/entities/xperters.entities.csproj", "../../xperters-libraries/src/entities/"]
COPY ["../../xperters-libraries/src/configurations/xperters.configurations.csproj", "../../xperters-libraries/src/configurations/"]
COPY ["../../xperters-libraries/src/fileutilities/xperters.fileutilities.csproj", "../../xperters-libraries/src/fileutilities/"]
COPY ["../../xperters-libraries/src/requestheaders/xperters.requestheaders.csproj", "../../xperters-libraries/src/requestheaders/"]
COPY ["../../xperters-libraries/src/queues/xperters.queues.csproj", "../../xperters-libraries/src/queues/"]
COPY ["../../xperters-libraries/src/business/xperters.business.csproj", "../../xperters-libraries/src/business/"]
COPY ["../../xperters-libraries/src/repositories/xperters.repositories.csproj", "../../xperters-libraries/src/repositories/"]
COPY ["../../xperters-libraries/src/azuread/xperters.azuread.csproj", "../../xperters-libraries/src/azuread/"]
COPY ["ServiceModel/Xperters.Admin.ServiceModel.csproj", "ServiceModel/"]
COPY ["ServiceInterface/Xperters.Admin.ServiceInterface.csproj", "ServiceInterface/"]
COPY ["libraries/authentication/Authentication/Xperters.Authentication.csproj", "libraries/authentication/Authentication/"]
COPY ["libraries/correlationid/xperters.correlationid.csproj", "libraries/correlationid/"]
RUN dotnet restore "api/xperters.admin.api.csproj"
COPY . .
WORKDIR "/src/api"
RUN dotnet build "xperters.admin.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "xperters.admin.api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "xperters.admin.api.dll"]
