using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DamBreakModelApplication
{
    delegate void updateProcessHandle(string text, int num);
    delegate void setProcessHandle(int min, int max);
    delegate void closeProcessHandle();
    

    class ProcessManager
    {
        ProcessWindow processShowWindow;
        public setProcessHandle setProcess;
        public updateProcessHandle updateProcess;
        public closeProcessHandle close;
        public Action<string> addInfomation;


        public void showProcessWindow()
        {
            processShowWindow = new ProcessWindow();
            setProcess = processShowWindow.setMinAndMaxValue;
            updateProcess =processShowWindow.setProcess;
            addInfomation = processShowWindow.addInfomation;
            close = processShowWindow.close;
            processShowWindow.Show();
            processShowWindow = null;
        }
        public MethodInvoker createProcessWindow()
        {
            return showProcessWindow;
        } 
 
    }
}
