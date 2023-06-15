using Terminal.Helpers;
using Terminal.Models.TerminalRequests.Base;

namespace Terminal.Handlers
{
    public class CommandHandler
    {
        #region Fields/Properties
        public static string CurrentDirectoryPath { get; private set; }
        private static List<TerminalRequest> terminalRequests;
        #endregion
        #region Constructors
        static CommandHandler() 
        {
            terminalRequests = new List<TerminalRequest>();
            CurrentDirectoryPath = @"C:\";
        }
        #endregion
        #region Methods
        //for registration command object
        public CommandHandler AddCommandObject(TerminalRequest terminalRequest)
        {
            terminalRequests.Add(terminalRequest);
            return this;
        }
        //for execute command
        public void ExecuteCommand(string command) 
        {
            try
            {
                string commandName = command.Split(' ').First();
                string commandBody = command.Split(' ').Length > 1 ? command.Substring(command.IndexOf(" ")) : "";
                TerminalRequest? currentTerminalRequest = null;
                foreach (TerminalRequest terminalRequest in terminalRequests)
                {
                    if (terminalRequest.CommandName == commandName)
                    {
                        currentTerminalRequest = terminalRequest;
                        break;
                    }
                }
                if (currentTerminalRequest == null)
                {
                    throw new Exception();
                }
                else
                {
                    currentTerminalRequest.Execute(this, commandBody.Trim());
                }
            }
            catch
            {
                ConsoleHelper.WriteColorLine("Error: Unknown command", ConsoleColor.DarkRed);
            }
        }
        //for set current directory
        public bool SetCurrentDirectory(string directoryPath)
        {
            directoryPath = directoryPath.Trim();
            if (Directory.Exists(directoryPath))
            {
                CurrentDirectoryPath = directoryPath;
                return true;
            }
            else if (Directory.Exists(CurrentDirectoryPath + "\\" + directoryPath))
            {
                CurrentDirectoryPath += "\\" + directoryPath;
                return true;
            }
            else 
                return false;
        }
        public List<TerminalRequest> GetCommandList() 
        {
            return terminalRequests;
        }
        #endregion
    }
}
