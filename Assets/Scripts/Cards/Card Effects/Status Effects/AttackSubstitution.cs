using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSubstitution : StatusEffect {
    private Character watchedCharacter;
    private Character substitutionCharacter;
    public AttackSubstitution(Character c, Character target){
        watchedCharacter = target;
        substitutionCharacter = c;
        target.onTargeted += Substitute;
    }

    public void Substitute(Character source, ref Character target){
        target.onTargeted -= Substitute;
        if(source.enemy){
            target = substitutionCharacter;
        }
    }

    /*
        New Targetting system:
        - Targetable.getTargetable requires a Character source
        - Character.Target requires a character source, returns a Character that is the target
        - Character.onTarget events can see the source and change the target
    
    */
}