using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** <summary>Card effect: Restore HP to a target character or alter their Corruption.</summary> */
[System.Serializable]
public class VitalityEffect : CardEffect {

    [SerializeField] private Enums.VitalityType vitalityType;
    [SerializeField] private int value;
    
    public virtual IEnumerable ApplyEffect () {
        foreach (Character c in targets) {
            switch (vitalityType) {
                case Enums.VitalityType.Health:
                    c.Health += value;
                    if (c.Health < 0) c.Health = 0;
                    if (c.Health > c.data.health) c.Health = c.data.health;
                    break;
                case Enums.VitalityType.Corruption:
                    c.Corruption += value;
                    if (c.Corruption < 0) c.Corruption = 0;
                    if (c.Corruption > 100) c.Corruption = 100;
                    break;
            }
        }
        yield return null;
    }
}