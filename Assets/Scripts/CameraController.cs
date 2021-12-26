using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] BoxCollider2D mapLimits;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private float offsetX;
        [SerializeField] private float lerpSpeed;
        [SerializeField] private PlayerController playerController;


        private float _yMin;
        private float _yMax;
        private float _xMin;
        private float _xMax;
        private float _camWidth;
        private float _camHeight;

        private void OnValidate()
        {
            if (playerController is null)
            {
                playerController = FindObjectOfType<PlayerController>();
            }
        }

        private void Start()
        {
            var bounds = mapLimits.bounds;
            _yMin = bounds.min.y;
            _yMax = bounds.max.y;
            _xMax = bounds.max.x;
            _xMin = bounds.min.x;
            if (mainCamera is null)
            {
                mainCamera = GetComponent<Camera>();
            }

            _camHeight = mainCamera.orthographicSize;
            _camWidth = mainCamera.aspect * _camHeight;
        }


        void Update()
        {
            float offset;
            if (playerController.GetComponent<SpriteRenderer>().flipX)
            {
                offset = -offsetX;
            }
            else
            {
                offset = offsetX;
            }

            var position = playerController.gameObject.transform.position;
            var cameraY = Mathf.Clamp(position.y,
                _yMin + _camHeight, _yMax - _camHeight);
            var cameraX = Mathf.Clamp(position.x + offset, _xMin + _camWidth,
                _xMax - _camWidth);
            var transPosition = new Vector3(cameraX, cameraY, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, transPosition,
               lerpSpeed * Time.deltaTime);
        }
    }
}