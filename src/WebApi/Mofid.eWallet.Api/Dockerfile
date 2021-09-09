
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_ENVIRONMENT=loadtest

COPY ./publish ./
ENTRYPOINT ["dotnet", "Mofid.eWallet.Api.dll"]