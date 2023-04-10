﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickOpenFolder
{
    public class MainWindow
    {
        /// <summary>
        /// 文件夹路径
        /// </summary>
        public string folderPath { get; set; }

        /// <summary>
        /// 默认选择路径
        /// </summary>
        public string defaultSelectFolderPath { get; set; }

        /// <summary>
        /// 剪切板内容
        /// </summary>
        public string clipboardText { get; set; }

        /// <summary>
        /// 自动匹配剪切板内容按钮状态
        /// </summary>
        public bool compareFolderNameButtonState = true;

        /// <summary>
        /// 用于匹配的目标名称，默认不包含于剪切板内容
        /// </summary>
        public string nameValue = "sdfsdrgse5rtg4etsergse5tw34twertgsedfgsdrfasdrgfsedfgsergsedrfgfs345w3t6w45tq3246tw46ujr68it65rr6fgws34rtq4rdrgdsretyhdr6ye45yw4e5rtgserfgsedr2";

        /// <summary>
        /// 获取一个文件夹下所有子文件夹的名称
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<string> GetSubfolders(string path)
        {
            var subfolders = new List<string>();
            var subfolderPaths = Directory.GetDirectories(path);
            foreach (var subfolderPath in subfolderPaths)
            {
                subfolders.Add(subfolderPath);
                subfolders.AddRange(GetSubfolders(subfolderPath));
            }
            return subfolders;
        }
    }
}