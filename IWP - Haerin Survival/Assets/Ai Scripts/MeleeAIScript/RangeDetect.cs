using UnityEngine;

public class RangeDetect : MonoBehaviour
{
    private Transform _player;

    public bool InRange => _player;
    public Transform Player => _player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player = null;
        }
    }
}
