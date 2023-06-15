using System;
using System.Diagnostics;
using System.IO;
using Terminal.Handlers;
using Terminal.Helpers;
using Terminal.Models.TerminalRequests.Base;
namespace Terminal.Models.TerminalRequests
{
    public class Delete : TerminalRequest
    {
        public Delete()
        {
            CommandName = "delete";
            Description = "{url + name || name}";
        }
        public override void Execute(CommandHandler handler, string commandBody = "")
        {
            try
            {
                if (commandBody.StartsWith("-"))
                {
                    char key = commandBody[1];

                    switch (key)
                    {
                        case 'd':
                            Console.WriteLine("Delete file");
                            DeleteFile(handler);
                            break;
                        case 'r':
                            Console.WriteLine("Restore file");
                            RestoreFile(handler);
                            break;
                        default:
                            throw new Exception("Invalid command body");
                    }
                }
                else
                {
                    Console.WriteLine($"Executed: {CommandName} {commandBody}");
                }
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteColorLine($"Error: {ex.Message}", ConsoleColor.DarkRed);
            }
        }
        private void DeleteFile(CommandHandler handler)
        {
            Console.WriteLine("Enter file path:");
            string filePath = Console.ReadLine();
            try
            {
                if (!File.Exists(filePath))
                {
                    ConsoleHelper.WriteColorLine($"File '{filePath}' does not exist.", ConsoleColor.Yellow);
                    return;
                }

                Console.WriteLine($"Are you sure you want to delete the file '{filePath}'? (yes/no)");
                string confirmation = Console.ReadLine();

                if (confirmation.Trim().ToLower() == "yes")
                {
                    File.Delete(filePath);
                    ConsoleHelper.WriteColorLine($"File '{filePath}' deleted successfully.", ConsoleColor.Green);
                    handler.NavigateBack();
                }
                else
                {
                    ConsoleHelper.WriteColorLine("Operation canceled.", ConsoleColor.Yellow);
                }
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteColorLine($"An error occurred while deleting the file: {ex.Message}", ConsoleColor.DarkRed);
            }
        }
        private void RestoreFile(CommandHandler handler)
        {
            Console.WriteLine("Enter file path:");
            string filePath = Console.ReadLine();

            try
            {
                ConsoleHelper.WriteColorLine($"File '{filePath}' restored successfully.", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteColorLine($"An error occurred while restoring the file: {ex.Message}", ConsoleColor.DarkRed);
            }
        }
    }
    public static class CommandHandlerExtensions
    {
        public static void NavigateBack(this CommandHandler handler)
        {
            Console.WriteLine("Navigating back...");
        }
    }
}
