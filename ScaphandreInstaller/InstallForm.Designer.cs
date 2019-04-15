namespace ScaphandreInstaller
{
    partial class InstallForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.selectFolderButton = new System.Windows.Forms.Button();
            this.installTextBox = new System.Windows.Forms.TextBox();
            this.installButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.uninstallButton = new System.Windows.Forms.Button();
            this.createModdingArchive = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // selectFolderButton
            // 
            this.selectFolderButton.Location = new System.Drawing.Point(272, 116);
            this.selectFolderButton.Name = "selectFolderButton";
            this.selectFolderButton.Size = new System.Drawing.Size(122, 29);
            this.selectFolderButton.TabIndex = 0;
            this.selectFolderButton.Text = "Browse folder";
            this.selectFolderButton.UseVisualStyleBackColor = true;
            this.selectFolderButton.Click += new System.EventHandler(this.selectFolderButton_Click);
            // 
            // installTextBox
            // 
            this.installTextBox.Location = new System.Drawing.Point(12, 88);
            this.installTextBox.Name = "installTextBox";
            this.installTextBox.Size = new System.Drawing.Size(382, 22);
            this.installTextBox.TabIndex = 1;
            this.installTextBox.TextChanged += new System.EventHandler(this.installTextBox_TextChanged);
            // 
            // installButton
            // 
            this.installButton.Location = new System.Drawing.Point(12, 116);
            this.installButton.Name = "installButton";
            this.installButton.Size = new System.Drawing.Size(110, 64);
            this.installButton.TabIndex = 2;
            this.installButton.Text = "Install";
            this.installButton.UseVisualStyleBackColor = true;
            this.installButton.Click += new System.EventHandler(this.installButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(225, 29);
            this.label1.TabIndex = 4;
            this.label1.Text = "Scaphandre Engine";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(218, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "v0.0.1.0 for Subnautica dec.2018";
            // 
            // uninstallButton
            // 
            this.uninstallButton.Location = new System.Drawing.Point(284, 151);
            this.uninstallButton.Name = "uninstallButton";
            this.uninstallButton.Size = new System.Drawing.Size(110, 29);
            this.uninstallButton.TabIndex = 3;
            this.uninstallButton.Text = "Uninstall";
            this.uninstallButton.UseVisualStyleBackColor = true;
            this.uninstallButton.Click += new System.EventHandler(this.uninstallButton_Click);
            // 
            // createModdingArchive
            // 
            this.createModdingArchive.AutoSize = true;
            this.createModdingArchive.Location = new System.Drawing.Point(15, 186);
            this.createModdingArchive.Name = "createModdingArchive";
            this.createModdingArchive.Size = new System.Drawing.Size(286, 21);
            this.createModdingArchive.TabIndex = 6;
            this.createModdingArchive.Text = "Create Scaphandre Modding API archive";
            this.createModdingArchive.UseVisualStyleBackColor = true;
            this.createModdingArchive.CheckedChanged += new System.EventHandler(this.createModdingArchive_CheckedChanged);
            // 
            // InstallForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(406, 215);
            this.Controls.Add(this.createModdingArchive);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.uninstallButton);
            this.Controls.Add(this.installButton);
            this.Controls.Add(this.installTextBox);
            this.Controls.Add(this.selectFolderButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "InstallForm";
            this.Text = "Scaphandre Engine Installer";
            this.Load += new System.EventHandler(this.InstallForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button selectFolderButton;
        private System.Windows.Forms.TextBox installTextBox;
        private System.Windows.Forms.Button installButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
        private System.Windows.Forms.Button uninstallButton;
        private System.Windows.Forms.CheckBox createModdingArchive;
    }
}

