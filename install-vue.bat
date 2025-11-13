@echo off
cd NewsSPA
if not exist "node_modules" (
	npm install
)