FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["SantaHelena.ClickDoBem.Services.Api/SantaHelena.ClickDoBem.Services.Api.csproj", "SantaHelena.ClickDoBem.Services.Api/"]
RUN dotnet restore "SantaHelena.ClickDoBem.Services.Api/SantaHelena.ClickDoBem.Services.Api.csproj"
COPY . .
WORKDIR "/src/SantaHelena.ClickDoBem.Services.Api"
RUN dotnet build "SantaHelena.ClickDoBem.Services.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SantaHelena.ClickDoBem.Services.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SantaHelena.ClickDoBem.Services.Api.dll"]
