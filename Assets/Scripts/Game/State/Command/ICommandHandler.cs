using System;
using UnityEngine;

public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    bool Handle(TCommand command);
}
