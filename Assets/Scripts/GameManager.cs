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
    public SOScene startingScene;
    public TMP_Text character1Text;
    public TMP_Text character2Text;
    public Image character1Image;
    public Image character2Image;
    public Image backGroundImage;

    public void Update()
    {
        
    }

    private SOScene currentScene;
    private SOConversation currentConversation;
    private int currentDialogue;
    private int stress;
    private void AdvanceDialogue()
    {
        currentDialogue++;
        if (currentDialogue >= currentConversation.speech.Length)
        {
            //display
            //make player choice
            //add stress
            //check if end of scene
                //change scene
            //else
            //advance dialogue
        }
        else
        {
            //display
        }
    }

    private void AdvanceScene(SOScene newScene)
    {
        currentScene = newScene;
        currentConversation = newScene.startingConversation;
        currentDialogue = -1;
        backGroundImage.sprite = newScene.background;
        AdvanceDialogue();
    }
}