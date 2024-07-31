using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISave
{
    void LoadProfile(PlayerProfile player);

    void SaveProfile(PlayerProfile player);   
}
