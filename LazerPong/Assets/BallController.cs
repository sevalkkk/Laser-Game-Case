using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public static BallController instance;
    public float forceSpeed;
    #region References
    private Rigidbody rb;   
    private SphereCollider colliderOfBall;
    #endregion

 
    private void Awake()
    {
        if(instance ==null)
        {
            instance = this;
        }
        rb = GetComponent<Rigidbody>();
        colliderOfBall = GetComponent<SphereCollider>();
    }
    void Start()
    {
        AddStartingForce();
    }

  

    private void AddStartingForce()
    {
        Vector3 direction = SetRandomDirectionForForce();
        rb.AddForce(direction * forceSpeed, ForceMode.Impulse);
    }

    public void AddForce()
    {
        rb.AddForce(transform.forward * forceSpeed, ForceMode.Impulse);
    }
    
    private Vector3 SetRandomDirectionForForce()
    {
        float randomX = UnityEngine.Random.Range(-1, 1);
        float randomZ = UnityEngine.Random.Range(-1, 1);
        if (randomX >= 0)
        {
            randomX = 1;
        }
        else
        {
            randomX = -1;
        }

        if (Mathf.Abs(randomZ) < 0.1f)
        {
            randomZ = 1;
        }
        Vector3 randomDirection = new Vector3(randomX, 0, randomZ).normalized;
        return randomDirection;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Constants.PLAYER_AREA_TAG))
        {
            IgnoreCollisionMethod(collision.gameObject);
        }

        if (collision.gameObject.CompareTag(Constants.PLAYER_TAG))
        {
            IgnoreCollisionMethod(collision.gameObject);
        }
    }

    void IgnoreCollisionMethod(GameObject anotherObject)
    {
        BoxCollider colliderOfOtherObject = anotherObject.GetComponent<BoxCollider>();
        Physics.IgnoreCollision(colliderOfOtherObject, colliderOfBall);
    }

}
