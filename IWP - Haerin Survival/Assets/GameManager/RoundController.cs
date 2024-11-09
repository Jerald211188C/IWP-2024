using UnityEngine;

public class RoundController : MonoBehaviour
{

    [SerializeField] private int _StartingRound;
    [SerializeField] private int _CurrentRound;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _CurrentRound = _StartingRound;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U)) //When there is no enemy left in the scene
        {
            EndOfRound();
        }
    }

    private void EndOfRound()
    {
        _CurrentRound++;
        BossRround();
    }

    private void BossRround()
    {
        int _BossRound;
        _BossRound = _CurrentRound / 10;


        if (_CurrentRound % 10 == 0)
        {
            Debug.Log("Boss Round");
            // Call function to spawn boss next round
        }
    }

   




}
