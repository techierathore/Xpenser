#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Xpenser.Web/Xpenser.Web.csproj", "Xpenser.Web/"]
COPY ["Xpenser.UI/Xpenser.UI.csproj", "Xpenser.UI/"]
COPY ["Xpenser.Models/Xpenser.Models.csproj", "Xpenser.Models/"]
RUN dotnet restore "Xpenser.Web/Xpenser.Web.csproj"
COPY . .
WORKDIR "/src/Xpenser.Web"
RUN dotnet build "Xpenser.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Xpenser.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Xpenser.Web.dll"]