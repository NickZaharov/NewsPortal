@echo off
setlocal
chcp 65001

echo Запуск .NET-приложения...
start "" ".\NewsApi\run-dotnet.bat"

echo Запуск Vue-приложения...
start "" ".\NewsSPA\run-vue.bat"