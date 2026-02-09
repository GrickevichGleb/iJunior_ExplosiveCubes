using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    private const float ChildScale = 0.5f;
    
    [SerializeField] private Cube _cubePrefab;
    [Space]
    [SerializeField] private int _minSpawnCubes;
    [SerializeField] private int _maxSpawnCudes;
    
    public bool TrySpawnCubes(Cube cube, out Cube[] childCubes)
    {
        childCubes = null;
        
        if (UtilsRandom.TryChance(cube.CubesSpawnChance))
        {
            childCubes = SpawnCubes(cube);
            
            return true;
        }
        
        Destroy(cube.gameObject);
        
        return false;
    }
    
    private Cube[] SpawnCubes(Cube cube)
    {
        int cubesToSpawn = UtilsRandom.GetRandomNumber(_minSpawnCubes, _maxSpawnCudes);
        
        Cube[] cubes = new Cube[cubesToSpawn];
        Vector3[] spawnPositions = GetSpawnPositions(cube.transform);
        
        for (int i = 0; i < cubesToSpawn; i++)
        {
            Cube childCube = Instantiate(_cubePrefab, spawnPositions[i], Quaternion.identity);
            childCube.Initialize(cube);

            cubes[i] = childCube;
        }

        return cubes;
    }
    
    private Vector3[] GetSpawnPositions(Transform cubeTransform)
    {
        Vector3[] positions = new Vector3 [6];
        Vector3 position = cubeTransform.position;
        float parentScale = cubeTransform.localScale.x;
        
        positions[0] = position + (Vector3.left * (ChildScale * parentScale));
        positions[1] = position + (Vector3.right * (ChildScale * parentScale));
        positions[2] = position + (Vector3.forward * (ChildScale * parentScale));
        positions[3] = position + (Vector3.back * (ChildScale * parentScale));
        positions[4] = position + (Vector3.up * (ChildScale * parentScale));
        positions[5] = position + (Vector3.down * (ChildScale * parentScale));

        return positions;
    }
}
