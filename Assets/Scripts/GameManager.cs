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
    public List<TurnOrderSlot> turnSlots;
    public Canvas messager;

    public List<Character> party = new List<Character>();
    public List<Character> foes = new List<Character>();

    public Dictionary<Enums.Character, Character> characters = new Dictionary<Enums.Character, Character>();

    public List<ITurnExecutable> turns;

    private int turnNumber = 0;
    private IEnumerator battleEnumerator;
    private bool gameOver = false;
    
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

    //Starts a new battle with listed enemies. This initializes the characers, decks, and starts a new coroutine: battleEnumerator (like a thread)
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

        //look to remove this later
        foes[0].enemy = true;

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

    // Planning phase
    public IEnumerator ExecutePlanning()
    {
        phase = Enums.GameplayPhase.Planning;
        Debug.Log("Planning phase");
        while(phase == Enums.GameplayPhase.Planning)
        {
            yield return new WaitForEndOfFrame();
        }
    }

    // Turn/action phase
    public IEnumerator ExecuteTurn()
    {
        turnNumber++;

        //UI turn resolving starts
        phase = Enums.GameplayPhase.Resolve;
        Debug.Log("Resolving Phase");
        turns = new List<ITurnExecutable>();

        foreach(TurnOrderSlot turnSlot in turnSlots)
        {
            turns.Add(turnSlot.Turn);
        }
        turns.Reverse();
        //TEMP WAY TO DO BOSS ACTIONS, REMOVE LATER
        switch (turnNumber % 2)
        {//Boss cards not saving to decks. Wonder why -Kevin
            case 0:
                foes[0].CardToPlay = (decks[Enums.Character.Driver].CardList[0]);
                Debug.Log($"boss is playing {foes[0].CardToPlay}");
                break;
            case 1:
                foes[0].CardToPlay = (decks[Enums.Character.Driver].CardList[1]);
                Debug.Log($"boss is playing {foes[0].CardToPlay}");
                break;

        }

        turns.Reverse(); //Currently the turn slots are being initialized bottom-up, resulting in the turn order being reversed
        turns.Insert(2, foes[0]); //Temporary, should add enemy turns more dynamically

        foreach(ITurnExecutable turn in turns)
        {
            yield return turn.GetTurn();
            if(gameOver){
                yield break;
            }
        }

        //Reset the actions of each character in the turn order
        foreach(ITurnExecutable turn in turns)
        {
            ((Character)turn).CardToPlay = null;
        }
    }

    //Executes while turn is in draw phase
    public IEnumerator ExecuteDrawPhase(){
        phase = Enums.GameplayPhase.Draw;
        Debug.Log("Draw phase");
        bool cardsToDraw = false;
        
        int cardsInHand = hand.DisplayedCards.Count;
        //Enable draw buttons (could be better optimized)
        foreach(TurnOrderSlot turnSlot in turnSlots)
        {
            var display = turnSlot.currentTurnDraggable.GetComponent<CharacterDisplayController>();
            if(party.Contains(display.Character) && decks[display.Character.data.characterType].CardList.Count > 0){
                display.ToggleDrawButton(true);
                cardsToDraw = true;
            }
        }
        //Only wait to draw if at least one deck has cards in it
        if(cardsToDraw){
            //Wait untill one card has been drawn
            while(hand.DisplayedCards.Count != cardsInHand + 1){
                yield return new WaitForEndOfFrame();
            }
            //Disable draw buttons
            foreach(TurnOrderSlot turnSlot in turnSlots)
            {
                var display = turnSlot.currentTurnDraggable.GetComponent<CharacterDisplayController>();
                if(party.Contains(display.Character)){
                    display.ToggleDrawButton(false);
                }
            }
        }
        
    }

    //Checks if the game is over. Should be called whenever a character or foe is Defeated
    public void CheckGameOver()
    {
        bool playerDefeated = true;
        //THIS DOESNT WORK. TPK GAMEOVERS DONT SUCCEED.
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
        gameOver = foesDefeated || playerDefeated;
    }

    //UI function, is called when the player presses the end planning button
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
    
    //Links data from the inspector's characters and enum's class.
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
        characters.TryGetValue(Enums.Character.Driver, out ch);
        decks[Enums.Character.Driver] = ch.data.Deck;
    }

    //Initialize each character in party list established. 
    public void InitializeCharacters()
    {

        //for each character in the party, make that character type in characters dictionary equal to the party member
        foreach (Character c in party)
        {
            characters[c.data.characterType] = c;
        }

        foreach (Character e in foes)
        {
            characters[e.data.characterType] = e;
        }

    }

}

//Interface inherited by anything that can take a turn
public interface ITurnExecutable {

    //Returns an ienumerator with the runtime logic of the object's turn that is executed when it's turn is resolved
    IEnumerator GetTurn();
}
