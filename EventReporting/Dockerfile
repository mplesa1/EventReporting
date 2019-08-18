FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["EventReporting/EventReporting.csproj", "EventReporting/"]
RUN dotnet restore "EventReporting/EventReporting.csproj"
COPY . .
WORKDIR "/src/EventReporting"
RUN dotnet build "EventReporting.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "EventReporting.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EventReporting.dll"]