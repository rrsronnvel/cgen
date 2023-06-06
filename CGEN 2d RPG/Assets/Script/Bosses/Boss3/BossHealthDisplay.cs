using UnityEngine;
using TMPro;

public class BossHealthDisplay : MonoBehaviour
{
    public Boss3 boss;
    public TextMeshProUGUI healthText;

    private void Update()
    {
         healthText.text = "Boss Health: " + boss.GetHealth();
    }
}
