using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardEffect {
    [SerializeField] protected Enums.Target target;
    protected List<Character> targets;
    protected Card card;

    protected int modifyingValue;
    protected Enums.Modifier modification;

    public Enums.Target Target { get { return target; } }

    public void SetOwnerCard (Card c) {
        card = c;
    }

    //Depending on the card, find the target for the card
    public IEnumerator DesignateTarget() {
        targets = new List<Character>();

        Debug.Log("From Card Effect.cs, find target");

        switch (target) {
            case Enums.Target.Self:
                Character c;
                GameManager.manager.characters.TryGetValue(card.Character, out c);
                targets.Add(c);
                break;
            case Enums.Target.Ally:
                if (card.AllyTarget == null) {
                    yield return Targetable.GetTargetable(Enums.TargetType.Allies, "Select Ally", 1);
                    card.AllyTarget = (Character) Targetable.currentTargets[0];
                }
                targets.Add(card.AllyTarget);
                break;
            case Enums.Target.Enemy:
                if (card.EnemyTarget == null) {
                    yield return Targetable.GetTargetable(Enums.TargetType.Foes, "Select Enemy", 1);
                    card.EnemyTarget = (Character)Targetable.currentTargets[0];
                }
                targets.Add(card.EnemyTarget);
                break;
            case Enums.Target.All_Ally:
                targets = new List<Character>(GameManager.manager.party);
                break;
            case Enums.Target.All_Enemy:
                targets = new List<Character>(GameManager.manager.foes);
                break;
        }
    }

    public virtual IEnumerator ApplyEffect() {
        //Tell game manager to skip the caster's turn
        yield return null;
    }

    public virtual void SetModification (int value, Enums.Modifier mod) {
        modifyingValue = value;
        modification = mod;
    }

    public virtual void ApplyModification () {
        return;
    }


    /*
        CardEffect Resolution and Targeting
    */
    // public IEnumerator Activate(){
    //     //Effect resolution goes here
    // }

    // public IEnumerator AccquireTarget(){
    //     yield return Targetable.GetTargetable(Enums.TargetType.Any, "Targetting ui msg", 1);
    //     this.targets = Targetable.currentTargets;
    // }
}
