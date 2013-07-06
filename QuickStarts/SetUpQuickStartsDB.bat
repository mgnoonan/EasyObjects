@echo off
@REM  ----------------------------------------------------------------------------
@REM  SetUpQuickStartsDB.bat file
@REM
@REM  This batch file calls the SQL script to create the tables required for the
@REM  EasyObjects.NET QuickStart. You must have the sample database named
@REM  Northwind installed. If the database does not yet exist, the SQL script
@REM  will fail.
@REM  
@REM  ----------------------------------------------------------------------------

echo.
echo =========================================================
echo   SetUpQuickStartsDB
echo      Creates the tables required for the EasyObjects.NET
echo      QuickStart. The procedures are created in the database
echo      Northwind. The script will fail if the database 
echo      does not exist.                           
echo =========================================================
echo.

set pause=true

if "%1"=="/?" goto HELP

if not Exist EasyObjectsQuickStarts.sql goto HELPSCRIPT

@REM  ----------------------------------------------------
@REM  If the first parameter is /q, do not pause
@REM  at the end of execution.
@REM  ----------------------------------------------------

if /i "%1"=="/q" (
 set pause=false
)

@REM  ------------------------------------------------
@REM  Shorten the command prompt for making the output
@REM  easier to read.
@REM  ------------------------------------------------
set savedPrompt=%prompt%
set prompt=*$g

@ECHO ----------------------------------------
@ECHO SetUpQuickStartsDB.bat Started
@ECHO ----------------------------------------
@ECHO.

OSQL -E -i EasyObjectsQuickStarts.sql

@ECHO.
@ECHO ----------------------------------------
@ECHO SetUpQuickStartsDB.bat Completed
@ECHO ----------------------------------------
@ECHO.

@REM  ----------------------------------------
@REM  Restore the command prompt and exit
@REM  ----------------------------------------
@goto :exit

@REM  -------------------------------------------
@REM  Handle errors
@REM
@REM  Use the following after any call to exit
@REM  and return an error code when errors occur
@REM
@REM  if errorlevel 1 goto :error	
@REM  -------------------------------------------
:error
@ECHO An error occured in SetUpQuickStartsDB.bat - %errorLevel%
if %pause%==true PAUSE
@exit errorLevel

:HELPSCRIPT
echo Error: Unable to locate the required SQL script EasyObjectsQuickStarts.sql
echo.
goto exit

:HELP
echo Usage: SetUpQuickStartsDB.bat 
echo.

@REM  ----------------------------------------
@REM  The exit label
@REM  ----------------------------------------
:exit
if %pause%==true PAUSE

set pause=
set prompt=%savedPrompt%
set savedPrompt=

echo on

