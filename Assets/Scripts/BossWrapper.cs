using UnityEngine;

public class BossWrapper : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Victory()
    {
        GameObject gameMaster = GameObject.FindGameObjectWithTag("GameController");
        gameMaster.GetComponent<GameMaster>().Finish("Mission accomplie !");
    }
}
