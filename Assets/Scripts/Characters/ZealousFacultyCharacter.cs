using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZealousFacultyCharacter : EnemyCharacter {
    
    public override IEnumerator GetTurn () {
        if (Action != Enums.Action.Stunned) {
            int card = Random.Range(0, 2);
            CardToPlay = deck.CardList[card];
            yield return CombatUIManager.Instance.RevealCard(CardToPlay); //Should extend this time when not testing
            Debug.Log($"{name} playing card {CardToPlay.Name}");
            animator.SetTrigger("Attack");
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
