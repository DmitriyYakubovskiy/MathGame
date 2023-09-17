using System;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class CheckerAnswers : MonoBehaviour
{
    [SerializeField] private Field[] fields;
    [SerializeField] private Professor professor;
    
    public void Check()
    {
        foreach (var field in fields)
        {
            if (!field.IsCorrect)
            {
                professor.NotCorrectAnswer();
                return;
            }
        }
        professor.CorrectAnswer();
    }
}
