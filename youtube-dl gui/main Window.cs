using System;
using System.Configuration;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace youtube_dl_gui
{
    public partial class mainWindow : Form
    {
        static string K_ARG = "/K youtube-dl ";
        static string C_ARG = "/C youtube-dl ";
        static string START_OF_COMMAND = "youtube-dl ";
        string currentVersion = "";

        public mainWindow()
        {
            InitializeComponent();
            loadSettings();
            fileFormatComboBox.SelectedIndex = 0;
            downloadFolderComboBox.SelectedIndex = 0;
        }

        private void dlButton_Click(object sender, EventArgs e)
        {
            dlButton.Enabled = false;
            inputBox.ReadOnly = true;

            List<string> urls = new List<string>(inputBox.Lines);

            string command = C_ARG + "--newline ";
            if (downloadFolderComboBox.SelectedItem != null) command += "-o " + downloadFolderComboBox.SelectedItem.ToString();
            command += "%(title)s.%(ext)s ";
            if (fileFormatComboBox.SelectedItem != null) switch (fileFormatComboBox.SelectedItem.ToString().Trim().ToLower())
                {
                    case "audio": command += "-x "; break;
                    case "mp3": command += "-x --audio-format mp3 "; break;
                    case "m4a": command += "-x --audio-format m4a "; break;
                    case "flac": command += "-x --audio-format flac "; break;
                    case "aac": command += "-x --audio-format aac "; break;
                    case "wav": command += "-x --audio-format wav "; break;
                    case "vorbis": command += "-x --audio-format vorbis "; break;
                    case "opus": command += "-x --audio-format opus "; break;
                    case "video": break;
                    case "mp4": command += "-f mp4 "; break;
                    case "webm": command += "-f webm "; break;
                    case "3gp": command += "-f webm "; break;
                }

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;

            while (urls.Count > 0)
            {
                string arg = urls[0];
                urls.Remove(urls[0]);
                inputBox.Lines = urls.ToArray();
                Uri url = new Uri(arg);

                if (url.Scheme == Uri.UriSchemeHttp || url.Scheme == Uri.UriSchemeHttps) arg = command + "\"" + url + "\"";
                else continue;

                startInfo.Arguments = arg;
                Process process = new Process();
                process.StartInfo = startInfo;

                downloadWorker.RunWorkerAsync(process);

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

        // This ain't working
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
                output = line;
            }
            
            process.Close();

            if(output.Contains("It looks like you installed"))
            {
                startInfo = new ProcessStartInfo();
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "pip install --upgrade youtube-dl";
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

            outputBox.Text += output + "\r\n";

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
            createSettings();
        }

        private void loadSettings()
        {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "settings.ini")) createSettings();
            else
            {
                StreamReader reader = new StreamReader(File.OpenRead("settings.ini"));
                string[] line;

                while (reader.EndOfStream)
                {
                    line = reader.ReadLine().Split(':');
                    if (line.Length == 2)
                    {
                        if (line[0].ToLower().Contains("file format"))
                        {
                            int format = fileFormatToNumber(line[1]);
                            if (format >= 0 && format <= 11) fileFormatComboBox.SelectedIndex = format;
                        }
                        if (line[0].ToLower().Contains("download folder"))
                        {
                            downloadFolderComboBox.Items.Add(line[1]);
                            downloadFolderComboBox.SelectedItem = line[1];
                        }
                    }
                }
                
                reader.Close();
            }
        }

        private void setSettings()
        {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "settings.ini")) createSettings();
            else
            {
                StreamWriter writer = new StreamWriter(File.OpenRead(AppDomain.CurrentDomain.BaseDirectory + "settings.ini"));
                writer.WriteAsync("file format : " + fileFormatComboBox.SelectedIndex);
                writer.WriteAsync("download folder : " + downloadFolderComboBox.SelectedText);
                writer.Close();
            }
        }

        private void createSettings()
        {
            using (FileStream fs = File.Create(AppDomain.CurrentDomain.BaseDirectory + "settings.ini"))
            {
                string downloadPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                Byte[] info = new System.Text.UTF8Encoding(true).GetBytes("File Format : video\r\nDownload Folder : " + downloadPath);
                fs.Write(info, 0, info.Length);
            }
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

        private void downloadWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Process process = (Process) e.Argument;
            process.Start();
            StreamReader reader = process.StandardOutput;

            while (reader.EndOfStream)
            {
                string outputLine = reader.ReadLine();
                if (outputLine.Contains("]")) outputLine = outputLine.Split(']')[1];
                outputLine = outputLine + "\r\n";
                if (!outputLine.Contains("Microsoft") && !outputLine.Trim().Equals("")) outputBox.Text += outputLine;
                outputBox.SelectionStart = outputBox.Text.Length;
                outputBox.ScrollToCaret();  // https://www.youtube.com/watch?v=qgnd5JvpAFc https://www.youtube.com/watch?v=fc1tg9qkGyI
            }
        }
    }
}