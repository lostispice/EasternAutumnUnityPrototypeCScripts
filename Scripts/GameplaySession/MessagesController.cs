using TMPro;
using UnityEngine;

/// <summary>
/// This script handles the "management messages" in the messages panel, used to inform the player of current events and the rules for the current session.
/// It is loaded alongside GameController.cs, MessagesController.cs, MailController.cs and SendSheetController.cs.
/// Note that Life-System messages are controlled by GameController.cs directly.
/// </summary>
public class MessagesController : MonoBehaviour, ISave
{
    [SerializeField] string playerName;
    [SerializeField] int difficulty;
    [SerializeField] TextMeshProUGUI bossPanel;

    /// <summary>
    /// Unity calls this method automatically when the GameSession screen is first loaded.
    /// </summary>
    void Start()
    {
        LoadProfile(SaveManager.instance.player);
        PopulateLetterPanel();
    }

    /// <summary>
    /// Retrieves the player's name.
    /// Uses the ISave interface.
    /// </summary>
    /// <param name="player"></param>
    public void LoadProfile(PlayerProfile player)
    {
        // Player name
        playerName = player.playerName;
    }

    /// <summary>
    /// Not used on this screen, changes are applied in the Results screen.
    /// Part of ISave interface. Prevents CS0535 error in VStudio.
    /// </summary>
    /// <param name="player"></param>
    public void SaveProfile(PlayerProfile player) { }

    /// <summary>
    /// Populates the Boss messages panel according to difficulty level.
    /// </summary>
    void PopulateLetterPanel()
    {
        bossPanel.text = "To: " + playerName + "\n\n";
        difficulty = PlayerPrefs.GetInt("difficulty");

        if (difficulty == 0) // Easy
        {
            bossPanel.text += "Welcome to your first day at Ruska! \n\n";
            bossPanel.text += "Our company is the lifeline between the West and the East, serving the postal needs of thousands of dignitaries, officials and citizens since the end of WW2. As a neutral nation, we serve a vital niche in these complicated times: a communication lifeline between the liberal west and the communist east. \n\n";
            bossPanel.text += "All mail from the western bloc is received here in the home office, which is then sent to one of our branches across the eastern bloc.  Your job here is to ensure each mail item goes to the correct national branch office respectively.  Our clients do no write country names on their addresses after that whole unpleasant Sevastopol incident back in '75. \n\n";
            bossPanel.text += "Your predecessor, Old Man Pekka, has told me that he's left his old cheat sheet behind for your reference. Use it but don't rely on it, there are only so many minutes in a day that you should be spending working not reading. \n\n";
            bossPanel.text += "Work fast, work efficiently and don't make mistakes. We've already lost the DDR, Czechoslovakia and Yugoslavia to our rival in Switzerland, and we don't need to lose any more. \n\n";
            bossPanel.text += "Good luck. \n\n";
            bossPanel.text += "PS: You may have heard in the news that there's been some unrest and protests lately in Eastern Europe. A few flags might change but as far as you should be concerned, your job should stay the same for the foreseeable future. Moscow has temporarily suspended all mail services for the Soviet Union, but we should expect things to return to normal eventually. \n\n";
        }
        else if (difficulty == 1) // Medium
        {
            bossPanel.text += "As you should know, the past few months have been incredibly tumultuous for Europe. The Supreme Soviet in Moscow has officially dissolved the USSR and it seems that the red star is finally falling across Eastern Europe. \n\n";
            bossPanel.text += "However, this does not mean that Ruska must follow. Our clients still have much work to do (perhaps more so in these exciting times) and our work will continue. After several years of dormancy, we have decided to reopen our services to the (now former) member republics of the Soviet Union. You will now receive mail items for countries from Central Asia to the Baltic Sea. Just be aware that many city names are currently being  localised or even changed entirely: \n\n";
            bossPanel.text += "We have received word that Leningrad in Russia has returned to its old name St. Petersburg, as well as the city of Leninakan (now Gyumri) in Armenia. Ukraine has also expressed a desire to Ukrainianise many of their city names and Kazakhstan has already done so with their capital Almaty (previously Alma-Alta). \n\n";
            bossPanel.text += "We expect these changes will take several years to take effect, though some of our clients may already switch to the new spelling. Use your common sense, and good luck. \n\n";
        }
        else if (difficulty == 2) // Hard
        {
            bossPanel.text += "As you may have heard, business for Ruska has been booming for the last two years. Mail activity has skyrocketed across the board and our staff have been busier than ever. Our operations have expanded greatly in this new and evolving Europe. \n\n";
            bossPanel.text += "It is my pleasure to inform you that you have been promoted and will now supervise the direction of mail for two branch offices in each country rather on just the capitals. You may expect to work slightly longer hours moving forward, but such is the price of success is it not? \n\n";
            bossPanel.text += "Do your job well, and the company does well. Expectations of you are high, but I have faith that you will deliver. Carry on. \n\n";
            bossPanel.text += "PS: You may also have heard rumours within the company that the economic situation on the ground in the local branch offices have been less than ideal since the introduction of economic shock therapy. This is irrelevant to your job as our (paying) clients are primarily based in the West, pay it no heed for we expect things to stabilise soon. \n\n";

        }
        else if (difficulty == 3) // Expert
        {
            bossPanel.text += "Per the management meeting, you should already be aware about plans to further expand the company’s operations to East Asia and North Africa. As such, there has been a shuffling of staff to accommodate these new operations resulting in a temporary shorthand of staff in the Eastern Europe-Central Asia department. \n\n";
            bossPanel.text += "Therefore, until further notice you will be responsible for mail management for all (3) branches throughout EECA theatre.  I should not need to remind you that our clients will continue to expect the same standard of service and precision expected of the company. I will continue to work with upper management to assign you more staff, but for the foreseeable future you and your team will likely be very busy for a few months. \n\n";
            bossPanel.text += "Keep calm, keep up the good work and good luck. \n\n";
            bossPanel.text += "PS: You may have heard about some unrest and unpleasantness in the Balkans these last couple of years, as well as that whole business in the Caucasus. For the time being, this should not affect our operations and we expect things to stabilise soon. \n\n";
        }
        else // Failsafe
        {
            bossPanel.text += "Error: Could not load text! \n\n";
        }
    }
}

