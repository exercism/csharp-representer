FROM mcr.microsoft.com/dotnet/sdk:7.0.202-alpine3.16-amd64 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY src/Exercism.Representers.CSharp/Exercism.Representers.CSharp.csproj .
RUN dotnet restore -r linux-musl-x64

# Copy everything else and build
COPY src/Exercism.Representers.CSharp/ ./
RUN dotnet publish -r linux-musl-x64 -c Release --self-contained true -o /opt/representer

# Build runtime image
FROM mcr.microsoft.com/dotnet/runtime-deps:7.0.4-alpine3.16-amd64 AS runtime
WORKDIR /opt/representer

COPY --from=build /opt/representer/ .
COPY --from=build /usr/local/bin/ /usr/local/bin/

COPY bin/run.sh bin/run.sh

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true

ENTRYPOINT ["sh", "/opt/representer/bin/run.sh"]
