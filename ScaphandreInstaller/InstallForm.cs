using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScaphandreInstaller
{
    public partial class InstallForm : Form
    {
        public InstallForm()
        {
            InitializeComponent();
        }

        string[] possibleInstallPaths = new[]
        {
            Properties.Settings.Default.subnauticaPath,
            "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Subnautica",
            "C:\\Program Files\\Epic Games\\Subnautica"
        };

        private void InstallForm_Load(object sender, EventArgs e)
        {
            versionLabel.Text = Application.ProductVersion;
            foreach(var possibleInstallPath in possibleInstallPaths)
            {
                if (Installer.IsValidPath(possibleInstallPath))
                {
                    installTextBox.Text = possibleInstallPath;
                    break;
                }
            }
            UpdateGuiButtons();
        }

        private void selectFolderButton_Click(object sender, EventArgs e)
        {
            folderBrowser.SelectedPath = installTextBox.Text;
            folderBrowser.ShowDialog();

            installTextBox.Text = folderBrowser.SelectedPath;
        }

        private void installTextBox_TextChanged(object sender, EventArgs e)
        {
            var settings = Properties.Settings.Default;
            settings.subnauticaPath = installTextBox.Text;
            settings.Save();

            UpdateGuiButtons();
        }

        public void UpdateGuiButtons()
        {
            if(Installer.IsScaphandreInstalled(installTextBox.Text))
            {
                installButton.Text = "Reinstall";
            } else
            {
                installButton.Text = "Install";
            }

            installButton.Enabled = Installer.IsValidPath(installTextBox.Text);
            uninstallButton.Enabled = Installer.IsScaphandreInstalled(installTextBox.Text);
        }

        private void installButton_Click(object sender, EventArgs e)
        {
            if(Installer.IsScaphandreInstalled(installTextBox.Text))
            {
                new TaskForm(TaskType.Reinstall).DoWork(this, installTextBox.Text, createModdingArchive.Checked);
            }
            else
            {
                new TaskForm(TaskType.Install).DoWork(this, installTextBox.Text, createModdingArchive.Checked);
            }
        }

        private void uninstallButton_Click(object sender, EventArgs e)
        {
            new TaskForm(TaskType.Uninstall).DoWork(this, installTextBox.Text, createModdingArchive.Checked);
        }

        private void createModdingArchive_CheckedChanged(object sender, EventArgs e)
        {
            if(createModdingArchive.Checked)
            {
                MessageBox.Show("Creation of Scaphandre Modding API archive enabled.\n" +
                    "Now, you need to press the '" + installButton.Text + "' button in order to create the archive.");
            }
        }
    }
}
