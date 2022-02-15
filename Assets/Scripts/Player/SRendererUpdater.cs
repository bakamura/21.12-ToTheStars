using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SRendererUpdater : MonoBehaviour {

    private SpriteRenderer sr;
    [SerializeField] private bool _atStartOnly;
    [SerializeField] private float _offset; // 1 : 0.1f

    private void Start() {
        sr = GetComponent<SpriteRenderer>();
        if (_atStartOnly) {
            sr.sortingOrder = (int)((-10 * transform.position.y) + _offset);
            Destroy(this);
        }
    }

    void Update() {
        sr.sortingOrder = (int)((-10 * transform.position.y) + _offset);
    }
}
