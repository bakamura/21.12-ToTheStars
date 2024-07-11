using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowderKegEffect : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.tag == "Obstacle") Destroy(collision.gameObject);
    }
}
