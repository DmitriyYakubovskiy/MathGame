using UnityEngine;
using System.Collections;
using TMPro;

public class Professor : Sounds
{    
    [SerializeField] public TextMeshProUGUI text;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private new Transform transform;
    [SerializeField] private GameObject taskPanel;
    [SerializeField] private Player player;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject[] wall;

    [SerializeField] private string correctAnswer;

    private int cnt=0;

    public void CheckAnswer(string answer)
    {
        if (answer == correctAnswer) CorrectAnswer();
        else NotCorrectAnswer();
    }

    public void ResetText()
    {
        text.text = "";
    }

    public void NotCorrectAnswer()
    {
        PlaySound(sounds[2], 0.3f);
        player.CountOfMistake++;
        Debug.Log(player.CountOfMistake);
        text.text = "Неверный ответ!";
        Invoke("ResetText", 2);
    }

    public void CorrectAnswer()
    {
        player.CountOfTaskComplete++;
        PlaySound(sounds[1], 0.3f);
        taskPanel.SetActive(false);
        StartCoroutine(DestroyWall());
    }

    private IEnumerator DestroyWall()
    {
        for(int i = 0; i <wall.Length-1; i++)
        {
            if (i == wall.Length - 2)
            {
                wall[i+1].gameObject.SetActive(false);
            }
            wall[i].gameObject.SetActive(false);
            PlaySound(sounds[0],0.1f);
            yield return new WaitForSeconds(1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        spriteRenderer.flipX = true;

        if (cnt != 0) return;
        taskPanel.SetActive(true);
        cnt++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        spriteRenderer.flipX = false;
    }
}
