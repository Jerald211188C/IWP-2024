using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill Tree/Skill")]
public class Skill : ScriptableObject
{
    
    public string skillName;       
    public string description;      
    public Sprite icon;             
    public int cost;                
    public Skill[] prerequisites;   
    public bool isUnlocked = false;
    public SkillEffectType SkillEffect;
    public float effectValue; // Magnitude of the effect



    public enum SkillEffectType
    {
        IncreaseDamageMultiplier,
        IncreaseDamageMultiplier2,
        AddCoins,
        IncreaseEXP
    }
    public void UnlockSkill(PlayerStats playerStats)
    {
        isUnlocked = true;
        ApplyEffect(playerStats);
    }
    public bool ArePrerequisitesMet()
    {
        foreach (Skill prerequisite in prerequisites)
        {
            if (!prerequisite.isUnlocked)
            {
                return false;
            }
        }
        return true;
    }

    public void ApplyEffect(PlayerStats playerStats)
    {
        switch (SkillEffect)
        {
            case SkillEffectType.IncreaseDamageMultiplier:
                playerStats._DamageMultiplier += effectValue;
                Debug.Log($"{skillName}: Damage multiplier increased by {effectValue}. New multiplier: {playerStats._DamageMultiplier}");
                break;
            case SkillEffectType.IncreaseDamageMultiplier2:
                playerStats._DamageMultiplier += effectValue;
                Debug.Log($"{skillName}: Damage multiplier increased by {effectValue}. New multiplier: {playerStats._DamageMultiplier}");
                break;
            case SkillEffectType.AddCoins:
                playerStats._CoinsToAdd += Mathf.RoundToInt(effectValue);
                Debug.Log($"{skillName}: Added {effectValue} coins. Current coins: {playerStats._CurrentCoins}");
                break;

            case SkillEffectType.IncreaseEXP:
                playerStats._EXP += Mathf.RoundToInt(effectValue);
                Debug.Log($"{skillName}: Added {effectValue} EXP. Current EXP: {playerStats._EXP}");
                break;

            default:
                Debug.LogWarning($"Skill {skillName} has no defined effect.");
                break;
        }
    }
}
