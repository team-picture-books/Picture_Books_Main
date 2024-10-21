using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NewPlayerCon : MonoBehaviour
{
    float inputHorizontal;
    float inputVertical;
    float movespeed = 3.0f;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
        
    }
    private void FixedUpdate()
    {
        ///Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        ///Vector3 moveForward = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;
        ///rb.velocity = moveForward * movespeed + new Vector3(0, rb.velocity.y, 0);
        ///if(moveForward != Vector3.zero)
        ///{
           /// transform.rotation = Quaternion.LookRotation(moveForward);
        ///}
        if (Input.GetKey("w"))
        {
            rb.velocity = -transform.right * movespeed;
        }
        if (Input.GetKey("s"))
        {
            rb.velocity = transform.right * movespeed;
        }
        if (Input.GetKey("d"))
        {
            rb.velocity = transform.forward * movespeed;
        }
        if (Input.GetKey("a"))
        {
            rb.velocity = -transform.forward * movespeed;
        }
    }
}
