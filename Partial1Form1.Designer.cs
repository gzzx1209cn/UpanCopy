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
    public partial class Form1 : Form   //里层函数
    {
        public void Copy()
        {
            CopyFile(drivestr);
        }

        public void CopyFile(string path) //拷贝函数
        {
            DirectoryInfo dir = new DirectoryInfo(path);

            foreach (DirectoryInfo DI in dir.GetDirectories())  //遍历目录
            {
                try
                {
                    CopyFile(DI.FullName);
                }
                catch (UnauthorizedAccessException)
                {
                    continue;
                }
            }

            foreach (FileInfo FI in dir.GetFiles())  //遍历文件
            {
                //if (FI.Length <= filesize * 1024 * 1024)
                if (FI.Extension == ".ppt" || FI.Extension == ".pptx")
                {
                    try
                    {
                        if (File.Exists(des + FI.Name) == true)  //文件存在时根据hash判断是否相同
                        {
                            int i = 1;
                            bool a = CalculateHash(FI.FullName, des + FI.Name);
                            if (a != true)
                            {
                                File.Copy(FI.FullName, des + i + FI.Name);
                                i += 1;
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

        public void MakeDir(string spath)  //建立文件夹
        {
            if (Directory.Exists(spath) == false)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(spath);
                directoryInfo.Create();
            }
        }

        public bool CalculateHash(string file1, string file2) //Hash计算
        {
            //计算第一个文件的哈希值
            var hash = System.Security.Cryptography.HashAlgorithm.Create();
            var stream_1 = new FileStream(file1, FileMode.Open);
            byte[] hashByte_1 = hash.ComputeHash(stream_1);
            stream_1.Close();

            //计算第二个文件的哈希值
            var stream_2 = new FileStream(file2, FileMode.Open);
            byte[] hashByte_2 = hash.ComputeHash(stream_2);
            stream_2.Close();

            //比较两个哈希值
            if (BitConverter.ToString(hashByte_1) == BitConverter.ToString(hashByte_2))
                return true;
            else
                return false;
        }
    }
}
