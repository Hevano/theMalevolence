using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardEffect {
    [SerializeField] protected Enums.Target target;
    protected List<Character> targets;

    public Enums.Target Target { get { return target; } }

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

    public virtual IEnumerable ApplyEffect() { yield return null; }
}
