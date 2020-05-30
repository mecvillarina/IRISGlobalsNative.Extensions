#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["SampleWebApp/IRISGlobalsNative.Extensions.SampleWebApp.csproj", "SampleWebApp/"]
COPY ["IRISGlobalsNative.Extensions/IRISGlobalsNative.Extensions.csproj", "IRISGlobalsNative.Extensions/"]
COPY ["IRISGlobalsNative.Extensions/NuGet.config", "IRISGlobalsNative.Extensions/"]
COPY ["IRISGlobalsNative.Extensions/nupkg/*.nupkg", "IRISGlobalsNative.Extensions/nupkg/"]
RUN dotnet restore "IRISGlobalsNative.Extensions/IRISGlobalsNative.Extensions.csproj"
RUN dotnet restore "SampleWebApp/IRISGlobalsNative.Extensions.SampleWebApp.csproj"
COPY . .
WORKDIR "/src/SampleWebApp"
RUN dotnet build "IRISGlobalsNative.Extensions.SampleWebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IRISGlobalsNative.Extensions.SampleWebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IRISGlobalsNative.Extensions.SampleWebApp.dll"]