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
            Description = "\"{url + name}\" \"{url + name}\" || {name} {name}";
        }

        public override void Execute(CommandHandler handler, string commandBody = "")
        {
            try
            {
                string[] paths;

                if (commandBody.Contains("\""))
                {
                    string[] intermediateArray = commandBody.Split('\"');

                    paths = new string[2];

                    int[] indicesToCopy = { 1, 3 };

                    for (int i = 0; i < indicesToCopy.Length; i++)
                    {
                        int sourceIndex = indicesToCopy[i];
                        int destinationIndex = i;

                        paths[destinationIndex] = intermediateArray[sourceIndex];
                    }

                    int indexToRemove = 1;
                    Array.Copy(paths, indexToRemove + 1, paths, indexToRemove, paths.Length - indexToRemove - 1);
                }
                else
                {
                    paths = commandBody.Split(' ');
                }

                string sourcePath = Path.IsPathRooted(paths[0]) ? paths[0] : Path.Combine(CommandHandler.CurrentDirectoryPath, paths[0]);
                string destinationPath = Path.IsPathRooted(paths[1]) ? paths[1] : Path.Combine(CommandHandler.CurrentDirectoryPath, paths[1]);


                if (!File.Exists(sourcePath))
                {
                    throw new Exception("Source file does not exist");
                }

                File.Move(sourcePath, destinationPath);

                Console.WriteLine("File moved successfully");
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteColorLine($"Error: {ex.Message}", ConsoleColor.DarkRed);
            }
        }
    }
}
