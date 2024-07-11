using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coletavel : MonoBehaviour {

    [SerializeField] private float AumentoVida;
    [SerializeField] private float IncrementoVelocidade;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            VidaJogador.Instance.MudancaVida(AumentoVida);
            movimentoJogador.Instance.MudancaVelocidade(IncrementoVelocidade);
            Destroy(gameObject);
        }
    }
}
