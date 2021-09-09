
dotnet publish -c Release -o ../src/WebApi/Mofid.eWallet.Api/publish ../src/WebApi/Mofid.eWallet.Api/Mofid.eWallet.Api.csproj
docker build -f ../src/WebApi/Mofid.eWallet.Api/dockerfile -t mofid_wallet ../src/WebApi/Mofid.eWallet.Api
docker run --rm -it -d -p 5000:80 -e ASPNETCORE_ENVIRONMENT=Development mofid_wallet
docker run -it --rm -d -p 3001:3000 -e CHOKIDAR_USEPOLLING=true wallet-admin:latest
docker-compose -f ../src/WebApi/Mofid.eWallet.Api/Docker-Compose.yml up  

