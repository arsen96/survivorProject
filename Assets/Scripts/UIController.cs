using UnityEngine;

public class UIController : MonoBehaviour
{

    public static UIController instance;
    public LevelUpSelectionButton[] levelUpButtons;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject levelUpPanel;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
