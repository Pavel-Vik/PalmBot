using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int levelsSectionID;

    public void OnLevelButtonPressed(int levelID)
    {
        SceneManager.LoadScene("Level_" + levelsSectionID + "-" + levelID);
    }

    public void GoToSections()
    {
        SceneManager.LoadScene("SelectSection");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToTheSection(int sectionID)
    {
        SceneManager.LoadScene("SelectLevel_" + sectionID);
    }
}
