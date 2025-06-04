using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using TMPro;
public class UIMaster : UILevelBtnModel
{
    public string sceneName = "SampleScene";
    public GameObject buttonPrefab;

    private Transform UIMasterCanvas;
    private GameMasterCanvas UIMasterCanvasScript;
    private GameMaster GameMaster;

    public TMP_FontAsset font;

    [System.Serializable]
    public class LevelsInfo
    {
        public int levelsDone;
        public int lengthTotal;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIMasterCanvas = transform.GetChild(0).transform;
        UIMasterCanvasScript = UIMasterCanvas.GetComponent<GameMasterCanvas>();

        if (gameObject.GetComponent<GameMaster>() != null)
        {
            GameMaster = gameObject.GetComponent<GameMaster>();
        }
        if (SceneManager.GetActiveScene().name == "UIScene")
        {
            CreateHome();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateHome()
    {
        string levelsInfo = PlayerPrefs.GetString("levelsInfo", "");
        int levelsDone = 0;
        int lengthTotal = 0;

        if (!string.IsNullOrEmpty(levelsInfo))
        {
            Debug.Log("levelsInfo: " + levelsInfo);
            try
            {
                var levelsInfoJson = JsonUtility.FromJson<LevelsInfo>(levelsInfo);
                if (levelsInfoJson != null)
                {
                    levelsDone = levelsInfoJson.levelsDone;
                    lengthTotal = levelsInfoJson.lengthTotal;
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Erreur lors de la désérialisation des données de niveaux: " + e.Message);
                // Réinitialiser en cas d'erreur
                levelsDone = 0;
                lengthTotal = 0;
            }
        }

        if (PlayerPrefs.GetInt("perdu", 0) == 1)
        {
            levelsDone = Mathf.Max(0, levelsDone - 1);
            PlayerPrefs.DeleteKey("perdu");
            PlayerPrefs.Save(); // Sauvegarder immédiatement après suppression
        }

        List<UILevelBtnModel> buttons = new List<UILevelBtnModel>();
        int highestLevelDone = PlayerPrefs.GetInt("highestLevelDone");
        Debug.Log("highestLevelDone after : " + highestLevelDone);
        for (int i = 0; i <= lengthTotal; i++)
        {
            if(highestLevelDone > 0){
                levelsDone = highestLevelDone;
            }
            bool isDone = i < levelsDone; 
            bool isCurrent = i == levelsDone;
            bool isAccessible = i == levelsDone + 1;
           

            
            buttons.Add(new UILevelBtnModel { btn = "buttonLevel " + i, isDone = isDone, isCurrent = isCurrent, isAccessible = isAccessible, chooseLevel = true });
        }

        CreateButtons(buttons);
    }

    public void CreateButtons(List<UILevelBtnModel> buttons)
    {
        // Nettoyer les boutons existants avant d'en créer de nouveaux
        foreach (Transform child in UIMasterCanvasScript.buttonsContainer.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var button in buttons)
        {
            string label = "";
            UnityAction action = null;

            if (button.chooseLevel)
            {
                int levelIndex = int.Parse(button.btn.Replace("buttonLevel ", ""));
                label = "Niveau " + (levelIndex + 1);
                action = () => SwitchToGame(levelIndex);
            }
            else if (button.btn == "buttonRetry")
            {
                label = "Rejouer";
                action = () => GameMaster.RestartGame();
            }
            else if (button.btn == "buttonHome")
            {
                label = "Accueil";
                action = () => MakeHome();
                MakeHome();
            }


            if(button.chooseLevel){
                if (label == "") continue;

                Button btn = Instantiate(buttonPrefab, UIMasterCanvas).GetComponent<Button>();

                btn.interactable = true;

                if(button.chooseLevel){
                    btn.GetComponent<RectTransform>().sizeDelta = new Vector2(250, 100);
                    if(button.isDone || button.isCurrent){
                        btn.GetComponent<Image>().color = new Color(0, 1, 0, 0.3f); // Vert pour niveau terminé
                    }else if(button.isAccessible){
                        btn.GetComponent<Image>().color = new Color(1, 0.5f, 0, 0.3f);
                    }else {
                        btn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.3f); // Gris pour niveau verrouillé
                        btn.interactable = false; // Désactiver les niveaux non accessibles
                    }
                }else{
                    btn.GetComponent<RectTransform>().sizeDelta = new Vector2(150, 100);
                }
                ButtonPrefabController btnController = btn.GetComponent<ButtonPrefabController>();

                // Changement du label
                btnController.label.text = label;
                btnController.label.font = font;
                btnController.label.fontSize = 30;

                // Action onClick du bouton (seulement si le bouton est interactable)

                Debug.Log("btn.interactable: " + btn.interactable);
                if (btn.interactable)
                {
                    btn.onClick.AddListener(action);
                }

                // Mettre le bouton en enfant du buttonsContainer
                btn.transform.SetParent(UIMasterCanvasScript.buttonsContainer.transform, false);
            }   
        }
    }

    public void SwitchToGame(int levelIndex)
    {
        SaveLevelIndex(levelIndex);
        SceneManager.LoadScene(sceneName);
    }

    private void SaveLevelIndex(int levelIndex)
    {
        PlayerPrefs.SetInt("levelIndex", levelIndex);
        PlayerPrefs.Save(); // Forcer la sauvegarde
    }

    public void MakeHome()
    {
        if (GameMaster?.spawnController != null)
        {
            int currentLevelIndex = GameMaster.spawnController.GetCurrentLevelWaveGroup();
            int lengthTotal = GameMaster.spawnController.waves.Count;
            
            // Créer l'objet LevelsInfo correctement
            LevelsInfo levelsInfo = new LevelsInfo
            {
                levelsDone = currentLevelIndex,
                lengthTotal = lengthTotal - 1 // -1 car les index commencent à 0
            };
            
            string jsonData = JsonUtility.ToJson(levelsInfo);
            PlayerPrefs.SetString("levelsInfo", jsonData);
            PlayerPrefs.Save(); // Forcer la sauvegarde
            
            Debug.Log("Sauvegarde des données de niveaux: " + jsonData);
        }
        else
        {
            Debug.LogError("GameMaster ou spawnController est null");
        }
        
        GameMaster?.SwitchToHome();
    }

    // Méthode utilitaire pour réinitialiser la progression
    public void ResetProgress()
    {
        PlayerPrefs.DeleteKey("levelsInfo");
        PlayerPrefs.DeleteKey("levelIndex");
        PlayerPrefs.DeleteKey("perdu");
        PlayerPrefs.Save();
        Debug.Log("Progression réinitialisée");
    }

}