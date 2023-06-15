using System.ComponentModel;
using System.Runtime.CompilerServices;
using Terminal.Handlers;
using Terminal.Helpers;
using Terminal.Models.TerminalRequests.Base;

namespace Terminal.Models.TerminalRequests
{
    internal class MKFILE_TerminalRequest : TerminalRequest
    {
        public MKFILE_TerminalRequest()
        {
            CommandName = "mkfile";
            Description = "{url + name || name}";
        }
        public override void Execute(CommandHandler handler, string commandBody = "")
        {
            try
            {
                string fileName = Path.Combine(CommandHandler.CurrentDirectoryPath, commandBody);
                File.Create(fileName);
                Console.WriteLine($"File '{fileName}' created successfully.");
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteColorLine($"Error: {ex.Message}", ConsoleColor.DarkRed);
            }
        }
    }
}
