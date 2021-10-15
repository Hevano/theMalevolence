using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** <summary>Special card effect: Allows the character to draw an additional card.</summary> */
[System.Serializable]
public class DrawEffect : CardEffect {
    
    /** <summary>The number of cards to draw.</summary> */
    [SerializeField] private int cardsToDraw;

    public int CardsToDraw { get { return cardsToDraw; } }
    public override string Display () { return "This is a draw effect"; }
}
