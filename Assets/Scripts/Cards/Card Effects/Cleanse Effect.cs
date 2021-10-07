using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** <summary>Card effect: Reduces a character's Corruption level.</summary> */
[System.Serializable]
public class CleanseEffect : CardEffect {

    /** <summary>The amount of Corruption to remove.</summary> */
    [SerializeField] private float cleanseValue;
}
