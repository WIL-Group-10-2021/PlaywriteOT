FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

ENV ASPNETCORE_Environment=Production

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["PlaywriteOT.csproj", "PlaywriteOT/"]
RUN dotnet restore "PlaywriteOT\PlaywriteOT.csproj"

WORKDIR "/src/PlaywriteOT"
COPY . .

RUN dotnet build "PlaywriteOT.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PlaywriteOT.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PlaywriteOT.dll"] 
