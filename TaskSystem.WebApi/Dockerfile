FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY ./*.sln ./
COPY ./TaskSystem.WebApi/*.csproj ./TaskSystem.WebApi/
RUN dotnet restore

COPY ./TaskSystem.WebApi/. ./TaskSystem.WebApi/
WORKDIR /app/TaskSystem.WebApi
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/TaskSystem.WebApi/out ./
ENTRYPOINT ["dotnet", "TaskSystem.WebApi.dll"]