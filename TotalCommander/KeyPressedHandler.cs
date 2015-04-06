using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace TotalCommander
{
    class KeyPressedHandler
    {
        private const Key KeyCopy = Key.F5;
        private const Key KeyMove = Key.F6;
        private const Key KeyProperties = Key.F1;

        private const Key KeyBack = Key.Back;
        private const Key KeyDelete = Key.Delete;
        private const Key KeyEnter = Key.Return;

        public KeyPressedHandler(MainWindow mainWindow)
        {
            mainWindow.KeyUp += new KeyEventHandler(KeyHandlerFunction);
        }

        private static void KeyHandlerFunction(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case KeyCopy:
                    MyEventHandler.Copy();
                    break;
                case KeyMove:
                    MyEventHandler.Move();
                    break;
                case KeyDelete:
                    MyEventHandler.Delete();
                    break;
                
                case KeyEnter:
                    MyEventHandler.Enter();
                    break;
                case KeyBack:
                    MyEventHandler.Back();
                    break;

                case KeyProperties:
                    MyEventHandler.Properties();
                    break;
            }
        }
        
    }
}
