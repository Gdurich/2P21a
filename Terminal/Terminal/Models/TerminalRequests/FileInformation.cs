using Terminal.Handlers;
using Terminal.Models.TerminalRequests.Base;

internal class FileInformation : TerminalRequest
{
    public FileInformation()
    {
        CommandName = "fileinfo";
        Description = "{full path to the file}";
    }

    public override void Execute(CommandHandler handler, string commandBody = "")
    {
        string searchText = Console.ReadLine();

        string fullPath = GetFullPath(searchText);
        if (!string.IsNullOrEmpty(fullPath))
        {
            WritePathAndFileInfo(fullPath);
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }

    private string GetFullPath(string userInput)
    {
        string fullPath = Path.GetFullPath(userInput);
        if (File.Exists(fullPath))
        {
            return fullPath;
        }

        return string.Empty;
    }

    private void WritePathAndFileInfo(string path)
    {
        Console.WriteLine(path);

        try
        {
            FileInfo fileInfo = new FileInfo(path);
            Console.WriteLine($"\tFile Name: {fileInfo.Name}");
            Console.WriteLine($"\tFile Size: {fileInfo.Length / (1024 * 1024)} megabytes");
            Console.WriteLine($"\tCreation Time: {fileInfo.CreationTime}");
            Console.WriteLine($"\tLast Write Time: {fileInfo.LastWriteTime}");
        }
        catch (Exception)
        {
            Console.WriteLine("Error reading file information.");
        }
    }
}
