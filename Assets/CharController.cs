using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    public float speed = 5f;
    public ConfigurableJoint joint;
    public Rigidbody rb;

    public Animator animator;

    private bool running = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0f, v);
        dir = dir.normalized;

        if (dir.magnitude > 0.1f)
        {
            dir.y = 0.1f;
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            Debug.Log(targetAngle);
            this.joint.targetRotation = Quaternion.Euler(0, -targetAngle, 0);
            this.rb.AddForce(dir * this.speed);

            running = true;
            animator.SetBool("Running", true);
        }
        else
        {
            running = false;
            animator.SetBool("Running", false);
        }
    }
}
