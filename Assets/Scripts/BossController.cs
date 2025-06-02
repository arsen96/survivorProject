using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossController : MonoBehaviour
{
    public GameObject firePrefab;
    public Transform firePoint;
    public FireWeapon fireWeapon;
    private float _launchCooldown;
    private Transform target;

    public Transform bossWrapper;

    private float duration = 3f;
    private float baseScale = 1f;
    private float currentScale = 1f;
    private float scaleSpeed = 2f;

    public float moveSpeed;

    public float knockBackTime = .5F;
    private float knockBackCounter;

    public bool isDead = false;

    private List<Transform> activeFireballs = new List<Transform>();
    private List<float> fireballScales = new List<float>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (fireWeapon != null)
        {
            _launchCooldown = fireWeapon.stats[fireWeapon.weaponLevel].timeBetweenAttacks;
            baseScale = fireWeapon.stats[fireWeapon.weaponLevel].range;
        }
        
        if (PlayerHealthController.instance != null)
        {
            target = PlayerHealthController.instance.transform;
        }
        else
        {
            Debug.LogError("PlayerHealthController.instance is null!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_launchCooldown > 0)
        {
            _launchCooldown -= Time.deltaTime;
        }
        else
        {
            GameObject fireball = Instantiate(firePrefab, firePoint.position, Quaternion.identity, bossWrapper);
            Vector2 direction = (target.position - firePoint.position).normalized;
            Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();

            rb.linearVelocity = direction * fireWeapon.stats[fireWeapon.weaponLevel].speed;
         
            _launchCooldown = fireWeapon.stats[fireWeapon.weaponLevel].timeBetweenAttacks;
            fireball.transform.localScale = Vector3.one;
            
            // Add new fireball to tracking lists
            activeFireballs.Add(fireball.transform);
            fireballScales.Add(1f);
        }

        for (int i = activeFireballs.Count - 1; i >= 0; i--)
        {
            if (activeFireballs[i] == null)
            {
                activeFireballs.RemoveAt(i);
                fireballScales.RemoveAt(i);
                continue;
            }

            fireballScales[i] = Mathf.MoveTowards(fireballScales[i], baseScale, scaleSpeed * Time.deltaTime);
            activeFireballs[i].localScale = Vector3.one * fireballScales[i];

            // Remove fireball if it has reached target scale
            if (fireballScales[i] >= baseScale)
            {
                activeFireballs.RemoveAt(i);
                fireballScales.RemoveAt(i);
            }
        }
    }
}
