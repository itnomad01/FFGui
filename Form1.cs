using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFGui
{
    public partial class Form1 : Form
    {
        public Form2 f2;
        public RegistryKey regCU;
        public RegistryKey regS;
        public RegistryKey regA;
        public RegistryKey regP;
        public int mediaType;
        public string pathYtdlp;
        public string pathFfmpeg;
        public string pathSave;

        public Form1()
        {
            InitializeComponent();
            regCU = Registry.CurrentUser;
            regS = regCU.CreateSubKey("SOFTWARE");
            regA = regS.CreateSubKey("itnomad01");
            regP = regA.CreateSubKey("FFGui");
            mediaType = (int) regP.GetValue("MediaType", 1);
            switch (mediaType) {
                case 1:
                    radioButton1.Checked = true;
                    break;
                case 2:
                    radioButton2.Checked = true;
                    break;
                case 3:
                    radioButton3.Checked = true;
                    break;
            }
            pathSave = (string) regP.GetValue("PathSave", "");
            if (pathSave == "") {
                pathSave = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders", "{374DE290-123F-4565-9164-39C4925E467B}", String.Empty).ToString();
            }
            textBox2.Text = pathSave;
            pathYtdlp = (string) regP.GetValue("PathYtdlp", "");
            if (pathYtdlp == "")
            {
                pathYtdlp = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\yt-dlp.exe";
            }
            pathFfmpeg = (string) regP.GetValue("PathFfmpeg", "");
            if (pathFfmpeg == "")
            {
                pathFfmpeg = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\ffmpeg.exe";
            }
            f2 = new Form2(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            f2.ShowDialog(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) {
                textBox2.Text = folderBrowserDialog1.SelectedPath;
                pathSave = textBox2.Text;
                regP.SetValue("PathSave", pathSave);
            }
        }

        private void mediaTypeChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.Checked) {
                switch (radioButton.Name) {
                    case "radioButton1":
                        mediaType = 1;
                        break;
                    case "radioButton2":
                        mediaType = 2;
                        break;
                    case "radioButton3":
                        mediaType = 3;
                        break;

                }
                regP.SetValue("MediaType", mediaType, RegistryValueKind.DWord);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            regP.Close();
            regA.Close();
            regS.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Process.Start(pathSave);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string arguments = "--ffmpeg-location \"" + pathFfmpeg + "\" ";
            switch (mediaType)
            {
                case 2:
                    arguments = "-f bv[acodec=none] ";
                    break;
                case 3:
                    arguments += "--extract-audio --audio-format mp3 ";
                    break;
            }
            arguments += "\"" + textBox1.Text + "\"";
            ProcessStartInfo startInfo = new ProcessStartInfo(pathYtdlp);
            startInfo.WorkingDirectory = pathSave;
            startInfo.Arguments = arguments;
            Process.Start(startInfo);
            textBox1.Clear();
        }
    }
}