$registry = 'dockerhub.fh-hagenberg.at/swk5'

$images = @(
  'mcr.microsoft.com/dotnet/runtime:5.0-alpine',
  'mcr.microsoft.com/dotnet/sdk:5.0-alpine'
)

foreach ($image in $images) {
  docker pull $registry/$image
  docker tag $registry/$image $image
  docker rmi $registry/$image # untag image
}