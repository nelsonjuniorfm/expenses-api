# Usando a imagem oficial do .NET 8 como base para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia os arquivos do projeto e restaura as dependências
COPY *.csproj ./
RUN dotnet restore

# Copia o restante dos arquivos e compila a aplicação
COPY . ./
RUN dotnet publish -c Release -o /out

# Usando a imagem runtime para a execução da aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out ./

# Definir a porta de exposição
EXPOSE 8080
EXPOSE 8081

ENTRYPOINT ["dotnet", "expenses-api.dll"]