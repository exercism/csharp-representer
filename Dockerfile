FROM mcr.microsoft.com/dotnet/sdk:8.0.100-1-alpine3.18-amd64 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY src/Exercism.Representers.CSharp/Exercism.Representers.CSharp.csproj .
RUN dotnet restore -r linux-musl-x64

# Copy everything else and build
COPY src/Exercism.Representers.CSharp/ ./
RUN dotnet publish -r linux-musl-x64 -c Release --self-contained true -o /opt/representer

# Build runtime image
FROM mcr.microsoft.com/dotnet/runtime-deps:8.0.2-alpine3.18-amd64 AS runtime
WORKDIR /opt/representer

COPY --from=build /opt/representer/ .
COPY --from=build /usr/local/bin/ /usr/local/bin/

COPY bin/run.sh bin/run.sh

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true

ENTRYPOINT ["sh", "/opt/representer/bin/run.sh"]
