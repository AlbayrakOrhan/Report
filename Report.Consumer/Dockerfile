FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Report.Consumer/Report.Consumer.csproj", "Report.Consumer/"]
RUN dotnet restore "Report.Consumer/Report.Consumer.csproj"
COPY . .
WORKDIR "/src/Report.Consumer"
RUN dotnet build "Report.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Report.Consumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Report.Consumer.dll"]
