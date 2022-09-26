#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Follow-Up.API/Follow-Up.API.csproj", "Follow-Up.API/"]
RUN dotnet restore "Follow-Up.API/Follow-Up.API.csproj"
COPY . .
WORKDIR "/src/Follow-Up.API"
RUN dotnet build "Follow-Up.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Follow-Up.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Follow-Up.API.dll"]