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
    private TextMeshProUGUI dialogue, voices, events;
    [SerializeField]
    private Button skipScene, next;
    [SerializeField]
    private Image fadeImage, cutsceneImage, drawingImage, notebook, tutorial;
    [SerializeField]
    private List<Sprite> cutScene1Images, cutScene2Images;

    private List<Sprite> CurrentCutsceneImages;
    private float fadeTime = 3f, alphaToFadeTo;
    private int cutsceneNum, cutsceneImageStage = 0, cutsceneStage = 0;
    private Color currentTextCol;
    private string gothSColor = "purple", popSColor = "yellow", jockSColor = "red", nerdSColor = "green";
    private Color gothColor = new Color (160/ 255.0f, 32/255.0f, 240/255.0f), popColor = Color.yellow, jockColor = Color.red, nerdColor = Color.green;

    private bool coroutineRunning = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
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

    IEnumerator Shake(GameObject toShake, float scale)
    {
        coroutineRunning = true;

        originalPosition = toShake.transform.position;
        modifiedObject = toShake;

        do
        {
            float random = Random.Range(originalPosition.x-scale, originalPosition.x+scale);
            float random2 = Random.Range(originalPosition.y - scale, originalPosition.y + scale);
            float random3 = Random.Range(originalPosition.z - scale, originalPosition.z + scale);

            toShake.transform.position = new Vector3(random, random2, random3);
            Debug.Log($"{toShake.transform.position} is the current position of the object");

            yield return new WaitForSeconds(.1f);

        }
        while (true);
        

    }

    IEnumerator Tilt(GameObject toTilt)
    {
        coroutineRunning = true;

        originalRotation = toTilt.transform.rotation;
        modifiedObject = toTilt;

        do
        {
            float random = Random.Range(originalRotation.z - .03f, originalPosition.z + .03f);

            toTilt.transform.rotation = new Quaternion(originalRotation.x, originalRotation.y, random, originalRotation.w);
            Debug.Log($"{toTilt.transform.rotation} is the current rotation of the object");

            yield return new WaitForSeconds(.1f);

        }
        while (true);

    }

    

    private void toggleTutorialImage()
    {
        if(tutorial.color == new Color(1f,1f,1f,1f))
            tutorial.color = new Color(1f,1f,1f,0f);
        else
            tutorial.color = new Color(1f, 1f, 1f, 1f);
    }

    private void updateTutorial(int index)
    {

        tutorial.sprite = CurrentCutsceneImages[index];

    }

    private void updateDrawing(int index)
    {

        drawingImage.sprite = CurrentCutsceneImages[index];

    }

    private void updateImage()
    {
        cutsceneImage.sprite = CurrentCutsceneImages[cutsceneImageStage];
    }

    private void updateText(string newText)
    {
        dialogue.color = currentTextCol;
        dialogue.text = newText;
    }

    private void changeImageAlpha(Image image, float alpha)
    {
        Color currentColor = image.color;
        image.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
    }

    private void eventText(string newText)
    {
        
        events.color = currentTextCol;
        events.text = newText;

    }


    // Start is called before the first frame update
    void Start()
    {
        fadeImage.color = new Color(0,0,0,1f);
        dialogue.text = "";
        voices.text = "";
        events.text = "";

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
            { 
                modifiedObject.transform.position = originalPosition;
                modifiedObject.transform.rotation = originalRotation;
            }

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
            case 0:
                Debug.Log("Moving to next scene.");

                break;
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
                updateDrawing(6);
                currentTextCol = Color.black;
                updateText("...What the hell?? This is <i>not</i> normal. The fog is getting thicker!");
                break;
            case 6:
                voices.text = "Children are safe";
                StartCoroutine(Shake(voices.gameObject, 500f));
                StartCoroutine(Tilt(voices.gameObject));
                break;
            case 7:
                voices.text = "";
                updateText("Huh? Who said that?? And what do you mean the children are safe? The children are safe???");
                break;
            case 8:
                toggleTutorialImage();
                updateTutorial(7);
                changeImageAlpha(tutorial, .1f);
                updateText("The... children... are safe.");
                break;
            case 9:
                changeImageAlpha(tutorial, 0f);
                updateText("The children... are safe… You’re right...");
                break;
            case 10:
                updateText("I can stop driving...");
                fadeImage.color = new Color(0f, 0f, 0f, .16f);
                fadeImage.color = new Color(0f, 0f, 0f, .33f);
                break;
            case 11:
                changeImageAlpha(tutorial, .25f);
                updateText("They're safe... ");
                fadeImage.color = new Color(0f, 0f, 0f, .498f);
                break;
            case 12:
                currentTextCol = Color.white;
                changeImageAlpha(tutorial, .5f);
                updateText("safe...");
                fadeImage.color = new Color(0f, 0f, 0f, .664f);
                break;
            case 13:
                changeImageAlpha(tutorial, .75f);
                updateText("I'm safe.");
                fadeImage.color = new Color(0f, 0f, 0f, .830f);
                break;
            case 14:
                changeImageAlpha(tutorial, 1f);
                updateText("We're safe.");
                fadeImage.color = new Color(0f, 0f, 0f, 1f);
                break;
            case 15:
                toggleTutorialImage();
                updateText("");
                currentTextCol = Color.white;
                eventText("CRASH");
                StartCoroutine(Shake(events.gameObject, 100f));
                break;
            case 16:
                eventText("");
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
                currentTextCol = Color.grey;
                updateText($"Kids... are... safe... Kids... are... safe...");
                break;
            case 34:
                currentTextCol = jockColor;
                updateText($"I... don't like that look in his eyes.");
                break;
            case 35:
                currentTextCol = Color.grey;
                updateText($"Kids... come here... You’re safe now... Join me... Join us...");
                break;
            case 36:
                currentTextCol = jockColor;
                updateText($"Whoa, hey there bucko, that's a lil too close, even for hockey standards.");
                break;
            case 37:
                currentTextCol = Color.grey;
                updateText($"Come");
                break;
            case 38:
                updateText($"HERE");
                StartCoroutine(Shake(dialogue.gameObject, 4f));
                break;
            case 39:
                currentTextCol = jockColor;
                updateText($"Okay guys, we need to do something about this, <i>now</i>. Here, use this notebook! Write down stuff you can do, and let's figure out a gameplan, <i>stat</i>.");
                break;
            case 40:
                currentTextCol = nerdColor;
                updateText($"Got it! Ummm, ok... Lemme throw something together..");
                break;
            case 41:
                updateText($"Ok, I'll keep track of our actions using these scraps of paper.");
                break;
            case 42:
                toggleTutorialImage();
                updateTutorial(2);
                updateText($"This is the driver. I doodled an eye to represent whatever is affecting him. Hopefully we can make him snap out of it.");
                break;
            case 43:
                updateTutorial(3);
                updateText($"This is what we look like. Give me a heads up if you want to change the <color=white>order of our actions</color>, and I'll move them around.");
                break;
            case 44:
                updateText($"These thumbtacks should keep them in place and let me move them around.");
                break;
            case 45:
                currentTextCol = gothColor;
                updateText($"Shouldn't you also make some sort of counter to track whatever force is affecting the driver? Clearly something's up, and I'm not sure we're invulnerable.");
                break;
            case 46:
                currentTextCol = nerdColor;
                updateText($"...yeah, I guess you're right.");
                break;
            case 47:
                updateTutorial(4);
                updateText($"Here, this eye will represent our affliction.");
                break;
            case 48:
                updateText($"If you feel something start talking to you, kinda in a weird ritualistic way, like how the driver is speaking, then this should be increased.");
                break;
            case 49:
                updateText($"The higher it gets, the more we'll have to try to resist it I suppose...");
                break;
            case 50:
                updateText($"Let's make sure to <color=white>resist whatever force</color> is affecting the driver");
                break;
            case 51:
                updateText($"And <color=white>keep that percentage low</color> so it's easier to resist this force.");
                break;
            case 52:
                updateTutorial(5);
                updateText($"We can use this symbol to represent an attempt to resist. I don't know what's going on, but let's <i>try</i> to understand it.");
                break;
            case 53:
                currentTextCol = jockColor;
                updateText($"Sounds good. Let's do this!");
                break;
            default:
                Debug.Log($"<color=red>Error: {cutsceneStage} is not in scope of available stages for cutscene #{cutsceneNum}</color>");
                break;

        }
        
    }

}
