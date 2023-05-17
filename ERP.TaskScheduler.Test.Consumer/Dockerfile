FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ERP.TaskScheduler.Test.Consumer/ERP.TaskScheduler.Test.Consumer.csproj", "ERP.TaskScheduler.Test.Consumer/"]
RUN dotnet restore "ERP.TaskScheduler.Test.Consumer/ERP.TaskScheduler.Test.Consumer.csproj"
COPY . .
WORKDIR "/src/ERP.TaskScheduler.Test.Consumer"
RUN dotnet build "ERP.TaskScheduler.Test.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ERP.TaskScheduler.Test.Consumer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ERP.TaskScheduler.Test.Consumer.dll"]
