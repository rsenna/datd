@echo off

for %%A in ( Dilab.Server.exe ) do sc create DilabServer binpath= "%%~fA" start= auto DisplayName= "Dataweb Dilab Server"
if errorlevel 1 (
	pause
	exit /b 1
)
