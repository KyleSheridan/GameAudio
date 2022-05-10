using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsUI : MonoBehaviour
{
    public TMP_Text money;
    public TMP_Text charisma;
    public TMP_Text intelligence;
    public TMP_Text strength;

    public Slider hunger;
    public Slider energy;

    public void SetStatsUI()
    {
        PlayerStats stats = PlayerStats.Instance;
        money.text = stats.Money.ToString() + "$";
        charisma.text = stats.Charisma.ToString();
        intelligence.text = stats.Intelligence.ToString();
        strength.text = stats.Strength.ToString();

        hunger.value = (float)stats.Hunger / 100f;
        energy.value = (float)stats.Energy / 100f;
    }
}
