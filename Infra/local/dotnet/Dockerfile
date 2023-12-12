FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
ARG ENVIRONMENT
ENV ASPNETCORE_ENVIRONMENT $ENVIRONMENT
WORKDIR "/app"
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR "/src"
COPY server .
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:~/.dotnet/tools"
RUN dotnet ef migrations bundle \
  -o efbundle \
  -c AppDbContext \
  -p src/Recipes.Infrastructure/Recipes.Infrastructure.csproj \
  -s src/Recipes.WebApi/Recipes.WebApi.csproj \
  --configuration $BUILD_CONFIGURATION
WORKDIR "/src/src/Recipes.WebApi"
RUN dotnet restore
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR "/src/src/Recipes.WebApi"
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
RUN apt update && apt install dos2unix
WORKDIR "/app"
COPY --from=publish /app/publish .
COPY --from=build /src/efbundle .
COPY infra/local/dotnet/entrypoint.sh .
RUN dos2unix entrypoint.sh
RUN chmod +x entrypoint.sh
ENTRYPOINT ["./entrypoint.sh"]
