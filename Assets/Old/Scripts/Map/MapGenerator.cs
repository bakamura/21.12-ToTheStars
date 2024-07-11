using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public static MapGenerator Instance { get; private set; } = null;

    [SerializeField] private GameObject[] _presetArea;
    //[SerializeField] private Vector3 _spawnLocation; //
    [SerializeField] private float _screenLeftmostPoint; //can be a constant in the future

    [HideInInspector] public bool isMoving = true;
    public static float baseSpeed = .75f;
    [SerializeField] private float _speedMultiplier;
    public float currentSpeedMultiplier;
    [SerializeField] private float _speedIncrease;
    private List<GameObject> _currentAreas = new List<GameObject>();
    [SerializeField] private float _distanceToFirstArea;

    private void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

    private void Start() {
        // Generates the first terrain and some upcoming ones.
        GenerateStartArea();
        currentSpeedMultiplier = _speedMultiplier;
    }

    public void GenerateStartArea(){
        for (int i = 0; i < 3; i++) GenerateNewArea();
    }

    private void Update() {
        if (isMoving) {
            Vector3 displacement = Vector3.left * baseSpeed * VelocityCalc() * Time.deltaTime;
            foreach (GameObject area in _currentAreas) area.transform.position += displacement;
            if (_currentAreas[0].transform.position.x + _currentAreas[0].GetComponent<BoxCollider2D>().size.x / 2 < _screenLeftmostPoint) {
                GameObject areaToRemove = _currentAreas[0];
                _currentAreas.Remove(areaToRemove);
                Destroy(areaToRemove);

                GenerateNewArea();
            }
        }

        currentSpeedMultiplier += Time.deltaTime * _speedIncrease; // Test
    }

    private void GenerateNewArea() {
        GameObject area = _presetArea[Random.Range(0, _presetArea.Length)];
        GameObject instantiatedArea;
        instantiatedArea = _currentAreas.Count > 0 ?
            Instantiate(area,
            new Vector3(_currentAreas[_currentAreas.Count - 1].transform.position.x + _currentAreas[_currentAreas.Count - 1].GetComponent<BoxCollider2D>().size.x / 2 + area.GetComponent<BoxCollider2D>().size.x / 2, transform.position.y, 0), 
            Quaternion.identity) :

            Instantiate(area, 
            new Vector3(area.GetComponent<BoxCollider2D>().size.x / 2 + _distanceToFirstArea, transform.position.y, 0),
            Quaternion.identity);
        _currentAreas.Add(instantiatedArea);
    }

    public void ClearAllAreas(){
        foreach(GameObject area in _currentAreas){
            if (area != null) Destroy(area);
        }
        _currentAreas.Clear();

        currentSpeedMultiplier = _speedMultiplier;
    }

    public float VelocityCalc() {
        return Mathf.Sqrt(currentSpeedMultiplier);
    }
}
