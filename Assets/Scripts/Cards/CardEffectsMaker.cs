using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardEffectsMaker : MonoBehaviour {
    public Enums.CardEffects effectType;

    public CardEffect effect;

    public CardEffect cardEffect = new CardEffect();
    public AfflictEffect afflictEffect = new AfflictEffect();
    public AttackEffect attackEffect = new AttackEffect();
    public CleanseEffect cleanseEffect = new CleanseEffect();
    public DrawEffect drawEffect = new DrawEffect();
    public InsertEffect insertEffect = new InsertEffect();
    public ModifierEffect modifyEffect = new ModifierEffect();
    public ReshuffleEffect reshuffleEffect = new ReshuffleEffect();
    public SummonEffect summonEffect = new SummonEffect();
    public VitalityEffect vitalityEffect = new VitalityEffect();
    
    public void SetEffect (CardEffect effect) {
        switch (effect.Effect) {
            case Enums.CardEffects.Afflict:
                afflictEffect = (AfflictEffect)effect;
                break;
            case Enums.CardEffects.Attack:
                attackEffect = (AttackEffect)effect;
                break;
            case Enums.CardEffects.Cleanse:
                cleanseEffect = (CleanseEffect)effect;
                break;
            case Enums.CardEffects.Draw:
                drawEffect = (DrawEffect)effect;
                break;
            case Enums.CardEffects.Insert:
                insertEffect = (InsertEffect)effect;
                break;
            case Enums.CardEffects.Modify:
                modifyEffect = (ModifierEffect)effect;
                break;
            case Enums.CardEffects.Reshuffle:
                reshuffleEffect = (ReshuffleEffect)effect;
                break;
            case Enums.CardEffects.Summon:
                summonEffect = (SummonEffect)effect;
                break;
            case Enums.CardEffects.Vitality:
                vitalityEffect = (VitalityEffect)effect;
                break;
        }
    }
}
