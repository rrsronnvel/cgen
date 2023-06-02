using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
    public HealthBarTB healthBar;

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = "Lvl " + unit.unitLevel;
        healthBar.SetMaxHealth(unit.maxHP);
        healthBar.SetHealth(unit.currentHP);
    }

    public void SetHP(float hp)
    {
        healthBar.SetHealth(hp);
    }
}
