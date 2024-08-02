/*using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Save slot script, not used in this version of the game. TODO - Implement multiple profile/save slot system
public class SaveSlot : MonoBehaviour
{
    public string profileID;

    // Used by save slot "buttons"
    [SerializeField] GameObject emptySave;
    [SerializeField] GameObject gameSave;
    [SerializeField] TextMeshProUGUI saveName;

    public void ProfileChecker(PlayerProfile player)
    {
        // No save stored in this slot
        if (player == null)
        {
            emptySave.SetActive(true);
            gameSave.SetActive(false);
        }

        // Save file found in this slot
        else
        {
            emptySave.SetActive(false);
            gameSave.SetActive(true);
            saveName.text = player.playerName;
        }
    }
}
*/