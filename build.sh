#!/bin/bash
set -ev
dotnet restore
dotnet build ./src/TelegramBot.WebApi/project.json -c Release