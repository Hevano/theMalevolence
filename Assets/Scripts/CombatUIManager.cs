using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatUIManager : MonoBehaviour {

    private static CombatUIManager instance;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI displayText;

    [SerializeField] private GameObject CardRevealDisplay;

    private DamageText PopupDamageText;

    public static CombatUIManager Instance { get { return instance; } }

    void Awake () {
        if (instance == null)
            instance = this;
        else if (this != instance)
            Destroy(this);
        PopupDamageText = Resources.Load<DamageText>("Prefabs/DamagePopUp");
    }


    public void SetDamageText (int value, Transform location) {
        DamageText instance = Instantiate(PopupDamageText);
        Vector2 screenPos = Camera.main.WorldToScreenPoint(location.position);
        instance.transform.SetParent(this.transform, false);
        instance.transform.position = screenPos;
        instance.SetText(Mathf.Abs(value).ToString());
    }
    
    public void SetDamageText (int value, Transform location, Color32 color) {
        DamageText instance = Instantiate(PopupDamageText);
        Vector2 screenPos = Camera.main.WorldToScreenPoint(location.position);
        instance.transform.SetParent(this.transform, false);
        instance.transform.position = screenPos;
        instance.SetText(Mathf.Abs(value).ToString());
        instance.SetColor(color);
    }

    public IEnumerator DisplayMessage (string msg) {
        displayText.text = msg;
        yield return new WaitForSeconds(1f);
        displayText.text = "";
    }

    public IEnumerator DisplayMessage (string msg, float duration) {
        displayText.text = msg;
        yield return new WaitForSeconds(duration);
        displayText.text = "";
    }

    public IEnumerator RevealCard(Card card, float duration = 1f){
        CardDisplayController display = CardDisplayController.CreateCard(card);
        display.GetComponent<Draggable>().followMouse = false;

        RectTransform cardRectTransform = display.GetComponent<RectTransform>();
        RectTransform DisplayArea = CardRevealDisplay.GetComponent<RectTransform>();
        cardRectTransform.SetParent(DisplayArea.transform);

        //cardRectTransform.offsetMin = new Vector2(0, 120);
        //cardRectTransform.sizeDelta = new Vector2(60, 90);
        yield return new WaitForSeconds(duration);
        displayText.text = "Press any key to continue";
        while(!Input.anyKey){
            yield return new WaitForEndOfFrame();
        }
        displayText.text = "";
        Destroy(display.gameObject);
    }

    //The list given will given will be modified to contain only the selected card
    public IEnumerator DisplayChoice(List<Card> choices){
        List<CardDisplayController> displays = new List<CardDisplayController>();
        int selectedIndex = -1;
        foreach(Card card in choices){
            CardDisplayController display = CardDisplayController.CreateCard(card);
            display.GetComponent<Draggable>().followMouse = false;
            display.GetComponent<Draggable>().planningPhaseOnly = false;

            display.transform.SetParent(displayText.transform);

            RectTransform cardRectTransform = display.GetComponent<RectTransform>();
            RectTransform DisplayArea = CardRevealDisplay.GetComponent<RectTransform>();
            cardRectTransform.SetParent(DisplayArea.transform);

            //cardRectTransform.offsetMin = new Vector2((choices.IndexOf(card)) * 250 - ((choices.Count - 1) * 125), 120);
            //cardRectTransform.sizeDelta = new Vector2(60, 90);

            displays.Add(display);
            display.GetComponent<Draggable>().onDragStart += (drag, drop) => {
                selectedIndex = displays.IndexOf(display);
            };
        }
        while(selectedIndex == -1){
            yield return new WaitForEndOfFrame();
        }
        var choice = choices[selectedIndex];
        choices.Clear();
        choices.Add(choice);
        foreach(CardDisplayController display in displays){
            Destroy(display.gameObject);
        }
        
    }
}
