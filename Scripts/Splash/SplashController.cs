using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script handles all functionality within the Splash screen.
/// </summary>
public class SplashController : MonoBehaviour
{
    /// <summary>
    /// This handles the functionality of the "Start" button.
    /// </summary>
    public void Launch()
    {
        SceneManager.LoadScene("ProfileSelect");
    }
}
