namespace App.Application.Configuration.Commands
{
    public class CommandBase : ICommand
    {
 
    }

    public abstract class CommandBase<TResult> : ICommand<TResult>
    {
      
    }
}