using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyLimb : MonoBehaviour
{
    [SerializeField] private Transform target;
    private ConfigurableJoint joint;

    Quaternion targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        this.targetRotation = this.target.transform.localRotation;
        this.joint = this.GetComponent<ConfigurableJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        this.joint.targetRotation = Quaternion.Inverse(this.target.localRotation) * this.targetRotation;
    }
}
