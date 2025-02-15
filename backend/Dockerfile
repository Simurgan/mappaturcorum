FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

ARG DATABASE_CONNECTION_STRING
# ENV DATABASE_CONNECTION_STRING=${DATABASE_CONNECTION_STRING}

# copy csproj and restore as distinct layers
COPY *.sln .
COPY *.csproj ./
RUN dotnet restore
# install dotnet ef
RUN dotnet tool install --version 8.0.11 --create-manifest-if-needed dotnet-ef

# copy everything else and build app
COPY . ./
WORKDIR /source/
RUN dotnet ef database update --connection "$DATABASE_CONNECTION_STRING"
RUN dotnet publish -c release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "mappa.dll"]