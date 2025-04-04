using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerXpController : MonoBehaviour
{
    public BarSliderController bar;

    public float currentXp = 0f, maxXp = 10f;
    public int level = 1;

    // Start is called before the first frame update
    void Start()
    {
        bar.max = maxXp;
        bar.label.text = "Nv " + level.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        bar.current = currentXp;
        if (currentXp >= maxXp)
        {
            level += 1;
            currentXp = currentXp - maxXp;
            maxXp *= 1.5f;
            if (bar != null)
            {
                bar.max = maxXp;
                bar.current = currentXp;
                bar.label.text = "Nv " + level.ToString();
            }
        }
    }
}