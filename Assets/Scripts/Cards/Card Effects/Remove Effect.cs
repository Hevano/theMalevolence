using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** <summary>Special card effect: Removes a card from play.</summary> */
[System.Serializable]
public class RemoveEffect : CardEffect {

    /** 
     * <summary>Whether the card is removed from the game. 
     * If true, the card is destroyed. 
     * If false, it is sent to the discard pile.</summary> 
     */
    [SerializeField] private float exile;
}