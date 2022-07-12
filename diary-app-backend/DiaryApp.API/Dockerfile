FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DiaryApp.API/DiaryApp.API.csproj", "DiaryApp.API/"]
COPY ["DiaryApp.Infrastructure/DiaryApp.Infrastructure.csproj", "DiaryApp.Infrastructure/"]
COPY ["DiaryApp.Services/DiaryApp.Services.csproj", "DiaryApp.Services/"]
COPY ["DiaryApp.Core/DiaryApp.Core.csproj", "DiaryApp.Core/"]
RUN dotnet restore "DiaryApp.API/DiaryApp.API.csproj"
COPY . .
WORKDIR "/src/DiaryApp.API"
RUN dotnet build "DiaryApp.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DiaryApp.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DiaryApp.API.dll"]