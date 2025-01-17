using UnityEngine;

public class SkillTesting : MonoBehaviour
{
    [SerializeField] private Rifle rifle;
    [SerializeField] private UISkillTree skillTree;
    [SerializeField] private SimpleEnemy enemy;
    [SerializeField] private PlayerMovement player;

    private void Start()
    {
        // Assign player skills and rifle to the skill tree
        skillTree.SetPlayerSkills(rifle.GetPlayerSkills());
        skillTree.SetRifle(rifle);
        skillTree.SetEnemy(enemy);
        skillTree.SetPlayer(player);


    }
}
