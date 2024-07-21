using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISave
{
    void LoadProfile(PlayerProfile profile);

    // Uses pass-by-reference to write data 
    void SaveProfile(ref PlayerProfile profile);   
}
