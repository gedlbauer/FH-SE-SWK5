dotnet publish PrimeCalc.Client --configuration Release --self-contained --runtime win-x64 --output $pwd/scd-win-x64

dotnet publish PrimeCalc.Client --configuration Release --self-contained --runtime linux-musl-x64 --output $pwd/scd-linux-musl-x64
