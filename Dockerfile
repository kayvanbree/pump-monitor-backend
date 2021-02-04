FROM mcr.microsoft.com/dotnet/sdk:3.1 as build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY pump-monitor-backend/*.csproj ./pump-monitor-backend/
RUN dotnet restore

# copy everything else and build app
COPY pump-monitor-backend/. ./pump-monitor-backend/
WORKDIR /source/pump-monitor-backend
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build /app ./
ENV ASPNETCORE_URLS="http://*:$PORT"
RUN echo $ASPNETCORE_URLS
ENTRYPOINT ["dotnet", "pump-monitor-backend.dll"]