FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GeneticAlgoDemo/GeneticAlgoDemo.csproj", "GeneticAlgoDemo/"]
RUN dotnet restore "GeneticAlgoDemo/GeneticAlgoDemo.csproj"
COPY . .
WORKDIR "/src/GeneticAlgoDemo"
RUN dotnet build "GeneticAlgoDemo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GeneticAlgoDemo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GeneticAlgoDemo.dll"]
