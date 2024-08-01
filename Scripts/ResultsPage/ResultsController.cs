using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultsController : MonoBehaviour, ISave
{
    // Player name
    [SerializeField] string playerName;

    // Gameplay session values
    [SerializeField] int difficulty;
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
    [SerializeField] TextMeshProUGUI dateText;
    [SerializeField] TextMeshProUGUI playerHeader;
    [SerializeField] TextMeshProUGUI playerSubHeader;
    [SerializeField] TextMeshProUGUI playerScore;
    [SerializeField] TextMeshProUGUI targetMinText;
    [SerializeField] TextMeshProUGUI targetCommText;
    [SerializeField] TextMeshProUGUI targetAwardText;
    [SerializeField] TextMeshProUGUI remarksText;

    // Cross/Tick symbols used to report if player has met target, .SetActive(true);
    [SerializeField] GameObject commMarkPass;
    [SerializeField] GameObject commMarkFail;
    [SerializeField] GameObject awardMarkPass;
    [SerializeField] GameObject awardMarkFail;

    // New gameplay sessions buttons
    [SerializeField] GameObject retryLevel;
    [SerializeField] GameObject nextLevel;


    // Start is called before the first frame update
    void Start()
    {
        LoadProfile(SaveManager.instance.player);
        RetrieveValues();
        ResultCheckerTarget();
        PopulateReport();        
        SaveProfile(SaveManager.instance.player); // Saves results to the active player profile.        
        SaveManager.instance.SaveProfile(); // Saves profile changes to JSON save file
    }

    // Update is called once per frame
    void Update()
    {
    }

    // (ISave) Used for saving the player's award progress 
    public void SaveProfile(PlayerProfile player)
    {
        player.awards[difficulty + "T"] = targetPass;
        player.awards[difficulty + "A"] = awardPass;
        player.extraLives[difficulty] = commendationPass;
    }

    // (ISave) Used for retreiving the player's name
    public void LoadProfile(PlayerProfile player) 
    {
        playerName = player.playerName;
    }

    // Retrieve values from game session
    public void RetrieveValues()
    {
        difficulty = PlayerPrefs.GetInt("difficulty");
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
            NextButtonVisiblity();
            // checks secondary objectives
            ResultCheckerCommendation();
            ResultCheckerAward();
        }
        else 
        { 
            targetPass = false;
            retryLevel.gameObject.SetActive(true);
            commendationPass = false;
            awardPass = false;
        }
    }

    // Prevents the Next Level button from appearing if the player is already at the last level
    public void NextButtonVisiblity()
    {
        if (difficulty < 3)
        {
            nextLevel.gameObject.SetActive(true);
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
        DifficultyDate();
        playerScore.text = score.ToString();
        targetMinText.text = targetMin.ToString();
        targetCommText.text = targetComm.ToString();
        targetAwardText.text = targetAward.ToString();
        MarkCommendation();
        MarkAward();
        ResultRemarks();
    }

    public void DifficultyDate()
    {
        if (difficulty == 0)
        {
            dateText.text = "1989";
        }
        if (difficulty == 1)
        {
            dateText.text = "1992";
        }
        if (difficulty == 2)
        {
            dateText.text = "1994";
        }
        if (difficulty == 3)
        {
            dateText.text = "1996";
        }
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
            remarksText.text = SaveManager.instance.player.playerName + " has proven themselves to be an exemplary employee. \n Loppu hyvin, kaikki hyvin! - Karhu";
        }
        else if (commendationPass)
        {
            // Finnish - "Slowly itíll go well."
            remarksText.text = SaveManager.instance.player.playerName + " has exceeded expectations admirably. \n Hiljaa hyv‰ tulee. - Karhu";
        }
        else if (targetPass)
        {
            // Finnish - "No one is born a smith."
            remarksText.text = SaveManager.instance.player.playerName + " conducts their duties acceptably, if unremarkably. \n Ei kukaan ole sepp‰ syntyess‰‰n. - Karhu";
        }
        else
        {
            // Finnish - "Itís no use crying at the marketplace."
            remarksText.text = SaveManager.instance.player.playerName + " appears unfit for this role and has been marked for termination. \n Ei auta itku markkinoilla. - Karhu";
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

    public void NextLevel()
    {
        difficulty++;
        PlayerPrefs.SetInt("difficulty", difficulty);
        SceneManager.LoadScene("GameplaySession");
    }
}
