using UnityEngine;
using System;

public class LevelSystemAnimation : MonoBehaviour
{
    public static LevelSystemAnimation Instance { get; private set; }

    private LevelUpSystem levelUpSystem;
    private bool isAnimating;

    [SerializeField] private int level;
    [SerializeField] private int exp;
    [SerializeField] private int expToNextLevel;

    private void Awake()
    {
        // Ensure singleton instance
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Initialize(LevelUpSystem levelUpSystem)
    {
        SetLevelSystem(levelUpSystem);
    }

    public void SetLevelSystem(LevelUpSystem levelUpSystem)
    {
        this.levelUpSystem = levelUpSystem;

        level = levelUpSystem.GetLevelNumber();
        exp = levelUpSystem.GetExp();
        expToNextLevel = levelUpSystem.GetExpToNextLevel();

        levelUpSystem.OnExpChanged += LevelUpSystem_OnExpChanged;
        levelUpSystem.OnLevelUpChanged += LevelUpSystem_OnLevelUpChanged;
    }

    private void LevelUpSystem_OnLevelUpChanged(object sender, EventArgs e)
    {
        isAnimating = true;
    }

    private void LevelUpSystem_OnExpChanged(object sender, EventArgs e)
    {
        isAnimating = true;
    }

    private void Update()
    {
        if (isAnimating)
        {
            if (level < levelUpSystem.GetLevelNumber())
            {
                AddExp();
            }
            else if (exp < levelUpSystem.GetExp())
            {
                AddExp();
            }
            else
            {
                isAnimating = false;
            }
        }
        //Debug.Log(level + " " + exp);
    }

    private void AddExp()
    {
        exp++;
        if (exp >= expToNextLevel)
        {
            level++;
            exp = 0;
        }
    }
}
