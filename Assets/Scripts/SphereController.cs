using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SphereController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private Rigidbody rb;   
    [SerializeField] private float speed;
    [SerializeField] private bool is_jamp = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {

        anim.SetFloat("Velocity", rb.velocity.magnitude);

        if (Input.GetKey(KeyCode.W))
            rb.AddForce(0, 0, speed * Time.deltaTime, ForceMode.Impulse);
        if (Input.GetKey(KeyCode.S))
            rb.AddForce(0, 0, -speed * Time.deltaTime, ForceMode.Impulse);
        if (Input.GetKey(KeyCode.A))
            rb.AddForce(-speed * Time.deltaTime, 0, 0, ForceMode.Impulse);
        if (Input.GetKey(KeyCode.D))
            rb.AddForce(speed * Time.deltaTime, 0, 0, ForceMode.Impulse);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!is_jamp)
            {
                anim.SetBool("Jamp", true);
                rb.AddForce(0, speed, 0, ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            is_jamp = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            is_jamp = true;
    }
}
