using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }
    
    public bool TryCastRay(out Collider hitObject)
    {
        hitObject = null;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMask))
        {
            hitObject = hit.collider;
            
            return true;
        }

        return false;
    }
}
