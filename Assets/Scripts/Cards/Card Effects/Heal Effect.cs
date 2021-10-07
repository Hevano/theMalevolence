using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** <summary>Card effect: Restores HP to a character.</summary> */
[System.Serializable]
public class HealEffect : CardEffect {

    /** <summary>The amount of HP to restore.</summary> */
    [SerializeField] private float healValue;
}
