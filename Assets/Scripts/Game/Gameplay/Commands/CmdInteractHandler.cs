using System;
using UnityEngine;

public class CmdInteractHandler : ICommandHandler<CmdInteract>
{
    IInteractableEntityProxy _interactableStateProxy;
    public CmdInteractHandler(IInteractableEntityProxy interactableStateProxy)
    {
        _interactableStateProxy = interactableStateProxy;
    }

    public bool Handle(CmdInteract command)
    {
        
    }

}
