﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [System.Serializable]
public class Boundary {

    public float xMin, xMax, zMin, zMax; 

}

public class PlayerController : MonoBehaviour {

    private float nextFire;

    public float speed;
    public float tilt;
    public float fireRate;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;

    void Update() {

        if (Input.GetKey("space") && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            gameObject.GetComponent<AudioSource>().Play();
            
        }
    }

    void FixedUpdate() {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0F, moveVertical);
        GetComponent<Rigidbody>().velocity = (movement * speed);

        GetComponent<Rigidbody>().position = new Vector3
        (
            Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
            0.0F, 
            Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
        );

        GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0F, 0.0F, (GetComponent<Rigidbody>().velocity.x) * -tilt);

    }

}
