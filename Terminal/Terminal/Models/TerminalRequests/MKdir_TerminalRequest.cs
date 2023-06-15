using System;
using System.IO;
using Terminal.Handlers;
using Terminal.Helpers;
using Terminal.Models.TerminalRequests.Base;

namespace Terminal.Models.TerminalRequests
{
    internal class mkdir_TerminalRequest : TerminalRequest
    {
        public mkdir_TerminalRequest()
        {
            CommandName = "mkdir";
            Description = "{directoryName}";
        }

        public override void Execute(CommandHandler handler, string commandBody = "")
        {
            commandBody = commandBody.Replace("\\\\", "\\");

            try
            {
                string directoryPath = Path.Combine(CommandHandler.CurrentDirectoryPath, commandBody);
                Directory.CreateDirectory(directoryPath);
                Console.WriteLine($"Directory '{directoryPath}' created successfully.");
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteColorLine($"Error creating directory: {ex.Message}", ConsoleColor.DarkRed);
            }
        }
    }
}
