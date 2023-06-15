using Terminal.Handlers;

namespace Terminal.Models.TerminalRequests.Base
{
    public class TerminalRequest
    {
        #region Properties
        public string CommandName { get; protected set; }
        public string Description { get; protected set; }
        #endregion
        #region Methods
        public virtual void Execute(CommandHandler handler, string commandBody = "") 
        {
            Console.WriteLine($"Executed: {CommandName} {commandBody}");
        }
        #endregion
    }
}
