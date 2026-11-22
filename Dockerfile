FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:11.0.100-alpine3.24 AS build
ARG TARGETARCH

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY src/Exercism.Representers.CSharp/Exercism.Representers.CSharp.csproj .
RUN dotnet restore -a $TARGETARCH

# Copy everything else and build
COPY src/Exercism.Representers.CSharp/ ./
RUN dotnet publish -a $TARGETARCH --no-restore --self-contained true --output /opt/representer

# Build runtime image
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/runtime-deps:11.0.0-alpine3.24 AS runtime
WORKDIR /opt/representer

COPY --from=build /opt/representer/ .
COPY --from=build /usr/local/bin/ /usr/local/bin/

COPY bin/run.sh bin/run.sh

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true

ENTRYPOINT ["sh", "/opt/representer/bin/run.sh"]
