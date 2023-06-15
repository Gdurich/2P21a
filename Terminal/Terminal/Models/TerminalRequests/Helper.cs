using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Handlers;
using Terminal.Models.TerminalRequests.Base;

namespace Terminal.Models.TerminalRequests
{
    internal class Helper : TerminalRequest
    {
        public Helper()
        {
            CommandName = "help";
            Description = "{view information about commands}";
        }

        public override void Execute(CommandHandler handler, string commandBody = "")
        {
            Console.WriteLine("Available commands:");
            foreach (TerminalRequest terminalRequest in handler.GetCommandList())
            {
                Console.WriteLine($"\t{terminalRequest.CommandName}: {terminalRequest.Description}\n");
            }
        }
    }

}
