using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const int FireButton = 0;
    
    [SerializeField] private Raycaster _raycaster;
    
    public event Action<Collider> ObjectClicked;
 
    private void Update()
    {
        if(Input.GetMouseButtonDown(FireButton))
            ClickedMouse();
    }

    private void ClickedMouse()
    {
        if(_raycaster.TryCastRay(out Collider collider))
        {
            ObjectClicked?.Invoke(collider);
        }
    }

}
