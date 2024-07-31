using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardsController : MonoBehaviour
{
    // Award badges, Not-Earned (x)
    [SerializeField] GameObject xEasyT;
    [SerializeField] GameObject xEasyA;
    [SerializeField] GameObject xMedT;
    [SerializeField] GameObject xMedA;
    [SerializeField] GameObject xHardT;
    [SerializeField] GameObject xHardA;
    [SerializeField] GameObject xExpT;
    [SerializeField] GameObject xExpA;

    // Award badges, Earned
    [SerializeField] GameObject EasyT;
    [SerializeField] GameObject EasyA;
    [SerializeField] GameObject MedT;
    [SerializeField] GameObject MedA;
    [SerializeField] GameObject HardT;
    [SerializeField] GameObject HardA;
    [SerializeField] GameObject ExpT;
    [SerializeField] GameObject ExpA;

    // Dictionary used to store the GameOjects
    private Dictionary<string, GameObject> notEarnedAwards;
    private Dictionary<string, GameObject> earnedAwards;

    // Start is called before the first frame update
    void Start()
    {
        PopulateCheckerDictionaries();
    }

    // Update is called once per frame
    void Update()
    {
        AwardChecker();
    }

    // Populates the checker dictionaries with GameObjects, corresponding to the PlayerProfile awards dictionary
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

    // Checks if the player has earned that award and displays the correct badge icon
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
