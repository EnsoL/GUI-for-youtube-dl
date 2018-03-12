namespace youtube_dl_gui
{
    partial class mainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainWindow));
            this.dlButton = new System.Windows.Forms.Button();
            this.inputLabel = new System.Windows.Forms.Label();
            this.inputBox = new System.Windows.Forms.TextBox();
            this.outputLabel = new System.Windows.Forms.Label();
            this.outputBox = new System.Windows.Forms.RichTextBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setToDefaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileFormatComboBox = new System.Windows.Forms.ComboBox();
            this.fileFormatLabel = new System.Windows.Forms.Label();
            this.dlFolderLabel = new System.Windows.Forms.Label();
            this.downloadFolderComboBox = new System.Windows.Forms.ComboBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openFolderButton = new System.Windows.Forms.Button();
            this.keepBoth = new System.Windows.Forms.CheckBox();
            this.writeAutoSubs = new System.Windows.Forms.CheckBox();
            this.writeSubs = new System.Windows.Forms.CheckBox();
            this.writeThumbnail = new System.Windows.Forms.CheckBox();
            this.geoBypass = new System.Windows.Forms.CheckBox();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // dlButton
            // 
            this.dlButton.Location = new System.Drawing.Point(273, 425);
            this.dlButton.Name = "dlButton";
            this.dlButton.Size = new System.Drawing.Size(97, 25);
            this.dlButton.TabIndex = 0;
            this.dlButton.Text = "Download";
            this.dlButton.UseVisualStyleBackColor = true;
            this.dlButton.Click += new System.EventHandler(this.dlButton_Click);
            // 
            // inputLabel
            // 
            this.inputLabel.AutoSize = true;
            this.inputLabel.Location = new System.Drawing.Point(11, 30);
            this.inputLabel.Name = "inputLabel";
            this.inputLabel.Size = new System.Drawing.Size(42, 13);
            this.inputLabel.TabIndex = 1;
            this.inputLabel.Text = "URL/s:";
            // 
            // inputBox
            // 
            this.inputBox.AllowDrop = true;
            this.inputBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.inputBox.Location = new System.Drawing.Point(14, 46);
            this.inputBox.Multiline = true;
            this.inputBox.Name = "inputBox";
            this.inputBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.inputBox.Size = new System.Drawing.Size(615, 127);
            this.inputBox.TabIndex = 2;
            // 
            // outputLabel
            // 
            this.outputLabel.AutoSize = true;
            this.outputLabel.Location = new System.Drawing.Point(11, 293);
            this.outputLabel.Name = "outputLabel";
            this.outputLabel.Size = new System.Drawing.Size(42, 13);
            this.outputLabel.TabIndex = 4;
            this.outputLabel.Text = "Output:";
            // 
            // outputBox
            // 
            this.outputBox.BackColor = System.Drawing.SystemColors.Window;
            this.outputBox.Location = new System.Drawing.Point(14, 309);
            this.outputBox.Name = "outputBox";
            this.outputBox.ReadOnly = true;
            this.outputBox.Size = new System.Drawing.Size(615, 110);
            this.outputBox.TabIndex = 5;
            this.outputBox.Text = "";
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.helpMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(644, 24);
            this.menuStrip.TabIndex = 6;
            this.menuStrip.Text = "Menu";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadSettingsToolStripMenuItem,
            this.saveSettingsToolStripMenuItem,
            this.setToDefaultToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "&Settings";
            // 
            // loadSettingsToolStripMenuItem
            // 
            this.loadSettingsToolStripMenuItem.Name = "loadSettingsToolStripMenuItem";
            this.loadSettingsToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.loadSettingsToolStripMenuItem.Text = "&Load settings";
            this.loadSettingsToolStripMenuItem.Click += new System.EventHandler(this.loadSettingsToolStripMenuItem_Click);
            // 
            // saveSettingsToolStripMenuItem
            // 
            this.saveSettingsToolStripMenuItem.Name = "saveSettingsToolStripMenuItem";
            this.saveSettingsToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.saveSettingsToolStripMenuItem.Text = "S&ave settings";
            this.saveSettingsToolStripMenuItem.Click += new System.EventHandler(this.saveSettingsToolStripMenuItem_Click);
            // 
            // setToDefaultToolStripMenuItem
            // 
            this.setToDefaultToolStripMenuItem.Name = "setToDefaultToolStripMenuItem";
            this.setToDefaultToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.setToDefaultToolStripMenuItem.Text = "&Reset to Default";
            this.setToDefaultToolStripMenuItem.Click += new System.EventHandler(this.setToDefaultToolStripMenuItem_Click);
            // 
            // helpMenuItem
            // 
            this.helpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutMenuItem,
            this.updateMenuItem});
            this.helpMenuItem.Name = "helpMenuItem";
            this.helpMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpMenuItem.Text = "&Help";
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Name = "aboutMenuItem";
            this.aboutMenuItem.Size = new System.Drawing.Size(112, 22);
            this.aboutMenuItem.Text = "&About";
            this.aboutMenuItem.Click += new System.EventHandler(this.aboutMenuItem_Click);
            // 
            // updateMenuItem
            // 
            this.updateMenuItem.Name = "updateMenuItem";
            this.updateMenuItem.Size = new System.Drawing.Size(112, 22);
            this.updateMenuItem.Text = "&Update";
            this.updateMenuItem.Click += new System.EventHandler(this.updateMenuItem_Click);
            // 
            // fileFormatComboBox
            // 
            this.fileFormatComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fileFormatComboBox.FormattingEnabled = true;
            this.fileFormatComboBox.Items.AddRange(new object[] {
            "Audio",
            "  mp3",
            "  m4a",
            "  flac",
            "  aac",
            "  wav",
            "  vorbis",
            "  opus",
            "Video",
            "  mp4",
            "  webm",
            "  3gp"});
            this.fileFormatComboBox.Location = new System.Drawing.Point(15, 196);
            this.fileFormatComboBox.Name = "fileFormatComboBox";
            this.fileFormatComboBox.Size = new System.Drawing.Size(122, 21);
            this.fileFormatComboBox.TabIndex = 7;
            this.fileFormatComboBox.SelectedIndexChanged += new System.EventHandler(this.fileFormatComboBox_SelectedIndexChanged);
            // 
            // fileFormatLabel
            // 
            this.fileFormatLabel.AutoSize = true;
            this.fileFormatLabel.Location = new System.Drawing.Point(12, 180);
            this.fileFormatLabel.Name = "fileFormatLabel";
            this.fileFormatLabel.Size = new System.Drawing.Size(58, 13);
            this.fileFormatLabel.TabIndex = 8;
            this.fileFormatLabel.Text = "File format:";
            // 
            // dlFolderLabel
            // 
            this.dlFolderLabel.AutoSize = true;
            this.dlFolderLabel.Location = new System.Drawing.Point(12, 224);
            this.dlFolderLabel.Name = "dlFolderLabel";
            this.dlFolderLabel.Size = new System.Drawing.Size(87, 13);
            this.dlFolderLabel.TabIndex = 9;
            this.dlFolderLabel.Text = "Download folder:";
            // 
            // downloadFolderComboBox
            // 
            this.downloadFolderComboBox.FormattingEnabled = true;
            this.downloadFolderComboBox.Location = new System.Drawing.Point(15, 240);
            this.downloadFolderComboBox.Name = "downloadFolderComboBox";
            this.downloadFolderComboBox.Size = new System.Drawing.Size(238, 21);
            this.downloadFolderComboBox.TabIndex = 10;
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // openFolderButton
            // 
            this.openFolderButton.Location = new System.Drawing.Point(259, 239);
            this.openFolderButton.Name = "openFolderButton";
            this.openFolderButton.Size = new System.Drawing.Size(18, 21);
            this.openFolderButton.TabIndex = 16;
            this.openFolderButton.Text = "*";
            this.openFolderButton.UseVisualStyleBackColor = true;
            this.openFolderButton.Click += new System.EventHandler(this.openFolderButton_Click);
            // 
            // keepBoth
            // 
            this.keepBoth.AutoSize = true;
            this.keepBoth.Checked = global::youtube_dl_gui.Properties.Settings.Default.keepBothChecked;
            this.keepBoth.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::youtube_dl_gui.Properties.Settings.Default, "keepBothChecked", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.keepBoth.Location = new System.Drawing.Point(147, 200);
            this.keepBoth.Name = "keepBoth";
            this.keepBoth.Size = new System.Drawing.Size(130, 17);
            this.keepBoth.TabIndex = 15;
            this.keepBoth.Text = "Save audio and video";
            this.keepBoth.UseVisualStyleBackColor = true;
            // 
            // writeAutoSubs
            // 
            this.writeAutoSubs.AutoSize = true;
            this.writeAutoSubs.Checked = global::youtube_dl_gui.Properties.Settings.Default.writeAutoSubsChecked;
            this.writeAutoSubs.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::youtube_dl_gui.Properties.Settings.Default, "writeAutoSubsChecked", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.writeAutoSubs.Location = new System.Drawing.Point(297, 268);
            this.writeAutoSubs.Name = "writeAutoSubs";
            this.writeAutoSubs.Size = new System.Drawing.Size(241, 17);
            this.writeAutoSubs.TabIndex = 14;
            this.writeAutoSubs.Text = "Attempt to write auto subtitless(YouTube only)";
            this.writeAutoSubs.UseVisualStyleBackColor = true;
            this.writeAutoSubs.CheckedChanged += new System.EventHandler(this.writeAutoSubs_CheckedChanged);
            // 
            // writeSubs
            // 
            this.writeSubs.AutoSize = true;
            this.writeSubs.Checked = global::youtube_dl_gui.Properties.Settings.Default.writeSubsChecked;
            this.writeSubs.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::youtube_dl_gui.Properties.Settings.Default, "writeSubsChecked", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.writeSubs.Location = new System.Drawing.Point(297, 244);
            this.writeSubs.Name = "writeSubs";
            this.writeSubs.Size = new System.Drawing.Size(140, 17);
            this.writeSubs.TabIndex = 13;
            this.writeSubs.Text = "Attempt to write subtitles";
            this.writeSubs.UseVisualStyleBackColor = true;
            this.writeSubs.CheckedChanged += new System.EventHandler(this.writeSubs_CheckedChanged);
            // 
            // writeThumbnail
            // 
            this.writeThumbnail.AutoSize = true;
            this.writeThumbnail.Checked = global::youtube_dl_gui.Properties.Settings.Default.writeThumbnailChecked;
            this.writeThumbnail.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::youtube_dl_gui.Properties.Settings.Default, "writeThumbnailChecked", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.writeThumbnail.Location = new System.Drawing.Point(297, 220);
            this.writeThumbnail.Name = "writeThumbnail";
            this.writeThumbnail.Size = new System.Drawing.Size(147, 17);
            this.writeThumbnail.TabIndex = 12;
            this.writeThumbnail.Text = "Attempt to write thumbnail";
            this.writeThumbnail.UseVisualStyleBackColor = true;
            // 
            // geoBypass
            // 
            this.geoBypass.AutoSize = true;
            this.geoBypass.Checked = global::youtube_dl_gui.Properties.Settings.Default.geoBypassChecked;
            this.geoBypass.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::youtube_dl_gui.Properties.Settings.Default, "geoBypassChecked", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.geoBypass.Location = new System.Drawing.Point(297, 196);
            this.geoBypass.Name = "geoBypass";
            this.geoBypass.Size = new System.Drawing.Size(174, 17);
            this.geoBypass.TabIndex = 11;
            this.geoBypass.Text = "Attempt to bypass geo blocking";
            this.geoBypass.UseVisualStyleBackColor = true;
            // 
            // mainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 462);
            this.Controls.Add(this.openFolderButton);
            this.Controls.Add(this.keepBoth);
            this.Controls.Add(this.writeAutoSubs);
            this.Controls.Add(this.writeSubs);
            this.Controls.Add(this.writeThumbnail);
            this.Controls.Add(this.geoBypass);
            this.Controls.Add(this.downloadFolderComboBox);
            this.Controls.Add(this.dlFolderLabel);
            this.Controls.Add(this.fileFormatLabel);
            this.Controls.Add(this.fileFormatComboBox);
            this.Controls.Add(this.outputBox);
            this.Controls.Add(this.outputLabel);
            this.Controls.Add(this.inputBox);
            this.Controls.Add(this.inputLabel);
            this.Controls.Add(this.dlButton);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "mainWindow";
            this.RightToLeftLayout = true;
            this.Text = "YouTube DL GUI";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button dlButton;
        private System.Windows.Forms.Label inputLabel;
        private System.Windows.Forms.TextBox inputBox;
        private System.Windows.Forms.Label outputLabel;
        private System.Windows.Forms.RichTextBox outputBox;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem helpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutMenuItem;
        private System.Windows.Forms.ComboBox fileFormatComboBox;
        private System.Windows.Forms.Label fileFormatLabel;
        private System.Windows.Forms.Label dlFolderLabel;
        private System.Windows.Forms.ComboBox downloadFolderComboBox;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setToDefaultToolStripMenuItem;
        private System.Windows.Forms.CheckBox geoBypass;
        private System.Windows.Forms.CheckBox writeThumbnail;
        private System.Windows.Forms.CheckBox writeSubs;
        private System.Windows.Forms.CheckBox writeAutoSubs;
        private System.Windows.Forms.CheckBox keepBoth;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button openFolderButton;
    }
}

