using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardEffectsMaker {
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

    public void SetEffect () {
        
    }
}
