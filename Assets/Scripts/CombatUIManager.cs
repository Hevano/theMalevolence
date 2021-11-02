using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatUIManager : MonoBehaviour {

    private static CombatUIManager instance;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI displayText;

    private DamageText PopupDamageText;

    public static CombatUIManager Instance { get { return instance; } }

    void Awake () {
        if (instance == null)
            instance = this;
        else if (this != instance)
            Destroy(this);
    }

    // Start is called before the first frame update
    void Start() {
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
}
