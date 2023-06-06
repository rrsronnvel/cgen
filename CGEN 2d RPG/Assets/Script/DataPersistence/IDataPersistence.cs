using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{
    void LoadData(GameData data, bool isRestarting);

    void SaveData(ref GameData data);


}
