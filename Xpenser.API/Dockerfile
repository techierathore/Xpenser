#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Xpenser.API/Xpenser.API.csproj", "Xpenser.API/"]
COPY ["Xpenser.Models/Xpenser.Models.csproj", "Xpenser.Models/"]
RUN dotnet restore "Xpenser.API/Xpenser.API.csproj"
COPY . .
WORKDIR "/src/Xpenser.API"
RUN dotnet build "Xpenser.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Xpenser.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Xpenser.API.dll"]