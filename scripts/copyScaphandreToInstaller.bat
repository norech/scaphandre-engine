rmdir /S /Q "%2/ScaphandreInstaller/bin/%1/Scaphandre"
mkdir "%2/ScaphandreInstaller/bin/%1/Scaphandre"

cd "%2/Assembly-CSharp.ScaphandreEngine.mm/bin/%1"
xcopy ScaphandreInjector.CodeModification.dll "%2/ScaphandreInstaller/bin/%1/Scaphandre" /y /d
xcopy ScaphandreInjector.dll "%2/ScaphandreInstaller/bin/%1/Scaphandre" /y /d
xcopy ScaphandreEngine.dll "%2/ScaphandreInstaller/bin/%1/Scaphandre" /y /d
xcopy Assembly-CSharp.ScaphandreEngine.mm.dll "%2/ScaphandreInstaller/bin/%1/Scaphandre" /y /d
