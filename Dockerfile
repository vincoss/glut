FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY src/*.csproj ./src/
COPY src/*.config ./src/
RUN nuget restore

# copy everything else and build app
COPY src/. ./src/
WORKDIR /app/src
RUN msbuild /p:Configuration=Release


FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
WORKDIR /inetpub/wwwroot
COPY --from=build /app/src/GlutSvrWeb/bin/Release/netcoreapp3.0/publish/. ./
