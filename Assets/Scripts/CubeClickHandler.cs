using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeClickHandler : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private CubeExploder _cubeExploder;
    
    private void OnEnable()
    {
        _inputReader.ObjectClicked += OnObjectClicked;
    }
    
    private void OnObjectClicked(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Cube cube) == false)
            return;

        if (_cubeSpawner.TrySpawnCubes(cube, out Cube[] childCubes))
        {
            _cubeExploder.ApplyExplosion(cube.gameObject, childCubes);
        }
        else
        {
            _cubeExploder.CreateExplosion(cube.gameObject);
        }
    }
    
    private void OnDisable()
    {
        _inputReader.ObjectClicked -= OnObjectClicked;
    }
}
