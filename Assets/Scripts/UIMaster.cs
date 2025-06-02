using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UIMaster : MonoBehaviour
{
    public string sceneName = "GameScene";
    public GameObject buttonPrefab;

    private Transform UIMasterCanvas;
    private GameMasterCanvas UIMasterCanvasScript;
    private GameMaster GameMaster;
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
        CreateButtons(new string[] { "buttonLevel" });
    }

    public void CreateButtons(string[] buttons)
    {
        string label;
        UnityAction action;

        foreach (string button in buttons)
        {
            switch (button)
            {
                case "buttonRetry":
                    label = "Réessayer";
                    action = () => SwitchToGame();
                    break;
                case "buttonHome":
                    label = "Accueil";
                    action = () =>
                    {
                        MakeHome();
                    };
                    break;
                case "buttonLevel":
                    label = "Niveau 1";
                    action = () => {
                        Debug.Log("Clic sur bouton : " + label);
                        SwitchToGame();
                    };
                    break;
                default:
                    label = "";
                    action = null;
                    break;
            }

            if (label == "") continue;

            Button btn = Instantiate(buttonPrefab, UIMasterCanvas).GetComponent<Button>();
            ButtonPrefabController btnController = btn.GetComponent<ButtonPrefabController>();

            // Changement du label
            btnController.label.text = label;

            // Action onClick du bouton
            btn.onClick.AddListener(action);
            Debug.Log("Listener ajouté au bouton : " + label);

            // Mettre le bouton en enfant du buttonsContainer
            btn.transform.SetParent(UIMasterCanvasScript.buttonsContainer.transform, false);
        }

    }
    public void SwitchToGame()
    {
        Debug.Log("test");
        SceneManager.LoadScene(sceneName);
    }

    public void MakeHome()
    {
        GameMaster.SwitchToHome();
    }
}
