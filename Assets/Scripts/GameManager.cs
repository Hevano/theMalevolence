using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    public Enums.GameplayPhase phase;

    //Speeds the delay between phases, should also be applied to animations
    public float speedScale;

    public Dictionary<Enums.Character, Deck> decks;
    

    public List<Character> party;
    public List<Character> foes;

    public List<ITurnExecutable> turns;
    private IEnumerator battleEnumerator;
    
    void Start()
    {
        if(manager != null){
            Destroy(this);
        }
        manager = this;
        StartBattle();
    }

    //Temporary, CheckGameOver should be called whenever a character / foe is defeated, not every frame
    void Update(){
        CheckGameOver();
    }

    //Starts a new battle with listed enemies
    public void StartBattle(){
        //Play battle start effects
        //Draw starting hand
        battleEnumerator = ExecuteBattle();
        StartCoroutine(battleEnumerator);
    }

    //Loops through turns untill the battleEnumerator is stopped (By CheckGameOver)
    public IEnumerator ExecuteBattle(){
        while(true){
            yield return ExecutePlanning();
            yield return ExecuteTurn();
            yield return ExecuteDrawPhase();
        }
        
    }

    public IEnumerator ExecutePlanning(){
        phase = Enums.GameplayPhase.Planning;
        Debug.Log("Planning phase");
        while(phase == Enums.GameplayPhase.Planning){
            yield return new WaitForEndOfFrame();
        }
        //Temporary until UI is added to arrange turns and actions
        var turnsList = new List<ITurnExecutable>(party);
        turnsList.AddRange(foes);
        turns = turnsList;
    }

    public IEnumerator ExecuteTurn(){
        //UI turn resolving starts
        phase = Enums.GameplayPhase.Resolve;
        Debug.Log("Resolving Phase");
        foreach(ITurnExecutable turn in turns){
            yield return turn.GetTurn();
        }
        //Check if battle has been resolved

        //UI turn resolving true
    }

    public IEnumerator ExecuteDrawPhase(){
        phase = Enums.GameplayPhase.Draw;
        Debug.Log("Draw phase");
        //Draw cards
        yield return new WaitForSeconds(1.0f / speedScale);
    }

    //Checks if the game is over. Should be called whenever a character or foe is defeated
    public void CheckGameOver(){
        bool playerDefeated = true;
        foreach(Character partyMember in party){
            playerDefeated = playerDefeated && partyMember.defeated;
        }
        if(playerDefeated){
            StopCoroutine(battleEnumerator);
            Debug.Log("Game Over! TPK");
            //Return to main menu ui
        }

        bool foesDefeated = true;
        foreach(Character foe in foes){
            foesDefeated = foesDefeated && foe.defeated;
        }
        if(foesDefeated){
            StopCoroutine(battleEnumerator);
            Debug.Log("Game Over! Defeated enemies");
            //Card Drafting ui
        }
        
    }

    public void EndPlanning(){
        if(phase == Enums.GameplayPhase.Planning){
            phase = Enums.GameplayPhase.Resolve;
        }
    }

}

public interface ITurnExecutable {

    //Returns an ienumerator with the runtime logic of the object's turn that is executed when it's turn is resolved
    IEnumerator GetTurn();
}
