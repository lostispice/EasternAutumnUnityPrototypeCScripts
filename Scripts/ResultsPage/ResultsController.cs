using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script handles all functionality within the Results screen.
/// </summary>
public class ResultsController : MonoBehaviour, ISave
{
    /// <summary>
    /// This variable holds the player's name.
    /// </summary>
    [SerializeField] string playerName;

    /// <summary>
    /// These variables hold the Gameplay session values
    /// </summary>
    [SerializeField] int difficulty;
    [SerializeField] int score;
    [SerializeField] int lifeCount;
    [SerializeField] int targetMin;
    [SerializeField] int targetComm;
    [SerializeField] int targetAward;

    /// <summary>
    /// These variables are used to determines if player passed (true) or failed (false) their respective objectives
    /// </summary>
    [SerializeField] bool targetPass;
    [SerializeField] bool commendationPass;
    [SerializeField] bool awardPass;

    /// <summary>
    /// These variables are the textboxes on the report sheet to be updated by the script
    /// </summary>
    [SerializeField] TextMeshProUGUI dateText;
    [SerializeField] TextMeshProUGUI playerHeader;
    [SerializeField] TextMeshProUGUI playerSubHeader;
    [SerializeField] TextMeshProUGUI playerScore;
    [SerializeField] TextMeshProUGUI targetMinText;
    [SerializeField] TextMeshProUGUI targetCommText;
    [SerializeField] TextMeshProUGUI targetAwardText;
    [SerializeField] TextMeshProUGUI remarksText;

    /// <summary>
    /// These variables are Cross/Tick symbols used to report if player has met their respective targets
    /// </summary>
    [SerializeField] GameObject commMarkPass;
    [SerializeField] GameObject commMarkFail;
    [SerializeField] GameObject awardMarkPass;
    [SerializeField] GameObject awardMarkFail;

    /// <summary>
    /// These variables are buttons used to start new gameplay sessions
    /// </summary>
    [SerializeField] GameObject retryLevel;
    [SerializeField] GameObject nextLevel;


    /// <summary>
    /// Unity calls this method automatically when the Results screen is first loaded.
    /// These methods populate the contents of the results "page", including which buttons or objects are visible
    /// </summary>
    void Start()
    {
        LoadProfile(SaveManager.instance.player);
        RetrieveValues();
        ResultCheckerTarget();
        PopulateReport();        
        SaveProfile(SaveManager.instance.player); // Saves results to the active player profile.        
        SaveManager.instance.SaveProfile(); // Saves profile changes to JSON save file
    }

    /// <summary>
    /// Saves the player's award progress to their profile.
    /// Uses the ISave interface.
    /// </summary>
    /// <param name="player"></param>
    public void SaveProfile(PlayerProfile player)
    {
        player.awards[difficulty + "T"] = targetPass;
        player.awards[difficulty + "A"] = awardPass;
        player.extraLives[difficulty] = commendationPass;
    }

    /// <summary>
    /// Retreives the player's name from their profile.
    /// Uses the ISave interface.
    /// </summary>
    /// <param name="player"></param>
    public void LoadProfile(PlayerProfile player) 
    {
        playerName = player.playerName;
    }

    /// <summary>
    /// Retrieve values from game session.
    /// </summary>
    public void RetrieveValues()
    {
        difficulty = PlayerPrefs.GetInt("difficulty");
        score = PlayerPrefs.GetInt("score");
        lifeCount = PlayerPrefs.GetInt("lifeCount");
        targetMin = PlayerPrefs.GetInt("targetMin");
        targetComm = PlayerPrefs.GetInt("targetComm");
        targetAward = PlayerPrefs.GetInt("targetAward");
    }

    /// <summary>
    /// Determines if the player passed or failed the session overall.
    /// This determines which messages and buttons are displayed to the player.
    /// </summary>
    public void ResultCheckerTarget()
    {
        if (score >= targetMin && lifeCount > 0)
        {
            targetPass = true;
            NextButtonVisiblity();
            ResultCheckerCommendation(); // Checks secondary objectives
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

    /// <summary>
    /// Prevents the Next Level button from appearing if the player is already at the final level (i.e. highest difficulty)
    /// </summary>
    public void NextButtonVisiblity()
    {
        if (difficulty < 3)
        {
            nextLevel.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Checks if the player has met their Commendation gameplay target.
    /// </summary>
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

    /// <summary>
    /// Checks if player has met their Award gameplay target.
    /// </summary>
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

    /// <summary>
    /// Populates Results textboxes with gameplay statistics 
    /// </summary>& results
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

    /// <summary>
    /// Determines which gameplay date is shown.
    /// This is dependent on the difficulty level.
    /// </summary>
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

    /// <summary>
    /// Checks which symbol (tick/cross) should be displayed for the commendation section.
    /// </summary>
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

    /// <summary>
    /// Checks which symbol (tick/cross) should be displayed for the award section.
    /// </summary>
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

    /// <summary>
    /// Populates the Remarks box with comments.
    /// Comments are dependent on the player's performance.
    /// </summary>
    public void ResultRemarks()
    {
        if (awardPass)
        {            
            remarksText.text = SaveManager.instance.player.playerName + " has proven themselves to be an exemplary employee. \n Loppu hyvin, kaikki hyvin! - Karhu"; // Finnish - "Allís well that ends well!"
        }
        else if (commendationPass)
        {            
            remarksText.text = SaveManager.instance.player.playerName + " has exceeded expectations admirably. \n Hiljaa hyv‰ tulee. - Karhu"; // Finnish - "Slowly itíll go well."
        }
        else if (targetPass)
        {            
            remarksText.text = SaveManager.instance.player.playerName + " conducts their duties acceptably, if unremarkably. \n Ei kukaan ole sepp‰ syntyess‰‰n. - Karhu"; // Finnish - "No one is born a smith."
        }
        else
        {            
            remarksText.text = SaveManager.instance.player.playerName + " appears unfit for this role and has been marked for termination. \n Ei auta itku markkinoilla. - Karhu"; // Finnish - "Itís no use crying at the marketplace."
        }
    }

    /// <summary>
    /// Returns to the MainMenu screen.
    /// </summary>
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Returns to the GameplaySession screen.
    /// Starts a game session under the same parameters (i.e. "retry").
    /// </summary>
    public void RetrySession()
    {
        SceneManager.LoadScene("GameplaySession");
    }

    /// <summary>
    /// Returns to the GameplaySession screen.
    /// Starts a game session at the next difficulty level.
    /// </summary>
    public void NextLevel()
    {
        difficulty++;
        PlayerPrefs.SetInt("difficulty", difficulty);
        SceneManager.LoadScene("GameplaySession");
    }
}
