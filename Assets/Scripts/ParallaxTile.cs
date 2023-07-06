using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxTile : MonoBehaviour {

    public static ParallaxTile Instance;

    [SerializeField] private Transform[] _parallaxImages;
    [SerializeField] private float[] _parallaxMultiplier;
    private Transform _cameraPosition;
    [HideInInspector] public bool isMoving = true;

    private void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) {
            Debug.Log("Two parallax controllers! one deleted");
            Destroy(gameObject);
        }

        _cameraPosition = Camera.main.transform;
    }

    private void Update() {
        if (isMoving) {
            for (int i = 0; i < _parallaxImages.Length; i++) {
                _parallaxImages[i].position += new Vector3(-MapGenerator.baseSpeed * MapGenerator.Instance.VelocityCalc() * _parallaxMultiplier[i] * Time.deltaTime, 0, 0);
                float maxDistance = GetSpriteWidith(_parallaxImages[i]);
                if (Mathf.Abs(_parallaxImages[i].position.x) >= maxDistance) {
                    _parallaxImages[i].position = new Vector3(_cameraPosition.position.x, _parallaxImages[i].position.y, 0);
                }
            }
        }
    }

    private float GetSpriteWidith(Transform obj) {
        Sprite sp = obj.GetComponent<SpriteRenderer>().sprite;
        float widith = (sp.texture.width / sp.pixelsPerUnit) * obj.transform.localScale.x;
        return widith;
    }
}