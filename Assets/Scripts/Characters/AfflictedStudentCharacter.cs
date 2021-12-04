using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfflictedStudentCharacter : EnemyCharacter {
    
    public override IEnumerator GetTurn () {
        if (Action != Enums.Action.Stunned) {
            if (deck.DiscardList.Count == deck.CardList.Count) deck.Reshuffle();
            CardToPlay = deck.Draw();
            yield return CombatUIManager.Instance.RevealCard(CardToPlay); 
            Debug.Log($"{name} playing card {CardToPlay.Name}");
            animator.SetTrigger("Serve");
            CombatUIManager.Instance.DisplayMessage($"{name} plays {CardToPlay.Name}");
            yield return CardToPlay.Activate();
        }
    }

    protected override void OnDeath () {
        GameManager.manager.foes.Remove(this);
        Destroy(GetComponent<Targetable>());
        Destroy(gameObject);
    }
}
