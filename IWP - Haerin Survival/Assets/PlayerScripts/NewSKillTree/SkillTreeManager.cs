using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class SkillTreeManager : MonoBehaviour
{

    [System.Serializable]
    public class SkillButton
    {
        public Skill skill; // Reference to the ScriptableObject
        public Button button; // Reference to the UI button
    }

    [System.Serializable]
    public class SkillPath
    {
        public Skill prerequisiteSkill;   // The skill that must be unlocked first
        public Skill dependentSkill;      // The skill that is dependent on the prerequisite
        public Image pathImage;          // The line connecting these skills
    }
    public PlayerStats playerStats; // Reference to the PlayerStats ScriptableObject
    public List<SkillPath> skillPaths;
    [SerializeField] private SkillButton[] skillButtons; // Array of skills and their associated buttons
    [SerializeField] private TextMeshProUGUI description;
    public int skillPoints = 0; // Example skill points, manage this as needed

    private void Start()
    {
        // Reset skills at the start of the game
        ResetSkills();

        // Initialize button states
        UpdateSkillButtons();

        // Add listeners to buttons
        foreach (SkillButton skillButton in skillButtons)
        {
            skillButton.button.onClick.AddListener(() => TryUnlockSkill(skillButton.skill));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            skillPoints += 1;
            UpdateSkillButtons();
            UpdateSkillPaths();
        }
        description.text = "Skill Points: " + skillPoints.ToString();
    }

    public void SkillUpdate()
    {
        skillPoints += 1;
        UpdateSkillButtons();
        UpdateSkillPaths();
    }
    private void ResetSkills()
    {
        foreach (SkillButton skillButton in skillButtons)
        {
            skillButton.skill.isUnlocked = false;
        }
        Debug.Log("All skills have been reset.");
    }

    public void UpdateSkillButtons()
    {
        foreach (SkillButton skillButton in skillButtons)
        {
            if (skillButton.skill.isUnlocked)
            {
                // Disable the button for unlocked skills
                skillButton.button.interactable = false;
                skillButton.button.GetComponent<Image>().color = Color.green; // Optional: Change color to indicate unlocked
                
            }
            else if (CanUnlockSkill(skillButton.skill))
            {
                // Enable the button for unlockable skills
                skillButton.button.interactable = true;
                skillButton.button.GetComponent<Image>().color = Color.yellow; // Optional: Change color to indicate unlockable
            }
            else
            {
                // Disable the button for locked skills
                skillButton.button.interactable = false;
                skillButton.button.GetComponent<Image>().color = Color.red; // Optional: Change color to indicate locked
            }
        }
    }


    public void UpdateSkillPaths()
    {
        foreach (SkillPath skillPath in skillPaths)
        {
            if (skillPath.prerequisiteSkill.isUnlocked && skillPath.dependentSkill.isUnlocked)
            {
                // Both skills are unlocked, show the path as "active"
                skillPath.pathImage.color = Color.green; // Or any active color
            }
            else if (skillPath.prerequisiteSkill.isUnlocked)
            {
                // Prerequisite skill is unlocked, but dependent is not
                skillPath.pathImage.color = Color.gray; // Indicate it's unlockable
            }
            else
            {
                // Path is inactive
                skillPath.pathImage.color = new Color(0.5f, 0.5f, 0.5f); // Dimmed color
            }
        }
    }


    public bool CanUnlockSkill(Skill skill)
    {
        if (skill.isUnlocked || skillPoints < skill.cost) return false;

        // Check if all prerequisites are met
        return skill.ArePrerequisitesMet();
    }

    public void TryUnlockSkill(Skill skill)
    {
        if (CanUnlockSkill(skill))
        {
            skill.isUnlocked = true;
            skillPoints -= skill.cost;
            skill.UnlockSkill(playerStats); // Pass PlayerStats to apply the effect
            UpdateSkillButtons();
            UpdateSkillPaths();
            Debug.Log($"Unlocked skill: {skill.skillName}");
        }
        else
        {
            Debug.Log($"Cannot unlock skill: {skill.skillName}");
        }
    }
}
