using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** <summary>Card effect: Restore HP to a target character or alter their Corruption.</summary> */
[System.Serializable]
public class VitalityEffect : CardEffect {

    [SerializeField] private Enums.VitalityType vitalityType;
    [SerializeField] private int value;
    
    public override IEnumerator ApplyEffect () {
        foreach (Character c in targets) {
            switch (vitalityType) {
                case Enums.VitalityType.Health:
                    c.Health += value;
                    if (c.Health < 0) c.Health = 0;
                    if (c.Health > c.data.health) c.Health = c.data.health;
                    if (value > 0) {
                        CombatUIManager.Instance.SetDamageText(value, c.transform, Color.green);
                        yield return CombatUIManager.Instance.DisplayMessage($"{c.name} restored {value} HP");
                    } else {
                        CombatUIManager.Instance.SetDamageText(value, c.transform);
                        yield return CombatUIManager.Instance.DisplayMessage($"{c.name} lost {value} HP");
                    }
                    break;
                case Enums.VitalityType.Corruption:
                    c.Corruption += value;
                    if (c.Corruption < 0) c.Corruption = 0;
                    if (c.Corruption > 100) c.Corruption = 100;
                    if (value > 0) {
                        CombatUIManager.Instance.SetDamageText(value, c.transform, Color.yellow);
                        yield return CombatUIManager.Instance.DisplayMessage($"{c.name} cleansed {value} Corruption");
                    } else {
                        CombatUIManager.Instance.SetDamageText(value, c.transform, new Color32(139, 0, 139, 0));
                        yield return CombatUIManager.Instance.DisplayMessage($"{c.name} gained {value} Corruption");
                    }
                    break;
            }
        }
        yield return null;
    }
}