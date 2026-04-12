FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["src/MinsuXize.Web/MinsuXize.Web.csproj", "src/MinsuXize.Web/"]
RUN dotnet restore "src/MinsuXize.Web/MinsuXize.Web.csproj"

COPY . .
RUN dotnet publish "src/MinsuXize.Web/MinsuXize.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://0.0.0.0:10000
ENV ASPNETCORE_ENVIRONMENT=Production

COPY --from=build /app/publish .
RUN mkdir -p /app/App_Data

EXPOSE 10000

ENTRYPOINT ["dotnet", "MinsuXize.Web.dll"]
