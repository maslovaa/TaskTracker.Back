# Используем официальный образ .NET SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Копируем файл решения и все файлы проектов
COPY *.sln ./
COPY WebApi/*.csproj ./WebApi/
COPY Domain/*.csproj ./Domain/
COPY DataAccess/*.csproj ./DataAccess/
COPY Models/*.csproj ./Models/
COPY Services/*.csproj ./Services/
COPY Tests/*.csproj ./Tests/

# Восстанавливаем зависимости, указывая конкретный файл решения
RUN dotnet restore ./TaskTracker.Back.sln

# Копируем остальные файлы и собираем проект
COPY . ./
RUN dotnet publish ./WebApi/WebApi.csproj -c Release -o out

# Используем официальный образ .NET Runtime для запуска приложения
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Открываем порт, если это необходимо
EXPOSE 5067

# Указываем команду для запуска приложения
ENTRYPOINT ["dotnet", "WebApi.dll"]
