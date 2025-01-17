using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ExpWindow : MonoBehaviour
{
    private TextMeshProUGUI levelText;
    private Image expBarImage;
    private LevelUpSystem levelUpSystem;
    private LevelSystemAnimation systemAnimation;
    private SkillTree playerSkills;
    private void Awake()
    {
        levelText = transform.Find("levelText").GetComponent<TextMeshProUGUI>();
        expBarImage = transform.Find("bar").Find("fill").GetComponent<Image>();
        playerSkills = new SkillTree();
    }

    private void SetExpBarSize(float expNormalized)
    {
        expBarImage.fillAmount = expNormalized;
    }
    public SkillTree GetPlayerSkills()
    {
        return playerSkills;
    }

    private void SetlevelNumber(int levelnumber)
    {
        levelText.text = "LEVEL\n" + (levelnumber + 1);
    }
    public void SetLevelSystem(LevelUpSystem levelUpSystem)
    {
        this.levelUpSystem = levelUpSystem;

        SetlevelNumber(levelUpSystem.GetLevelNumber());
        SetExpBarSize(levelUpSystem.GetExpNormalised());

        levelUpSystem.OnExpChanged += LevelUpSystem_OnExpChanged;
        levelUpSystem.OnLevelUpChanged += LevelUpSystem_OnLevelUpChanged;
    }

    private void LevelUpSystem_OnExpChanged(object sender, System.EventArgs e)
    {
        SetExpBarSize(levelUpSystem.GetExpNormalised());
    }

    private void LevelUpSystem_OnLevelUpChanged(object sender, System.EventArgs e)
    {
        // Reset bar to 0 visually (optional since OnExpChanged handles this)
        SetExpBarSize(0);
        SetlevelNumber(levelUpSystem.GetLevelNumber());
        playerSkills.AddSkillPoints();
        
    }

  

}
