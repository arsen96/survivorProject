using UnityEngine;
using TMPro;


public class UIController : MonoBehaviour
{

    public static UIController instance;
    public LevelUpSelectionButton[] levelUpButtons;
    public MenuEnd[] menuEnd;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject levelUpPanel;
    public TMP_Text titleText;
    public GameObject gameOverPanel;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

     public void UpdateText(string text)
    {
        titleText.text = text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
