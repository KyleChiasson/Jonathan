using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Scene", menuName = "SO/Scene")]
public class SOScene : ScriptableObject
{
    public Sprite background;
    public SOConversation startingConversation;
    public SOConversation[] endingConversations;
    public SOScene[] nextScenes;
}
