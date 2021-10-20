using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    public Enums.GameplayPhase phase;

    //Speeds the delay between phases, should also be applied to animations
    public float speedScale;

    public Dictionary<Enums.Character, Deck> decks = new Dictionary<Enums.Character, Deck>();

    public HandDisplayController hand;
    public DropZone cardDropZone;

    public List<Character> party = new List<Character>();
    public List<Character> foes = new List<Character>();

    public Dictionary<Enums.Character, Character> characters = new Dictionary<Enums.Character, Character>();

    public List<ITurnExecutable> turns;
    private IEnumerator battleEnumerator;
    
    void Start()
    {
        if (manager != null)
        {
            Destroy(this);
        }
        manager = this;

        StartBattle();

        //Checks if something dropped in the card zone is actually a card, and only continues if it is
        cardDropZone.onDrop += (drag, drop) =>{
            CardDisplayController cardController = null;
            if(drag.gameObject.TryGetComponent(out cardController)){
                drag.Drop(drop);
            }
        };

        hand.GetComponent<DropZone>().onDrop += (drag, drop) => {
            CardDisplayController cardController = null;
            if (drag.gameObject.TryGetComponent(out cardController))
            {
                drag.Drop(drop);
            }
        };
    }

    //Temporary, CheckGameOver should be called whenever a character / foe is Defeated, not every frame
    void Update(){
        CheckGameOver();
    }

    //Starts a new battle with listed enemies
    public void StartBattle()
    {

        //Play battle start effects
        //Draw starting hand
        InitializeCharacters();
        InitializeDecks();

        foreach(Character c in party)
        {
            Draw(c.data.characterType);
        }
        Draw(Enums.Character.Popular);

        battleEnumerator = ExecuteBattle();
        StartCoroutine(battleEnumerator);
    }

    //Loops through turns untill the battleEnumerator is stopped (By CheckGameOver)
    public IEnumerator ExecuteBattle()
    {
        while(true)
        {
            yield return ExecutePlanning();
            yield return ExecuteTurn();
            yield return ExecuteDrawPhase();
        }
        
    }

    public IEnumerator ExecutePlanning()
    {
        phase = Enums.GameplayPhase.Planning;
        Debug.Log("Planning phase");
        while(phase == Enums.GameplayPhase.Planning)
        {
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator ExecuteTurn()
    {
        //UI turn resolving starts
        phase = Enums.GameplayPhase.Resolve;
        Debug.Log("Resolving Phase");
        turns = new List<ITurnExecutable>();

        foreach(TurnOrderSlot turnSlot in TurnOrderSlot.turnOrder)
        {
            turns.Add(turnSlot.Turn);
        }

        turns.Reverse(); //Currently the turn slots are being initialized bottom-up, resulting in the turn order being reversed
        turns.Insert(2, foes[0]); //Temporary, should add enemy turns more dynamically

        foreach(ITurnExecutable turn in turns)
        {
            yield return turn.GetTurn();
        }

        //Reset the actions of each character in the turn order
        foreach(ITurnExecutable turn in turns)
        {
            ((Character)turn).CardToPlay = null;
        }
    }

    public IEnumerator ExecuteDrawPhase()
    {
        phase = Enums.GameplayPhase.Draw;
        Debug.Log("Draw phase");
        //Draw cards
        yield return new WaitForSeconds(1.0f / speedScale);
    }

    //Checks if the game is over. Should be called whenever a character or foe is Defeated
    public void CheckGameOver()
    {
        bool playerDefeated = true;
        foreach(Character partyMember in party)
        {
            playerDefeated = playerDefeated && partyMember.Defeated;
        }

        if(playerDefeated)
        {
            StopCoroutine(battleEnumerator);
            Debug.Log("Game Over! TPK");
            //Return to main menu ui
        }

        bool foesDefeated = true;
        foreach(Character foe in foes)
        {
            foesDefeated = foesDefeated && foe.Defeated;
        }

        if(foesDefeated)
        {
            StopCoroutine(battleEnumerator);
            Debug.Log("Game Over! Defeated enemies");
            //Card Drafting ui
        }
        
    }

    public void EndPlanning(){
        if(phase == Enums.GameplayPhase.Planning)
        {
            phase = Enums.GameplayPhase.Resolve;
        }
    }

    public void Draw(Enums.Character characterDeckToDrawFrom)
    {
        var card = decks[characterDeckToDrawFrom].Draw();
        PlaceCardInHand(card);
    }

    public void PlaceCardInHand(Card c){
        hand.AddCard(CardDisplayController.CreateCard(c));
    }

    public void RemoveCardFromHand(CardDisplayController cd){
        hand.RemoveCard(cd);
    }
    
    public void InitializeDecks()
    {
        Character ch;
        characters.TryGetValue(Enums.Character.Goth, out ch);
        decks[Enums.Character.Goth] = ch.data.Deck;
        characters.TryGetValue(Enums.Character.Jock, out ch);
        decks[Enums.Character.Jock] = ch.data.Deck;
        characters.TryGetValue(Enums.Character.Nerd, out ch);
        decks[Enums.Character.Nerd] = ch.data.Deck;
        characters.TryGetValue(Enums.Character.Popular, out ch);
        decks[Enums.Character.Popular] = ch.data.Deck;
    }

    //Initialize each character in party list established. 
    public void InitializeCharacters()
    {

        //for each character in the party, make that character type in characters dictionary equal to the party member
        foreach(Character c in party){
            characters[c.data.characterType] = c;
        }
    }

}

public interface ITurnExecutable {

    //Returns an ienumerator with the runtime logic of the object's turn that is executed when it's turn is resolved
    IEnumerator GetTurn();
}
