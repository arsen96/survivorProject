using UnityEngine;

public class RotatingAttackController : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public GameObject sword;

    public float atkDamage;
    private PlayerXpController secondParent;
    private int lastLevel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (transform.parent != null)
        {
            PlayerController parentPlayer = transform.parent.GetComponent<PlayerController>();
            if (parentPlayer != null)
            {
                atkDamage = parentPlayer.atkDamage;
            }

            secondParent = transform.parent.GetComponent<PlayerXpController>();
            if (secondParent != null)
            {
                lastLevel = secondParent.level;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (secondParent != null && secondParent.level > lastLevel && transform.childCount <= 6)
        {
            lastLevel = secondParent.level;
            AddNewSword();
        }

        if (transform.childCount == 0)
        {
            AddNewSword();
        }
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    void AddNewSword()
    {
        Vector3 pos = transform.position;

        int nbrOfSwords = transform.childCount + 1; // � changer avec juste les sword si plus d'enfants
        float angleStep = 360f / nbrOfSwords;

        rotationSpeed += 20f;

        Vector3 refAngle = transform.childCount > 0 ? transform.GetChild(0).rotation * Vector3.forward : Vector3.forward;
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < nbrOfSwords; i++)
        {
            float angle = i * angleStep;

            Quaternion rota = Quaternion.AngleAxis(angle, refAngle);

            GameObject instance = Instantiate(sword, pos, rota, transform);
            instance.name = sword.name;
        }
    }


 
}
