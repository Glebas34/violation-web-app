
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /app

EXPOSE 5000

COPY ViolationWebApplication/*.csproj ./

RUN dotnet restore

COPY . ./

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS final

WORKDIR /app

COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "ViolationWebApplication.dll"]