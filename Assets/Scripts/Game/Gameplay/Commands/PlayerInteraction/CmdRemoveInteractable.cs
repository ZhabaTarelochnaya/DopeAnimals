using System;
using UnityEngine;

public class CmdRemoveInteractable : ICommand
{
    public int Id { get; }
    public CmdRemoveInteractable(int id)
    {
        Id = id;
    }
}
