using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashController : MonoBehaviour
{
    public void Launch()
    {
        SceneManager.LoadScene("ProfileSelect");
    }
}