using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script handles the functionality of the Awards window within the Main Menu screen.
/// It is loaded alongside MainMenuController.cs
/// </summary>
public class AwardsController : MonoBehaviour
{
    /// <summary>
    /// These variables are "not-yet-earned" award badges
    /// </summary>
    [SerializeField] GameObject xEasyT;
    [SerializeField] GameObject xEasyA;
    [SerializeField] GameObject xMedT;
    [SerializeField] GameObject xMedA;
    [SerializeField] GameObject xHardT;
    [SerializeField] GameObject xHardA;
    [SerializeField] GameObject xExpT;
    [SerializeField] GameObject xExpA;

    /// <summary>
    /// These variables are earned award badges
    /// </summary>
    [SerializeField] GameObject EasyT;
    [SerializeField] GameObject EasyA;
    [SerializeField] GameObject MedT;
    [SerializeField] GameObject MedA;
    [SerializeField] GameObject HardT;
    [SerializeField] GameObject HardA;
    [SerializeField] GameObject ExpT;
    [SerializeField] GameObject ExpA;

    /// <summary>
    /// These variables are used to store the award badge GameObjects into their respective categories
    /// </summary>
    private Dictionary<string, GameObject> notEarnedAwards;
    private Dictionary<string, GameObject> earnedAwards;

    /// <summary>
    /// Unity calls this method automatically when the MainMenu screen is first loaded.
    /// </summary>
    void Start()
    {
        PopulateCheckerDictionaries();
    }

    /// <summary>
    /// Unity calls this method continuously, once per frame.
    /// It ensures that the awards menu is up to date.
    /// </summary>
    void Update()
    {
        AwardChecker();
    }

    /// <summary>
    /// Populates the checker dictionaries with their respective GameObjects, this corresponds directly to the PlayerProfile awards dictionary.
    /// Future versions could reference an external AwardsFramework config file.
    /// </summary>
    void PopulateCheckerDictionaries()
    {
        notEarnedAwards = new Dictionary<string, GameObject>
        {
            { "0T", xEasyT },
            { "0A", xEasyA },
            { "1T", xMedT },
            { "1A", xMedA },
            { "2T", xHardT },
            { "2A", xHardA },
            { "3T", xExpT },
            { "3A", xExpA }
        };

        earnedAwards = new Dictionary<string, GameObject>
        {
            { "0T", EasyT },
            { "0A", EasyA },
            { "1T", MedT },
            { "1A", MedA },
            { "2T", HardT },
            { "2A", HardA },
            { "3T", ExpT },
            { "3A", ExpA }
        };
    }

    /// <summary>
    /// Checks if the player has earned that award and displays the correct badge icon accordingly.
    /// </summary>
    void AwardChecker()
    {
        foreach (var award in SaveManager.instance.player.awards)
        {
            string key = award.Key;
            bool earned = award.Value;

            if (earned)
            {
                earnedAwards[key].SetActive(true);
                notEarnedAwards[key].SetActive(false);
            }
        }
    }
}
