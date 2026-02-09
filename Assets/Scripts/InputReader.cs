using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const int FireButton = 0;
    
    [SerializeField] private LayerMask _layerMask;

    public event Action<Cube> CubeClicked;
    
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }
    
    private void Update()
    {
        if(Input.GetMouseButtonDown(FireButton))
            MouseClick();
    }

    private void MouseClick()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMask))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.TryGetComponent(out Cube cubeExplosive))
            {
                CubeClicked?.Invoke(cubeExplosive);
            }
        }
    }

}
