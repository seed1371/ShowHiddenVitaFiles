using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShowHiddenVitaFiles
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AddCombo();
            flatComboBox1.SelectedIndex = 0;
        }
        List<string> files = new List<string>();
        private void button1_Click(object sender, EventArgs e)
        {
            string[] subFolders = System.IO.Directory.GetFileSystemEntries(flatComboBox1.Text, "*", System.IO.SearchOption.AllDirectories);
            foreach (string a in subFolders)
            {
                System.IO.DirectoryInfo di1 = new System.IO.DirectoryInfo(a);
                di1.Attributes &= ~System.IO.FileAttributes.Hidden;
                di1.Attributes &= ~System.IO.FileAttributes.ReadOnly;
                di1.Attributes &= ~System.IO.FileAttributes.System;
            }
        }
        

        private void AddCombo()
        {
            // コンボボックスの中身を初期化
            flatComboBox1.Items.Clear();

            // ドライブ一覧取得
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            // コンボボックスにドライブ名追加
            foreach (DriveInfo d in allDrives)
            {
                flatComboBox1.Items.Add(d.Name);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddCombo();
        }

        private void flatComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(
            IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        [DllImportAttribute("user32.dll")]
        private static extern bool ReleaseCapture();
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //マウスのキャプチャを解除
                ReleaseCapture();
                //タイトルバーでマウスの左ボタンが押されたことにする
                SendMessage(Handle, WM_NCLBUTTONDOWN, (IntPtr)HT_CAPTION, IntPtr.Zero);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}