using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardEffect {
    public virtual string Display () { return "This is a blank effect"; }


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
