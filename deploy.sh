#!/bin/bash
set -ev

DOCKER_USERNAME=$1
DOCKER_PASSWORD=$2
$publishFolder = 'bin/publish'
$imageName = 'tarnenok/school-bot'

cd src/TelegramBot.WebApi
dotnet publish -o $publishFolder -c Release

docker login -u="$DOCKER_USERNAME" -p="$DOCKER_PASSWORD"
docker build $publishFolder -t $imageName
docker push $imageName

