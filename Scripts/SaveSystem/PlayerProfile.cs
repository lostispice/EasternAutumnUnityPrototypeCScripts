
/// <summary>
/// This class contains all long-term player data (including the player's name and any lives or awards earned) and is referenced throughout the game.
/// The game saves an instance of this class as a JSON file to the player's local drive using SaveFileHandler.cs, which can later be retrieved for use.
/// Future versions could include more data such as Options modifiers and unique player IDs for a multiple-slot save system.
/// </summary>
[System.Serializable]
public class PlayerProfile
{
    /// <summary>
    /// This variable holds the player's name.
    /// </summary>
    public string playerName;

    /// <summary>
    /// These variables are dictionaries containing the player's awards/extra lives, mapped to their respective difficulty levels.
    /// SerializableDictionary.cs is used to ensure they are stored to JSON in the correct format.
    /// </summary>
    public SerializableDictionary<string, bool> awards;
    public SerializableDictionary<int, bool> extraLives;

    /// <summary>
    /// Constructor for this class.
    /// Future versions could reference external AwardsFramework and ExtraLives config files.
    /// </summary>
    public PlayerProfile()
    {
        this.playerName = "";
        this.awards = new SerializableDictionary<string, bool> // String uses a simple code = Difficulty Number (0 - 3) + T (Target) or A (Award).
        {
            { "0T", false },
            { "0A", false },
            { "1T", false },
            { "1A", false },
            { "2T", false },
            { "2A", false },
            { "3T", false },
            { "3A", false }
        };
        this.extraLives = new SerializableDictionary<int, bool> // Int refers to difficulty level this extra life was earned.
        {
            { 0, false },
            { 1, false },
            { 2, false },
            { 3, false }
        };
    }
}
