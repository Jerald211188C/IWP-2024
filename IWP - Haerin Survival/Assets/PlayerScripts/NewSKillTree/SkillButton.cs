using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillButton : MonoBehaviour
{
    public Skill skill; // Reference to the skill represented by this button
    public Button button;
    public Image icon;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI Description;

    [SerializeField] private SkillTreeManager skillTreeManager;

    private void Start()
    {
        UpdateUI();
    }

    public void TryUnlockSkill()
    {
        if (skillTreeManager != null)
        {
            skillTreeManager.TryUnlockSkill(skill);
            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        icon.sprite = skill.icon;
        costText.text = "Skill Cost: " + skill.cost.ToString();
        Description.text = skill.description;

        // Disable the button if the skill is already unlocked
        button.interactable = !skill.isUnlocked;

        // Optionally, you can add a visual indicator for prerequisites not met
        if (!skillTreeManager.CanUnlockSkill(skill))
        {
            button.interactable = false;
        }
    }
}
