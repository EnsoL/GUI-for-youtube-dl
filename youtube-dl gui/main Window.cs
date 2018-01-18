using System;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;

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
            System.Threading.Tasks.Task updateVersionNumberAsyncTask = System.Threading.Tasks.Task.Factory.StartNew(() => updateVersionNumber());
            loadSettings();
            if (fileFormatToNumber(fileFormatComboBox.SelectedText) >= fileFormatToNumber("Video")) keepBoth.Enabled = false;
        }

        private void dlButton_Click(object sender, EventArgs e)
        {
            dlButton.Enabled = false;
            inputBox.ReadOnly = true;

            List<string> urls = new List<string>(inputBox.Lines);

            //string command = K_ARG + "--newline ";
            StringBuilder command = new StringBuilder(C_ARG + "--newline ");

            if (fileFormatComboBox.SelectedItem != null) switch (fileFormatComboBox.SelectedItem.ToString().Trim().ToLower())
            {
                case "audio": command.Append("-x URL "); break;
                case "mp3": command.Append("-x --audio-format mp3 URL "); break;
                        /*
                case "m4a": command += "-x --audio-format m4a "; break;
                case "flac": command += "-x --audio-format flac "; break;
                case "aac": command += "-x --audio-format aac "; break;
                case "wav": command += "-x --audio-format wav "; break;
                case "vorbis": command += "-x --audio-format vorbis "; break;
                case "opus": command += "-x --audio-format opus "; break;*/
                case "video": command.Append("URL "); break;
                        /*
                case "mp4": command += "-f mp4 "; break;
                case "webm": command += "-f webm "; break;
                case "3gp": command += "-f webm "; break;
                */
            }
            if (keepBoth.Checked) command.Append("-k ");
            if (downloadFolderComboBox.SelectedItem != null) command.Append("-o " + downloadFolderComboBox.SelectedItem.ToString());
            command.Append("%(title)s.%(ext)s ");

            if (geoBypass.Checked) command.Append("--geo-bypass ");
            //if (writeThumbnail.Checked) command += "--write-thumbnail ";
            //if (writeAutoSubs.Checked && writeSubs.Checked) writeSubs.Checked = false;
            //if (writeSubs.Checked) command += "--write-sub ";
            //if (writeAutoSubs.Checked) command += "--write-auto-sub ";

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;

            while (urls.Count > 0)
            {
                string arg = urls[0].Trim();
                Uri url;
                inputBox.Lines = urls.ToArray();

                if (Uri.IsWellFormedUriString(arg, UriKind.Absolute)) url = new Uri(arg);
                else continue;

                StringBuilder argument = new StringBuilder(command.ToString());
                if (url.Scheme == Uri.UriSchemeHttp || url.Scheme == Uri.UriSchemeHttps) argument.Replace("URL", url.ToString());
                else continue;

                outputBox.Text += "\r\n---------------------------------------------------------------------------------------------------------------------\r\n" + 
                    argument.ToString() + "\r\n---------------------------------------------------------------------------------------------------------------------\r\n";

                startInfo.Arguments = argument.ToString();
                Process process = new Process();
                process.StartInfo = startInfo;

                process.Start();
                
                while (!process.StandardOutput.EndOfStream && !process.HasExited)
                {
                    string outputLine = process.StandardOutput.ReadLine();
                    //if (outputLine.Contains("]")) outputLine = outputLine.Split(']')[1];
                    outputLine = outputLine + Environment.NewLine;

                    //if (!outputLine.Contains("Microsoft") && !outputLine.Trim().Equals("") &&
                    //  !(outputLine.Contains(">") || outputLine.Contains(":\\"))) outputBox.Text += outputLine;

                    // Scrolls the textBox to the bottom.
                    outputBox.SelectionStart = outputBox.Text.Length;
                    outputBox.ScrollToCaret();  // https://www.youtube.com/watch?v=qgnd5JvpAFc https://www.youtube.com/watch?v=fc1tg9qkGyI
                }

                process.Close();

                // This is last, so that only after something is downloaded, it's removed from the list.
                urls.Remove(urls[0]);
                inputBox.Lines = urls.ToArray();
            }

            inputBox.ReadOnly = false;
            dlButton.Enabled = true;
        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            if(currentVersion.Equals(""))
            {
                updateVersionNumber();
                outputBox.AppendText("hsdakjhdsfkjhdfs");
            }
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

            outputBox.Text += output + Environment.NewLine;

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
            //loadSettings(); // Decomment when it works.
        }

        private void loadSettings()
        {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "settings.ini")) createSettings();
            else
            {
                StreamReader reader = new StreamReader(File.OpenRead(AppDomain.CurrentDomain.BaseDirectory + "settings.ini"));
                string[] line = new string[1];

                while ((line[0] = reader.ReadLine()) != null)
                {
                    line = line[0].Split('=');

                    if (line.Length == 2)
                    {
                        if (line[0].ToLower().Contains("file format"))
                        {
                            int format;
                            line[1] = line[1].Trim().ToLower();

                            if (Regex.IsMatch(line[1], @"^[0-9]$")) format = int.Parse(line[1]);
                            else format = fileFormatToNumber(line[1]);

                            if (format >= 0 && format <= 11) fileFormatComboBox.SelectedIndex = format;
                            else outputBox.AppendText("Didn't load file type from settings." + Environment.NewLine);
                        }
                        if (line[0].ToLower().Contains("download folder"))
                        {
                            downloadFolderComboBox.Items.Add(line[1]);
                            downloadFolderComboBox.Text = line[1];
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
                FileStream stream = File.OpenWrite(AppDomain.CurrentDomain.BaseDirectory + "settings.ini");
                Byte[] info = new System.Text.UTF8Encoding(true).GetBytes("File Format = " + fileFormatComboBox.SelectedIndex + Environment.NewLine
                                                            + "Download Folder = " + downloadFolderComboBox.SelectedText.Trim() + Environment.NewLine);
                stream.WriteAsync(info, 0, info.Length);
                stream.Close();
            }
        }

        private void createSettings()
        {
            using (FileStream fs = File.Create(AppDomain.CurrentDomain.BaseDirectory + "settings.ini"))
            {
                string downloadPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                Byte[] info = new System.Text.UTF8Encoding(true).GetBytes("File Format = " + fileFormatToNumber("Video") + Environment.NewLine + "Download Folder = " + downloadPath);
                fs.Write(info, 0, info.Length);
                fs.Close();
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

        private void fileFormatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fileFormatToNumber(fileFormatComboBox.SelectedText) >= fileFormatToNumber("Video")) keepBoth.Enabled = false;
            else keepBoth.Enabled = true;
        }
    }
}