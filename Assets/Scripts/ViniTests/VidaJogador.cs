using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidaJogador : MonoBehaviour {

    public static VidaJogador Instance { get; private set; }
    [SerializeField] private Image BarraDeVida;
    private float vidaAtual;
    [SerializeField] private float vidaMax;
    [SerializeField] private float PercaDeVida;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            vidaAtual = vidaMax;
        }
    }

    private void Start() {
        StartCoroutine(this.DrenaVida());
    }

    IEnumerator DrenaVida() {
        while (vidaAtual > 0) {
            yield return new WaitForSeconds(Time.fixedDeltaTime);

            MudancaVida(-PercaDeVida);
        }
    }
    public void MudancaVida(float valor) {
        BarraDeVida.fillAmount += (1f / vidaMax) * (valor);
        vidaAtual += valor;
        if (vidaAtual > vidaMax)
            vidaAtual = vidaMax;
        else if (vidaAtual <= 0f)
            Debug.Log("Morreu");
    }
}
