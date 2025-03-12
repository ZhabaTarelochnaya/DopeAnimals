using System;
using System.Collections.Generic;
using UnityEngine;

public class CommandProcessor : ICommandProcessor
{
    readonly Dictionary<Type, object> _handlesMap = new();

    public CommandProcessor() { }

    public void RegisterHandler<TCommand>(ICommandHandler<TCommand> handler) where TCommand : ICommand
    {
        _handlesMap[typeof(TCommand)] = handler;
    }

    public bool Process<TCommand>(TCommand command) where TCommand : ICommand
    {
        if (_handlesMap.TryGetValue(typeof(TCommand), out var handler))
        {
            var typedHandler = (ICommandHandler<TCommand>)handler;
            var result = typedHandler.Handle(command);
            return result;
        }

        return false;
    }
}
