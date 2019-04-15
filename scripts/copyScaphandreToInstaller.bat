rmdir /S /Q "%2/ScaphandreInstaller/bin/%1/Scaphandre"
mkdir "%2/ScaphandreInstaller/bin/%1/Scaphandre"

cd "%2/Assembly-CSharp.ScaphandreEngine.mm/bin/%1"
xcopy *.dll "%2/ScaphandreInstaller/bin/%1/Scaphandre" /y /d