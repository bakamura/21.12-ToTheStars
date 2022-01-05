using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculo : MonoBehaviour {

    [SerializeField] private float Dano;
    [SerializeField] private float ReducaoVelocidade;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            VidaJogador.Instance.MudancaVida(-Dano);
            movimentoJogador.Instance.MudancaVelocidade(-ReducaoVelocidade);
            Destroy(gameObject);
        }
    }
}
