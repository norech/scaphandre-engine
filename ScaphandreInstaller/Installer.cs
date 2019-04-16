using ScaphandreInstaller.Utils;
using SharpCompress.Archives.Zip;
using SharpCompress.Common;
using SharpCompress.Readers;
using SharpCompress.Writers;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ScaphandreInstaller
{
    class Installer
    {
        public static string GetGameBinariesFolder(string path)
        {
            return Path.Combine(path, @"Subnautica_Data\Managed");
        }

        public static bool IsValidPath(string path)
        {
            var binariesFolder = GetGameBinariesFolder(path);

            return File.Exists(Path.Combine(binariesFolder, "Assembly-CSharp.dll"));
        }

        public static bool IsScaphandreInstalled(string path)
        {
            var binariesFolder = GetGameBinariesFolder(path);

            return File.Exists(Path.Combine(binariesFolder, "Assembly-CSharp.unpatched.dll"));
        }
        
        public static DateTime GetSubnauticaBuildTime(string path)
        {
            var buildFile = Path.Combine(path, "__buildtime.txt");
            DateTime minValue = DateTime.MinValue;

            try
            {
                if (File.Exists(buildFile))
                {
                    string s = File.ReadAllText(buildFile).Trim();
                    CultureInfo culture = new CultureInfo("en-US");
                    minValue = Convert.ToDateTime(s, culture);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return minValue;
        }

        BackgroundWorker worker;
        string gamePath;
        bool createModdingArchive;

        public Installer(BackgroundWorker worker, string gamePath, bool createModdingArchive)
        {
            this.worker = worker;
            this.gamePath = gamePath;
            this.createModdingArchive = createModdingArchive;
        }

        #region Install
        public void DownloadLatestMonomod(string targetFolder)
        {
            Console.WriteLine("Retrieving MonoMod latest release");
            var releaseInfos = NetworkUtil.GetJson("http://api.github.com/repos/0x0ade/MonoMod/releases/latest");

            var assetInfos = releaseInfos.Element("root").Elements("assets")
                                .First(a => {
                                    var name = a.Element("name").Value;
                                    return name.Contains("net35") && name.EndsWith(".zip");
                                });

            var fileUrl = assetInfos.Element("browser_download_url").Value;
            var filePath = Path.Combine(targetFolder, "monomod.zip");
            Console.WriteLine("Retrieved MonoMod latest release");

            Console.WriteLine("Download URL: " + fileUrl);
            Console.WriteLine("Downloading MonoMod");
            NetworkUtil.DownloadFile(fileUrl, filePath);
            Console.WriteLine("Downloaded MonoMod");
        }

        public void ExtractMonomod(string targetFolder)
        {
            Console.WriteLine("Extracting local MonoMod installation");
            var filePath = Path.Combine(targetFolder, "monomod.zip");
            var monomodFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MonoMod");

            using (Stream stream = File.OpenRead(filePath))
            using (IReader reader = ReaderFactory.Open(stream))
            {
                reader.WriteAllToDirectory(monomodFolder);
            }
            Console.WriteLine("Extracted local MonoMod installation");
        }

        public void CopyLocalMonomod(string targetFolder)
        {
            Console.WriteLine("Copying local MonoMod installation");
            var monomodFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MonoMod");

            FilesystemUtil.CopyDirectory(monomodFolder, targetFolder);
            Console.WriteLine("Copied local MonoMod installation");
        }

        public void CopyGameBinaries(string targetFolder)
        {
            Console.WriteLine("Copying game binaries");
            var binariesFolder = GetGameBinariesFolder(gamePath);

            FilesystemUtil.CopyDirectory(binariesFolder, targetFolder);
            Console.WriteLine("Copied game binaries");
        }

        public void CopyScaphandre(string targetFolder)
        {
            Console.WriteLine("Copying Scaphandre");
            var binariesFolder = GetGameBinariesFolder(gamePath);
            var executableFolder = AppDomain.CurrentDomain.BaseDirectory;
            var scaphandreFolder = Path.Combine(executableFolder, "Scaphandre");

            using (var sr = new StreamWriter(Path.Combine(binariesFolder, "Scaphandre.txt")))
            {
                var settings = Properties.Settings.Default;
                settings.installedManagedBinaries = new System.Collections.Specialized.StringCollection();

                foreach (var path in Directory.GetFiles(scaphandreFolder))
                {
                    var file = Path.GetFileName(path);
                    var fileInBinaries = Path.Combine(binariesFolder, file);
                    if (File.Exists(fileInBinaries))
                    {
                        Console.WriteLine("WARNING: " + fileInBinaries + " already exists");
                        continue;
                    }

                    File.Copy(path, Path.Combine(targetFolder, file));
                    File.Copy(path, Path.Combine(binariesFolder, file));

                    settings.installedManagedBinaries.Add(file);
                }
            }
            
            Console.WriteLine("Copied Scaphandre");
        }

        public void ExecutingMonomod(string targetFolder)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                WorkingDirectory = targetFolder,
                FileName = Path.Combine(targetFolder, "MonoMod.exe"),
                Arguments = "Assembly-CSharp.dll"
            };
            process.StartInfo = startInfo;
            
            Console.WriteLine("Running MonoMod");

            process.Start();
            process.WaitForExit();

            Console.WriteLine("Runned MonoMod");
        }

        public void PlacePatchedGameBinaries(string targetFolder)
        {
            Console.WriteLine("Placing game binaries");
            var binariesFolder = GetGameBinariesFolder(gamePath);

            if(File.Exists(Path.Combine(binariesFolder, "Assembly-CSharp.unpatched.dll")))
            {
                Console.WriteLine("Removing previous Assembly-CSharp.unpatched.dll");
                File.Delete(Path.Combine(binariesFolder, "Assembly-CSharp.unpatched.dll"));
                Console.WriteLine("Removed previous Assembly-CSharp.unpatched.dll");
            }

            File.Move(
                Path.Combine(binariesFolder, "Assembly-CSharp.dll"),
                Path.Combine(binariesFolder, "Assembly-CSharp.unpatched.dll")
            );

            File.Delete(Path.Combine(binariesFolder, "Assembly-CSharp.dll"));
            
            File.Move(
                Path.Combine(targetFolder, "MONOMODDED_Assembly-CSharp.dll"),
                Path.Combine(binariesFolder, "Assembly-CSharp.dll")
            );

            var modsFolder = Path.Combine(gamePath, "Mods");
            if (!Directory.Exists(modsFolder))
            {
                Console.WriteLine("Mods folder (" + modsFolder + ") not found, creating it.");
                Directory.CreateDirectory(modsFolder);
            }
            Console.WriteLine("Placed game binaries");
        }

        public void CreateModdingArchive(string targetFolder)
        {
            var archivePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ScaphandreModdingAPI.zip");
            using (var stream = File.Create(archivePath))
            using (var zipWriter = WriterFactory.Open(stream, ArchiveType.Zip, new WriterOptions(CompressionType.Deflate)))
            {
                var binariesFolder = GetGameBinariesFolder(gamePath);
                var archive = ZipArchive.Create();
                Console.WriteLine("Creating ScaphandreModAPI.zip");

                var moddingApiFiles = new string[]
                {
                    "Assembly-CSharp.dll",
                    "ScaphandreEngine.dll",
                    "UnityEngine.dll",
                    "UnityEngine.Networking.dll",
                    "UnityEngine.PerformanceTesting.dll",
                    "UnityEngine.UI.dll",
                    "UnityEngine.VR.dll"
                };

                foreach (var file in Directory.GetFiles(binariesFolder))
                {
                    var fileName = Path.GetFileName(file);
                    if (!moddingApiFiles.Contains(fileName)) continue;

                    using (var stream1 = File.OpenRead(file))
                    {
                        zipWriter.Write(fileName, stream1);
                        Console.WriteLine(" - " + fileName);
                    }
                }

                Console.WriteLine("Created ScaphandreModAPI.zip");
            }
        }

        public void Install(object sender, DoWorkEventArgs e)
        {
            Console.WriteLine("Running install task");
            var stepFactor = 100 / 8; // There is 7 steps, and this factor represents the percentage of each step
            var targetFolder = Path.Combine(Path.GetTempPath(), "Scaphandre_" + new Random().Next(0, 9999));

            Directory.CreateDirectory(targetFolder);

            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MonoMod")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MonoMod"));

                worker.ReportProgress(0 * stepFactor, "Downloading MonoMod...");
                Thread.Sleep(10);
                DownloadLatestMonomod(targetFolder);

                worker.ReportProgress(1 * stepFactor, "Extracting MonoMod...");
                Thread.Sleep(10);
                ExtractMonomod(targetFolder);
            }

            worker.ReportProgress(2 * stepFactor, "Extracting MonoMod...");
            CopyLocalMonomod(targetFolder);

            worker.ReportProgress(3 * stepFactor, "Copying Scaphandre libraries...");
            Thread.Sleep(10);
            CopyScaphandre(targetFolder);

            worker.ReportProgress(4 * stepFactor, "Copying Subnautica binaries...");
            Thread.Sleep(10);
            CopyGameBinaries(targetFolder);

            worker.ReportProgress(5 * stepFactor, "Patching Subnautica binaries...");
            Thread.Sleep(10);
            ExecutingMonomod(targetFolder);

            worker.ReportProgress(6 * stepFactor, "Applying patched binaries...");
            Thread.Sleep(10);
            PlacePatchedGameBinaries(targetFolder);

            if(createModdingArchive)
            {
                worker.ReportProgress(7 * stepFactor, "Creating modding archive...");
                Thread.Sleep(10);
                CreateModdingArchive(targetFolder);
            }

            worker.ReportProgress(8 * stepFactor, "Done. Doing a bit of cleaning...");
            Thread.Sleep(2000); // This make sure MonoMod is not using the folder
            FilesystemUtil.DeleteDirectory(targetFolder);
            Console.WriteLine("Finished install task");
        }
        #endregion

        #region Uninstall
        public void DeletePatchedAssemblies()
        {
            Console.WriteLine("Deleting patched assemblies");

            var binariesFolder = GetGameBinariesFolder(gamePath);
            var settings = Properties.Settings.Default;

            if(settings.installedManagedBinaries == null)
            {
                settings.installedManagedBinaries = new System.Collections.Specialized.StringCollection();
            }

            foreach(var file in settings.installedManagedBinaries)
            {
                Console.WriteLine(" - " + file);
                if (!File.Exists(Path.Combine(binariesFolder, file))) continue;

                File.Delete(Path.Combine(binariesFolder, file));
            }

            var assemblyCS = Path.Combine(binariesFolder, "Assembly-CSharp.dll");
            Console.WriteLine(" - " + assemblyCS);
            File.Delete(assemblyCS);

            Console.WriteLine("Deleted patched assemblies");
        }

        public void MovePreviousAssemblies()
        {
            var binariesFolder = GetGameBinariesFolder(gamePath);

            Console.WriteLine("Move Assembly-CSharp.unpatched.dll -> Assembly-CSharp.dll");
            File.Move(
                Path.Combine(binariesFolder, "Assembly-CSharp.unpatched.dll"),
                Path.Combine(binariesFolder, "Assembly-CSharp.dll")
            );
        }

        public void Uninstall(object sender, DoWorkEventArgs e)
        {
            Console.WriteLine("Running uninstall task");
            int stepFactor = 100 / 2; // There is 2 steps and full progressbar is 100%

            worker.ReportProgress(0 * stepFactor, "Deleting patched assemblies...");
            Thread.Sleep(10);
            DeletePatchedAssemblies();

            worker.ReportProgress(1 * stepFactor, "Moving back original assemblies...");
            Thread.Sleep(10);
            MovePreviousAssemblies();

            worker.ReportProgress(2 * stepFactor, "Done.");
            Thread.Sleep(10);
            Console.WriteLine("Finished uninstall task");
        }
        #endregion

        public void Reinstall(object sender, DoWorkEventArgs e)
        {
            Console.WriteLine("Running reinstall task");
            Uninstall(sender, e);
            Thread.Sleep(10);
            Install(sender, e);
            Console.WriteLine("FInished reinstall task");
        }
    }
}
