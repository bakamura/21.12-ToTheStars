using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimentoJogador : MonoBehaviour {

    public static movimentoJogador Instance { get; private set; }
    private Rigidbody rb;
    [SerializeField] private float velocidadeAtual;
    [SerializeField] private float velocidadeMinima;
    [SerializeField] private float velocidadeMaxima;
    [SerializeField] private float velocidadeTrocaPista;
    [SerializeField] private float velocidadePulo;
    [SerializeField] private float alturaPulo;
    private int PistaAtual = 1;
    private Vector3 posicaoAlvo;
    [HideInInspector] public int ForcaJogador = 0;
    private bool pulando = false;
    private int estadoPulo = 0;
    //private Vector3 direcaoMov;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            rb = GetComponent<Rigidbody>();
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.A) && transform.position.x == posicaoAlvo.x) {
            TrocarPista(-1);
        }
        else if (Input.GetKeyDown(KeyCode.D) && transform.position.x == posicaoAlvo.x) {
            TrocarPista(1);
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            InputPulo();
        }
        posicaoAlvo = new Vector3(posicaoAlvo.x, posicaoAlvo.y, transform.position.z);
        MovimentacaoPulo();
        MovimentacaoTrocaPista();
        //float movZ = Input.GetAxis("Vertical");
        //direcaoMov = new Vector3(movX, 0f, movZ).normalized;
    }

    private void FixedUpdate() {
        rb.velocity = Vector3.forward * velocidadeAtual;
    }

    private void InputPulo() {
        if (!pulando) {
            posicaoAlvo = new Vector3(posicaoAlvo.x, transform.position.y + alturaPulo, transform.position.z);
            pulando = true;
        }
    }

    private void MovimentacaoTrocaPista() {
        transform.position = Vector3.MoveTowards(transform.position, posicaoAlvo, velocidadeTrocaPista * Time.deltaTime);
    }

    private void MovimentacaoPulo() {
        if (pulando) {
            transform.position = Vector3.MoveTowards(transform.position, posicaoAlvo, velocidadePulo * Time.deltaTime);
            switch (estadoPulo) {
                case 0:
                    if (transform.position.y >= posicaoAlvo.y) {
                        posicaoAlvo = new Vector3(posicaoAlvo.x, transform.position.y - alturaPulo, transform.position.z);
                        estadoPulo = 1;
                    }
                    break;
                case 1:
                    if (transform.position.y == 0f) {
                        pulando = false;
                        estadoPulo = 0;
                    }
                    break;
            }
        }
    }

    private void TrocarPista(int novaPista) {
        if (PistaAtual + novaPista < 0 || PistaAtual + novaPista > ControleCena.TilesManagerOld.Instance.GetQntdDePistas() - 1) {
            return;
        }
        PistaAtual += novaPista;
        posicaoAlvo = new Vector3(transform.position.x + (novaPista * ControleCena.TilesManagerOld.Instance.larguraPistas), posicaoAlvo.y, transform.position.z);
        //if (novaPista > 0)
        //{
        //    
        //}
        //else if (novaPista < 0)
        //{
        //
        //}
    }

    public void MudancaVelocidade(float valor) {
        if (velocidadeAtual + valor > velocidadeMaxima)
            velocidadeAtual = velocidadeMaxima;
        else if (velocidadeAtual + valor < velocidadeMinima)
            velocidadeAtual = velocidadeMinima;
        else
            velocidadeAtual += valor;
    }

}
