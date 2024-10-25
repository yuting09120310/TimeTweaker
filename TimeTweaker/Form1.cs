using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeTweaker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void TxtBrowseFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                TxtBrowseFolder.Text = folderDialog.SelectedPath;
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            string folderPath = TxtBrowseFolder.Text;
            if (Directory.Exists(folderPath))
            {
                try
                {
                    // 遞迴修改資料夾和子資料夾內所有檔案的時間
                    UpdateFolderTimes(folderPath);
                    MessageBox.Show("所有檔案的時間已成功更新！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"修改檔案時間時發生錯誤：{ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("資料夾不存在！");
            }
        }

        private void UpdateFolderTimes(string folderPath)
        {
            // 獲取該資料夾內的所有檔案
            string[] files = Directory.GetFiles(folderPath);
            foreach (var file in files)
            {
                try
                {
                    // 修改每個檔案的時間屬性
                    File.SetCreationTime(file, dateTimePicker1.Value);
                    File.SetLastWriteTime(file, dateTimePicker1.Value);
                    File.SetLastAccessTime(file, dateTimePicker1.Value);
                }
                catch (Exception ex)
                {
                    // 顯示錯誤但不停止迴圈
                    MessageBox.Show($"修改檔案時間時發生錯誤：{file}\n錯誤訊息：{ex.Message}");
                }
            }

            // 獲取該資料夾內的所有子資料夾
            string[] subFolders = Directory.GetDirectories(folderPath);
            foreach (var subFolder in subFolders)
            {
                // 遞迴處理每個子資料夾
                UpdateFolderTimes(subFolder);
            }
        }
    }
}
