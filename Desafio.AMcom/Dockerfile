#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Desafio.AMcom/Desafio.AMcom.csproj", "Desafio.AMcom/"]
COPY ["Desafio.AMcom.Infra/Desafio.AMcom.Infra.csproj", "Desafio.AMcom.Infra/"]
COPY ["Desafio.AMcom.Domain/Desafio.AMcom.Domain.csproj", "Desafio.AMcom.Domain/"]
COPY ["Desafio.AMcom.Application/Desafio.AMcom.Application.csproj", "Desafio.AMcom.Application/"]
RUN dotnet restore "Desafio.AMcom/Desafio.AMcom.csproj"
COPY . .
WORKDIR "/src/Desafio.AMcom"
RUN dotnet build "Desafio.AMcom.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Desafio.AMcom.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Desafio.AMcom.dll"]