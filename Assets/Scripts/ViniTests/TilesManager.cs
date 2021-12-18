using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControleCena {
    public class TilesManager : MonoBehaviour {

        public static TilesManager Instance { get; private set; }
        [SerializeField] private GameObject chaoPrefab;
        [SerializeField] private GameObject paredePrefab;
        [SerializeField] private GameObject coletavelPrefab;
        [SerializeField] private float chanceParede;
        [SerializeField] private float chanceColetavel;
        [HideInInspector] public float larguraPistas;
        private const int NdePistas = 3;
        private const int NMaxDePistasEmCena = 3;
        private Vector3 posDeCriacaoAtual = Vector3.down;
        private List<GameObject> TilesEmcena = new List<GameObject>();

        private void Awake() {
            if (Instance == null) {
                Instance = this;
                larguraPistas = chaoPrefab.GetComponent<TileScript>().GetLarguraPistas();
                chanceColetavel += chanceParede;

            }
            for (int i = 0; i < 4; i++)
                CriaCenario(false);
        }

        public void CriaCenario(bool gerarProps) {
            GameObject gobj = Instantiate(chaoPrefab, posDeCriacaoAtual, Quaternion.identity);
            if (gerarProps)
                GeraProps(gobj.GetComponent<TileScript>());
            posDeCriacaoAtual = gobj.transform.Find("PontoFinal").position;
            DestruirCenario();
            /*larguraChao = chaoPrefab.transform.localScale.x;
            for (int i = 0; i < 3; i++)
            {
                GameObject gobj = Instantiate(chaoPrefab, posDeCriacaoAtual, Quaternion.identity);
                if (i == 0)
                    proxPontodeCriacao = gobj.transform.Find("PontoFinal").position;
                posDeCriacaoAtual += new Vector3(larguraChao, 0f, 0f);
            }
            posDeCriacaoAtual = proxPontodeCriacao;
            DestruirCenario();*/
        }

        private void DestruirCenario() {
            if (TilesEmcena.Count > NMaxDePistasEmCena) {
                Destroy(TilesEmcena[0]);
                TilesEmcena.RemoveAt(0);
            }
            /*if (TilesEmcena.Count > 9)
            {
                for (int i = 0; i < 3; i++)// remove os ultimos 3 tiles criados
                {
                    Destroy(TilesEmcena[i]);
                    TilesEmcena.RemoveAt(i);
                }
            }*/
        }

        public void AdicionarNaListaDeTilesEmCena(GameObject gobj) {
            TilesEmcena.Add(gobj);
        }

        public int GetQntdDePistas() {
            return NdePistas;
        }

        private void GeraProps(TileScript script) {
            foreach (Transform pontosDeProp in script.PontosDosProps) {
                float r = Random.Range(0f, 101f);
                if (r != 0) {
                    if (r <= chanceParede)
                        Instantiate(paredePrefab, pontosDeProp.position, Quaternion.identity, pontosDeProp);
                    else if (r > chanceParede && r <= chanceColetavel)
                        Instantiate(coletavelPrefab, pontosDeProp.position, Quaternion.identity, pontosDeProp);
                }
            }
        }
    }

}

