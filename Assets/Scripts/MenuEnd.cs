using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class MenuEnd : UILevelBtnModel
{
   public TMP_Text titleText;
   public string sceneName = "UIScene";
   private SpawnController spawnController;

    [System.Serializable]
    public class LevelsInfo
    {
        public int levelsDone;
        public int lengthTotal;
    }

   public void Start()
   {
      spawnController = FindObjectOfType<SpawnController>();
   }

   public void UpdateText(string text)
   {
      titleText.text = text;
   }


    public void SwitchToHome()
    {

        int currentLevelIndex = spawnController.GetCurrentLevelWaveGroup();
        int lengthTotal = spawnController.waves.Count;
        
        LevelsInfo levelsInfo = new LevelsInfo
        {
            levelsDone = currentLevelIndex,
            lengthTotal = lengthTotal - 1
        };
        
        string jsonData = JsonUtility.ToJson(levelsInfo);
        PlayerPrefs.SetString("levelsInfo", jsonData);
        PlayerPrefs.Save(); 
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }


    public void RestartLevel()
    {   
            int currentLevel = spawnController.GetCurrentLevelWaveGroup();
            PlayerPrefs.SetInt("levelIndex", currentLevel);
            PlayerPrefs.SetString("restartingLevel", "true"); 
            PlayerPrefs.Save();
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
