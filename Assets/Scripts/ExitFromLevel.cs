using UnityEngine;
public class ExitFromLevel : Sounds
{
    [SerializeField] private GameObject taskPanel;
    [SerializeField] private Player player;

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlaySound(sounds[0], 0.3f);
        if (taskPanel != null)
        {
            taskPanel.SetActive(true);
            player.PrintResult();
        }
    }
}
