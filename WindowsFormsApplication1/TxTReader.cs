using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace DamBreakModelApplication
{
    class TxTReader
    {

          public TxTReader()
        {

        }


       static  public List<string> txt2List(string path)
        {
            List<string> list = new List<string>();
            try
            {
                StreamReader sr = null;
                sr = File.OpenText(path);
                //
                string linetext;
                while ((linetext = sr.ReadLine()) != null)//StreamReader.ReadLine()每次执行一次，读取下一行内容。
                {
                    list.Add(linetext);
                }
                //
            }
            catch { }
            return list; 
        }

       static public List<string[]> txt2List2(string path)
        {
            List<string[]> listMit = new List<string[]>();
            try
            {
                StreamReader sr = null;
                sr = File.OpenText(path);
                //
                string linetext;

                while ((linetext = sr.ReadLine()) != null)
                {
                    string[] linetextSplit = new string[2];
                    linetextSplit[0] = linetext.Substring(0, linetext.LastIndexOf(","));
                    linetextSplit[1] = linetext.Substring(linetext.LastIndexOf(",") + 1);
                    listMit.Add(linetextSplit);
                }
                //
            }
            catch { }
            return listMit;
        }

       static public List<string[]> txt2List3(string path, int num)
       {
           List<string[]> listMit = new List<string[]>();
           int startIndex;

           StreamReader sr = null;
           sr = File.OpenText(path);
           //
           string linetext;

           while ((linetext = sr.ReadLine()) != null)
           {
               startIndex = 0;
               string[] linetextSplit = new string[num + 1];
               for (int i = 0; i < num; i++)
               {

                   linetextSplit[i] = linetext.Substring(startIndex, linetext.IndexOf(",", startIndex) - startIndex);
                   startIndex = linetext.IndexOf(",", startIndex) + 1;
               }
               linetextSplit[num] = linetext.Substring(startIndex);
               listMit.Add(linetextSplit);

           }
           return listMit;
       }

                    


    }
}
