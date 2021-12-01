using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    private Animator anim;
    private CharacterController controller;

    public float speed = 100.0f;
    public float turnSpeed = 400.0f;
    private Vector3 moveDirection = Vector3.zero;
    public float gravity = 20.0f;
    public float jump = 50.0f;
    private bool inBox = true;
    private Vector3 startPos;
    private Vector3 startForward;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = gameObject.GetComponentInChildren<Animator>();
        startPos = transform.position;
        startForward = transform.forward;
        Debug.Log(startPos);
    }

    void Update()
    {
        if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
        {
            anim.SetInteger("AnimationPar", 1);
        }
        else
        {
            anim.SetInteger("AnimationPar", 0);
        }


        // Forward/Backward
        if (controller.isGrounded) {
            moveDirection.x = transform.forward.x * Input.GetAxis("Vertical") * speed;
            moveDirection.z = transform.forward.z * Input.GetAxis("Vertical") * speed;
        }
        if (!controller.isGrounded) {
            moveDirection.x -= moveDirection.x / 3 * Time.deltaTime - transform.forward.x * Input.GetAxis("Vertical") * speed / 3 * Time.deltaTime;
            moveDirection.z -= moveDirection.z / 3 * Time.deltaTime - transform.forward.z * Input.GetAxis("Vertical") * speed / 3 * Time.deltaTime;
        }


        if(controller.isGrounded && Input.GetKey(KeyCode.Space)) {
            moveDirection.y += jump;
        }

        // Gravity
		moveDirection.y -= gravity * Time.deltaTime;		

        // Rotation
        float turn = Input.GetAxis("Horizontal");
        transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);

        // Oob Reset
        if (!inBox) {
            controller.enabled = false;
            controller.transform.position = startPos;
            controller.transform.forward = startForward;
            moveDirection = Vector3.zero;
            controller.enabled = true;
        }

        controller.Move(moveDirection * Time.deltaTime);
    }

    void OnTriggerExit(Collider other) {
        if (other.name == "Game Box") {
            inBox = false;
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.name == "Game Box") {
            inBox = true;
        }
    }
}
