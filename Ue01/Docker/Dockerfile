FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine
COPY ./fdd /app
WORKDIR /app
ENTRYPOINT ["dotnet", "/app/PrimeCalc.Client.dll"]