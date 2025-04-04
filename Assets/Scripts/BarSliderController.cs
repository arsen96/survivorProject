using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarSliderController : MonoBehaviour
{
    public TextMeshProUGUI label;
    public Image img;
    public float max;
    [SerializeField]
    private float _current;

    public float current
    {
        get
        {
            return _current;
        }
        set
        {
            _current = value;
            img.rectTransform.localScale = new Vector3(_current / max, 1f, 1f);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
