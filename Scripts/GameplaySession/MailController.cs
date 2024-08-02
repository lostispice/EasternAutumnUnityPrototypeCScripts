using UnityEngine;

/// <summary>
/// This script handles movement of the mail item within the GameplaySession screen.
/// It is loaded alongside GameController.cs, MessagesController.cs, StampController.cs and SendSheetController.cs.
/// Physics are used to simulate the mail item "arriving" on the player's desk (on-screen) and "leaving" the player's desk (off-screen).
/// </summary>
public class MailController : MonoBehaviour
{
    /// <summary>
    /// These variables are used for "new mail item" animations
    /// </summary>
    [SerializeField] bool newMailRequest = false;
    [SerializeField] Vector2 destinationStart = new Vector2(-10, 0);
    [SerializeField] Vector2 destinationNew = new Vector2(0, 0);

    /// <summary>
    /// These variables are used for "send mail item" animations
    /// </summary>
    [SerializeField] bool sendMail = false;
    [SerializeField] Vector2 destinationSend = new Vector2(100, 0);

    /// <summary>
    /// Unity calls this method continuously, once per frame.
    /// It is used to determine which position the mail item should be moving to.
    /// </summary>
    void Update()
    {
        if (newMailRequest)
        {
            transform.position = Vector2.Lerp(transform.position, destinationNew, Time.deltaTime);
        }
        if (sendMail)
        {
            transform.position = Vector2.Lerp(transform.position, destinationSend, Time.deltaTime);
        }
    }

    /// <summary>
    /// Moves the mail item into the player's FOV
    /// </summary>
    public void NewMailRequested()
    {
        transform.position = destinationStart; // "Teleports" the mail item to the Start position
        newMailRequest = true;
        sendMail = false;
    }

    /// <summary>
    /// Sends the mail item offscreen, beyond the player's FOV
    /// </summary>
    public void SendMailOut()
    {
        sendMail = true;
        newMailRequest = false;
    }
}
