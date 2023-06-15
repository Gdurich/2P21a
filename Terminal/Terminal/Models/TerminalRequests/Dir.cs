using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Handlers;
using Terminal.Models.TerminalRequests.Base;
using System.IO;

namespace Terminal.Models.TerminalRequests
{
    internal class Dir : TerminalRequest
    {
        public Dir()
        {
            CommandName = "dir";
            Description = "{write \"dir\" and this allows you to view information about the files}";
        }

        public override void Execute(CommandHandler handler, string commandBody = "")
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            string[] files = GetFilesInDirectory(currentDirectory);
            string[] directories = GetDirectoriesInDirectory(currentDirectory);

            PrintFiles(files);
            PrintDirectories(directories);
        }

        static string[] GetFilesInDirectory(string directoryPath)
        {
            return Directory.GetFiles(directoryPath);
        }

        static string[] GetDirectoriesInDirectory(string directoryPath)
        {
            return Directory.GetDirectories(directoryPath);
        }

        static void PrintFiles(string[] files)
        {
            Console.WriteLine("Файли:");
            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                Console.WriteLine($"Ім'я: {fileInfo.Name}");
                Console.WriteLine($"Розмір: {fileInfo.Length} байт");
                Console.WriteLine($"Дата зміни: {fileInfo.LastWriteTime}");
                Console.WriteLine();
            }
        }

        static void PrintDirectories(string[] directories)
        {
            Console.WriteLine("Папки:");
            foreach (string directory in directories)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(directory);
                Console.WriteLine($"Ім'я: {directoryInfo.Name}");
                Console.WriteLine($"Дата створення: {directoryInfo.CreationTime}");
                Console.WriteLine();
            }
        }
    }
}
