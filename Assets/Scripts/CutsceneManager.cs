using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CutsceneManager : MonoBehaviour
{

    [SerializeField]
    private string cutsceneName;
    [SerializeField]
    private TextMeshProUGUI dialogue;
    [SerializeField]
    private Button skipScene, next;
    [SerializeField]
    private Image fadeImage, cutsceneImage, drawingImage, notebook;
    [SerializeField]
    private List<Sprite> cutScene1Images, cutScene2Images;

    private List<Sprite> CurrentCutsceneImages;
    private float fadeTime = 3f, alphaToFadeTo;
    private int cutsceneNum, cutsceneImageStage = 0, cutsceneStage = 0;
    private Color currentTextCol;
    private string gothSColor = "purple", popSColor = "yellow", jockSColor = "red", nerdSColor = "green";
    private Color gothColor = new Color (128, 0 , 128), popColor = Color.yellow, jockColor = Color.red, nerdColor = Color.green;

    private bool coroutineRunning = false;
    private Vector3 originalPosition;
    private Vector4 targetColor;
    private GameObject modifiedObject;

    
    //fade simply fades the 'fadeImage' panel to a new color, which is changing the alpha mainly.
    private void fade()
    {

        if (fadeImage.color == new Color(0f, 0f, 0f, 1f))
            alphaToFadeTo = 0f;
        else
            alphaToFadeTo = 1f;

        Debug.Log($"{fadeImage.color} fading now to {alphaToFadeTo}...");

        StartCoroutine(FadeTo(alphaToFadeTo, fadeTime));

    }

    IEnumerator FadeTo(float aValue, float aTime)
    {
        coroutineRunning = true;

        targetColor = new Vector4(0,0,0,aValue);

        float alpha = fadeImage.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(0, 0, 0, Mathf.Lerp(alpha, aValue, t));
            fadeImage.color = newColor;
            yield return null;
        }

        coroutineRunning = false;
    }

    IEnumerator Shake(GameObject toShake)
    {
        coroutineRunning = true;

        originalPosition = toShake.transform.position;
        modifiedObject = toShake;

        do
        {
            float random = Random.Range(originalPosition.x-4f, originalPosition.x+4f);
            float random2 = Random.Range(originalPosition.y - 4f, originalPosition.y + 4f);
            float random3 = Random.Range(originalPosition.z - 4f, originalPosition.z + 4f);

            toShake.transform.position = new Vector3(random, random2, random3);
            Debug.Log($"{toShake.transform.position} is the current position of the object");

            yield return new WaitForSeconds(.1f);

        }
        while (true);
        

    }

    private void updateImage()
    {
        cutsceneImage.sprite = CurrentCutsceneImages[cutsceneImageStage];
    }

    private void updateDrawing(int index)
    {

        drawingImage.sprite = CurrentCutsceneImages[index];

    }

    private void updateText(string newText)
    {
        dialogue.color = currentTextCol;
        dialogue.text = newText;
    }

    private void eventText(string newText)
    {



    }


    // Start is called before the first frame update
    void Start()
    {
        fadeImage.color = new Color(0,0,0,1f);
        dialogue.text = "";

        switch (cutsceneName)
        {
            case "PreBossOne":
                cutsceneNum = 1;
                CurrentCutsceneImages = cutScene1Images;
                drawingImage.sprite = cutScene1Images[0];
                fade();
                break;
            case "PostBossOne":
                cutsceneNum = 2;
                CurrentCutsceneImages = cutScene2Images;
                break;
            default:
                Debug.Log($"<color=red>Error: {cutsceneName} cutscene not found. Setting to intro scene...</color>");
                cutsceneNum = 1;
                break;
        }

        next.GetComponent<Button>().onClick.AddListener(() => {
            nextStage();
        });
    }

    private void nextStage()
    {
        Debug.Log($"Changing Cutscene stage...");

        //Update the current stage of the cutscene and call the function associated to the cutsceneNum this script is using
        //Which would have been sent by the previous scene
        cutsceneStage++;


        //If a coroutine is running, finish the routine and continue the cutscene.
        if (coroutineRunning)
        { 
            StopAllCoroutines();

            if (modifiedObject != null)
                modifiedObject.transform.position = originalPosition;

            fadeImage.color = targetColor;

            coroutineRunning = false;
        }

        targetColor = fadeImage.color;

        switch (cutsceneNum)
        {
            case 1: progressPreBusDriver();
                break;
            default:
                Debug.Log($"<color=red>Error: {cutsceneNum} is not in scope of available cutscenes.</color>");
                break;
        }

    }

    private void progressPreBusDriver()
    {
        switch (cutsceneStage)
        {
            case 1:
                currentTextCol = Color.white;
                updateText("<i>sigh</i>, the road’s really foggy today.");
                break;
            case 2:
                updateText("Been a while since I’ve seen this much fog. Almost as bad as the day I drew up in my notebook. I was hoping to get home before that pizza got there, not after.");
                break;
            case 3:
                updateDrawing(1);
                updateText("But... so long as the kids are safe, I guess that’s all that matters. ");
                break;
            case 4:
                updateText("");
                updateDrawing(0);
                break;
            case 5:
                updateText("...What the hell?? This is <i>not</i> normal. The fog is getting thicker!");
                break;
            case 6:
                updateText("Huh? Who said that?? And what do you mean the children are safe? The children are safe???");
                break;
            case 7:
                updateText("The... children... are safe.");
                break;
            case 8:
                updateText("The children... are safe… You’re right...");
                break;
            case 9:
                updateText("I can stop driving... They are safe... and away... they're safe... safe...");
                fadeImage.color = new Color(0f, 0f, 0f, .16f);
                break;
            case 10:
                updateText("and away...");
                fadeImage.color = new Color(0f, 0f, 0f, .33f);
                break;
            case 11:
                updateText("They're safe... ");
                fadeImage.color = new Color(0f, 0f, 0f, .498f);
                break;
            case 12:
                updateText("safe...");
                fadeImage.color = new Color(0f, 0f, 0f, .664f);
                break;
            case 13:
                updateText("I'm safe.");
                fadeImage.color = new Color(0f, 0f, 0f, .830f);
                break;
            case 14:
                updateText("We're safe.");
                fadeImage.color = new Color(0f, 0f, 0f, 1f);
                break;
            case 15:
                updateText("");
                eventText("CRASH");
                break;
            case 16:
                currentTextCol = popColor;
                updateText($"Ugghhhh, what was that about?");
                break;
            case 17:
                currentTextCol = gothColor;
                updateText($"You okay <color={popSColor}>Jacklyn</color>?");
                break;
            case 18:
                currentTextCol = popColor;
                updateText($"I think so. My harmonica managed to stab my side, but other than that, it could be worse. I guess…");
                break;
            case 19:
                currentTextCol = gothColor;
                updateText($"Well, at least it wasn’t a switch blade.");
                break;
            case 20:
                currentTextCol = popColor;
                updateText($"<color={gothSColor}>Charles</color>, you have a switch blade??? Is that even legal?");
                break;
            case 21:
                currentTextCol = gothColor;
                updateText($"...No comment.");
                break;
            case 22:
                updateText($"...Yo, <color={nerdSColor}>Zoey</color>, you doing okay over there?");
                break;
            case 23:
                currentTextCol = nerdColor;
                updateText($"Yeah, I seem stable enough.");
                break;
            case 24:
                updateText($"WAIT.");
                break;
            case 25:
                updateText($"Oh thank god, my keyboard’s fine. I just bought this thing, jeez. I was hoping to get home before I broke it in…");
                break;
            case 26:
                updateText($"Ummm, where's the bus driver?");
                break;
            case 27:
                currentTextCol = jockColor;
                updateText($"I found his notebook over here.");
                break;
            case 28:
                currentTextCol = nerdColor;
                updateText($"<color={jockSColor}>Johny</color>, that is way too quick to be searching through his stuff. Did we not just crash a few minutes ago?");
                break;
            case 29:
                currentTextCol = jockColor;
                updateText($"Eh, what can I say. My adrenaline is high right about now. Plus, this fog isn't helping.");
                break;
            case 30:
                currentTextCol = nerdColor;
                updateText($"Yeah, maybe let's head outside and see if we can find him. I'd rather get home in one piece.");
                break;
            case 31:
                currentTextCol = gothColor;
                updateText($"Agreed.");
                break;
            case 32:
                currentTextCol = jockColor;
                updateText($"...Hey, there he is!");
                break;
            case 33:
                currentTextCol = Color.white;
                updateText($"Kids... are... safe... Kids... are... safe...");
                break;
            case 34:
                currentTextCol = jockColor;
                updateText($"I... don't like that look in his eyes.");
                break;
            case 35:
                currentTextCol = Color.white;
                updateText($"Kids... come here... You’re safe now... Join me... Join us...");
                break;
            case 36:
                currentTextCol = jockColor;
                updateText($"Whoa, hey there bucko, that's a lil too close, even for hockey standards.");
                break;
            case 37:
                currentTextCol = Color.white;
                updateText($"Come");
                break;
            case 38:
                updateText($"HERE");
                StartCoroutine(Shake(dialogue.gameObject));
                break;
            default:
                Debug.Log($"<color=red>Error: {cutsceneStage} is not in scope of available stages for cutscene #{cutsceneNum}</color>");
                break;

        }
        
    }

}
