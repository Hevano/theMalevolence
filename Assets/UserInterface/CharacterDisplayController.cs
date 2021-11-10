using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Draggable))]
public class CharacterDisplayController : MonoBehaviour, IPointerClickHandler {
    [SerializeReference]
    private Text _hptxt;
    [SerializeReference]
    private Text _cptxt;
    [SerializeReference]
    private Text _nametxt;
    //Temporary UI for alpha
    [SerializeReference]
    private Text _actiontxt;

    public Text HealthDisplay { get { return _hptxt;} set { _hptxt = value; } } 
    public Text CorruptionDisplay { get { return _cptxt;} set { _cptxt = value; } } 
    public Text NameDisplay { get { return _nametxt; } set { _nametxt = value; } } 
    public Text ActionDisplay { get { return _actiontxt; } set { _actiontxt = value; } } 

    public Button drawButton;

    //private Dictionary<string, StatusEffectDisplay> statusEffects;

    [SerializeField]
    private Character _character;
    public Character Character {
        get{
            return _character;
        }
        set{
            _character = value;
            //Set the fields based on the character data
            //Subscribe to character events to continually update
            _character.onStatChange += (string statName, ref int oldValue, ref int newValue) => {
                //Debug.Log("Stat change");
                if(statName == "health"){
                    ChangeHealth(newValue);
                } else if(statName == "corruption"){
                    ChangeCorruption(newValue);
                }
            };

            ChangeHealth(Character.Health);
            ChangeCorruption(Character.Corruption);

            _character.onActionChange += ChangeAction;
        }
    }

    public void ToggleDrawButton(bool enabled){
        drawButton.gameObject.SetActive(enabled);
    }

    public void ChangeHealth(int currentHealth) {
        HealthDisplay.text = currentHealth + "/" + Character.data.health;
    }
    public void ChangeCorruption(int currentCorruption) {
        CorruptionDisplay.text = currentCorruption.ToString();
    }

    public void ChangeAction(Enums.Action oldAction, Enums.Action newAction) {
        if(oldAction == newAction) return;
        switch(newAction){
            case Enums.Action.Attack:
                ActionDisplay.text = "Attacking";
                break;
            case Enums.Action.Card:
                ActionDisplay.text =  $"Playing Card: {_character.CardToPlay.Name}";
                break;
            case Enums.Action.Stunned:
                ActionDisplay.text = "Stunned";
                break;
                
        }
        
    }

    public TurnOrderSlot currentTurnSlot;
    public void Start(){
        Character = _character; //Cludgey workaround for initializing character events when character is set via the inspector
        var d = GetComponent<Draggable>();
        d.followMouse = false;
        d.onDrag += (drag, drop) =>{
            Vector3 pos = d.transform.position;
            pos.y = Input.mousePosition.y;
            transform.position = pos;
        };
        d.onDragStart += (drag, drop) =>{
            ToggleRayCastOnOthers(false);
        };

        d.onDragStop += (drag, drop) =>{
            ToggleRayCastOnOthers(true);
        };
        drawButton.onClick.AddListener(() => {
            GameManager.manager.Draw(Character.data.characterType);
        });
    }

    //Toggles the graphic raycast component on all other (Slightly jank, a better method probably exists)
    private void ToggleRayCastOnOthers(bool enabled){
        var gr = GetComponent<GraphicRaycaster>();
        bool myToggleState = gr.enabled;
        foreach(TurnOrderSlot slot in GameManager.manager.turnSlots){
            slot.currentTurnDraggable.GetComponent<GraphicRaycaster>().enabled = enabled;
        }
        gr.enabled = myToggleState;
    }

    //Reset the character back to attacking if their display is right clicked
    public void OnPointerClick(PointerEventData d){
        if(d.button == PointerEventData.InputButton.Right && Character.CardToPlay != null){
            GameManager.manager.PlaceCardInHand(Character.CardToPlay);
            Character.CardToPlay = null;
        }
    }
}
