using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultsController : MonoBehaviour, ISave
{
    // Foreign key values from GameController.cs
    [SerializeField] string playerName;
    [SerializeField] int score;
    [SerializeField] int lifeCount;
    [SerializeField] int targetMin;
    [SerializeField] int targetComm;
    [SerializeField] int targetAward;

    // Determines if player passed (1) or failed (0) their respective objectives
    [SerializeField] bool targetPass;
    [SerializeField] bool commendationPass;
    [SerializeField] bool awardPass;

    // Textboxes to be updated on the report sheet
    public TextMeshProUGUI playerHeader;
    public TextMeshProUGUI playerSubHeader;
    public TextMeshProUGUI playerScore;
    public TextMeshProUGUI targetMinText;
    public TextMeshProUGUI targetCommText;
    public TextMeshProUGUI targetAwardText;
    public TextMeshProUGUI remarksText;

    // Cross/Tick symbols used to report if player has met target, .SetActive(true);
    public GameObject commMarkPass;
    public GameObject commMarkFail;
    public GameObject awardMarkPass;
    public GameObject awardMarkFail;


    // Start is called before the first frame update
    void Start()
    {
        // TODO - Player's progress should be saved to their profile here
        RetrieveValues();
        ResultCheckerTarget();
        PopulateReport();
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Saves player progress (ISave)
    public void SaveProfile(ref PlayerProfile playerProfile)
    {
        playerProfile.target = this.targetPass;
        playerProfile.award = this.awardPass;
    }

    // From ISave
    public void LoadProfile(PlayerProfile playerProfile) { }

    // Retrieve values from game session
    public void RetrieveValues()
    {
        playerName = PlayerPrefs.GetString("playerName");
        score = PlayerPrefs.GetInt("score");
        lifeCount = PlayerPrefs.GetInt("lifeCount");
        targetMin = PlayerPrefs.GetInt("targetMin");
        targetComm = PlayerPrefs.GetInt("targetComm");
        targetAward = PlayerPrefs.GetInt("targetAward");
    }

    // Determines if the player passed or failed the session overall
    public void ResultCheckerTarget()
    {
        if (score >= targetMin && lifeCount > 0)
        {
            targetPass = true;
            // checks secondary objectives
            ResultCheckerCommendation();
            ResultCheckerAward();
        }
        else 
        { 
            targetPass = false;
            commendationPass = false;
            awardPass = false;
        }
    }

    // Checks if player met their Commendation gameplay target,
    public void ResultCheckerCommendation()
    {
        if (score >= targetComm)
        {
            commendationPass = true;
        }
        else
        {
            commendationPass = false;
        }

    }

    // Checks if player met their Award gameplay target,
    public void ResultCheckerAward()
    {
        if (score >= targetAward)
        {
            awardPass = true;
        }
        else
        {
            awardPass = false;
        }
    }

    // Populates textboxes with gameplay statistics & results
    public void PopulateReport()
    {
        playerHeader.text = "Staff Report: " + playerName;
        playerSubHeader.text = playerName + ": ";
        playerScore.text = score.ToString();
        targetMinText.text = targetMin.ToString();
        targetCommText.text = targetComm.ToString();
        targetAwardText.text = targetAward.ToString();
        MarkCommendation();
        MarkAward();
        ResultRemarks();
    }

    // Checks which symbol (tick/cross) should be displayed for commendation
    public void MarkCommendation()
    {
        if (commendationPass)
        {
            commMarkPass.SetActive(true);
            commMarkFail.SetActive(false);
        }
        else
        {
            commMarkPass.SetActive(false);
            commMarkFail.SetActive(true);
        }
    }

    // Checks which symbol (tick/cross) should be displayed for award
    public void MarkAward()
    {
        if (awardPass)
        {
            awardMarkPass.SetActive(true);
            awardMarkFail.SetActive(false);
        }
        else
        {
            awardMarkPass.SetActive(false);
            awardMarkFail.SetActive(true);
        }
    }

    // Populates the Remarks box with comments corresponding to the player's performance
    public void ResultRemarks()
    {
        if (awardPass)
        {
            // Finnish - "Allís well that ends well!"
            remarksText.text = playerName + " has proven themselves to be an exemplary employee. \n Loppu hyvin, kaikki hyvin! - Karhu";
        }
        else if (commendationPass)
        {
            // Finnish - "Slowly itíll go well."
            remarksText.text = playerName + " has exceeded expectations admirably. \n Hiljaa hyv‰ tulee. - Karhu";
        }
        else if (targetPass)
        {
            // Finnish - "No one is born a smith."
            remarksText.text = playerName + " conducts their duties acceptably, if unremarkably. \n Ei kukaan ole sepp‰ syntyess‰‰n. - Karhu";
        }
        else
        {
            // Finnish - "Itís no use crying at the marketplace."
            remarksText.text = playerName + " appears unfit for this role and has been marked for termination. \n Ei auta itku markkinoilla. - Karhu";
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RetrySession()
    {
        SceneManager.LoadScene("GameplaySession");
    }
}
