$imageName = 'tarnenok/school-bot'
$containerVolume = ''
$localVolume = ''
docker run -d -it -p 5001:5001 -v $localVolume:$containerVolume $imageName