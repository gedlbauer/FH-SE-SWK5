param ([switch]$Clean = $false)

if ($Clean) {
  Remove-Item -ErrorAction SilentlyContinue -Force -Recurse ./bin,./obj
  return
}

dotnet build
if ($?) {
  dotnet run
}
