using System.ComponentModel;
using System.Runtime.CompilerServices;
using Terminal.Handlers;
using Terminal.Helpers;
using Terminal.Models.TerminalRequests.Base;

namespace Terminal.Models.TerminalRequests
{
    internal class CD_TerminalRequest : TerminalRequest
    {
        public CD_TerminalRequest()
        {
            CommandName = "cd";
            Description = "{ || .. || dirName || url + dirName}";
        }
        public override void Execute(CommandHandler handler, string commandBody = "")
        {
            commandBody = commandBody.Replace("\\\\", "\\");
            try
            {
                switch (commandBody)
                {
                    case "":
                        Console.WriteLine(CommandHandler.CurrentDirectoryPath);
                        break;
                    case "..":
                        if (!handler.SetCurrentDirectory(CommandHandler.CurrentDirectoryPath.Substring(0, CommandHandler.CurrentDirectoryPath.LastIndexOf('\\')))) throw new Exception("Directory not exists");
                        break;
                    default:
                        if (!handler.SetCurrentDirectory(commandBody)) throw new Exception("Directory not exists");
                        break;
                }
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteColorLine($"Error: {ex.Message}", ConsoleColor.DarkRed);
            }
        }
    }
}
