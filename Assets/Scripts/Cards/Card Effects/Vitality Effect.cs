using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** <summary>Card effect: Restore HP to a target character or alter their Corruption.</summary> */
[System.Serializable]
public class VitalityEffect : CardEffect {

    [SerializeField] private Enums.VitalityType vitalityType;
    [SerializeField] private int value;

    public override string Display () { return "This is a vitality effect"; }
}