cd
dotnet publish -c Release -o ../src/WebApi/Mofid.eWallet.Api/publish ../src/WebApi/Mofid.eWallet.Api/Mofid.eWallet.Api.csproj
docker build -f ../src/WebApi/Mofid.eWallet.Api/dockerfile -t mofid_wallet ../src/WebApi/Mofid.eWallet.Api
docker run --rm -it -p 5000:80 -e ASPNETCORE_ENVIRONMENT=loadtest mofid_wallet
docker build -f ../src/clientApp/admin/dockerfile -t mofid_wallet ../src/clientApp/admin