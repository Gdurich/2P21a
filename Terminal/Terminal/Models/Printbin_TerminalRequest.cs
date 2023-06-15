using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Handlers;
using Terminal.Models.TerminalRequests.Base;

namespace Terminal.Models.TerminalRequests
{
    internal class Printbin_TerminalRequest : TerminalRequest
    {
        public Printbin_TerminalRequest()
        {
            CommandName = "printbin";
            Description = "{url + name || name}";
        }

        public override void Execute(CommandHandler handler, string commandBody = "")
        {
            string fileName = commandBody.Trim();

            switch (!string.IsNullOrWhiteSpace(fileName) && !fileName.Contains("\\"))
            {
                case true:
                    RecursiveSearchAndConvertToBinary(fileName);
                    break;
                case false:
                    ConvertFileToBinary(fileName);
                    break;
            }
        }

        static void RecursiveSearchAndConvertToBinary(string fileName)
        {
            string[] drivers = GetRootFolders();
            foreach (string driver in drivers)
            {
                Console.WriteLine("Папка: " + driver);
                Console.WriteLine("--------------------------------------");

                Action<string> searchAction = new Action<string>(driver =>
                {
                    SearchFile(driver, fileName);
                });

                FolderTraversal(driver, searchAction);

            }
        }

        static void ConvertFileToBinary(string filePath)
        {
            try
            {
                string fileContents = File.ReadAllText(filePath);
                string binary = ConvertToBinaryString(fileContents);
                Console.WriteLine("Текст файлу у двійковому форматі:");
                Console.WriteLine(binary);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Сталася помилка при конвертації в двійковий формат: " + ex.Message);
            }
        }

        static string[] GetRootFolders()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            string[] rootFolders = new string[drives.Length];

            for (int i = 0; i < drives.Length; i++)
            {
                rootFolders[i] = drives[i].RootDirectory.FullName;
            }

            return rootFolders;
        }

        static void FolderTraversal(string folder, Action<string> searchAction)
        {
            try
            {
                string[] files;
                try
                {
                    files = Directory.GetFiles(folder);
                }
                catch (UnauthorizedAccessException)
                {
                    return;
                }

                foreach (string file in files)
                {
                    searchAction(file);
                }

                string[] subdirectories;
                try
                {
                    subdirectories = Directory.GetDirectories(folder);
                }
                catch (UnauthorizedAccessException)
                {
                    return;
                }

                foreach (string subdirectory in subdirectories)
                {
                    FolderTraversal(subdirectory, searchAction);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Сталася помилка: " + ex.Message);
            }
        }

        static void SearchFile(string path, string targetName)
        {
            try
            {
                if (Path.GetFileName(path).Equals(targetName, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(path);
                    ConvertToBinary(path);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Сталася помилка: " + ex.Message);
            }
        }

        static void ConvertToBinary(string filePath)
        {
            try
            {
                string fileContents = File.ReadAllText(filePath);
                string binary = ConvertToBinaryString(fileContents);
                Console.WriteLine("Текст файлу у двійковому форматі:");
                Console.WriteLine(binary);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Сталася помилка при конвертації в двійковий формат: " + ex.Message);
            }
        }

        static string ConvertToBinaryString(string text)
        {
            byte[] bytes = System.Text.Encoding.Default.GetBytes(text);
            string binary = "";
            foreach (byte b in bytes)
            {
                binary += Convert.ToString(b, 2).PadLeft(8, '0') + " ";
            }
            return binary;
        }
    }
}
