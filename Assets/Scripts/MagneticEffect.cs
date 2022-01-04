using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticEffect : MonoBehaviour {
    [SerializeField] private float _PullForce;
    private List<Rigidbody2D> _objectsInsideRadius = new List<Rigidbody2D>();

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Coal") {
            _objectsInsideRadius.Add(collision.GetComponent<Rigidbody2D>());
        }
    }

    private void FixedUpdate() {
        for (int i = 0; i < _objectsInsideRadius.Count; i++){
            if (_objectsInsideRadius[i] != null) _objectsInsideRadius[i].velocity = (this.transform.position - _objectsInsideRadius[i].transform.position).normalized * _PullForce;
            else _objectsInsideRadius.Remove(_objectsInsideRadius[i]);
        }
    }
    public void ClearList(){
        _objectsInsideRadius.Clear();
    }
}
