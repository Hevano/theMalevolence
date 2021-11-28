using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

/*
Display text behaviour
- Instantiate


*/

public class DisplayText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI primaryMessage;
    public TextMeshProUGUI secondaryMessage;
    public bool forceVisible = false;
    private IEnumerator fadeEnumerator;
    public void OnPointerEnter(PointerEventData d){
        SetVisible();
    }

    public void OnPointerExit(PointerEventData d){
        if(!forceVisible){
            StartFade();
        }
    }

    public void SetMessage(string msg){
        secondaryMessage.text = primaryMessage.text;
        primaryMessage.text = msg;
        SetVisible();
    }

    public void StartFade(){
        forceVisible = false;
        fadeEnumerator = Fade();
        StartCoroutine(fadeEnumerator);
    }

    private IEnumerator Fade(){
        Color primaryColor = primaryMessage.color;
        Color secondaryColor = primaryMessage.color;
        while(primaryColor.a != 0 && secondaryColor.a != 0){
            primaryColor.a = Mathf.Lerp(0, primaryColor.a, 0.01f);
            primaryMessage.color = primaryColor;
            secondaryColor.a = Mathf.Lerp(0, secondaryColor.a, 0.01f);
            secondaryMessage.color = secondaryColor;
            yield return new WaitForEndOfFrame();
        }
        fadeEnumerator = null;
    }

    public void SetVisible(){
        if(fadeEnumerator != null) StopCoroutine(fadeEnumerator);
        Color primaryColor = primaryMessage.color;
        Color secondaryColor = primaryMessage.color;
        primaryColor.a = 1;
        secondaryColor.a = 1;
        primaryMessage.color = primaryColor;
        secondaryMessage.color = secondaryColor;
    }
}
