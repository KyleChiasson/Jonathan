using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(this);
        Application.targetFrameRate = 60;
    }
    //Text stuff
    public SOScene startingScene;
    public SOScene endingScene;
    public TMP_Text characterText;
    public TMP_Text character1NameText;
    public TMP_Text character2NameText;
    public Image character1Image;
    public Image character2Image;
    public Image backGroundImage;
    public TMP_Text choice1;
    public Button button1;
    public TMP_Text choice2;
    public Button button2;

    public void Update()
    {
        if (playing && !choosing && Input.GetMouseButtonDown(0))
        {
            AdvanceDialogue();
        }
    }

    //Sleep Stuff
    private bool asleep = true;
    private bool changingState = false;
    public Material sleepMat;
    private float sleepAmt = 0;
    private delegate void VOID();
    private IEnumerator ChangeSleepState(VOID callback = null)
    {
        changingState = true;
        while (asleep ? (sleepAmt < .3f) : (sleepAmt > 0))
        {
            yield return new WaitForSeconds(.1f);
            sleepAmt += asleep ? .01f : -.01f;
            sleepMat.SetFloat("_Float", sleepAmt);
        }
        asleep = !asleep;
        changingState = false;
        if(callback != null)
            callback();
    }

    //Dialogue Stuff
    private bool playing = false;
    private SOScene currentScene;
    private SOConversation currentConversation;
    private int currentDialogue;
    private int stress = 25; //goes between 0-60
    private bool choosing = false;
    public Sprite J1;
    public Sprite J2;
    public Sprite J3;
    public Sprite J4;
    private void AdvanceDialogue()
    {
        if (currentDialogue == currentConversation.speech.Length - 1 && currentConversation.choices.Length == 0)
        {
            AdvanceScene(currentScene.nextScenes[0]);
            return;
        }
        currentDialogue++;
        if (currentConversation.speech[currentDialogue].character.character_name == "You")
        {
            character1NameText.text = currentConversation.speech[currentDialogue].character.character_name;
            if(stress < 20)
                character1Image.sprite = J1;
            else if (stress < 40)
                character1Image.sprite = J2;
            else if (stress < 50)
                character1Image.sprite = J3;
            else
                character1Image.sprite = J4;
            characterText.alignment = TextAlignmentOptions.MidlineLeft;
        }
        else
        {
            character2NameText.text = currentConversation.speech[currentDialogue].character.character_name;
            character2Image.sprite = currentConversation.speech[currentDialogue].character.art;
            if (currentConversation.speech[currentDialogue].character.character_name == "")
                characterText.alignment = TextAlignmentOptions.Midline;
            else characterText.alignment = TextAlignmentOptions.MidlineRight;
        }
        characterText.text = currentConversation.speech[currentDialogue].line;
        if (currentConversation.choices.Length != 0 && currentDialogue == currentConversation.speech.Length - 1)
        {
            choosing = true;
            button1.interactable = true;
            button2.interactable = true;
            choice1.text = currentConversation.choices[0];
            choice2.text = currentConversation.choices[1];
        }
    }
    public void MakeChoice(int choice)
    {
        choosing = false;
        button1.interactable = false;
        button2.interactable = false;
        choice1.text = "";
        choice2.text = "";
        stress += currentConversation.choiceStressImpact[choice];
        currentConversation = currentConversation.outcomes[choice];
        for (int i = 0; i < currentScene.endingConversations.Length; i++)
        {
            if (currentScene.endingConversations[i] == currentConversation)
            {
                AdvanceScene(currentScene.nextScenes[i]);
                return;
            }
        }
        currentDialogue = -1;
        AdvanceDialogue();
    }

    private void AdvanceScene(SOScene newScene)
    {
        character1NameText.text = "";
        character2NameText.text = "";
        character1Image.sprite = blankSprite;
        character2Image.sprite = blankSprite;
        currentScene = newScene;
        if(currentScene == endingScene)
        {
            EndGame();
            return;
        }
        currentConversation = currentScene.startingConversation;
        currentDialogue = -1;
        backGroundImage.sprite = newScene.background;
        AdvanceDialogue();
    }

    //Start and end
    public Sprite blankSprite;
    public void StartGame()
    {
        stress = 25;
        sleepMat.SetFloat("_Float", sleepAmt);
        StartCoroutine(ChangeSleepState());
        AdvanceScene(startingScene);
        playing = true;
    }
    private void EndGame()
    {
        playing = false;
        StartCoroutine(ChangeSleepState(EndCallback));
    }
    public GameObject gameUI;
    public GameObject endUI;
    public TMP_Text stressText;
    public GameObject wonText;
    public GameObject replayButton;
    public GameObject mainMenuButton;
    private void EndCallback()
    {
        endUI.SetActive(true);
        gameUI.SetActive(false);
        stressText.text = $"Stress: {Mathf.RoundToInt(100f * stress / 60f)}%";
        wonText.SetActive((stress == 60 || stress == 0) ? true : false);
        replayButton.SetActive((stress == 60 || stress == 0) ? false : true);
        mainMenuButton.SetActive((stress == 60 || stress == 0) ? true : false);
    }
}