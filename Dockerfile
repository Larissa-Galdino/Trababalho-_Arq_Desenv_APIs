# Estágio de Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia os arquivos e restaura as dependências
COPY *.csproj ./
RUN dotnet restore

# Copia tudo e compila
COPY . ./
RUN dotnet publish -c Release -o out

# Estágio de Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# --- AJUSTES PARA .NET 8 ---
# Força a API a escutar na porta 8080 interna
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 8080

ENTRYPOINT ["dotnet", "TrabalhoApi.dll"]