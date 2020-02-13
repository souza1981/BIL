#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#docker build -f BIL.API/Dockerfile -t bil .
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["/BIL.API/BIL-API.csproj", "./"]
COPY ["/BIL.Shared/BIL.Shared.csproj", "../BIL.Shared/"]
COPY ["/BIL.Data/BIL.Data.csproj", "../BIL.Data/"]
COPY ["/BIL.Logica/BIL.Logica.csproj", "../BIL.Logica/"]
RUN dotnet restore "BIL-API.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "BIL.API/BIL-API.csproj" -c Release -o /app/build

RUN chmod +x BIL.API/entrypoint.sh
CMD /bin/bash BIL.API/entrypoint.sh

FROM build AS publish
RUN dotnet publish "BIL.API/BIL-API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BIL-API.dll"]
