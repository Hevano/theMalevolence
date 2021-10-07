using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** <summary>Card effect: The character increases their Corruption value.</summary> */
[System.Serializable]
public class CorruptEffect : CardEffect {

    /** <summary>The amount of Corruption to inflict.</summary> */
    [SerializeField] private int corruptionValue;
}
