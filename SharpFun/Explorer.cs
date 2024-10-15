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
        //bool leftSideSel

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
            //string names = new List<string>();
            box.Clear();
            foreach (string item in paths)
            {
                box.Add(new Displayed("tets", item));
            }
            UpdateDisp(listBox, box);
        }
        void UpdateDisp(ListBox listBox, List<Displayed> displayeds)
        {
            listBox.Items.Clear();
            listBox.Items.Add("↵");
            foreach (Displayed disp in displayeds)
            {
                listBox.Items.Add(disp.PathWay);
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
            doubleClickBox(listBox1.Text, leftBox, listBox1,textBox1);
        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            doubleClickBox(listBox2.Text, rightBox, listBox2, textBox2);
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
    }
}
