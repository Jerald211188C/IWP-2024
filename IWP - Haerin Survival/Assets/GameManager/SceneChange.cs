using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    private void Start()
    {
        HandleCursorState();
    }

    public void GoStart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void GoEnd()
    {
        SceneManager.LoadScene("End");
    }

    public void GoMain()
    {
        SceneManager.LoadScene("Start");
    }

    private void HandleCursorState()
    {
        // Check the current active scene
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        if (currentSceneName == "End" || (currentSceneName == "Start"))
        {
            // Unlock and make the cursor visible in the End scene
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            // Lock the cursor and make it invisible in other scenes
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

}
