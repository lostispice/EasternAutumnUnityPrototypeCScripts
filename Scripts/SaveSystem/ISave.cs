
/// <summary>
/// This interface defines methods for loading and saving player data.
/// It is used by ProfileSelectController.cs, GameController.cs, MessagesController.cs and ResultsController.cs.
/// </summary>
public interface ISave
{
    /// <summary>
    /// Loads player data.
    /// </summary>
    /// <param name="player"></param>
    void LoadProfile(PlayerProfile player);

    /// <summary>
    /// Saves player data.
    /// </summary>
    /// <param name="player"></param>
    void SaveProfile(PlayerProfile player);   
}
