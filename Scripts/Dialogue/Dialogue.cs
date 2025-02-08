using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Dialogue")]
public class Dialogue : ScriptableObject
{
    // Dialogue Variables
    public string speakerName;
    public Color textColor;

    [TextArea(3, 10)]
    public string[] sentences;
}