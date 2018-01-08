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
        static bool debug = false;
        static string START_OF_COMMAND = "/K youtube-dl ";
        static string START_OF_COMMAND_C = "/C youtube-dl ";
        string currentVersion = "";
        string currentDirectory;

        public mainWindow()
        {
            InitializeComponent();

            if (!debug)
            {
                currentDirectory = Application.ExecutablePath;
                string exeName = AppDomain.CurrentDomain.FriendlyName;
                if (currentDirectory.Contains(exeName)) currentDirectory.Remove(currentDirectory.IndexOf(exeName), exeName.Length);
                outputBox.Text += currentDirectory + "\r\n";
            }


            loadSettings();
        }

        private void dlButton_Click(object sender, EventArgs e)
        {
            List<string> lines = new List<string>(inputBox.Lines);

            string arg = START_OF_COMMAND + "--newline -o ";
            if (downloadFolderComboBox.SelectedItem != null) arg += downloadFolderComboBox.SelectedItem.ToString();
            arg += "%(title)s.%(ext)s ";
            if (fileFormatComboBox.SelectedItem != null) switch (fileFormatComboBox.SelectedItem.ToString().Trim().ToLower())
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
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardInput = true;
            startInfo.CreateNoWindow = true;

            while (lines.Count > 0)
            { 
                startInfo.Arguments = arg + "\"" + lines[0] + "\"";
                outputBox.Text += "\r\n" + arg + "\"" + lines[0] + "\"" + "\r\n";
                lines.Remove(lines[0]);
                inputBox.Lines = lines.ToArray();

                Process process = new Process();
                process.StartInfo = startInfo;

                process.OutputDataReceived += new DataReceivedEventHandler((object sendingProcess,
            DataReceivedEventArgs outLine) =>
                {
                    // Prepend line numbers to each line of the output.
                    if (!String.IsNullOrEmpty(outLine.Data))
                    {
                        outputBox.Text += outLine.Data + "\r\n";
                    }
                });


                string output;
                process.Start();
                process.BeginOutputReadLine();
                //process.StandardInput.WriteLine(arg + "\"" + lines[0] + "\"");
                /*
                while (!process.StandardOutput.EndOfStream)
                {
                    string line = process.StandardOutput.ReadLine();
                    if (line.Contains("]")) line = line.Split(']')[1];
                    output = line + "\r\n";
                    outputBox.Text += output;
                    outputBox.SelectionStart = outputBox.Text.Length;
                    outputBox.ScrollToCaret();  // https://www.youtube.com/watch?v=qgnd5JvpAFc 
                }*/
                

                /*process.StandardInput.WriteLine("ls");
                while (!process.StandardOutput.EndOfStream)
                {
                    string line = process.StandardOutput.ReadLine();
                    output = line + "\r\n";
                    outputBox.Text += output;
                    outputBox.SelectionStart = outputBox.Text.Length;
                    outputBox.ScrollToCaret();
                }



                // Set our event handler to asynchronously read the sort output.
                process.OutputDataReceived += new DataReceivedEventHandler(SortOutputHandler);

                // Redirect standard input as well.  This stream
                // is used synchronously.
                process.StartInfo.RedirectStandardInput = true;
                process.Start();

                // Use a stream writer to synchronously write the sort input.
                StreamWriter sortStreamWriter = process.StandardInput;

                // Start the asynchronous read of the sort output stream.
                process.BeginOutputReadLine();
                Console.WriteLine("Ready to sort up to 50 lines of text");
                String inputText;
                int numInputLines = 0;
                do
                {
                    Console.WriteLine("Enter a text line (or press the Enter key to stop):");

                    inputText = Console.ReadLine();
                    if (!String.IsNullOrEmpty(inputText))
                    {
                        numInputLines++;
                        sortStreamWriter.WriteLine(inputText);
                    }
                } while (true);*/



                process.Close();
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
            startInfo.Arguments = START_OF_COMMAND_C + "--update";
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
            startInfo.Arguments = START_OF_COMMAND_C + "--version";
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

        private void mainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfigurationSettings.AppSettings["file format"] = fileFormatComboBox.SelectedItem.ToString();
            ConfigurationSettings.AppSettings["downlad folder"] = downloadFolderComboBox.SelectedItem.ToString();
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
            if (!File.Exists("settings.ini")) createSettings();
            else
            {
                StreamReader reader = new StreamReader(File.OpenRead("settings.ini"));
                string[] line;

                while (reader.EndOfStream)
                {
                    line = reader.ReadLine().Split(':');
                    if (line.Length == 2)
                    {
                        if (line[0].ToLower().Contains("file format")) fileFormatComboBox.SelectedIndex = Convert.ToInt16(line[1]);
                        if (line[0].ToLower().Contains("download folder")) downloadFolderComboBox.Items.Add(line[1]);
                    }
                }

                reader.Close();
            }
        }

        private void setSettings()
        {
            if (!File.Exists("settings.ini")) createSettings();
            else
            {
                StreamWriter writer = new StreamWriter(File.OpenRead("settings.ini"));
                writer.WriteAsync("file format : " + fileFormatComboBox.SelectedIndex);
                writer.WriteAsync("download folder : " + downloadFolderComboBox.SelectedText);
                writer.Close();
            }
        }

        private void createSettings()
        {
            using (FileStream fs = File.Create("settings.ini"))
            {
                string downloadPath = "C:\\Users\\Enso\\Desktop\\";
                Byte[] info = new System.Text.UTF8Encoding(true).GetBytes("File Format : 8\r\nDownload Folder : " + downloadPath);
                fs.Write(info, 0, info.Length);
            }
        }
    }
}
