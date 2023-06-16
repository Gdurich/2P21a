using System.ComponentModel;
using System.Runtime.CompilerServices;
using Terminal.Handlers;
using Terminal.Helpers;
using Terminal.Models.TerminalRequests.Base;

namespace Terminal.Models.TerminalRequests
{
    internal class MOVE_TerminalRequest : TerminalRequest
    {
        public MOVE_TerminalRequest()
        {
            CommandName = "move";
            Description = "{url + name || name} {url + name}";
        }
        public override void Execute(CommandHandler handler, string commandBody = "")
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteColorLine($"Error: {ex.Message}", ConsoleColor.DarkRed);
            }
        }
    }
}
