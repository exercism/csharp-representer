FROM mcr.microsoft.com/dotnet/core/sdk:3.0.101-alpine3.10 AS build-env
WORKDIR /app

COPY generate.sh /opt/representer/bin/

# Copy csproj and restore as distinct layers
COPY src/Exercism.Representers.CSharp/Exercism.Representers.CSharp.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -r linux-musl-x64 -c Release -o /opt/representer

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/runtime-deps:3.0.1-alpine3.10
WORKDIR /opt/representer
COPY --from=build-env /opt/representer/ .
ENTRYPOINT ["sh", "/opt/representer/bin/generate.sh"]
