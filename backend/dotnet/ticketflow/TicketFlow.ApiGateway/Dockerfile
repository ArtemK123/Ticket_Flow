FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /build

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish --no-restore -v q -c Release -o publishFolder

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /build/publishFolder .
ENTRYPOINT ["dotnet", "TicketFlow.ApiGateway.dll"]