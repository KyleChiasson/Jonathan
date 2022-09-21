using UnityEngine;
[CreateAssetMenu(fileName = "New Conversation", menuName = "SO/Conversation")]
public class SOConversation : ScriptableObject
{
    public class dialogue
    {
        public SOCharacter character;
        public string line;
    }
    public dialogue[] speech;
    public string[] choices;
    public int[] choiceStressImpact;
    public SOConversation[] outcomes;
}