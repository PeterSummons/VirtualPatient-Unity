using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public Button[] lvlButtons;

    private void Awake()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt", 3);

        if (PlayerPrefs.GetInt("levelAt") >= 4)
        {
            levelAt = PlayerPrefs.GetInt("levelAt");
        }

        for (int i = 0; i < lvlButtons.Length; i++)
        {
            if (i + 1 > levelAt)
                lvlButtons[i].interactable = false;
        }
    }
    public void LoadScene(int level)
    {
        PlayerPrefs.SetInt("levelAt", level);
        SceneManager.LoadScene(level);
    }
}
