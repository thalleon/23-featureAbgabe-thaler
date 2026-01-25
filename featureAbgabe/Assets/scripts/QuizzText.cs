using UnityEngine;

[System.Serializable]
public class QuizQuestion : MonoBehaviour
{
    [TextArea]
    public string questionText;

    public string[] answers = new string[4];

    [Range(0, 3)]
    public int correctAnswerIndex;
}
