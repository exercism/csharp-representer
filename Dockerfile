FROM mcr.microsoft.com/dotnet/sdk:7.0.100-alpine3.16-amd64 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY src/Exercism.Representers.CSharp/Exercism.Representers.CSharp.csproj ./
RUN dotnet restore -r linux-musl-x64

# Copy everything else and build
COPY src/Exercism.Representers.CSharp/ ./
RUN dotnet publish -r linux-musl-x64 -c Release -o /opt/representer --no-restore

# Build runtime image
FROM mcr.microsoft.com/dotnet/runtime-deps:7.0.0-alpine3.16-amd64 AS runtime
WORKDIR /opt/representer

COPY --from=build /opt/representer/ .
COPY --from=build /usr/local/bin/ /usr/local/bin/

COPY run.sh /opt/representer/bin/

ENTRYPOINT ["sh", "/opt/representer/bin/run.sh"]
