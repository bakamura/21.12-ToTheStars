using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public static MapGenerator Instance { get; private set; } = null;

    [SerializeField] private GameObject[] _presetArea;
    [SerializeField] private Vector3 _spawnLocation; //
    [SerializeField] private float _maxDistanceToDestroyArea;//can be a constant in the future

    [System.NonSerialized] public bool isMoving = true;
    [SerializeField] private float _baseSpeed;
    public float speedMultiplier;
    private List<GameObject> _currentAreas = new List<GameObject>();
    [SerializeField] private float _distanceToNewArea;

    private void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

    private void Start() {
        // Generates the first terrain and the upcoming one.
        // an idea on how to calculate spawns for the next areas
        //GenerateNewArea();
        //GenerateNewArea();
        GenerateArea(new Vector3(9, 0, 0));
        GenerateArea(new Vector3(27, 0, 0));
    }

    private void FixedUpdate() {
        if (isMoving) {
            // Speed multiplier is in sqrt to assure a "caping" nature to the game
            Vector3 displacement = Vector3.left * _baseSpeed * Mathf.Sqrt(speedMultiplier) * Time.deltaTime;
            foreach (GameObject area in _currentAreas) area.transform.position += displacement;

            if (_currentAreas[0].transform.position.x < _maxDistanceToDestroyArea) {
                GameObject areaToRemove = _currentAreas[0];
                _currentAreas.Remove(areaToRemove);
                Destroy(areaToRemove);
                //GenerateNewArea();
            }
            if (_currentAreas[_currentAreas.Count - 1].transform.position.x < _distanceToNewArea)
            {
                GenerateArea(_currentAreas[_currentAreas.Count - 2].transform.position + new Vector3(_distanceToNewArea /*+ _presetArea[_currentAreas.Count].GetComponent<BoxCollider2D>().size.x*/, 0, 0));
            
            }
        }

        speedMultiplier += Time.fixedDeltaTime / 5; // Test
    }

    private void GenerateNewArea() {
        GameObject area = _presetArea[Random.Range(0, _presetArea.Length)];
        GameObject instantiatedArea;
        instantiatedArea = _currentAreas.Count > 0 ?
            Instantiate(area, _currentAreas[_currentAreas.Count - 1].transform.position + new Vector3(_currentAreas[_currentAreas.Count - 1].GetComponent<BoxCollider2D>().size.x / 2 + area.GetComponent<BoxCollider2D>().size.x / 2 + _distanceToNewArea, 0, 0), Quaternion.identity) 
            :
            Instantiate(area, new Vector3(area.GetComponent<BoxCollider2D>().size.x / 2 + _distanceToNewArea, 0, 0), Quaternion.identity);
        _currentAreas.Add(instantiatedArea);
    }

    public float VelocityCalc(){
        Vector3 velocity = Vector3.left * _baseSpeed * Mathf.Sqrt(speedMultiplier) * Time.deltaTime;
        return velocity.x;
    }

    private GameObject GenerateArea(Vector3 spawnPosition) {
        //GameObject instantiatedArea = Instantiate(_presetArea[Random.Range(0, (_presetArea.Length))], spawnPosition, Quaternion.identity);
        GameObject instantiatedArea = Instantiate(_presetArea[Random.Range(0, (_presetArea.Length - 1))], spawnPosition, Quaternion.identity);
        _currentAreas.Add(instantiatedArea);
        return instantiatedArea;
    }
}
