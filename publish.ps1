$publishFolder = 'bin/publish'
$imageName = 'tarnenok/school-bot'
dotnet publish -o $publishFolder -c Release
docker build $publishFolder -t $imageName
docker push $imageName
