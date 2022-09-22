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
    }
    private void Start()
    {
        AdvanceScene(startingScene);
    }
    public SOScene startingScene;
    public TMP_Text character1Text;
    public TMP_Text character1NameText;
    public TMP_Text character2Text;
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
        if (!choosing && Input.GetMouseButtonDown(0))
        {
            AdvanceDialogue();
        }
    }

    private SOScene currentScene;
    private SOConversation currentConversation;
    private int currentDialogue;
    private int stress = 25;
    private bool choosing = false;
    private void AdvanceDialogue()
    {
        currentDialogue++;
        character1Image.sprite = currentConversation.speech[currentDialogue].character.art;
        character1Text.text = currentConversation.speech[currentDialogue].line;
        if (currentDialogue == currentConversation.speech.Length - 1)
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
        currentScene = newScene;
        currentConversation = newScene.startingConversation;
        currentDialogue = -1;
        //backGroundImage.sprite = newScene.background;
        AdvanceDialogue();
    }
}