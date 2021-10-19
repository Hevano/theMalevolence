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

    public void DesignateTarget() {
        switch(target) {
            case Enums.Target.Self:
                //Ask game manager for card owner's character
                break;
            case Enums.Target.Ally:
                //Ask game manager for player to choose a target
                break;
            case Enums.Target.Enemy:
                //Ask game manager for player to choose a target
                break;
            case Enums.Target.All_Ally:
                //Ask game manager for all allies
                break;
            case Enums.Target.All_Enemy:
                //Ask game manager for all enemies
                break;
        }
    }

    public virtual IEnumerable ApplyEffect() {
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
