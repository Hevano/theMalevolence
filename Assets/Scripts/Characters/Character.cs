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
                onStatChange("health", _health, newValue);
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
                onStatChange("corruption", _corruption, value);
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
            if((onActionChange != null) && (enemy == false)){
                onActionChange(_cardToPlay, newCard);
                _cardToPlay = newCard;
            }
        }
    }

    public string CardToPlayName
    {
        get
        {
            //I think the enemy card isnt established, which is why this doesnt work. It's accessing a null object -For kevin
            return _cardToPlay.Name;
        }
    }

    public delegate void StatChangeHandler(string statName, int oldValue, int newValue);
    public event StatChangeHandler onStatChange;

    public delegate void ActionChangeHandler(Card prev, Card newCard);
    public ActionChangeHandler onActionChange;

    public bool CorruptionCheck(){
        int corruptionCheck = Random.Range(0, 100);
        return corruptionCheck >= Corruption;
    }

    void Start(){
        Health = data.health;
        Corruption = data.corruption;
    }

    //Pull current characters basic attack (can create new one and save to the data object for specific chars)
    public IEnumerator BasicAttack(Damage damage = null){
        yield return Targetable.GetTargetable(Enums.TargetType.Foes, "Select the boss", 1);
        Character target = (Character)Targetable.currentTargets[0];
        int value = damage == null ? data.basicAttack.Value : damage.Value;
        target.Health -= value;
        CombatUIManager.Instance.SetDamageText(data.basicAttack.Value, target.transform);
    }

    //Temporary implementation of character's turn
    public IEnumerator GetTurn(){

        Debug.Log($"{name}'s turn");
        if(Defeated)
        {
            Debug.Log($"{data.name} has been defeated and cannot continue the fight");
        }
        else if(CardToPlay != null)
        {
            Debug.Log($"{name} playing card {CardToPlay.Name}");
            CombatUIManager.Instance.DisplayMessage($"{name} plays {CardToPlay.Name}");
            //Execute the selected card from the dropzone.
            yield return CardToPlay.Activate();
        }
        else
        {
            //Do a damage attack
            if (!enemy) {
               yield return BasicAttack();
            } else {

                Character target;

                do {
                    target = GameManager.manager.party[Random.Range(1, 4)];
                    Debug.Log("Picking target");
                } while (target.Defeated == true);

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
        yield return new WaitForSeconds(1f);
    }
}
