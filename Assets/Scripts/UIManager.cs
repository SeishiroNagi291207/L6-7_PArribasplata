using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI hpText;
    public Player player;

    private void Update()
    {
        if (player != null && hpText != null)
            hpText.text = "Vida: " + player.playerHp.ToString("F0");
    }
}