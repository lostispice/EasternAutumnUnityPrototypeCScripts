using UnityEngine;

/// <summary>
/// This script handles the modern flags displayed (stamped) on the mail item when the player selects an answer.
/// It is loaded alongside GameController.cs, MessagesController.cs, MailController.cs and SendSheetController.cs.
/// When the player selects an answer nation, a modern-day flag appears on the mail item via Unity hooks.
/// </summary>
public class StampController : MonoBehaviour
{
    /// <summary>
    /// Resets the flag stamp on the mail item.
    /// It checks the toggle-group for any active (i.e. checked) boxes and resets them to inactive (unchecked).
    /// </summary>
    public void ResetStamp()
    {
        for (int n = 0; n < transform.childCount; n++)
        {
            transform.GetChild(n).gameObject.SetActive(false);
        }
    }
}
