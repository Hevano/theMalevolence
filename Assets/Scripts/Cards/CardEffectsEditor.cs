using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CardEffectsMaker)), CanEditMultipleObjects]
public class CardEffectsEditor : Editor {
    CardEffectsMaker effectMaker;
    SerializedObject GetTarget;

    #region EFFECT TYPES
    SerializedProperty AttackEffect;
    SerializedProperty CleanseEffect;
    SerializedProperty CorruptEffect;
    SerializedProperty DrawEffect;
    SerializedProperty HasteEffect;
    SerializedProperty HealEffect;
    SerializedProperty ModifyEffect;
    SerializedProperty ProtectEffect;
    SerializedProperty RemoveEffect;
    SerializedProperty ReplaceValueEffect;
    SerializedProperty ReshuffleEffect;
    SerializedProperty CardEffect;
    #endregion

    private void OnEnable () {
        effectMaker = (CardEffectsMaker)target;
        GetTarget = new SerializedObject(effectMaker);

        #region EFFECT TYPES
        AttackEffect = GetTarget.FindProperty("attackEffect");
        CleanseEffect = GetTarget.FindProperty("cleanseEffect");
        CorruptEffect = GetTarget.FindProperty("corruptEffect");
        DrawEffect = GetTarget.FindProperty("drawEffect");
        HasteEffect = GetTarget.FindProperty("hasteEffect");
        HealEffect = GetTarget.FindProperty("healEffect");
        ModifyEffect = GetTarget.FindProperty("modifyEffect");
        ProtectEffect = GetTarget.FindProperty("protectEffect");
        RemoveEffect = GetTarget.FindProperty("removeEffect");
        ReplaceValueEffect = GetTarget.FindProperty("replaceValueEffect");
        ReshuffleEffect = GetTarget.FindProperty("reshuffleEffect");
        ReshuffleEffect = GetTarget.FindProperty("cardEffect");
        #endregion
    }

    public override void OnInspectorGUI () {
        GetTarget.Update();
        
        using (new EditorGUILayout.VerticalScope("HelpBox")) {
            effectMaker.effectType = (Enums.CardEffects)EditorGUILayout.EnumPopup("Card Effect", effectMaker.effectType);

            if (GUILayout.Button("Set Effect"))
                effectMaker.SetEffect();

            EditorGUILayout.Space();

            switch (effectMaker.effectType) {
                case Enums.CardEffects.Attack:
                    EditorGUILayout.PropertyField(AttackEffect);
                    break;
                case Enums.CardEffects.Cleanse:
                    EditorGUILayout.PropertyField(CleanseEffect);
                    break;
                case Enums.CardEffects.Corrupt:
                    EditorGUILayout.PropertyField(CorruptEffect);
                    break;
                case Enums.CardEffects.Draw:
                    EditorGUILayout.PropertyField(DrawEffect);
                    break;
                case Enums.CardEffects.Haste:
                    EditorGUILayout.PropertyField(HealEffect);
                    break;
                case Enums.CardEffects.Heal:
                    EditorGUILayout.PropertyField(HealEffect);
                    break;
                case Enums.CardEffects.Modify:
                    EditorGUILayout.PropertyField(ModifyEffect);
                    break;
                case Enums.CardEffects.Protect:
                    EditorGUILayout.PropertyField(ProtectEffect);
                    break;
                case Enums.CardEffects.Remove:
                    EditorGUILayout.PropertyField(RemoveEffect);
                    break;
                case Enums.CardEffects.ReplaceValue:
                    EditorGUILayout.PropertyField(ReplaceValueEffect);
                    break;
                case Enums.CardEffects.Reshuffle:
                    EditorGUILayout.PropertyField(ReshuffleEffect);
                    break;
                default:
                    EditorGUILayout.PropertyField(CardEffect);
                    break;
            }
        }
    }
}
