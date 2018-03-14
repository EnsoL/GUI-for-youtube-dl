using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading.Tasks;

/* Links for testing.
 * https://www.youtube.com/watch?v=16W7c0mb-rE
 * https://www.youtube.com/watch?v=pYXVdgvjQAE
 */

namespace youtube_dl_gui
{
    public partial class mainWindow : Form
    {
        const string K_ARG = "/K youtube-dl ";
        const string C_ARG = "/C youtube-dl ";
        const string START_OF_COMMAND = "youtube-dl ";
        string currentVersion = "";

        public mainWindow()
        {
            InitializeComponent();

            Task updateVersionNumberAsyncTask = Task.Factory.StartNew(() => updateVersionNumber());

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string downloadFolderSetting = Properties.Settings.Default.downloadFolder;

            downloadFolderComboBox.Items.Add(desktopPath + "\\");
            downloadFolderComboBox.Items.Add(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\");
            downloadFolderComboBox.Items.Add(desktopPath.ToString().Replace("Desktop", "Downloads") + "\\");
            downloadFolderComboBox.Items.Add(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) + "\\");
            downloadFolderComboBox.Items.Add(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos) + "\\");

            if (downloadFolderComboBox.Items.Contains(downloadFolderSetting))
            {
                downloadFolderComboBox.SelectedItem = downloadFolderSetting;
            }
            else
            {
                downloadFolderComboBox.Items.Add(downloadFolderSetting);
                downloadFolderComboBox.SelectedItem = downloadFolderSetting;
            }
                        
            loadSettings();
        }

        private void dlButton_Click(object sender, EventArgs e)
        {
            if (inputBox.Text.Trim().Equals("")) return;
            dlButton.Enabled = false;
            inputBox.ReadOnly = true;

            List<string> urls = new List<string>(inputBox.Lines);

            CLIString template = new CLIString();
            template.createPlaceholderStartOfCommand();
        
            template.addFileFormat(fileFormatComboBox.SelectedItem.ToString());
            if (keepBoth.Checked && keepBoth.Enabled) template.addKeepBoth();
            template.addDownloadLocation(downloadFolderComboBox.Text);

            if (geoBypass.Checked) template.addGeoBypass();
            if (writeThumbnail.Checked) template.addThumbnail();
            if (writeSubs.Checked) template.addWriteSubs();
            if (writeAutoSubs.Checked) template.addWriteAutoSubs();

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;

            while (urls.Count > 0)
            {
                CLIString command = new CLIString(template);
                string url = urls[0].Trim();

                if (!command.addUrl(url))
                {
                    outputBox.AppendText("Failed to add URL to youtube-dl command." + Environment.NewLine + 
                                        "The URL is: " + url + Environment.NewLine);
                    break;
                }
                if (!command.isAValidCommand())
                {
                    outputBox.AppendText("The command for youtube-dl is incomplete: " + command.ToString() + Environment.NewLine);
                    break;
                }
                /* for debugging
                outputBox.Text += "\r\n---------------------------------------------------------------------------------------------------------------------\r\n" + 
                    command.ToString() + "\r\n---------------------------------------------------------------------------------------------------------------------\r\n";
                    */
                startInfo.Arguments = "/K" + command.ToString();
                Process process = new Process();
                process.StartInfo = startInfo;

                process.Start();

                string outputLine = "";

                while (!process.StandardOutput.EndOfStream && !process.HasExited)
                {
                    outputLine = process.StandardOutput.ReadLine();

                    outputLine = outputLine + Environment.NewLine;

                    if(!outputLine.Contains(">")) outputBox.Text += outputLine;

                    outputBox.SelectionStart = outputBox.Text.Length;
                    outputBox.ScrollToCaret();  // https://www.youtube.com/watch?v=fc1tg9qkGyI
                }

                if (outputLine.Contains("downloading webpage") || outputLine.Contains("[download]"))
                {
                    outputBox.Text += "An error might have occured, are you connected to the internet?" + Environment.NewLine;
                }
                else
                {
                    outputBox.Text += url + " - Done" + Environment.NewLine;
                    outputBox.SelectionStart = outputBox.Text.Length;
                    outputBox.ScrollToCaret();
                }

                urls.Remove(urls[0]);
                inputBox.Lines = urls.ToArray();
                process.Close();
            }

            inputBox.ReadOnly = false;
            dlButton.Enabled = true;
        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            if(currentVersion.Equals("")) updateVersionNumber();
               
            About about = new About(currentVersion);
            about.Show();
        }

        private void updateMenuItem_Click(object sender, EventArgs e)
        {  
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = C_ARG + "--update";
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;

            Process process = new Process();
            process.StartInfo = startInfo;
            
            string output = "";
            process.Start();
            
            while (!process.StandardOutput.EndOfStream)
            {
                string line = process.StandardOutput.ReadLine();
                output += line + Environment.NewLine;
            }
            
            process.Close();
            
            if(output.Contains("you installed with a"))
            {
                startInfo = new ProcessStartInfo();
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/K pip install --upgrade youtube-dl";
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                startInfo.CreateNoWindow = true;

                process = new Process();
                process.StartInfo = startInfo;

                process.Start();
                while (!process.StandardOutput.EndOfStream)
                {
                    string line = process.StandardOutput.ReadLine();
                    output += line;
                }
                process.Close();

                if (output.Contains("is not recognized")) output = "Please manully update youtube-dl";
            }

            // Use only for debugging.
            // outputBox.Text += output + Environment.NewLine;

            updateVersionNumber();
        }

        private void updateVersionNumber()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = C_ARG + "--version";
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;

            Process process = new Process();
            process.StartInfo = startInfo;

            process.Start();
            while (!process.StandardOutput.EndOfStream)
            {
                string line = process.StandardOutput.ReadLine();
                currentVersion = line + ".";
            }
            process.Close();
        }

        private void loadSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadSettings();
        }

        private void saveSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setSettings();
        }

        private void setToDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resetSettings();
            loadSettings();
        }

        private void loadSettings()
        {
            Properties.Settings.Default.Reload();
        }

        private void setSettings()
        {
            Properties.Settings.Default.Save();
        }

        private void resetSettings()
        {
            Properties.Settings.Default.Reset();
            setSettings();
        }

        private int fileFormatToNumber(string format)
        {
            switch (format.Trim().ToLower())
            {
                case "audio": return 0;
                case "mp3": return 1;
                case "m4a": return 2;
                case "flac": return 3;
                case "aac": return 4;
                case "wav": return 5;
                case "vorbis": return 6;
                case "opus": return 7;
                case "video": return 8;
                case "mp4": return 9;
                case "webm": return 10;
                case "3gp": return 11;
            }

            return -1;
        }

        private void fileFormatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fileFormatComboBox.SelectedIndex >= fileFormatToNumber("Video")) keepBoth.Enabled = false;
            else keepBoth.Enabled = true;
        }

        private void writeAutoSubs_CheckedChanged(object sender, EventArgs e)
        {
            if (writeSubs.Checked && writeAutoSubs.Checked) writeSubs.Checked = false;
        }

        private void writeSubs_CheckedChanged(object sender, EventArgs e)
        {
            if (writeSubs.Checked && writeAutoSubs.Checked) writeAutoSubs.Checked = false;
        }

        private void openFolderButton_Click(object sender, EventArgs e)
        {
            string selectedPath;

            folderBrowserDialog.ShowDialog();
            selectedPath = folderBrowserDialog.SelectedPath;

            if(selectedPath == null || selectedPath.Length < 3) return;
            if(selectedPath[selectedPath.Length - 1] != '\\') selectedPath += "\\";

            downloadFolderComboBox.Items.Add(selectedPath);
            downloadFolderComboBox.SelectedItem = selectedPath;
        }
    }
}