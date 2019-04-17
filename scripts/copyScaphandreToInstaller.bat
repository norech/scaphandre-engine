rmdir /S /Q "%2/ScaphandreInstaller/bin/%1/Scaphandre"
mkdir "%2/ScaphandreInstaller/bin/%1/Scaphandre"

cd "%2/Assembly-CSharp.ScaphandreEngine.mm/bin/%1"
xcopy Assembly-CSharp.ScaphandreEngine.mm.dll "%2/ScaphandreInstaller/bin/%1/Scaphandre" /y /d
xcopy ScaphandreEngine.dll "%2/ScaphandreInstaller/bin/%1/Scaphandre" /y /d
