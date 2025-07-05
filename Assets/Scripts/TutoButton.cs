using UnityEngine;

public class TutoButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // public void OpenModal(){
    //     GameMasterCanvas.instance.gameOverPanel.SetActive(true);
    // }

    // public void CloseModal(){
    //     GameMasterCanvas.instance.gameOverPanel.SetActive(false);
    // }

    public void ToggleModal(){
        GameMasterCanvas.instance.gameOverPanel.SetActive(!GameMasterCanvas.instance.gameOverPanel.activeSelf);
    }
}
