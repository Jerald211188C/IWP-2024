using UnityEngine;
using System;
using System.Collections;
using TMPro;

public class LevelUpSystem : MonoBehaviour
{
    public static LevelUpSystem Instance { get; private set; }

    public event EventHandler OnExpChanged;
    public event EventHandler OnLevelUpChanged;

    [SerializeField] private int level;
    [SerializeField] private int exp;
    [SerializeField] private int expToNextLevel;


    public LevelUpSystem()
    {
        Instance = this;

        level = 0;
        exp = 0;
        expToNextLevel = 100;
    }

    public void AddExp(int amount)
    {
        exp += amount;
        while (exp >= expToNextLevel)
        {
            level++;
            exp -= expToNextLevel;
            if (OnLevelUpChanged != null)
            {
                OnLevelUpChanged(this, EventArgs.Empty);
            }
        }
        if (OnExpChanged != null)
        {
            OnExpChanged(this, EventArgs.Empty);
        }
    }



    public int GetLevelNumber()
    {
        return level;
    }

    public float GetExpNormalised()
    {
        return (float)exp / expToNextLevel;
    }
    public int GetExp()
    {
        return exp;
    }

    public int GetExpToNextLevel()
    {
        return expToNextLevel;
    }

}
