# ���������� ����������� ����� .NET SDK ��� ������
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# �������� ���� ������� � ��� ����� ��������
COPY *.sln ./
COPY WebApi/*.csproj ./WebApi/

# ��������������� �����������
WORKDIR /app/WebApi
RUN dotnet restore

# �������� ��������� ����� � �������� ������
COPY . ./
RUN dotnet publish -c Release -o out

# ���������� ����������� ����� .NET Runtime ��� ������� ����������
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/WebApi/out .

# ��������� ����, ���� ��� ����������
EXPOSE 80

# ��������� ������� ��� ������� ����������
ENTRYPOINT ["dotnet", "WebApi.dll"]