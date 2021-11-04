using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** <summary>Card effect: Restore HP to a target character or alter their Corruption.</summary> */
[System.Serializable]
public class VitalityEffect : CardEffect {

    /** <summary>Which part of the character's vitality to affect</summary> */
    [SerializeField] private Enums.VitalityType vitalityType;
    /** <summary>The value to change the character's vitality</summary> */
    [SerializeField] private int value;

    /** <summary>Applies the effect onto the relevant targets</summary> */
    public override IEnumerator ApplyEffect () {
        ApplyModification();

        foreach (Character c in targets) {
            switch (vitalityType) {
                //Modify the character's health
                case Enums.VitalityType.Health:
                    c.Health += value;
                    //Clamp value to min and max values
                    if (c.Health < 0) c.Health = 0;
                    if (c.Health > c.data.health) c.Health = c.data.health;
                    //Inform the player what just happened
                    //If value is positive, Health was restored, color = green
                    //If value is negative, Health was lost, color = white
                    if (value > 0)
                        yield return CombatUIManager.Instance.DisplayMessage($"{c.name} restored {value} HP");
                    else
                        yield return CombatUIManager.Instance.DisplayMessage($"{c.name} lost {value} HP");
                    break;
                //Modify the character's corruption
                case Enums.VitalityType.Corruption:
                    c.Corruption += value;
                    //Clamp value to min and max values
                    if (c.Corruption < 0) c.Corruption = 0;
                    if (c.Corruption > 100) c.Corruption = 100;
                    //Inform the player what just happened
                    //If value is positive, Corruption was gained, color = purple
                    //If value is negative, Corruption was cleansed, color = purple
                    if (value < 0)
                        yield return CombatUIManager.Instance.DisplayMessage($"{c.name} cleansed {value} Corruption");
                    else
                        yield return CombatUIManager.Instance.DisplayMessage($"{c.name} gained {value} Corruption");
                    break;
            }
        }
        yield return null;
    }

    /** <summary>Takes the modification from the MODIFY effect and increases "value" value</summary> */
    public override void ApplyModification () {
        if (modifyingValue != 0) {
            switch (modification) {
                case Enums.Modifier.Add:
                    value += modifyingValue;
                    break;
                case Enums.Modifier.Subtract:
                    value -= modifyingValue;
                    break;
                case Enums.Modifier.Multiply:
                    value *= modifyingValue;
                    break;
                case Enums.Modifier.Divide:
                    value /= modifyingValue;
                    break;
            }
        }
    }
}