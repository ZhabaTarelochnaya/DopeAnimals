using System;
using UnityEngine;

public class CmdInteractHandler : ICommandHandler<CmdInteract>
{
    IInteractableStateProxy _interactableStateProxy;
    public CmdInteractHandler(IInteractableStateProxy interactableStateProxy)
    {
        _interactableStateProxy = interactableStateProxy;
    }

    public bool Handle(CmdInteract command)
    {
        
    }

}
