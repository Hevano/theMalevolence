using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeckBuilder : MonoBehaviour
{
    private static DeckBuilder _instance;
    public static DeckBuilder Instance {
        get { return _instance; }
    }

    public string nextScene;

    public CardDraftPool cardDraftPool;
    public int cardsToPull;
    public int cardsToKeep;
    public List<Card> pulledCards;
    public List<CardDisplayController> selectedCards;

    [SerializeField]
    private List<CharacterData> _party;
    public Dictionary<Enums.Character, CharacterData> party = new Dictionary<Enums.Character, CharacterData>();

    public HandDisplayController characterDeckDisplay;
    private Enums.Character characterDisplayed;
    public HandDisplayController draftDisplay;
    public TMPro.TextMeshProUGUI draftMessage;

    public bool drafting = false;
    public GameObject draftButton;
    public GameObject exitButton;

    public void Start(){
        if(_instance != null){
            Destroy(this);
        }
        _instance = this;
        foreach(CharacterData c in _party){
            party[c.characterType] = c;
        }
        DisplayDeck(Enums.Character.Goth);
        StartDraft();
    }



    public IEnumerator PullCards(){
        pulledCards = cardDraftPool.GetShuffled().GetRange(0, cardsToPull);
        DisplayPulledCards();
        yield return null;
    }

    public void DisplayDeck(Enums.Character character){
        characterDisplayed = character;
        ClearDeckDisplay();
        foreach(Card c in party[character].Deck.CardList){
            c.Color = party[c.Character].color;
            var display = CardDisplayController.CreateCard(c);
            display.GetComponent<Draggable>().enabled = false;
            characterDeckDisplay.AddCard(display);
        }
    }

    public void DisplayGoth(){
        if(Enums.Character.Goth == characterDisplayed) return;
        DisplayDeck(Enums.Character.Goth);
    }

    public void DisplayJock(){
        if(Enums.Character.Jock == characterDisplayed) return;
        DisplayDeck(Enums.Character.Jock);
    }

    public void DisplayNerd(){
        if(Enums.Character.Nerd == characterDisplayed) return;
        DisplayDeck(Enums.Character.Nerd);
    }

    public void DisplayPopular(){
        if(Enums.Character.Popular == characterDisplayed) return;
        DisplayDeck(Enums.Character.Popular);
    }

    private void ClearDeckDisplay(){
        var displays = new List<CardDisplayController>(characterDeckDisplay.DisplayedCards);
        foreach(CardDisplayController display in displays){
            characterDeckDisplay.RemoveCard(display);
        }
    }

    public void DisplayPulledCards(){
        foreach(Card c in pulledCards){
            c.Color = party[c.Character].color;
            var display = CardDisplayController.CreateCard(c);
            var draggable = display.GetComponent<Draggable>();
            draggable.followMouse = false;
            draggable.planningPhaseOnly = false;
            draggable.returnIfNotDropped = false;
            draggable.ClearHandlers();
            draggable.onDragStart += (a, b) => {
                if(selectedCards.Contains(display)){
                    selectedCards.Remove(display);
                } else if(selectedCards.Count == cardsToKeep){
                    //unhighlight index 0
                    Destroy(selectedCards[0].transform.GetChild(0).gameObject);
                    selectedCards.RemoveAt(0);
                    selectedCards.Add(display);
                    //highlight card
                    var glow = Instantiate(Resources.Load<GameObject>("UserInterface/CardGlow"), display.transform);
                    glow.transform.SetAsFirstSibling();
                } else {
                    selectedCards.Add(display);
                    //highlight card
                    var glow = Instantiate(Resources.Load<GameObject>("UserInterface/CardGlow"), display.transform);
                    glow.transform.SetAsFirstSibling();
                }
            };
            draggable.onDragStop += (a,b) => {
                display.transform.SetParent(draftDisplay.transform);
            };
            draftDisplay.AddCard(display);
        }
    }

    public void StartDraft(){
        draftDisplay.transform.parent.transform.parent.gameObject.SetActive(true);
        StartCoroutine(PullCards());
        draftButton.SetActive(true);
        exitButton.GetComponentInChildren<Text>().text = "Continue";
        exitButton.SetActive(false);
        exitButton.GetComponent<Button>().onClick.RemoveAllListeners();
        exitButton.GetComponent<Button>().onClick.AddListener(Continue);
        draftMessage.gameObject.SetActive(true);
        draftMessage.text = $"Pick {cardsToKeep} cards";
    }

    public void ConfirmDraft(){
        if(selectedCards.Count == cardsToKeep){
            Enums.Character draftedCharacter = Enums.Character.Popular;
            draftButton.SetActive(false);
            exitButton.SetActive(true);
            foreach(CardDisplayController display in selectedCards){
                draftedCharacter = display.CardData.Character;
                party[display.CardData.Character].cards.Add(display.CardData);
            }
            draftDisplay.transform.parent.transform.parent.gameObject.SetActive(false);
            DisplayDeck(draftedCharacter);
            draftMessage.text = $"";
        }
        
    }

    public void Continue(){
        SceneManager.LoadScene(nextScene);
    }
}
