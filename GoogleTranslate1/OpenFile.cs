using AutoItX3Lib;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleTranslate1
{
    class OpenFile
    {

        private const string filePath = @"C:\Users\DM\Desktop\Testing\Hello.txt";

        public OpenFile()
        {
        }

        public void OpenFileFromLocalMachine()
        {
            AutoItX3 autoIt = new AutoItX3();
            autoIt.WinActivate("Open");
            WaitUntil.WaitSomeInterval();
            autoIt.Send(filePath);
            WaitUntil.WaitSomeInterval();
            autoIt.Send("{ENTER}");
        }
    }
}
