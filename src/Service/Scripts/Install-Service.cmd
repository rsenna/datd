@echo off

for %%A in ( Dataweb.Dilab.Service.exe ) do sc create DatawebDilabService binpath= "%%~fA" start= auto DisplayName= "Dataweb Dilab Service"
if errorlevel 1 (
	pause
	exit /b 1
)
