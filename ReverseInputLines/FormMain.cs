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
            openFile.Filter = "Text|*.txt";

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

            listTextLines = allLines.ToList();

            for (int i = 0; i < allLines.Length; i++)
            {
                stackTextLines.Push(allLines.ElementAtOrDefault(i));
            }
        }

        private void BtnReverse_Click(object sender, EventArgs e)
        {
            string listLinesDir = Environment.CurrentDirectory + @"\listLines.txt";
            string stackLinesDir = Environment.CurrentDirectory + @"\stackLines.txt";

            SaveListLinesToTextFile(listTextLines, listLinesDir);
            SaveStackLinesToTextFile(stackTextLines, stackLinesDir);

            Process.Start("notepad.exe", listLinesDir);
            Process.Start("notepad.exe", stackLinesDir);
        }

        private void SaveListLinesToTextFile(List<string> listTextLines, string saveDir)
        {
            using (StreamWriter sw = new StreamWriter(saveDir))
            {
                for (int i = listTextLines.Count - 1; i >= 0; i--)
                {
                    sw.WriteLine(listTextLines.ElementAtOrDefault(i));
                }
            }
        }

        private void SaveStackLinesToTextFile(Stack<string> stackTextLines, string saveDir)
        {
            using (StreamWriter sw = new StreamWriter(saveDir))
            {
                for (int i = 0; i < stackTextLines.Count; i++)
                {
                    sw.WriteLine(stackTextLines.ElementAtOrDefault(i));
                }
            }
        }
    }
}
