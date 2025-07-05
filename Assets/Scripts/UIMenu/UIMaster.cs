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
                levelsDone = 0;
                lengthTotal = 0;
            }
        }


        if (PlayerPrefs.HasKey("perdu") && PlayerPrefs.GetInt("perdu") == 1)
        {
            levelsDone = levelsDone - 1;
            // PlayerPrefs.DeleteKey("perdu");
            PlayerPrefs.Save(); 
        }
        Debug.Log("levelsDone apres: " + levelsDone);

        List<UILevelBtnModel> buttons = new List<UILevelBtnModel>();
        int highestLevelDone = PlayerPrefs.GetInt("highestLevelDone");

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
                }
                ButtonPrefabController btnController = btn.GetComponent<ButtonPrefabController>();

                // Changement du label
                btnController.label.text = label;
                btnController.label.font = font;
                btnController.label.fontSize = 30;

                // Action onClick du bouton (seulement si le bouton est interactable)

                if (btn.interactable)
                {
                    btn.onClick.AddListener(action);
                }

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
        PlayerPrefs.Save();
    }

   


}