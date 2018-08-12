using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CAFloodModel
{
    static class RWdefault
    {
        public static void readDefault(this Form form) {
            FileStream fs = new FileStream("../../Resources/default_"+form.Name+".txt", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            foreach (var control in form.Controls) {
                if (control is TextBox) {
                    (control as TextBox).Text = sr.ReadLine();
                }
                if (control is GroupBox)
                {
                    foreach (var controlIn in (control as GroupBox).Controls)
                    {
                        if (controlIn is TextBox) {
                            (controlIn as TextBox).Text = sr.ReadLine();
                        }
                    }
                }
            }
            sr.Close();
            fs.Close();
        }
        public static void writeDefault(this Form form)
        {
            Console.WriteLine("haha");
            FileStream fs = new FileStream("../../Resources/default_" + form.Name + ".txt", FileMode.Create,FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            foreach (var control in form.Controls)
            {
                Console.WriteLine("haha2");
                if (control is TextBox)
                {
                    sw.WriteLine((control as TextBox).Text);
                }
                if (control is GroupBox) {
                    foreach (var controlIn in (control as GroupBox).Controls) {
                        if (controlIn is TextBox)
                        {
                            sw.WriteLine((controlIn as TextBox).Text);
                        }
                    }
                }
            }
            sw.Flush();
            sw.Close();
            fs.Close();
        }
    }
}
