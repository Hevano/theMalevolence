using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, ITurnExecutable, ITargetable
{
    //For scrolling health bars, healthbar should be it's own class that dictates how scrolling health works
    [SerializeField]
    private int _health;
    public int Health
    {
        get{
            return _health;
        }
        set{
            var newValue = Mathf.Max(Mathf.Min(value, data.health), 0);
            if(onStatChange != null){
                onStatChange("health", ref _health, ref newValue);
            }
            _health = newValue;
            if(Health == 0){
                Defeated = true;
            }
        }
    }

    [SerializeField]
    private int _corruption;
    public int Corruption
    {
        get{
            return _corruption;
        }
        set{
            if(onStatChange != null){
                onStatChange("corruption", ref _corruption, ref value);
            }
            _corruption = value;
        }
    }
    public bool enemy = false; //ONCE WE MAKE A CHILD CLASS FOR CHARS AND BOSSES, REMOVE THIS 

    private bool _defeated = false;
    public bool Defeated
    {
        get
        {
            return _defeated;
        }
        set
        {
            _defeated = value;
            GameManager.manager.CheckGameOver();
        }
    }

    public CharacterData data;

    [SerializeField]
    private Card _cardToPlay = null;
    public Card CardToPlay
    {
        get
        {
            return _cardToPlay;
        }
        set {
            var newCard = value;
            if (enemy == true)
            {
                newCard = value;
            }
            else if (CardToPlay != null && newCard != null){
                GameManager.manager.PlaceCardInHand(CardToPlay);
            }
            if((onActionChange != null)){
                onActionChange(_cardToPlay, newCard);
            }
            if(!enemy){
                _cardToPlay = newCard;
            }
            if(_cardToPlay != null){
                action = Enums.Action.Card;
            } else {
                action = Enums.Action.Attack;
            }
        }
    }

    public Enums.Action action = Enums.Action.Attack;

    public string CardToPlayName
    {
        get
        {
            //I think the enemy card isnt established, which is why this doesnt work. It's accessing a null object -For kevin
            return _cardToPlay.Name;
        }
    }

    //Character Events
    public delegate void StatChangeHandler(string statName, ref int oldValue, ref int newValue); //we should make some static statName strings to prevent bugs
    public event StatChangeHandler onStatChange;

    public delegate void ActionChangeHandler(Card prev, Card newCard);
    public ActionChangeHandler onActionChange;

    //Modifying corruptionValueForCheck will change the int the random roll is compared to
    public delegate void CorruptionCheckAttemptHandler(ref int corruptionValueForCheck);
    public event CorruptionCheckAttemptHandler onCorruptionCheckAttempt;

    public delegate void CorruptionCheckResultHandler(bool passed);
    public event CorruptionCheckResultHandler onCorruptionCheckResult;
    public delegate void TurnHandler();
    public event TurnHandler onTurnStart;
    public event TurnHandler onTurnEnd;

    public delegate void TargetedHandler(Character source, ref Character target);
    public event TargetedHandler onTargeted;

    public delegate void AttackHandler(Character target, ref Damage d);
    public event AttackHandler onAttack;

    public bool CorruptionCheck(){
        int corruptionValue = Corruption;
        if(onCorruptionCheckAttempt != null){
            onCorruptionCheckAttempt(ref corruptionValue);
        }
        int corruptionCheck = Random.Range(1, 100);
        return corruptionCheck > corruptionValue;
    }

    //Called whenever a character is targetted. Returns the actual target, which will most likely be that character
    public Character Targeted(Character source){
        Character target = this;
        if(onTargeted != null){
            onTargeted(source, ref target);
        }
        return target;
    }

    void Start(){
        Health = data.health;
        Corruption = data.corruption;
        action = Enums.Action.Attack;
    }

    //Pull current characters basic attack (can create new one and save to the data object for specific chars)
    public IEnumerator BasicAttack(Damage damage = null){
        yield return Targetable.GetTargetable(Enums.TargetType.Foes, this, "Select the boss", 1);
        Character target = (Character)Targetable.currentTargets[0];
        damage = damage == null ? data.basicAttack : damage;
        if(onAttack != null){
            onAttack(target, ref damage);
        }
        
        target.Health -= damage.Value;
        CombatUIManager.Instance.SetDamageText(damage.Value, target.transform);
    }

    //Temporary implementation of character's turn
    public IEnumerator GetTurn(){
        if(onTurnStart != null){
            onTurnStart();
        }
        Debug.Log($"{name}'s turn");
        if(Defeated)
        {
            Debug.Log($"{data.name} has been defeated and cannot continue the fight");
        }
        else if(action == Enums.Action.Card && CardToPlay != null)
        {
            Debug.Log($"{name} playing card {CardToPlay.Name}");
            CombatUIManager.Instance.DisplayMessage($"{name} plays {CardToPlay.Name}");
            //Execute the selected card from the dropzone.
            yield return CardToPlay.Activate();
        }
        else if(action == Enums.Action.Attack)
        {
            //Do a damage attack
            if (!enemy) {
               yield return BasicAttack();
            } else {

                Character target;

                do {
                    target = GameManager.manager.party[Random.Range(1, 4)];
                    Debug.Log("Picking target");
                    //temp
                    target = GameManager.manager.party[0];
                } while (target.Defeated == true);
                target = target.Targeted(this); //let the target know it has been targetted, and allow it to reassign the arget if it can
                int dmg = data.basicAttack.Value;
                Debug.Log($"Boss is attacking {target.data.name} for {dmg} HP!");
                StartCoroutine(CombatUIManager.Instance.DisplayMessage($"Boss attacks {target.data.name} for {dmg} HP!"));

                //Damage health
                target.Health -= dmg;
                CombatUIManager.Instance.SetDamageText(dmg, target.transform);
                yield return new WaitForSeconds(0.25f);
                //Increase corruption
                target.Corruption += dmg * 2;
                CombatUIManager.Instance.SetDamageText(dmg * 2, target.transform, new Color32(139, 0, 139, 0));
            }

        }
        if(onTurnEnd != null){
            onTurnEnd();
        }
        yield return new WaitForSeconds(1f);
    }
}
