@echo off

sc stop DilabServer

sc delete DilabServer 
if errorlevel 1 (
	pause
	exit /b 1
)