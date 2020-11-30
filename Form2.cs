using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Net;
using MediaToolkit;
using VideoLibrary;

namespace YouConverter
{

    public partial class Form2 : Form
    {
        private void ProgBar()
        {
            bunifuProgressBar1.Value += 5;
        }

        public Form2()
        {
            InitializeComponent();

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            label4.Visible = false;

        }


        private void label1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.github.com/xwbash");

        }

        private void label2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.instagram.com/yigitaydn.py");

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void bunifuDropdown1_onItemSelected(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        

        bool format_durum = true;
        // true == mp3
        // false == mp4
        private async void btnDownload_Click(object sender, EventArgs e)
        {
            label4.Visible = false;
            ProgBar();
            GetTitle();
            ProgBar();
            if (comboBox1.SelectedText == "MP4")
            {
                ProgBar();
                format_durum = false;
            }
            else
            {
                ProgBar();
                format_durum = true;
            }
            using (FolderBrowserDialog fdb = new FolderBrowserDialog() { Description = "Please Select File Path" })
            {
                ProgBar();
                if (fdb.ShowDialog()==DialogResult.OK)
                {
                    ProgBar();
                    var youtube = YouTube.Default;
                    ProgBar(); 
                    var video = await youtube.GetVideoAsync(txtUrl.Text);
                    ProgBar();
                    File.WriteAllBytes(fdb.SelectedPath + @"\" + video.FullName, await video.GetBytesAsync());
                    ProgBar();
                    var inputFile = new MediaToolkit.Model.MediaFile { Filename = fdb.SelectedPath + @"\" + video.FullName };
                    ProgBar();
                    var outputFile = new MediaToolkit.Model.MediaFile { Filename = $"{fdb.SelectedPath + @"\" + video.FullName}.mp3" };
                    ProgBar();
                    using (var enging = new Engine())
                    {
                        enging.GetMetadata(inputFile);
                        ProgBar();
                        enging.Convert(inputFile,outputFile);
                    }

                    if (format_durum == true) 
                    {
                        ProgBar();
                        File.Delete(fdb.SelectedPath + @"\" + video.FullName);
                    }
                    else
                    {
                        ProgBar();
                        File.Delete($"{fdb.SelectedPath + @"\" + video.FullName}.mp3");
                    }
                }
                else

                {
                    ProgBar();
                    MessageBox.Show("Please Select File Path", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                label4.Visible = true;
                bunifuProgressBar1.Value = 100;
            }

        }
        
        void GetTitle()
        {
            WebRequest istek = HttpWebRequest.Create(txtUrl.Text);
            WebResponse yanit;
            yanit = istek.GetResponse();
            StreamReader bilgiler = new StreamReader(yanit.GetResponseStream());
            string gelen = bilgiler.ReadToEnd();
            int baslangic = gelen.IndexOf("<title>") + 7;
            int bitis = gelen.Substring(baslangic).IndexOf("</title>");
            string gelenbilgiler = gelen.Substring(baslangic, bitis);
            label3.Text = (gelenbilgiler);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
