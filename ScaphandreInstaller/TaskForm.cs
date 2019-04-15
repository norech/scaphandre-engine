using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScaphandreInstaller
{
    public partial class TaskForm : Form
    {
        public TaskType type;
        private string successMessage;
        public TaskForm(TaskType type)
        {
            InitializeComponent();
            this.type = type;
        }

        public void DoWork(InstallForm owner, string path, bool createModdingArchive)
        {
            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;

            var patcher = new Installer(backgroundWorker, path, createModdingArchive);

            switch(type)
            {
                case TaskType.Install:
                    Text = "Installation";
                    successMessage = "Scaphandre was successfully installed.";
                    backgroundWorker.DoWork += patcher.Install;
                    break;
                case TaskType.Uninstall:
                    Text = "Uninstallation";
                    successMessage = "Scaphandre was successfully removed.";
                    backgroundWorker.DoWork += patcher.Uninstall;
                    break;
                case TaskType.Reinstall:
                    Text = "Reinstallation";
                    successMessage = "Scaphandre was successfully reinstalled.";
                    backgroundWorker.DoWork += patcher.Reinstall;
                    break;
            }

            backgroundWorker.ProgressChanged += OnProgress;
            backgroundWorker.RunWorkerCompleted += (sender1, o) =>
            {
                MessageBox.Show(this, o.Error != null ? o.Error.Message : successMessage);
                owner.UpdateGuiButtons();
                Close();
            };

            backgroundWorker.RunWorkerAsync();
            ShowDialog(owner);
        }

        public void OnProgress(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            userState.Text = e.UserState.ToString();
        }
    }
}
