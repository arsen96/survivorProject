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
   


    public void SwitchToHome()
    {
        SceneManager.LoadScene(sceneName);
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