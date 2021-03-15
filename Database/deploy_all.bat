@echo off

echo(
echo(
echo(    Attention! This script will delete all data in database banking_system and then create it.
echo(    Press any key to proceed...
echo(
echo(
pause >nul

sqlcmd -S "(LocalDB)\MSSQLLocalDB" -v path="%CD%" -i deploy_all.sql -b

pause