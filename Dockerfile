FROM mcr.microsoft.com/dotnet/core/sdk:2.2-alpine AS build-env
WORKDIR /app

COPY generate_representation.sh /opt/representer/bin/

# Copy csproj and restore as distinct layers
COPY src/Exercism.Representers.CSharp/Exercism.Representers.CSharp.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -r linux-musl-x64 -o /opt/representer

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/runtime-deps:2.2-alpine
WORKDIR /opt/representer
COPY --from=build-env /opt/representer/ .
ENTRYPOINT ["sh", "/opt/representer/bin/generate_representation.sh"]
