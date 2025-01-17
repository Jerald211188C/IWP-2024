using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using TMPro;

public class UISkillTree : MonoBehaviour
{
    [Header("Skill Materials")]
    [SerializeField] private Material skillLockedMaterial;
    [SerializeField] private Material skillUnlockableMaterial;
    [SerializeField] private SkillUnlockPath[] skillUnlockPathArray;

    private SkillTree skillTree;
    private Rifle rifle;
    private SimpleEnemy enemy;
    private PlayerMovement player;

    [Header("Stats UI")]
    private TextMeshProUGUI _SP;

    private void Awake()
    {
        _SP = transform.Find("SPText").GetComponent<TextMeshProUGUI>();
        //GameObject skillButtonObject = GameObject.FindGameObjectWithTag("_SP");
        //_SP = skillButtonObject.GetComponent<TextMeshProUGUI>();

        SetSkillButton("Dmg_1", SkillTree.SkillType.MoreDamage);
        SetSkillButton("Dmg_2", SkillTree.SkillType.MoreDamage2);
        SetSkillButton("EXP_!", SkillTree.SkillType.MoreEXP);
        SetSkillButton("EXP_2", SkillTree.SkillType.MoreEX2);
        SetSkillButton("Coins_1", SkillTree.SkillType.MoreCoins);
        SetSkillButton("Coins_2", SkillTree.SkillType.MoreCoins2);


        // Initial visual update
        
        UpdateVisual();
    }


    public void SetPlayerSkills(SkillTree skillTree)
    {
        Debug.Log("HAERINNNN");
        this.skillTree = skillTree;

        skillTree.OnSkillPointsChanged.AddListener(SkillTree_OnSkillPointsChanged);
        // Apply effects if the skill is already unlocked
        ApplySkillEffects();
        UpdateVisual();
    }

    private void SkillTree_OnSkillPointsChanged()
    {
        UpdateSkillPoints();
    }

    private void UpdateSkillPoints()
    {
        _SP.SetText(skillTree.GetSkillPoints().ToString());
    }

    public void SetRifle(Rifle rifle)
    {
        this.rifle = rifle;
    }

    public void SetEnemy(SimpleEnemy enemy)
    {
        this.enemy = enemy;
    }

    public void SetPlayer(PlayerMovement player)
    {
        this.player = player;
    }

    private void SetSkillButton(string tag, SkillTree.SkillType skillType)
    {
        GameObject skillButtonObject = GameObject.FindGameObjectWithTag(tag);
        if (skillButtonObject != null)
        {
            skillButtonObject.GetComponent<Button_UI>().ClickFunc = () =>
            {
                if (skillTree.TryUnlockSkill(skillType))
                {
                    ApplySkillEffects();
                    UpdateVisual(); // Update visuals after unlocking the skill
                }
            };
        }
    }

    private void ApplySkillEffects()
    {
        if (skillTree == null) return;

        if (skillTree.IsSkillUnlocked(SkillTree.SkillType.MoreDamage))
        {
            rifle.IncreaseDamage(1000);
            Debug.Log("Damage increased due to skill unlock.");
        }

        if (skillTree.IsSkillUnlocked(SkillTree.SkillType.MoreEXP))
        {
            player.IncreaseEXPGain(200);
            Debug.Log("MoreEXP unlocked.");
        }

        if (skillTree.IsSkillUnlocked(SkillTree.SkillType.MoreDamage2))
        {
            rifle.IncreaseDamage(1000);
            Debug.Log("Additional damage unlocked.");
        }

        if (skillTree.IsSkillUnlocked(SkillTree.SkillType.MoreEX2))
        {
            player.IncreaseEXPGain(500);
            Debug.Log("Additional EXP gain unlocked.");
        }

        if (skillTree.IsSkillUnlocked(SkillTree.SkillType.MoreCoins))
        {
            player.IncreaseCoinGain(10);
            Debug.Log("Additional Coins gain unlocked.");
        }

        if (skillTree.IsSkillUnlocked(SkillTree.SkillType.MoreCoins))
        {
            player.IncreaseCoinGain(10);
            Debug.Log("Additional Coins gain unlocked.");
        }

        if (skillTree.IsSkillUnlocked(SkillTree.SkillType.MoreCoins2))
        {
            player.IncreaseCoinGain(30);
            Debug.Log("Additional Coins gain unlocked.");
        }
    }

    private void UpdateVisual()
    {
        UpdateButtonVisual("Dmg_1", SkillTree.SkillType.MoreDamage);
        UpdateButtonVisual("Dmg_2", SkillTree.SkillType.MoreDamage2);
        UpdateButtonVisual("EXP_!", SkillTree.SkillType.MoreEXP);
        UpdateButtonVisual("EXP_2", SkillTree.SkillType.MoreEX2);
        UpdateButtonVisual("Coins_1", SkillTree.SkillType.MoreCoins);
        UpdateButtonVisual("Coins_2", SkillTree.SkillType.MoreCoins2);
    }

    private void UpdateButtonVisual(string tag, SkillTree.SkillType skillType)
    {
        GameObject skillButtonObject = GameObject.FindGameObjectWithTag(tag);
        if (skillButtonObject != null && skillTree != null)
        {
            // Get the Image components for the button and background
            Image buttonImage = skillButtonObject.GetComponent<Image>();
            Image backgroundImage = skillButtonObject.GetComponent<Image>();
            Button_UI buttonUI = skillButtonObject.GetComponent<Button_UI>();

            if (buttonImage == null || backgroundImage == null || buttonUI == null) return;

            // Update visuals based on skill state
            if (skillTree.IsSkillUnlocked(skillType))
            {
                // Skill is unlocked
                buttonImage.material = null; // Reset material to default
                backgroundImage.material = null;
                backgroundImage.color = Color.white; // Default color for unlocked background
                buttonUI.enabled = false; // Disable interaction
            }
            else if (skillTree.CanUnlock(skillType))
            {
                // Skill is unlockable
                buttonImage.material = skillUnlockableMaterial;
                backgroundImage.material = null; // Optional: Use a different material for unlockable background
                backgroundImage.color = UtilsClass.GetColorFromString("4B677D"); // Unlockable background color
                buttonUI.enabled = true; // Enable interaction
            }
            else
            {
                // Skill is locked
                buttonImage.material = skillLockedMaterial;
                backgroundImage.material = skillLockedMaterial; // Optional: Apply the locked material to background
                backgroundImage.color = new Color(.3f, .3f, .3f); // Locked background color
                buttonUI.enabled = false; // Disable interaction
            }
        }
    }

    [System.Serializable]
    public class SkillUnlockPath
    {
        public SkillTree.SkillType skillType;
        public Image[] linkImageArray;
    }

}
