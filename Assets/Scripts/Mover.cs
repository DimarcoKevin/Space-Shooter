using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    public float speed;
    

    void Start() {
        if (gameObject.tag == "Asteroid") {
            gameObject.GetComponent<Rigidbody>().velocity = (transform.forward * (-speed));
        } else {
            gameObject.GetComponent<Rigidbody>().velocity = (transform.forward * speed);
        }
    }
}
