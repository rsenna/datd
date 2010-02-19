@echo off

sc stop DatawebDilabService

sc delete DatawebDilabService 
if errorlevel 1 (
	pause
	exit /b 1
)