using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Character))]
public class Targetable : MonoBehaviour, IPointerClickHandler
{
    private static bool targetting = false;
    private static Enums.TargetType targetType;
    public static List<ITargetable> currentTargets = new List<ITargetable>();

    [Tooltip("List of target types that apply to this gameobject")]
    public List<Enums.TargetType> targetTypes;
    public ITargetable target; //For now, we can assume characters are the only targetable entities

    public void Start(){
        target = GetComponent<Character>();
    }

    public void OnPointerClick(PointerEventData data){ //Camera needs to have the PhysicsRaycast Component
        Debug.Log("Clicked");

        if(targetting && targetTypes.Contains(targetType))
        {
            currentTargets.Add(target);//May want to change so that a target already in the list cannot be added a second time
            Debug.Log($"{name} has been targeted");
            ((Character)target).Targeted();        
        }
    }

    
    public static IEnumerator GetTargetable(Enums.TargetType type, string msg, int count = 1){

        //send msg to some Text object in the screen to inform the player what they are targetting
        Canvas messagePrompt = Instantiate(GameManager.manager.messager);
        messagePrompt.GetComponentInChildren<Text>().text = msg;
        messagePrompt.transform.position = new Vector3(0, 0, 0);

        //Disables raycast of dropzone to prevent from being targetted.
        GameManager.manager.cardDropZone.GetComponent<UnityEngine.UI.Image>().raycastTarget = false;

        currentTargets = new List<ITargetable>();
        targetting = true;
        targetType = type;

        //loop while target is being found based on 'targetting'. The onpointerclick function is utilized while this keeps the function from ending \
        // Checks each frame if the number of targets is returned.
        while (currentTargets.Count < count) {
            yield return new WaitForEndOfFrame();
        }


        GameManager.manager.cardDropZone.GetComponent<UnityEngine.UI.Image>().raycastTarget = true;
        targetting = false;

        foreach(GameObject e in GameObject.FindGameObjectsWithTag("Message"))
        { 
            Destroy(e);
        }
    }
}

