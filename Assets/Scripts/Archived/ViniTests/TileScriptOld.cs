using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScriptOld : MonoBehaviour {

    public Transform[] PontosDosProps;
    [SerializeField] private Transform transformPista;

    private void Awake() {
        ControleCena.TilesManagerOld.Instance.AdicionarNaListaDeTilesEmCena(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") ControleCena.TilesManagerOld.Instance.CriaCenario(true);
    }
    public float GetLarguraPistas() {
        return Mathf.Abs(transformPista.localScale.x);
    }
}
