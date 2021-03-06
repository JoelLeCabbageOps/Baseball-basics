﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballBat : MonoBehaviour
{
    public float rotateSpeed;

    public Transform player;
    public Transform lookTarget;
    public Transform lookGoal;
    public float lookTargetSpeed;
    Transform lookGoalParent;

    Rigidbody lookTargetRb;


    private void Start()
    {
        lookTargetRb = lookTarget.GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        FindLookTargetsTarget();
        BatRotate(); 
    }

    void FindLookTargetsTarget()
    {
        Vector3 towardsTarget = lookGoal.position - lookTarget.position;

        if (lookTargetRb.velocity.magnitude > lookTargetSpeed && Vector3.Distance(lookTarget.position, lookGoal.position) > 3)
        {
            //Debug.Log("YEET");
            //towardsTarget.Normalize();
            //lookTargetRb.AddForce(towardsTarget * lookTargetSpeed * Time.deltaTime, ForceMode.VelocityChange);
            Mathf.Clamp(lookTargetRb.velocity.magnitude, 0, lookTargetSpeed - 5);
            lookTargetRb.velocity = towardsTarget;
        }
        else if (lookTargetRb.velocity.magnitude < lookTargetSpeed)
        {
            lookTargetRb.AddForce(towardsTarget * lookTargetSpeed * Time.deltaTime, ForceMode.VelocityChange);
        }
    }
    void TargetRotate()
    {
        lookGoalParent = lookGoal.parent.transform;

        Vector3 targetDirection = lookTarget.position - lookGoalParent.position;

        float singleStep = lookTargetSpeed * Time.deltaTime;

        Vector3 newDirection = Vector3.RotateTowards(lookGoalParent.forward, targetDirection, singleStep, 0.0f);

        lookGoalParent.rotation = Quaternion.LookRotation(newDirection);
    }

    void BatRotate()
    {
        Vector3 targetDirection = lookTarget.position - transform.position;

        float singleStep = rotateSpeed * Time.deltaTime;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
      
        Debug.DrawRay(transform.position, newDirection, Color.red);
      
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ObjectInformation obInfo = collision.gameObject.GetComponent<ObjectInformation>();

        if (obInfo != null)
        {
            obInfo.currentHealth -= 1f;
        }
    }

}
