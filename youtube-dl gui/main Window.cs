using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using youtube_dl_gui;

namespace youtube_dl_gui
{
    public partial class mainWindow : Form
    {
        static bool debug = true;
        static String START_OF_COMMAND = "/C youtube-dl ";
        String currentVersion = "";

        public mainWindow()
        {
            InitializeComponent();
            fileTypeComboBox.SelectedIndex = 0;
            fileTypeComboBox.SelectedItem = System.Configuration.ConfigurationSettings.AppSettings["file format"];
            downloadFolderComboBox.SelectedItem = System.Configuration.ConfigurationSettings.AppSettings["downlad folder"];
        }

        private void dlButton_Click(object sender, EventArgs e)
        {
            List<string> lines = new List<string>(inputBox.Lines);

            string arg = START_OF_COMMAND + "--newline -o ";
            if (downloadFolderComboBox.SelectedItem != null) arg += downloadFolderComboBox.SelectedItem.ToString();
            arg += "%(title)s.%(ext)s ";
            if (fileTypeComboBox.SelectedItem != null) switch (fileTypeComboBox.SelectedItem.ToString().Trim().ToLower())
            {
                case "audio": arg += "-x "; break;
                case "mp3": arg += "-x --audio-format mp3 "; break;
                case "m4a": arg += "-x --audio-format m4a "; break;
                case "flac": arg += "-x --audio-format flac "; break;
                case "aac": arg += "-x --audio-format aac "; break;
                case "wav": arg += "-x --audio-format wav "; break;
                case "vorbis": arg += "-x --audio-format vorbis "; break;
                case "opus": arg += "-x --audio-format opus "; break;
                case "video": break;
                case "mp4": arg += "-f mp4 "; break;
                case "webm": arg += "-f webm "; break;
                case "3gp": arg += "-f webm "; break;
            }

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.UseShellExecute = false;
            if(!debug) startInfo.RedirectStandardOutput = true;
            if(!debug) startInfo.CreateNoWindow = true;

            while (lines.Count > 0)
            {
                startInfo.Arguments = arg + "\"" + lines[0] + "\"";
                outputBox.Text += "\r\n" + arg + "\"" + lines[0] + "\"" + "\r\n";
                lines.Remove(lines[0]);
                inputBox.Lines = lines.ToArray();

                Process process = new Process();
                process.StartInfo = startInfo;
                //if(!debug)process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

                String output;
                process.Start();
                if(!debug) while (!process.StandardOutput.EndOfStream)
                {
                    string line = process.StandardOutput.ReadLine();
                    if (line.Contains("]")) line = line.Split(']')[1];
                    output = line + "\r\n";
                    outputBox.Text += output;
                    outputBox.SelectionStart = outputBox.Text.Length;
                    outputBox.ScrollToCaret();
                }
                if (debug) System.Threading.Thread.Sleep(10000); 
                if (!debug) process.Close();
            }
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
            startInfo.Arguments = START_OF_COMMAND + "--update";
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;

            Process process = new Process();
            process.StartInfo = startInfo;
            process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            String output = "";
            process.Start();
            while (!process.StandardOutput.EndOfStream)
            {
                string line = process.StandardOutput.ReadLine();
                output += line;
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
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

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
            startInfo.Arguments = START_OF_COMMAND + "--version";
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;

            Process process = new Process();
            process.StartInfo = startInfo;
            process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            process.Start();
            while (!process.StandardOutput.EndOfStream)
            {
                string line = process.StandardOutput.ReadLine();
                currentVersion += line;
            }
            currentVersion += ".";
            process.Close();
        }

        private void mainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Configuration.ConfigurationSettings.AppSettings["file format"] = fileTypeComboBox.SelectedItem.ToString();
            System.Configuration.ConfigurationSettings.AppSettings["downlad folder"] = downloadFolderComboBox.SelectedItem.ToString();
        }
    }
}
