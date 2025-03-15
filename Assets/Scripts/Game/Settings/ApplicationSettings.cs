using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ApplicationSettings", menuName = "Game Settings/New Application Settings")]
public class ApplicationSettings : ScriptableObject
{
    public ApplicationInitialStateSettings InitialState;
    public int MusicVolume;
    public int SFXVolumep;
}
