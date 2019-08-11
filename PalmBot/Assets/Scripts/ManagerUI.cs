using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerUI : MonoBehaviour
{
    public GameObject nextLevelButton;

    public void OnRestartPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Level restarted");
    }

    public void OnNextLevelPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ShowNextLevelButton(bool showing)
    {
        nextLevelButton.SetActive(showing);
    }

    public void OnBackButtonPressed()
    {
        SceneManager.LoadScene("SelectLevel_" + gameObject.GetComponent<GameController>().levelSectionID);
    }
}
