FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App
# Copy everything
COPY . ./

RUN ls
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish /App/NatureAPI.csproj -c Release -o /App/build


# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/build .
ENTRYPOINT ["dotnet", "NatureAPI.dll"]