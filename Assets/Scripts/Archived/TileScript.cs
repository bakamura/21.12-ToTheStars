using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {

    public Transform[] propsSpawnPoints;
    [SerializeField] private Transform _laneSize;

    private Rigidbody2D _rb;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        TilesManager.Instance.AddInFloorsInSceneList(this.gameObject); //
    }

    private void FixedUpdate() {
        _rb.velocity = Vector2.left * TilesManager.Instance.currentVelocity; //
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") TilesManager.Instance.CreateFloors(true); //
    }

    public float GetWidth() {
        return Mathf.Abs(_laneSize.localScale.x);
    }

    public float GetHeight() {
        return Mathf.Abs(_laneSize.localScale.y);
    }
}
