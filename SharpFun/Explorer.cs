using Explorer;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace SharpFun
{
    public partial class Explorer : Form
    {
        List<Displayed> leftBox = new List<Displayed>();
        List<Displayed> rightBox = new List<Displayed>();
        int selSide = 0;

        public Explorer()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            updateExploredDirs(@"C:\", leftBox, listBox1, textBox1);
            updateExploredDirs(@"C:\", rightBox, listBox2, textBox2);
        }

        void Dirinfo(string path, List<Displayed> box, ListBox listBox, TextBox? textbox)
        {
            if (textbox != null)
            {
                textbox.Text = path;
            }
            List<string> paths = Directory.GetDirectories(path).Concat(Directory.GetFiles(path)).ToList<string>();
            box.Clear();
            foreach (string item in paths)
            {
                box.Add(new Displayed(Path.GetFileName(item), item));
            }
            UpdateDisp(listBox, box);
        }
        void UpdateDisp(ListBox listBox, List<Displayed> displayeds)
        {
            listBox.Items.Clear();
            listBox.Items.Add("↵");
            foreach (Displayed disp in displayeds)
            {
                listBox.Items.Add(disp.Name);
            }
        }
        void updateExploredDirs(String pathway, List<Displayed> displayeds, ListBox listBox,TextBox? textBox)
        {
            try
            {
                if (Directory.Exists(pathway))
                {
                    if (textBox1 != null)
                    {

                        Dirinfo(pathway, displayeds, listBox, textBox);
                    }
                    else
                    {
                        Dirinfo(pathway, displayeds, listBox, null);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            updateExploredDirs(textBox1.Text, leftBox, listBox1,null);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            updateExploredDirs(textBox2.Text, rightBox, listBox2,null);
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex - 1 < 0)
                doubleClickBox("↵", leftBox, listBox1, textBox1);
            else
                doubleClickBox(leftBox[listBox1.SelectedIndex - 1].PathWay, leftBox, listBox1, textBox1);
        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex - 1 < 0)
                doubleClickBox("↵", rightBox, listBox2, textBox2);
            else
                doubleClickBox(rightBox[listBox2.SelectedIndex-1].PathWay, rightBox, listBox2, textBox2);
        }

        void doubleClickBox(String pathway, List<Displayed> displayeds, ListBox listBox, TextBox? textBox)
        {
            if (listBox.SelectedIndex == 0)
            {
                if (Directory.GetDirectoryRoot(textBox.Text) != textBox.Text)
                {
                    updateExploredDirs(Directory.GetParent(textBox.Text).FullName, displayeds, listBox, textBox);
                }
            }
            else if (Directory.Exists(pathway))
            {
                updateExploredDirs(pathway, displayeds, listBox, textBox);
            }
            else if (File.Exists(pathway))
            {
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo(pathway);
                    startInfo.UseShellExecute = true;
                    Process.Start(startInfo);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        void Copy()
        {
            switch (selSide)
            {
                case 1:
                    File.Copy(textBox1.Text,textBox2.Text + ".copy");
                    break;
                case 2:

                    break;
                default:
                    break;
            }
        }
    }
}
