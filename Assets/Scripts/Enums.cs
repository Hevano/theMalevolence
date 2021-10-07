using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Enums {

    public enum CardEffects {
        Attack,
        Cleanse,
        Corrupt,
        Draw,
        Haste,
        Heal,
        Modify,
        Protect,
        Remove,
        ReplaceValue,
        Reshuffle
    }

    /** <summary>A list of possible player characters.</summary> */
    public enum Character {
        Goth,
        Jock,
        Nerd,
        Popular
    };

    /** <summary>A list of factors that can affect card effects.</summary> */
    public enum ModifierFactors {
        Cards_Played,
        Corruption,
        Hand_Size,
        Health
    }

    /** <summary>The types of effects on values in card effects.</summary> */
    public enum Modifier {
        Add,
        Divide,
        Multiply,
        Subtract
    }
}
