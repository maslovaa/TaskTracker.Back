# ���������� ����������� ����� .NET SDK ��� ������
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# �������� ���� ������� � ��� ����� ��������
COPY *.sln ./
COPY WebApi/*.csproj ./WebApi/
COPY Domain/*.csproj ./Domain/
COPY DataAccess/*.csproj ./DataAccess/
COPY Models/*.csproj ./Models/
COPY Services/*.csproj ./Services/
COPY Tests/*.csproj ./Tests/

# ��������������� �����������, �������� ���������� ���� �������
RUN dotnet restore ./TaskTracker.Back.sln

# �������� ��������� ����� � �������� ������
COPY . ./
RUN dotnet publish ./WebApi/WebApi.csproj -c Release -o out

# ���������� ����������� ����� .NET Runtime ��� ������� ����������
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# ��������� ����, ���� ��� ����������
EXPOSE 5067

# ��������� ������� ��� ������� ����������
ENTRYPOINT ["dotnet", "WebApi.dll"]
