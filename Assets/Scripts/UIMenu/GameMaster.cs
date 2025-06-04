using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections.Generic;
using System.Collections;


public class GameMaster : UILevelBtnModel
{
    public string sceneName = "UIScene";
    // public GameObject cardPrefab;

    private Transform gameMasterCanvas;
    private GameMasterCanvas gameMasterCanvasScript;
    private UIMaster UIMaster;

    [HideInInspector]
    public SpawnController spawnController;

    void Start()
    {
        spawnController = GetComponent<SpawnController>();
        gameMasterCanvas = transform.GetChild(0).transform;
        gameMasterCanvasScript = gameMasterCanvas.GetComponent<GameMasterCanvas>();

        if (gameObject.GetComponent<UIMaster>() != null)
        {
            UIMaster = gameObject.GetComponent<UIMaster>();
        }
    }

    void Update()
    {
    
    }

    public void Finish(string label)
    {
        spawnController.DestroyEnemies();
        Debug.Log("Dans le Finish " + label);
        EnablePopupLastScreen(label);
    }

    private void EnablePopupLastScreen(string label)
    {
        // Changer le label du popup
        gameMasterCanvasScript.title.text = label;

        if(label.StartsWith("Perdu")){
            PlayerPrefs.SetInt("perdu", 1);
        }

        // Créer les boutons
        List<UILevelBtnModel> buttons = new List<UILevelBtnModel>();
        buttons.Add(new UILevelBtnModel { btn="buttonRetry",isDone=true,chooseLevel=false, isAccessible = false});
        buttons.Add(new UILevelBtnModel { btn="buttonHome",isDone=true,chooseLevel=false, isAccessible = false});
        UIMaster.CreateButtons(buttons);


        // Désactivé le header
        gameMasterCanvasScript.header.SetActive(false);

        // Afficher le titre et le popup
        gameMasterCanvasScript.title.enabled = true;
        gameMasterCanvasScript.popup.SetActive(true);
        Debug.Log("Dans le EnablePopupLastScreen " + label);
    }

    public void SwitchToHome()
    {
        SceneManager.LoadScene(sceneName);
    }



    public void RestartGame()
    {   
        if (spawnController != null)
        {
            // Faire progresser au niveau suivant si disponible
            PlayerHealthController.instance.StartGame();
            spawnController.RestartLevel();
            // Cacher le menu
            HidePopup();
            
            // Réactiver le header si nécessaire
            gameMasterCanvasScript.header.SetActive(true);
        }
    }


    public void GoToNextLevel()
    {
        Debug.Log("GoToNextLevel");
        if (spawnController != null)
        {
            spawnController.GoToNextLevel();
            HidePopup();
        }
        else
        {
            Debug.LogError("SpawnController non trouvé !");
        }
    }


    private void HidePopup()
    {
        gameMasterCanvasScript.title.enabled = false;
        gameMasterCanvasScript.popup.SetActive(false);
        
        // Détruire les boutons créés
        foreach (Transform child in gameMasterCanvasScript.buttonsContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }
}