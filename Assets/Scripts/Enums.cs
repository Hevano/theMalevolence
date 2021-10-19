using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Enums {

    public enum CardEffects {
        None,
        Afflict,
        Attack,
        Cleanse,
        Draw,
        Insert,
        Modify,
        Reshuffle,
        Summon,
        Vitality
    }

    /** <summary>A list of possible player characters.</summary> */
    public enum Character {
        Goth,
        Jock,
        Nerd,
        Popular
    };

    /** <summary>The types of effects on values in card effects.</summary> */
    public enum Modifier {
        Add,
        Divide,
        Multiply,
        Subtract
    }

    /** <summary>A list of factors that can affect card effects.</summary> */
    public enum ModifierFactors {
        Cards_Played,
        Corruption,
        Hand_Size,
        Health
    }

    /** <summary>Targeting options for card effects</summary> */
    public enum Target {
        None,
        Self,
        Ally,
        Enemy,
        All_Ally,
        All_Enemy
    }

    /** <summary>The types of effects on values in card effects.</summary> */
    public enum VitalityType {
        Health,
        Corruption
    }

    public enum GameplayPhase {
        Planning,
        Resolve,
        Draw
    }

    
    public enum TargetType{
        Any,
        Allies,
        Foes
    }
}
