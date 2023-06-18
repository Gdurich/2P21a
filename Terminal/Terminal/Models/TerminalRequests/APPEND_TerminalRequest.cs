using System.ComponentModel;
using System.Runtime.CompilerServices;
using Terminal.Handlers;
using Terminal.Helpers;
using Terminal.Models.TerminalRequests.Base;

namespace Terminal.Models.TerminalRequests
{
    internal class APPEND_TerminalRequest : TerminalRequest
    {
        public APPEND_TerminalRequest()
        {
            CommandName = "append";
            Description = "{-b || -c || -e} {url + name || name}";
        }
        public override void Execute(CommandHandler handler, string commandBody = "")
        {
            try
            {
                Console.WriteLine(handler);
                Console.WriteLine(commandBody);
                if (commandBody.StartsWith("-"))
                {
                    char key = commandBody[1];

                    string filePath = Path.Combine(CommandHandler.CurrentDirectoryPath, commandBody.Substring(3));

                    Console.WriteLine(filePath);

                    switch (key)
                    {
                        case 'b':
                            Console.WriteLine("Edit file at the beginning");
                            EditFile(filePath, handler, EditPosition.Beginning);
                            break;
                        case 'c':
                            Console.WriteLine("Edit file at the center");
                            EditFile(filePath, handler, EditPosition.Center);
                            break;
                        case 'e':
                            Console.WriteLine("Edit file at the end");
                            EditFile(filePath, handler, EditPosition.End);
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

        private void EditFile(string filePath, CommandHandler handler, EditPosition position)
        {
            try
            {
                string fileContent = File.ReadAllText(filePath);
                Console.WriteLine($"File content: {fileContent}");
                string content = string.Empty;
                
                Console.WriteLine("Enter content to append:");
                content = Console.ReadLine();
                
                switch (position)
                {
                    case EditPosition.Beginning:
                        fileContent = content + fileContent;
                        break;
                    case EditPosition.Center:
                        int insertIndex = fileContent.Length / 2;
                        fileContent = fileContent.Insert(insertIndex, content);
                        break;
                    case EditPosition.End:
                        fileContent = fileContent + content;
                        break;
                }
                File.WriteAllText(filePath, fileContent);
                Console.WriteLine($"Content successfully edited in file '{filePath}' at {position}.");
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteColorLine($"Error editing file: {ex.Message}", ConsoleColor.DarkRed);
            }
        }
    }

    public enum EditPosition
    {
        Beginning,
        Center,
        End
    }
}
