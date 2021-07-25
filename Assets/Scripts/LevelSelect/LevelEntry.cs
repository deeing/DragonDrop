using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelEntry : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Text label for the level entry")]
    private TMP_Text levelLabel;
    [SerializeField]
    [Tooltip("Button to click to go to that level")]
    private Button levelButton;

    private string levelName;

    public void SetName(string name)
    {
        levelName = name;
        levelLabel.text = name;
    }

    public void SetupButtonCallback()
    {
        levelButton.onClick.AddListener(GoToLevel);
    }

    private void GoToLevel()
    {
        SceneChanger.ChangeScene(levelName);
    }
}
