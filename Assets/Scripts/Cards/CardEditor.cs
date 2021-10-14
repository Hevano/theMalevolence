using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Card)), CanEditMultipleObjects]
public class CardEditor : Editor {
    Card card;
    CardEffectsMaker effectMaker;
    SerializedObject GetTarget;

    #region CARD DATA
    SerializedProperty CardName;
    SerializedProperty CardDescription;
    SerializedProperty CardFlavor;
    SerializedProperty CardCharacter;
    SerializedProperty CardFront;
    SerializedProperty CardBack;
    SerializedProperty CardEffects;
    SerializedProperty CardCorPass;
    SerializedProperty CardCorFail;
    SerializedProperty CardEffectBuffer;
    #endregion

    private void OnEnable () {
        card = (Card)target;
        GetTarget = new SerializedObject(card);

        #region CARD DATA
        CardName = GetTarget.FindProperty("cardName");
        CardDescription = GetTarget.FindProperty("cardDescription");
        CardFlavor = GetTarget.FindProperty("cardFlavor");
        CardCharacter = GetTarget.FindProperty("cardCharacter");
        CardFront = GetTarget.FindProperty("cardFront");
        CardBack = GetTarget.FindProperty("cardBack");
        CardEffects = GetTarget.FindProperty("cardEffects");
        CardCorPass = GetTarget.FindProperty("corruptionPassEffects");
        CardCorFail = GetTarget.FindProperty("corruptionFailEffects");
        CardEffectBuffer = GetTarget.FindProperty("effectBuffer");
        #endregion
    }

    public override void OnInspectorGUI () {
        GetTarget.Update();
        
        using (new EditorGUILayout.VerticalScope("HelpBox")) {
            EditorGUILayout.LabelField("Card Information", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(CardName);
            EditorGUILayout.PropertyField(CardDescription);
            EditorGUILayout.PropertyField(CardFlavor);
            EditorGUILayout.PropertyField(CardCharacter);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Card Art", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(CardFront);
            EditorGUILayout.PropertyField(CardBack);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Card Effects", EditorStyles.boldLabel);
            InsertCardEffectFields(CardEffects, 0);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Corruption Effects", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Pass");
            InsertCardEffectFields(CardCorPass, 1);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Fail");
            InsertCardEffectFields(CardCorFail, 2);
        }

        GetTarget.ApplyModifiedProperties();
    }

    private void InsertCardEffectFields(SerializedProperty effectsList, int listNo) {
        //Go through each effect in the list
        for (int i = 0; i < effectsList.arraySize; i++) {
            Enums.CardEffects targetEffect;
            //Add the effect dropdown select, and remove button
            GUILayout.BeginHorizontal();
            targetEffect = (Enums.CardEffects)EditorGUILayout.EnumPopup("Card Effect", card.GetEffect(i, listNo).Effect);
            card.CheckCardEffect(i, targetEffect, listNo);
            if (GUILayout.Button("Remove", EditorStyles.miniButtonLeft, GUILayout.Width(60f))) {
                effectsList.DeleteArrayElementAtIndex(i);
            }
            GUILayout.EndHorizontal();

            //Use effect maker to load effect
            effectMaker.SetEffect(card.GetEffect(i, listNo));

            #region EFFECT TYPES
            SerializedProperty AfflictEffect = CardEffectBuffer.FindPropertyRelative("afflictEffect");
            SerializedProperty AttackEffect = CardEffectBuffer.FindPropertyRelative("attackEffect");
            SerializedProperty CleanseEffect = CardEffectBuffer.FindPropertyRelative("cleanseEffect");
            SerializedProperty DrawEffect = CardEffectBuffer.FindPropertyRelative("drawEffect");
            SerializedProperty InsertEffect = CardEffectBuffer.FindPropertyRelative("insertEffect");
            SerializedProperty ModifyEffect = CardEffectBuffer.FindPropertyRelative("modifyEffect");
            SerializedProperty ReshuffleEffect = CardEffectBuffer.FindPropertyRelative("reshuffleEffect");
            SerializedProperty SummonEffect = CardEffectBuffer.FindPropertyRelative("summonEffect");
            SerializedProperty VitalityEffect = CardEffectBuffer.FindPropertyRelative("vitalityEffect");
            #endregion
            //Draw input fields based on chosen effect
            switch (targetEffect) {
                case Enums.CardEffects.Afflict:
                    break;
                case Enums.CardEffects.Attack:
                    break;
                case Enums.CardEffects.Cleanse:
                    break;
                case Enums.CardEffects.Draw:
                    EditorGUILayout.PropertyField(DrawEffect.FindPropertyRelative("cardsToDraw"));
                    break;
                case Enums.CardEffects.Insert:
                    break;
                case Enums.CardEffects.Modify:
                    EditorGUILayout.PropertyField(ModifyEffect.FindPropertyRelative("modifierEffect"));
                    EditorGUILayout.PropertyField(ModifyEffect.FindPropertyRelative("modifierFactor"));
                    EditorGUILayout.PropertyField(ModifyEffect.FindPropertyRelative("modifierPerFactor"));
                    EditorGUILayout.PropertyField(ModifyEffect.FindPropertyRelative("perFactorValue"));
                    EditorGUILayout.PropertyField(ModifyEffect.FindPropertyRelative("effectIndex"));
                    break;
                case Enums.CardEffects.Reshuffle:
                    break;
                case Enums.CardEffects.Summon:
                    break;
                case Enums.CardEffects.Vitality:
                    EditorGUILayout.PropertyField(VitalityEffect.FindPropertyRelative("vitalityType"));
                    EditorGUILayout.PropertyField(VitalityEffect.FindPropertyRelative("value"));
                    break;
            }

            EditorGUILayout.Space();
        }

        if (GUILayout.Button("Add Effect")) {
            card.AddCardEffect(listNo);
            GetTarget.ApplyModifiedProperties();
        }

        //if (effectsList.Count > 0) {
        //for (int i = 0; i < effectsList.Count; i++) {
        /**effectMaker = effectsList[i];

        GUILayout.BeginHorizontal();
        effectMaker.effectType = (Enums.CardEffects)EditorGUILayout.EnumPopup("Card Effect", effectMaker.effectType);
        if (GUILayout.Button("Remove", EditorStyles.miniButtonLeft, GUILayout.Width(60f))) {
            effectsList.RemoveAt(i);
        }
        GUILayout.EndHorizontal();*/

        /**switch (effectMaker.effectType) {
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
        }*/
        //}
        //}
    }
}
