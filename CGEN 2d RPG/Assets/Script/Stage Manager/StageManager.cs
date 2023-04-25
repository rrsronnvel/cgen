using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour, IDataPersistence
{
    public static StageManager instance;

    public List<bool> stageCompletionStatus;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void LoadData(GameData data, bool isRestarting)
    {
        stageCompletionStatus = data.stageCompletionStatus;
    }

    public void SaveData(ref GameData data)
    {
        data.stageCompletionStatus = stageCompletionStatus;
    }

    public void SetStageCompletionStatus(int stageNumber, bool completionStatus)
    {
        stageCompletionStatus[stageNumber - 1] = completionStatus;
    }

    public bool GetStageCompletionStatus(int stageNumber)
    {
        return stageCompletionStatus[stageNumber - 1];
    }

    public bool IsStageUnlocked(int stageNumber)
    {
        if (stageNumber == 1)
        {
            return true;
        }
        return GetStageCompletionStatus(stageNumber - 1);
    }
}
