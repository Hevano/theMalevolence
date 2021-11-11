using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** <summary>Special card effect: Summons a friendly unit to the caster's side.</summary> */
[System.Serializable]
public class SummonEffect : CardEffect {
    [SerializeField] private Enums.Character Boss;

    public override IEnumerator ApplyEffect () {
        switch (Boss) {
            case Enums.Character.PuzzleBox:
                PuzzleboxCharacter boss = (PuzzleboxCharacter)GameManager.manager.foes[0];
                yield return boss.SpawnShard();
                break;
        }
    }
}
