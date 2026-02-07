using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }
    
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
            MouseClick();
    }

    private void MouseClick()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            GameObject hitObject = hit.collider.gameObject;
            Debug.Log($"Hit object {hitObject.name}");
            if (hitObject.TryGetComponent(out CubeExplosive cubeExplosive))
            {
                cubeExplosive.Explode();
            }
        }
    }

}
