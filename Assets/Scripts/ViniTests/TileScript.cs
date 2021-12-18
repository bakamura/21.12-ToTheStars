using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {

    public Transform[] PontosDosProps;
    [SerializeField] private Transform transformPista;

    private void Awake() {
        ControleCena.TilesManager.Instance.AdicionarNaListaDeTilesEmCena(this.gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        ControleCena.TilesManager.Instance.CriaCenario(true);
    }

    public float GetLarguraPistas() {
        return Mathf.Abs(transformPista.localScale.x);
    }
}
