Remove-Item -Path "*.db" -Verbose
Remove-Item -Path ".\Migrations\*" -Recurse -Verbose

dotnet ef migrations add "DB Rebuild" -c "SecureAPIDb"

dotnet ef database update -c "SecureAPIDb"


