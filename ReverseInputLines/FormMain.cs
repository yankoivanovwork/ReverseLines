using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReverseInputLines
{
    public partial class FormMain : Form
    {
        private OpenFileDialog openFile = new OpenFileDialog();
        private List<string> listTextLines = new List<string>();
        private Stack<string> stackTextLines = new Stack<string>();

        public FormMain()
        {
            InitializeComponent();

            CenterToScreen();
        }

        private void BtnLoadFile_Click(object sender, EventArgs e)
        {
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                SaveLines2Collections(openFile.FileName);
            }
        }

        private void SaveLines2Collections(string path)
        {
            listTextLines = new List<string>();
            stackTextLines = new Stack<string>();

            string[] allLines = File.ReadAllLines(path);

            if (path.Contains(".txt"))
            {
                listTextLines = allLines.ToList();

                for (int i = 0; i < allLines.Length; i++)
                {
                    stackTextLines.Push(allLines.ElementAtOrDefault(i));
                }
            }
        }

        private void BtnReverse_Click(object sender, EventArgs e)
        {
            string listLinesDir = Environment.CurrentDirectory + @"\listLines.txt";
            string stackLinesDir = Environment.CurrentDirectory + @"\stackLines.txt";

            SaveListLines2Text(listTextLines, listLinesDir);
            SaveStackLines2Text(stackTextLines, stackLinesDir);

            Process.Start("notepad.exe", listLinesDir);
            Process.Start("notepad.exe", stackLinesDir);
        }

        private void SaveListLines2Text(List<string> listTextLines, string saveDir)
        {
            StringBuilder lineBuilder = new StringBuilder();
            string saveLines = string.Empty;

            listTextLines.Reverse();

            if (listTextLines.Count <= 8)
            {
                for (int i = 0; i < listTextLines.Count; i++)
                {
                    saveLines += (listTextLines.ElementAtOrDefault(i) + Environment.NewLine);
                }
                File.WriteAllText(saveDir, saveLines);
            }
            else
            {
                for (int i = 0; i < listTextLines.Count; i++)
                {
                    lineBuilder.AppendLine(listTextLines.ElementAtOrDefault(i));
                }
                File.WriteAllText(saveDir, lineBuilder.ToString());
            }
        }

        private void SaveStackLines2Text(Stack<string> stackTextLines, string saveDir)
        {
            StringBuilder lineBuilder = new StringBuilder();
            string saveLines = string.Empty;

            if (listTextLines.Count <= 8)
            {
                foreach (var line in stackTextLines)
                {
                    saveLines += (line + Environment.NewLine);
                }
                File.WriteAllText(saveDir, saveLines);
            }
            else
            {
                foreach (var line in stackTextLines)
                {
                    lineBuilder.AppendLine(line);
                }
                File.WriteAllText(saveDir, lineBuilder.ToString());
            }
        }
    }
}
