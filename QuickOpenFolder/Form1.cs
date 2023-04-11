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
        /// 打开文件夹
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
            Reset();
        }

        /// <summary>
        /// 更新文件夹名称按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateFoldersName_Click(object sender, EventArgs e)
        {
            UpdateFoldersNames();
            Reset();
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

            // 在列表中查找包含指定文本的第一个项,删去特殊字符
            string searchText = folderName;
            string searchTextWithoutExtension = Path.GetFileNameWithoutExtension(searchText);
            searchText = searchTextWithoutExtension;
            string match = list.FirstOrDefault(item =>
            {
                string fileName = item;
                int lastBackslashIndex = fileName.LastIndexOf('\\');
                if (lastBackslashIndex >= 0 && lastBackslashIndex < fileName.Length - 1)
                {
                    fileName = fileName.Substring(lastBackslashIndex + 1);
                }
                string cleanedFileName = Regex.Replace(fileName, "[,\\s.\\t\\n\\r:;\"'\\-_/\\\\=+*%|\\$#?]", "");
                string cleanedSearchText = Regex.Replace(searchText, "[,\\s.\\t\\n\\r:;\"'\\-_/\\\\=+*%|\\$#?]", "");
                return cleanedFileName.Equals(cleanedSearchText.Replace(" ", ""), StringComparison.OrdinalIgnoreCase);
            });

            // 输出匹配项（如果找到了）
            if (match != null)
            {
                OpenFolder(match);
                return match;
            }
            else
            {
                return "Not Found";
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
        /// 定时检测剪切板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (mainWindow.compareFolderNameButtonState == true)
            {
                // 检查剪贴板内容是否更改
                if (Clipboard.ContainsText())
                {
                    mainWindow.clipboardText = Clipboard.GetText();
                    ShowClipboardContentTextBox.Text = mainWindow.clipboardText;
                    //检测剪切板内容是否已包含目标名称
                    if (!mainWindow.clipboardText.Contains(mainWindow.nameValue))
                    {
                        // 根据是否是json格式分别处理
                        try
                        {
                            JObject jsonObject = JObject.Parse(mainWindow.clipboardText);
                            foreach (JProperty property in jsonObject.Properties())
                            {
                                if (property.Value.Type == JTokenType.String)
                                {
                                    mainWindow.nameValue = property.Value.ToString();
                                    mainWindow.folderPath = CompareFolderName(mainWindow.nameValue);
                                    ShowDirectoryTextBox.Text = mainWindow.folderPath;
                                    if (mainWindow.folderPath != "Not Found")
                                    {
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
                                        if (mainWindow.folderPath != "Not Found")
                                        {
                                            ShowDirectoryTextBox.Text = mainWindow.folderPath;
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
                        }
                    }
                }
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
