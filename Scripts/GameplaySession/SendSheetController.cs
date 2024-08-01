using UnityEngine;

public class SendSheetController : MonoBehaviour
{

    // Handles new mail item animations, placeholer values
    [SerializeField] bool newMailRequest = false;
    // Item used to "move" into view using physics, replaced with a "teleporting" sheet as two objects moving on screen simultaneously can be disorientating
    /*public Vector2 destinationStart = new Vector2(-3, 6);*/
    public Vector2 destinationNew = new Vector2(-3, -1);

    // Handles mail item being "sent off(screen)"
    [SerializeField] bool sendMail = false;
    [SerializeField] Vector2 destinationSend = new Vector2(10, 0);

    // Start is called before the first frame update
    void Start()
    {
    }
    
    // Update is called once per frame
    void Update()
    {
        if (newMailRequest)
        {
            transform.position = destinationNew;
        }
        if (sendMail)
        {
            transform.position = Vector2.Lerp(transform.position, destinationSend, Time.deltaTime);
        }
    }


    // Moves the object into the player's FOV
    public void NewMailRequested()
    {
        // No longer required now that object "teleports" instead of moving
        // transform.position = destinationStart;
        newMailRequest = true;
        sendMail = false;
    }

    // Sends the object offscreen beyond the player's FOV
    public void SendMailOut()
    {
        sendMail = true;
        newMailRequest = false;
    }

}
