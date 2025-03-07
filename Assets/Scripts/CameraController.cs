using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
    }

    // LateUpdate is called after Update
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x,target.position.y,transform.position.z);
    }
}
