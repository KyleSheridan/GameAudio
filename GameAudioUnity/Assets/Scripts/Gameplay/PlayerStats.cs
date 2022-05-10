using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    Money,
    Charisma,
    Intelligence,
    Strength,
    Hunger,
    Energy
}

[System.Serializable]
public struct StatModifier
{
    public StatType type;
    public int amount;
}

public class PlayerStats : MonoBehaviour
{
    private static PlayerStats _instance;

    public static PlayerStats Instance { get { return _instance; } }

    public StatsUI ui;

    private int _money;
    private int _charisma;
    private int _intelligence;
    private int _strength;
    private int _hunger;
    private int _energy;

    public int Money 
    {
        get
        {
            return _money;
        }
    }
    public int Charisma 
    { 
        get
        {
            return _charisma;
        }
    }
    public int Intelligence 
    {
        get
        {
            return _intelligence;
        }
    }
    public int Strength
    {
        get
        {
            return _strength;
        }
    }
    public int Hunger
    {
        get
        {
            return Mathf.Clamp(_hunger, 0, 100);
        }
    }
    public int Energy
    {
        get
        {
            return Mathf.Clamp(_energy, 0, 100);
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        _money = 0;
        _charisma = 5;
        _intelligence = 1;
        _strength = 1;
        _hunger = 100;
        _energy = 30;

        ui.SetStatsUI();
    }

    public void ModifyStat(StatModifier modifier)
    {
        switch (modifier.type)
        {
            case StatType.Money:
                _money += modifier.amount;
                break;
            case StatType.Charisma:
                _charisma += modifier.amount;
                break;
            case StatType.Intelligence:
                _intelligence += modifier.amount;
                break;
            case StatType.Strength:
                _strength += modifier.amount;
                break;
            case StatType.Hunger:
                _hunger += modifier.amount;
                _hunger = Mathf.Clamp(_hunger, 0, 100);
                break;
            case StatType.Energy:
                _energy += modifier.amount;
                _energy = Mathf.Clamp(_energy, 0, 100);
                break;
            default:
                break;
        }

        ui.SetStatsUI();
    }
}
