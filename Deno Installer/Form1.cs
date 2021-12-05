using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ByteSizeLib;
using System.IO.Compression;



namespace Deno_Installer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Point lastPoint;
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.BorderSize = 0;
           // Environment.SetEnvironmentVariable("Path", EnvironmentVariableTarget.User);
        

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string specificFolderamafolderolan = Path.Combine(folder, "denoland");



            if (!Directory.Exists(specificFolderamafolderolan))
            {

                string specificFolder = Path.Combine(folder, "deno-x86_64-pc-windows-msvc.zip");
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                webClient.DownloadFileAsync(new Uri("https://github.com/denoland/deno/releases/download/v1.16.4/deno-x86_64-pc-windows-msvc.zip"), specificFolder);
            } else
            {
                MessageBox.Show("You Already Have A Folder Called denoland In Your AppData Folder!");
            }



        }


        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string fileName = Path.Combine(folder, "deno-x86_64-pc-windows-msvc.zip");
            FileInfo fi = new FileInfo(fileName);
            this.gunaLabel1.Visible = true;
            this.gunaLabel1.Text = "Downloading(" + ByteSize.FromBytes(fi.Length).ToString() + "/" + "25,60 MB)";
            gunaProgressBar1.Visible = true;
            gunaProgressBar1.Value = e.ProgressPercentage;
        }


        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string targetFolder = @Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string sourceZipFile = Path.Combine(folder, "deno-x86_64-pc-windows-msvc.zip");
            ZipFile.ExtractToDirectory(sourceZipFile, targetFolder);
            string denofull = Path.Combine(folder, "deno.exe");
            Console.WriteLine(Path.Combine(folder, "deno"));
            Directory.CreateDirectory(Path.Combine(folder, "denoland"));
            string move = Path.Combine(folder, "denoland");
            File.Move(denofull, Path.Combine(move, "deno.exe"));
            var value = System.Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.User);
            String respls = value + move;
            Environment.SetEnvironmentVariable("Path", respls, EnvironmentVariableTarget.User);
            MessageBox.Show("Installed");
        }
    }
}
