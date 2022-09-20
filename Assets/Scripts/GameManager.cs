using TMPro;
using UnityEngine;

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

    //advance dialogue
    //advance scene
}