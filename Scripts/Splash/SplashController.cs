using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script manages functionality within the Splash screen.
/// </summary>
public class SplashController : MonoBehaviour
{
    /// <summary>
    /// Proceeds to the ProfileSelect screen.
    /// </summary>
    public void Launch()
    {
        SceneManager.LoadScene("ProfileSelect");
    }
}
