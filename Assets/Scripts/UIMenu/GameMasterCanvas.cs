using TMPro;
using UnityEngine;

public class GameMasterCanvas : MonoBehaviour
{
    public static GameMasterCanvas instance;
    public GameObject header;
    public BarSliderController bar;
    public GameObject main;
    public GameObject popup;
    public GameObject buttonsContainer;
    public TextMeshProUGUI title;

    public GameObject gameOverPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    public void DisplayTutoButton(){
        gameOverPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}