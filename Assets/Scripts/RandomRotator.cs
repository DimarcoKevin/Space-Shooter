using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour {

    public float tumble;
    public float speed;


    void Start() {

        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
    }

}
