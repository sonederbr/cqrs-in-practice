using CSharpFunctionalExtensions;
using Logic.AppServices;
using Newtonsoft.Json;
using System;

namespace Logic.Decorators
{
    public sealed class AuditLoggingDecorator<TCommand> :
        ICommandHandler<TCommand> where TCommand : ICommand
    {
        private readonly ICommandHandler<TCommand> _handler;

        public AuditLoggingDecorator(ICommandHandler<TCommand> handler)
        {
            _handler = handler;
        }

        public Result Handle(TCommand command)
        {
            var commandSerialized = JsonConvert.SerializeObject(command);
            Console.WriteLine(commandSerialized);
            return _handler.Handle(command);
        }
    }
}
