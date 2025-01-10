using UnityEngine;
using System.Collections.Generic;

public class SkillTree
{
    public enum SkillType
    {
        MoreDamage,
    }

    private List<SkillType> UnlockedSkillTypeList;

    public SkillTree()
    {
        UnlockedSkillTypeList = new List<SkillType>();
    }

    public void UnlockSkill(SkillType skill)
    {
        UnlockedSkillTypeList.Add(skill);
    }

    public bool IsSkillUnlocked(SkillType skill)
    {
        return UnlockedSkillTypeList.Contains(skill);
    }
}
