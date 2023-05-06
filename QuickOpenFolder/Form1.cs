using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace QuickOpenFolder
{
    public partial class Form1 : Form
    {
        MainWindow mainWindow = new MainWindow();

        public Form1()
        {
            InitializeComponent();
            //判断是否存在folderpath.txt和folderNames.json，不存在则生成
            string exePath = System.Reflection.Assembly.GetEntryAssembly().Location;
            string folderPath = Path.GetDirectoryName(exePath);
            string folderPathFile = Path.Combine(folderPath, "folderpath.txt");
            string folderNamesFile = Path.Combine(folderPath, "folderNames.json");
            if (!File.Exists(Path.Combine(folderPath, "folderpath.txt")))
            {
                File.WriteAllText(folderPathFile, "");
            }
            if (!File.Exists(Path.Combine(folderPath, "folderNames.json")))
            {
                File.WriteAllText(folderNamesFile, "[\"\"]");
            }
            mainWindow.defaultSelectFolderPath = System.IO.File.ReadAllText("folderpath.txt");
            //UpdateFoldersNames();
            txtFolderPathTextBox.Text = mainWindow.defaultSelectFolderPath;

            // 设置 Timer 控件的间隔时间为 1 秒
            timer1.Interval = 1000;

            // 启动 Timer 控件
            timer1.Start();

        }

        /// <summary>
        /// 打开文件夹按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            if (mainWindow.folderPath != "Not Found")
            { OpenFolder(mainWindow.folderPath); }
            else
            {
                OpenFolder(mainWindow.defaultSelectFolderPath);
            }
        }

        /// <summary>
        /// 打开文件夹,并保存前一个路径
        /// </summary>
        /// <param name="directory"></param>
        private void OpenFolder(string directory)
        {
            mainWindow.folderPath = directory;
            System.Diagnostics.Process.Start("explorer.exe", "\"" + mainWindow.folderPath + "\"");
        }

        /// <summary>
        /// 选择文件夹按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            var dlg = new FolderPicker();
            dlg.InputPath = mainWindow.defaultSelectFolderPath;
            if (dlg.ShowDialog(IntPtr.Zero) == true)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dlg.ResultPath);
                if (dirInfo.Exists)
                {
                    System.IO.File.WriteAllText("folderPath.txt", dlg.ResultPath);
                    mainWindow.defaultSelectFolderPath = System.IO.File.ReadAllText("folderPath.txt");

                    // 显示所选文件夹路径
                    txtFolderPathTextBox.Text = mainWindow.defaultSelectFolderPath;
                }
            }
            UpdateFoldersNames();
            DealWithClipboard();
        }

        /// <summary>
        /// 更新文件夹名称按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateFoldersName_Click(object sender, EventArgs e)
        {
            UpdateFoldersNames();
            DealWithClipboard();
        }

        /// <summary>
        /// 更新文件夹名称
        /// </summary>
        private void UpdateFoldersNames()
        {
            ShowUpdateStateTextBox.Text = "更新中……";
            List<string> list = new List<string>();
            list = mainWindow.GetSubfolders(mainWindow.defaultSelectFolderPath);
            string json = JsonConvert.SerializeObject(list);

            // 将JSON字符串保存为文件
            string path = "folderNames.json";
            File.WriteAllText(path, json);
            ShowUpdateStateTextBox.Text = "更新完毕";
            ShowUpdateStateTextBox.ForeColor = Color.Green;
        }

        /// <summary>
        /// 匹配文件夹名称并打开文件夹
        /// </summary>
        /// <param name="folderName"></param>
        /// <returns></returns>
        private string CompareFolderName(string folderName)
        {
            // 读取JSON文件并将其反序列化为一个对象列表
            string json = File.ReadAllText("folderNames.json");
            List<string> list = JsonConvert.DeserializeObject<List<string>>(json);

            // 在列表中查找包含指定文本的第一个项,删去特殊字符和压缩包后缀
            string searchText = folderName;
            string cleanedSearchText = Regex.Replace(searchText, "\\.zip|\\.rar|\\.7z$", "");
            cleanedSearchText = Regex.Replace(cleanedSearchText, "[,\\s.\\t\\n\\r:;\"'\\-_/\\\\=+*%|\\$#?]", "");
            string match = list.FirstOrDefault(item =>
            {
                string fileName = item;
                int lastBackslashIndex = fileName.LastIndexOf('\\');
                if (lastBackslashIndex >= 0 && lastBackslashIndex < fileName.Length - 1)
                {
                    fileName = fileName.Substring(lastBackslashIndex + 1);
                }
                string cleanedFileName = Regex.Replace(fileName, "[,\\s.\\t\\n\\r:;\"'\\-_/\\\\=+*%|\\$#?]", "");
                return cleanedFileName.Equals(cleanedSearchText.Replace(" ", ""), StringComparison.OrdinalIgnoreCase);
            });

            // 输出匹配项（如果找到了）
            if (match != null)
            {
                return match;
            }
            else
            {
                //保留压缩包后缀再次匹配
                searchText = folderName;
                searchText = Regex.Replace(searchText, "[,\\s.\\t\\n\\r:;\"'\\-_/\\\\=+*%|\\$#?]", "");
                match = list.FirstOrDefault(item =>
                {
                    string fileName = item;
                    int lastBackslashIndex = fileName.LastIndexOf('\\');
                    if (lastBackslashIndex >= 0 && lastBackslashIndex < fileName.Length - 1)
                    {
                        fileName = fileName.Substring(lastBackslashIndex + 1);
                    }
                    string cleanedFileName = Regex.Replace(fileName, "[,\\s.\\t\\n\\r:;\"'\\-_/\\\\=+*%|\\$#?]", "");
                    return cleanedFileName.Equals(searchText.Replace(" ", ""), StringComparison.OrdinalIgnoreCase);
                });
                if (match != null)
                {
                    return match;
                }
                else
                {
                    return "Not Found";
                }
            }
        }

        /// <summary>
        /// 切换自动匹配文件夹名称按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CompareFolderNameButton_Click(object sender, EventArgs e)
        {
            if (mainWindow.compareFolderNameButtonState == true) // 当前为第一个状态
            {
                // 执行第一个状态的操作
                CompareClipboardContentButton.Text = "自动匹配剪切板内容(已关闭)";
                CompareClipboardContentButton.ForeColor = Color.Black;
                mainWindow.compareFolderNameButtonState = false; // 切换为第二个状态
            }
            else // 当前为第二个状态
            {
                // 执行第二个状态的操作
                CompareClipboardContentButton.Text = "自动匹配剪切板内容(已开启)";
                CompareClipboardContentButton.ForeColor = Color.Red;
                mainWindow.compareFolderNameButtonState = true; // 切换为第一个状态
            }
        }

        /// <summary>
        /// 重命名提醒按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RenameButton_Click(object sender, EventArgs e)
        {
            if (mainWindow.renameButtonState == true) // 当前为第一个状态
            {
                // 执行第一个状态的操作
                RenameButton.Text = "重命名提醒(已关闭)";
                RenameButton.ForeColor = Color.Black;
                mainWindow.renameButtonState = false; // 切换为第二个状态
            }
            else // 当前为第二个状态
            {
                // 执行第二个状态的操作
                RenameButton.Text = "重命名提醒(已开启)";
                RenameButton.ForeColor = Color.Red;
                mainWindow.renameButtonState = true; // 切换为第一个状态
            }
        }

        /// <summary>
        /// 定时检测剪切板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (mainWindow.compareFolderNameButtonState == true)
            {
                // 检查剪贴板是否有内容
                if (Clipboard.ContainsText())
                {
                    mainWindow.clipboardText = Clipboard.GetText();
                    ShowClipboardContentTextBox.Text = mainWindow.clipboardText;
                    if (!mainWindow.clipboardText.Contains(mainWindow.nameValue))
                    {
                        DealWithClipboard();
                    }
                }
            }
        }

        /// <summary>
        /// 处理剪切板
        /// </summary>

        private void DealWithClipboard()
        {
            mainWindow.clipboardText = Clipboard.GetText();
            ShowClipboardContentTextBox.Text = mainWindow.clipboardText;

            // 根据是否是json格式分别处理
            try
            {
                JObject jsonObject = JObject.Parse(mainWindow.clipboardText);
                string nameForRename = "";
                foreach (JProperty property in jsonObject.Properties())
                {
                    if (property.Value.Type == JTokenType.String)
                    {
                        mainWindow.nameValue = property.Value.ToString();
                        mainWindow.folderPath = CompareFolderName(mainWindow.nameValue);
                        ShowDirectoryTextBox.Text = mainWindow.folderPath;
                        if (mainWindow.folderPath == "Not Found")
                        {
                            UpdateFoldersNames();
                            mainWindow.folderPath = CompareFolderName(mainWindow.nameValue);
                            ShowDirectoryTextBox.Text = mainWindow.folderPath;
                        }
                        if (mainWindow.folderPath != "Not Found")
                        {
                            OpenFolder(mainWindow.folderPath);
                            nameForRename = mainWindow.nameValue;
                            break;
                        }
                        //删去后缀，重复匹配
                        string match = Regex.Replace(mainWindow.nameValue, "\\.zip|\\.rar|\\.7z$", "");
                        mainWindow.h1Name = match;
                        mainWindow.folderPath = CompareFolderName(mainWindow.nameValue);
                        ShowDirectoryTextBox.Text = mainWindow.folderPath;
                        if (mainWindow.folderPath == "Not Found")
                        {
                            UpdateFoldersNames();
                            mainWindow.folderPath = CompareFolderName(mainWindow.nameValue);
                            ShowDirectoryTextBox.Text = mainWindow.folderPath;
                        }
                        if (mainWindow.folderPath != "Not Found")
                        {
                            OpenFolder(mainWindow.folderPath);
                            nameForRename = mainWindow.nameValue;
                            break;
                        }
                    }
                    else if (property.Value.Type == JTokenType.Array)
                    {
                        JArray array = (JArray)property.Value;
                        foreach (JToken token in array)
                        {
                            mainWindow.nameValue = token.ToString();
                            mainWindow.folderPath = CompareFolderName(mainWindow.nameValue);
                            ShowDirectoryTextBox.Text = mainWindow.folderPath;
                            if (mainWindow.folderPath == "Not Found")
                            {
                                UpdateFoldersNames();
                                mainWindow.folderPath = CompareFolderName(mainWindow.nameValue);
                                ShowDirectoryTextBox.Text = mainWindow.folderPath;
                            }
                            if (mainWindow.folderPath != "Not Found")
                            {
                                //ShowDirectoryTextBox.Text = mainWindow.folderPath;
                                if (mainWindow.renameButtonState == true)
                                {
                                    Regex regex = new Regex(@".*\\");
                                    Match match = regex.Match(mainWindow.folderPath);
                                    mainWindow.previousDirectory = match.Value;
                                    RenameFolder(mainWindow.folderPath, mainWindow.previousDirectory + mainWindow.h1Name);
                                    OpenFolder(mainWindow.folderPath);
                                }
                                break;
                            }
                        }
                    }
                }
            }
            catch
            {
                mainWindow.folderPath = CompareFolderName(mainWindow.clipboardText);
                mainWindow.nameValue = mainWindow.clipboardText;
                ShowDirectoryTextBox.Text = mainWindow.folderPath;
                if (mainWindow.folderPath == "Not Found")
                {
                    UpdateFoldersNames();
                    mainWindow.folderPath = CompareFolderName(mainWindow.nameValue);
                    ShowDirectoryTextBox.Text = mainWindow.folderPath;
                }
                if (mainWindow.folderPath != "Not Found")
                {
                    OpenFolder(mainWindow.folderPath);
                }
            }

        }

        /// <summary>
        /// 重命名磁链文本
        /// </summary>
        /// <param name="oldName"></param>
        /// <param name="newName"></param>
        private void RenameFolder(string oldName, string newName)
        {
            DialogResult result = MessageBox.Show("此文本为磁链内文本，不是eh标题文本，是否重命名？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            if (result == DialogResult.Yes)
            {
                try
                {
                    Directory.Move(oldName, newName);
                    Console.WriteLine("Folder renamed successfully.");
                    mainWindow.folderPath = mainWindow.previousDirectory + mainWindow.h1Name;
                    UpdateFoldersNames();
                }
                catch (IOException ex)
                {
                    Console.WriteLine("Error renaming folder: " + ex.Message);
                }
            }
            else
            {
                return;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
        }

        /// <summary>
        /// 清空剪切板按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetButton_Click(object sender, EventArgs e)
        {
            Reset();
        }

        /// <summary>
        /// 清空剪切板
        /// </summary>
        private void Reset()
        {
            Clipboard.Clear();
            mainWindow.clipboardText = "";
            mainWindow.nameValue = "sdfsdrgse5rtg4etsergse5tw34twertgsedfgsdrfasdrgfsedfgsergsedrfgfs345w3t6w45tq3246tw46ujr68it65rr6fgws34rtq4rdrgdsretyhdr6ye45yw4e5rtgserfgsedr2";
            ShowDirectoryTextBox.Text = "";
            ShowClipboardContentTextBox.Text = "";
        }
    }
}
