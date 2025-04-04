using UnityEngine;

public class RotatingAttackController : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public GameObject sword;

    public float atkDamage;
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
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.E) && transform.childCount <= 6) || transform.childCount == 0) // V�rifie s'il n'a pas d'enfants et si la touche E est press�e
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
