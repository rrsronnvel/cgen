using UnityEngine;
using UnityEngine.UI;

public class HealthBarTB : MonoBehaviour
{

    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;

    public void SetMaxHealth(float maxHealth)
    {
        totalhealthBar.fillAmount = maxHealth / 10f;  // Assuming 100 is your max health in this case
    }

    public void SetHealth(float currentHealth)
    {
        currenthealthBar.fillAmount = currentHealth / 10f;  // Assuming 100 is your max health in this case
    }
}
