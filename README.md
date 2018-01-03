# SecureAPI

To install follow the below steps:

  1. Download the repository.
  2. If it is not installed install dotnetcore and SQLLite.
  3. Navigate to the containing folder in a powershell window.
  4. Run dotnet add package Microsoft.EntityFrameworkCore.Design
  5. Run dotnet -restore.
  6. Run .\DatabaseRebuild.ps1.
  7. Run dotnet run.
  
I have included a few test calls for Postman. If you decide to use this be sure to change the keys for the Token (.\Shared\TokenOperator.cs TokenServerKey) and the password hash (.\Shared\PasswordOperator.cs BuildSaltForUser).
