using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterEnum{
    Goth,
    Nerd,
    Jock,
    Popular
}
public class Character : MonoBehaviour
{
    public int Health {get; set;}
    public int Corruption{get;set;}
    
    public CharacterData data;
    public bool CorruptionCheck(){
        return false;
    }
}
