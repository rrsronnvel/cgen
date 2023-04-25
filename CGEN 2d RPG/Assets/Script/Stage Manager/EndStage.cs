using UnityEngine;
using UnityEngine.SceneManagement;
public class EndStage : MonoBehaviour, Interactable
{
    public void Interact()
    {
        StageManager.instance.SetStageCompletionStatus(SceneManager.GetActiveScene().buildIndex, true);
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadScene("SampleScene");
    }
}
