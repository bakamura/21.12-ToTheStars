using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//namespace SceneControl {

    public class TilesManager : MonoBehaviour {
    
        public static TilesManager Instance { get; private set; }
        [SerializeField] private GameObject _floorPrefab;
        [SerializeField] private GameObject _obstaclePrefab;
        [SerializeField] private GameObject _collectablePrefab;

        [SerializeField] private float _obstacleChance;
        [SerializeField] private float _collectableChance;

        [HideInInspector] public float widithLane;
        private const int _nOfLanes = 3;
        private const int _maxFloorsInScene = 3;
        private Vector2 _currentInstantiatePosition = Vector2.zero;
        private List<GameObject> _floorsInScene = new List<GameObject>();

        public float currentVelocity;
        public float maxVelocity;
        public float minVelocity;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
                widithLane = _floorPrefab.GetComponent<TileScript>().GetWidth();
                if (_collectableChance > 0) _collectableChance += _obstacleChance;
            }
            for (int i = 0; i < _maxFloorsInScene; i++) CreateFloors(false);
        }

        public void CreateFloors(bool GenerateProps) {
            _currentInstantiatePosition = GetNewFloorSpawnPoint();
            GameObject gobj = Instantiate(_floorPrefab, _currentInstantiatePosition, Quaternion.identity);
            if (GenerateProps) CreateProps(gobj.GetComponent<TileScript>());
            DestroyFloors();

            //larguraChao = chaoPrefab.transform.localScale.x;
            //for (int i = 0; i < 3; i++)
            //{
            //    GameObject gobj = Instantiate(chaoPrefab, posDeCriacaoAtual, Quaternion.identity);
            //    if (i == 0)
            //        proxPontodeCriacao = gobj.transform.Find("PontoFinal").position;
            //    posDeCriacaoAtual += new Vector3(larguraChao, 0f, 0f);
            //}
            //posDeCriacaoAtual = proxPontodeCriacao;
            //DestruirCenario();
        }

        private Vector2 GetNewFloorSpawnPoint() {
            if (_floorsInScene.Count > 0) return _currentInstantiatePosition = _floorsInScene[_floorsInScene.Count - 1].transform.Find("EndPoint").position + new Vector3(_floorsInScene[_floorsInScene.Count - 1].GetComponent<TileScript>().GetWidth() / 2f, 0, 0);
            else return Vector2.zero;
        }

        private void DestroyFloors() {
            if (_floorsInScene.Count > _maxFloorsInScene) {
                Destroy(_floorsInScene[0]);
                _floorsInScene.RemoveAt(0);
            }

            //if (TilesEmcena.Count > 9)
            //{
            //    for (int i = 0; i < 3; i++)// remove os ultimos 3 tiles criados
            //    {
            //        Destroy(TilesEmcena[i]);
            //        TilesEmcena.RemoveAt(i);
            //    }
            //}
        }

        public void VelocityChange(float SpeedChange) {
            if (currentVelocity + SpeedChange > maxVelocity) currentVelocity = maxVelocity;
            else if (currentVelocity + SpeedChange < minVelocity) currentVelocity = minVelocity;
            else currentVelocity += SpeedChange;
        }

        public void AddInFloorsInSceneList(GameObject gobj) {
            _floorsInScene.Add(gobj);
        }

        public int GetNumbOfLanes() {
            return _nOfLanes;
        }

        private void CreateProps(TileScript script) {
            foreach(Transform spawnPoints in script.propsSpawnPoints) {
                float random = Random.Range(0f,101f);
                if (random != 0) {
                    if (random <= _obstacleChance) Instantiate(_obstaclePrefab, spawnPoints.position, Quaternion.identity, spawnPoints);
                    else if (random > _obstacleChance && random <= _collectableChance) Instantiate(_collectablePrefab, spawnPoints.position, Quaternion.identity, spawnPoints);
                }
            }
        }
    }

//}

