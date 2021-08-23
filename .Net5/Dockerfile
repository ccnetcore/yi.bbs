#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
ADD http://ftp.us.debian.org/debian/pool/main/c/ca-certificates/ca-certificates_20210119_all.deb .
RUN dpkg -i ca-certificates_20210119_all.deb
RUN apt-get update && apt-get install -y ca-certificates && update-ca-certificates && rm -rf /var/lib/apt/lists/*
WORKDIR /src
COPY ["CC.Yi.API/CC.Yi.API.csproj", "CC.Yi.API/"]
COPY ["CC.Yi.Model/CC.Yi.Model.csproj", "CC.Yi.Model/"]
COPY ["CC.Yi.ViewModel/CC.Yi.ViewModel.csproj", "CC.Yi.ViewModel/"]
COPY ["CC.Yi.IBLL/CC.Yi.IBLL.csproj", "CC.Yi.IBLL/"]
COPY ["CC.Yi.Common/CC.Yi.Common.csproj", "CC.Yi.Common/"]
COPY ["CC.Yi.DAL/CC.Yi.DAL.csproj", "CC.Yi.DAL/"]
COPY ["CC.Yi.IDAL/CC.Yi.IDAL.csproj", "CC.Yi.IDAL/"]
COPY ["CC.Yi.BLL/CC.Yi.BLL.csproj", "CC.Yi.BLL/"]
RUN dotnet restore "CC.Yi.API/CC.Yi.API.csproj"
COPY . .
WORKDIR "/src/CC.Yi.API"
RUN dotnet build "CC.Yi.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CC.Yi.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CC.Yi.API.dll"]