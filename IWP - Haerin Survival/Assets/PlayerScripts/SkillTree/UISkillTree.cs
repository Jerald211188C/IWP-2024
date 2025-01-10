using UnityEngine;
using UnityEngine.UI;

public class UISkillTree : MonoBehaviour
{
    private SkillTree skillTree;
    private Rifle rifle;

    private void Awake()
    {
        // Add the button listener
        transform.Find("SkillButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            skillTree.UnlockSkill(SkillTree.SkillType.MoreDamage);
            ApplySkillEffects(); // Apply effects after unlocking the skill
        });
    }

    public void SetPlayerSkills(SkillTree skillTree)
    {
        this.skillTree = skillTree;

        // Apply effects if the skill is already unlocked
        if (skillTree.IsSkillUnlocked(SkillTree.SkillType.MoreDamage))
        {
            ApplySkillEffects();
        }
    }

    public void SetRifle(Rifle rifle)
    {
        this.rifle = rifle;
    }

    private void ApplySkillEffects()
    {
        // Check for null references
        if (rifle == null)
        {
            Debug.LogWarning("Rifle reference is not set in UISkillTree.");
            return;
        }

        if (skillTree == null)
        {
            Debug.LogWarning("SkillTree reference is not set in UISkillTree.");
            return;
        }

        // Apply the skill effect
        if (skillTree.IsSkillUnlocked(SkillTree.SkillType.MoreDamage))
        {
            rifle.IncreaseDamage(1000); // Increase the damage by 10
            Debug.Log("Damage increased due to skill unlock.");
        }
    }
}
