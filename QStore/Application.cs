using QStore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QStore
{
    public class Application
    {
       
        public static void RunCommand()
        {
            ConsoleHelper consoleHelper = new ConsoleHelper();
            int selector = consoleHelper.MainMenu();
            while (selector != 0)
            {
                consoleHelper.ActionMenu((Request)selector);
                selector = consoleHelper.MainMenu();
            }
        }
    }
}
