#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CeoHelper/CeoHelper.Web.csproj", "CeoHelper/"]
COPY ["CeoHelper.Data/CeoHelper.Data.csproj", "CeoHelper.Data/"]
COPY ["CeoHelper.Shared/CeoHelper.Shared.csproj", "CeoHelper.Shared/"]
COPY ["CeoHelper.Services/CeoHelper.Services.csproj", "CeoHelper.Services/"]
COPY ["OpenAI/OpenAI.csproj", "OpenAI/"]
RUN dotnet restore "CeoHelper/CeoHelper.Web.csproj"
COPY . .
WORKDIR "/src/CeoHelper"
RUN dotnet build "CeoHelper.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CeoHelper.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CeoHelper.Web.dll"]