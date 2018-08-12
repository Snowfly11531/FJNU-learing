using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace CAFloodModel
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }
        public void addlist(List<string> lists) {
            foreach (var list in lists) {
                listBox1.Items.Add(list);
            }
        }
        public List<String> getList() {
            List<String> lists = new List<string>();
            foreach (var item in listBox2.Items) {
                lists.Add(item.ToString());
            }
            return lists;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = listBox2.SelectedItems.Count - 1; i >= 0; i--)
            {
                var name = listBox2.SelectedItems[i].ToString();

                for (int j = 0; j < listBox1.Items.Count; j++)
                {
                    var inName = listBox1.Items[j].ToString();
                    if(name.CompareTo(inName)==-1){
                        listBox1.Items.Insert(j,name);
                        break;
                    }
                }
                listBox2.Items.Remove(name);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = listBox1.SelectedItems.Count - 1; i >= 0; i--) {
                listBox2.Items.Insert(0,listBox1.SelectedItems[i]);
                listBox1.Items.Remove(listBox1.SelectedItems[i]);
            }
        }
    }
}
