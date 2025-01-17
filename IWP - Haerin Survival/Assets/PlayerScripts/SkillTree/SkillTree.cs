using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class SkillTree
{
    public UnityEvent OnSkillPointsChanged = new UnityEvent();
    private int skillpoint = 2;  // This will be the only variable to track skill points

    public event EventHandler<OnSkillUnlockedEventArgs> OnSkillUnlocked;

    public class OnSkillUnlockedEventArgs : EventArgs
    {
        public SkillType skillType;
    }

    public enum SkillType
    {
        None,
        MoreDamage,
        MoreDamage2,
        MoreDamage3,
        MoreCoins,
        MoreCoins2,
        MoreEXP,
        MoreEX2,
    }

    private List<SkillType> UnlockedSkillTypeList;

    public SkillTree()
    {
        UnlockedSkillTypeList = new List<SkillType>();
    }

    public void AddSkillPoints()
    {
        Debug.Log($"Before: {skillpoint}");
        skillpoint++;  // Increment skillpoint when called
        Debug.Log($"After: {skillpoint}");
        OnSkillPointsChanged.Invoke();  // Notify UI to update skill points
    }


    private void UnlockSkill(SkillType skill)
    {
        if (!IsSkillUnlocked(skill))
        {
            UnlockedSkillTypeList.Add(skill);
            OnSkillUnlocked?.Invoke(this, new OnSkillUnlockedEventArgs { skillType = skill });
        }
    }

    public bool IsSkillUnlocked(SkillType skill)
    {
        return UnlockedSkillTypeList.Contains(skill);
    }

    public SkillType GetSkillRequirement(SkillType skillType)
    {
        switch (skillType)
        {
            case SkillType.MoreDamage2:
                return SkillType.MoreDamage;
            case SkillType.MoreDamage3:
                return SkillType.MoreDamage2;
            case SkillType.MoreCoins2:
                return SkillType.MoreCoins;
            case SkillType.MoreEX2:
                return SkillType.MoreEXP;
        }
        return SkillType.None;
    }

    public bool TryUnlockSkill(SkillType skillType)
    {
        if (CanUnlock(skillType))
        {
            skillpoint--;  // Decrement skillpoint for unlocking a skill
            OnSkillPointsChanged.Invoke();  // Notify UI to update skill points
            UnlockSkill(skillType);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CanUnlock(SkillType skillType)
    {
        SkillType skillRequirement = GetSkillRequirement(skillType);

        if (skillRequirement != SkillType.None)
        {
            return IsSkillUnlocked(skillRequirement);
        }
        else
        {
            return true;
        }
    }

    public int GetSkillPoints()
    {
        return skillpoint;  // Return the value of skillpoint
    }
}

