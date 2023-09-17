using UnityEngine;
using UnityEngine.UI;

public class Field : MonoBehaviour
{
    [SerializeField] private Text input;
    [SerializeField] private string correctAnswer;
    public bool IsCorrect { get; set; }

    public void Check()
    {
        string str = (input.text).ToLower();

        if(str.Equals(correctAnswer)) IsCorrect = true;
        else IsCorrect = false;
        Debug.Log(correctAnswer+" "+str);
    }
}
