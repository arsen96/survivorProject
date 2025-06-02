using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameMaster : MonoBehaviour
{
    public string sceneName = "UIScene";
    public GameObject cardPrefab;

    private Transform gameMasterCanvas;
    private GameMasterCanvas gameMasterCanvasScript;
    private UIMaster UIMaster;

    void Start()
    {
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
        gameObject.GetComponent<SpawnController>().DestroyEnemies();
        EnablePopupLastScreen(label);
    }

    private void EnablePopupLastScreen(string label)
    {
        // Changer le label du popup
        gameMasterCanvasScript.title.text = label;

        // Créer les boutons
        UIMaster.CreateButtons(new string[] { "buttonRetry", "buttonHome" });


        // Désactivé le header
        gameMasterCanvasScript.header.SetActive(false);

        // Afficher le titre et le popup
        gameMasterCanvasScript.title.enabled = true;
        gameMasterCanvasScript.popup.SetActive(true);
    }

    public void SwitchToHome()
    {
        SceneManager.LoadScene(sceneName);
    }
}
