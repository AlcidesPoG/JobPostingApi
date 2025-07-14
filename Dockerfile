FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

RUN mkdir -p /app/wwwroot

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["JobPostingApi.csproj", "."]
COPY ["JobPostingApplication/JobPostingApplication.csproj", "JobPostingApplication/"]
COPY ["JobPostingDomain/JobPostingDomain.csproj", "JobPostingDomain/"]
COPY ["Utilities/JobPostingUtilities.csproj", "Utilities/"]
COPY ["JobPostingInfraestructure/JobPostingInfraestructure.csproj", "JobPostingInfraestructure/"]

RUN dotnet restore "./JobPostingApi.csproj"

COPY . .

RUN dotnet build "./JobPostingApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./JobPostingApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

RUN mkdir -p /app/wwwroot

ENTRYPOINT ["dotnet", "JobPostingApi.dll"]