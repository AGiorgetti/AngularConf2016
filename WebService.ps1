#cd "C:\Work\Angular2CrashCourseWebService\WebService"
#dotnet restore
Start-Process -FilePath 'dotnet' -WorkingDirectory '.\WebService' -ArgumentList 'run'
