using System;
using System.Collections.Generic;

public class CommandProcessor : ICommandProcessor
{
    readonly Dictionary<Type, object> _handlesMap = new();

    public void RegisterHandler<TCommand>(ICommandHandler<TCommand> handler) where TCommand : ICommand
    {
        _handlesMap[typeof(TCommand)] = handler;
    }

    public bool Process<TCommand>(TCommand command) where TCommand : ICommand
    {
        if (_handlesMap.TryGetValue(typeof(TCommand), out object handler))
        {
            var typedHandler = (ICommandHandler<TCommand>)handler;
            bool result = typedHandler.Handle(command);
            return result;
        }

        return false;
    }
}