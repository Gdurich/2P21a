using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Handlers;
using Terminal.Models.TerminalRequests.Base;

namespace Terminal.Models.TerminalRequests
{

    internal class ClearInfo : TerminalRequest
    {
        public ClearInfo()
        {
            CommandName = "clearinfo";
            Description = "{full path to the file}";
        }

        public override void Execute(CommandHandler handler, string commandBody = "")
        {
            string input = Console.ReadLine();

            string filePath = GetFilePath(input);

            if (string.IsNullOrEmpty(filePath))
            {
                Console.WriteLine("\tInvalid file path.");
            }
            else
            {
                ClearFileContents(filePath);
                Console.WriteLine("\tThe contents of the file have been cleared.");
            }

        }

        static string GetFilePath(string input)
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            if (Path.IsPathRooted(input))
            {
                return input;
            }

            string filePath = Path.Combine(currentDirectory, input);

            if (File.Exists(filePath))
            {
                return filePath;
            }

            return string.Empty;
        }

        static void ClearFileContents(string filePath)
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(filePath))
                {
                    streamWriter.Write(string.Empty);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\tAn error occurred: {ex.Message}");
            }
        }
    }
}
