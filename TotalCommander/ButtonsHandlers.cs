using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalCommander
{
    class ButtonsHandlers
    {
        public static RelayCommand Copy { get; private set; }
        public static RelayCommand Move { get; private set; }
        public static RelayCommand Delete { get; private set; }
        public static RelayCommand Properties { get; private set; }

        public ButtonsHandlers()
        {
            Copy = new RelayCommand(MyEventHandler.Copy);
            Move = new RelayCommand(MyEventHandler.Move);
            Delete = new RelayCommand(MyEventHandler.Delete);
            Properties = Properties = new RelayCommand(MyEventHandler.Properties);
        }
    }
}
