using UnityEngine;

public class SkillTesting : MonoBehaviour
{
    [SerializeField] private Rifle rifle;
    [SerializeField] private UISkillTree skillTree;

    private void Start()
    {
        // Assign player skills and rifle to the skill tree
        skillTree.SetPlayerSkills(rifle.GetPlayerSkills());
        skillTree.SetRifle(rifle);
    }
}
