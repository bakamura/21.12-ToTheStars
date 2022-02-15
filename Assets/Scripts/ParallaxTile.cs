using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxTile : MonoBehaviour {

    [SerializeField] private float _parallaxSpeed;
    [SerializeField] private Transform[] _parallaxImages;
    [SerializeField] private float[] _parallaxMultiplier;
    private Transform _cameraPosition;

    private void Awake() {
        _cameraPosition = Camera.main.transform;
    }

    private void Update() {
        for (int i = 0; i < _parallaxImages.Length; i++) {
            _parallaxImages[i].position += new Vector3(_parallaxSpeed * _parallaxMultiplier[i] * Time.deltaTime, 0, 0);
            float maxDistance = GetSpriteWidith(_parallaxImages[i]);
            if (Mathf.Abs(_parallaxImages[i].position.x) >= maxDistance){
                _parallaxImages[i].position = new Vector3 (_cameraPosition.position.x, 0, 0);
            }
        }
    }

    private float GetSpriteWidith(Transform obj) {
        Sprite sp = obj.GetComponent<SpriteRenderer>().sprite;
        float widith = (sp.texture.width / sp.pixelsPerUnit) * obj.transform.localScale.x;
        return widith;
    }
}