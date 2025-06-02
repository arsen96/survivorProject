using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeScript : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject homeBgPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadHome()
    {
        Image homeBG = Instantiate(homeBgPrefab, transform).GetComponent<Image>();
        TextMeshProUGUI homeLabel = Instantiate(homeBgPrefab, transform).GetComponent<TextMeshProUGUI>();

        Button buttonLevel = Instantiate(buttonPrefab, transform).GetComponent<Button>();
        buttonLevel.transform.SetParent(homeBG.transform, false);

        ButtonPrefabController buttonLevelController = buttonLevel.GetComponent<ButtonPrefabController>();

        if (buttonLevelController != null)
        {
            buttonLevelController.label.text = "Niveau 1";
        }

        buttonLevel.onClick.AddListener(handleNewLevel);
    }

    public void handleNewLevel()
    {
        Debug.Log("Tu as choisi un niveau !");
    }
}
