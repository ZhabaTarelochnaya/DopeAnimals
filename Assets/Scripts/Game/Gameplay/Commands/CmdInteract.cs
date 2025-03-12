using System;
using UnityEngine;

public class CmdInteract : ICommand
{
    public int Id { get; }
    public CmdInteract(int id)
    {
        Id = id;
    }
}
