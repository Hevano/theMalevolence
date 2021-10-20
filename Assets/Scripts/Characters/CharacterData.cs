using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterData : ScriptableObject
{
    //Allows selection of character based on Enums script.
    public Enums.Character characterType;

    //Base values for a new character asset (which can be modified in the inspector)
    public new string name;
    public int health;
    public int corruption;
    public GameObject avatar;

    [SerializeField] private List<Card> cards = new List<Card>();

    //The basic attack damange attached to a new character. This uses the damage script to create a new type following
    // the format int dieNum, int dieSize, int bonus
    [SerializeField]
    public Damage basicAttack = new Damage(0,0,0);

    public Deck Deck {
        get { return new Deck(cards); }
    }
}
