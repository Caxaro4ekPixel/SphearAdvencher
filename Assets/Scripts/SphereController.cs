using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SphereController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private Rigidbody rb;   
    [SerializeField] private float speed;
    [SerializeField] private float jamp;
    [SerializeField] private bool isJamp = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void FixedUpdate()
    {

        anim.SetFloat("Velocity", rb.velocity.magnitude);

        if (Input.GetKey(KeyCode.W))
            rb.AddForce(0, 0, speed, ForceMode.Force);
        if (Input.GetKey(KeyCode.S))
            rb.AddForce(0, 0, -speed, ForceMode.Force);
        if (Input.GetKey(KeyCode.A))
            rb.AddForce(-speed, 0, 0, ForceMode.Force);
        if (Input.GetKey(KeyCode.D))
            rb.AddForce(speed, 0, 0, ForceMode.Force);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isJamp)
            {
                anim.SetBool("Jamp", true);
                rb.AddForce(0, jamp, 0, ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isJamp = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isJamp = true;
    }
}
