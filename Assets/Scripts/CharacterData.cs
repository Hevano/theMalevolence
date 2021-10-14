using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterData : ScriptableObject
{
    public Enums.Character characterType;
    public new string name;
    public int health;
    public int corruption;
    public GameObject avatar;

    [SerializeField]
    public Damage basicAttack = new Damage(0,0,0);
}
