@echo off
chcp 65001

setlocal

set PROJECT=./NewsAPI/NewsAPI.csproj

set UseRedis=false

dotnet run --project %PROJECT%
