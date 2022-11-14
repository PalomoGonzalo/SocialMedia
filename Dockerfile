FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY *.sln .
COPY ["SocialMedia.Api/SocialMedia.Api.csproj","SocialMedia.Api/"]
COPY ["SocialMedia.Core/SocialMedia.Core.csproj","SocialMedia.Core/"]
COPY ["SocialMedia.Infraestructura/SocialMedia.Infraestructura.csproj","SocialMedia.Infraestructura/"]
COPY ["SocialMedia.UniTest/SocialMedia.UniTest.csproj","SocialMedia.UniTest/"]

RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet","SocialMedia.Api.dll"]

CMD ASPNETCORE_URLS=https://*:$PORT dotnet SocialMedia.Api.dll