using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveInput = new Vector3(0f,0f,0f);
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize(); // forcer la longueur du vecteur à être toujours de 1
        transform.position += moveInput * moveSpeed * Time.deltaTime;

        if(moveInput != Vector3.zero){
            anim.SetBool("isMoving", true);
        }else{
            anim.SetBool("isMoving", false);
        }
    }
}
