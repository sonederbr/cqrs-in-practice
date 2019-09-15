using CSharpFunctionalExtensions;
using Logic.AppServices;
using System;

namespace Logic.Decorators
{
    public sealed class DatabaseRetryDecorator<TCommand> : 
        ICommandHandler<TCommand> where TCommand : ICommand
    {
        private readonly ICommandHandler<TCommand> _handler;

        public DatabaseRetryDecorator(ICommandHandler<TCommand> handler)
        {
            _handler = handler;
        }

        public Result Handle(TCommand command)
        {
            for (int i = 0; ; i++)
            {
                try
                {
                    return _handler.Handle(command);
                }
                catch (Exception ex)
                {
                    if(i >= 3 || !IsDatabaseException(ex))
                        throw;
                }
            }
        }

        private bool IsDatabaseException(Exception exception)
        {
            string message = exception.InnerException?.Message;

            if (message == null)
                message = exception.Message;

            if (message == null)
                return false;

            return message.Contains("The connection is broken and recovery is not possible")
                || message.Contains("error occurred while establishing a connection");
        }
    }
}
