using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class CubeExplosive : MonoBehaviour
{
    private const float ChildScale = 0.5f;
    
    [SerializeField] private CubeExplosive _cubePrefab;
    [Space]
    [Range(0f, 1f)]
    [SerializeField] private float _cubesSpawnChance;
    [SerializeField] private int _minSpawnCubes;
    [SerializeField] private int _maxSpawnCudes;

    private float _explosionPower = 220f;
    private float _explosionRadius = 5f;
    private float _explosionUpwardsModif = 0.8f;
    
    public void Initialize(float cubesSpawnChance, int minSpawnCubes, int maxSpawnCubes)
    {
        _minSpawnCubes = minSpawnCubes;
        _maxSpawnCudes = maxSpawnCubes;
        
        _cubesSpawnChance = cubesSpawnChance * ChildScale;
        gameObject.transform.localScale *= ChildScale;

        if (gameObject.TryGetComponent(out MeshRenderer meshRenderer))
        {
            meshRenderer.material.color = UtilsRandom.GetRandomColor();
        }
    }

    public void Explode()
    {
        if (UtilsRandom.CheckChance(_cubesSpawnChance))
            ExplodeCube();
        else
            Destroy(gameObject);
    }

    private void ExplodeCube()
    {
        SpawnCubes();
        ApplyExplosion();
    }

    private void SpawnCubes()
    {
        int cubesToSpawn = UtilsRandom.GetRandomNumber(_minSpawnCubes, _maxSpawnCudes);
        Vector3[] spawnPositions = GetSpawnPositions();
        
        for (int i = 0; i < cubesToSpawn; i++)
        {
            CubeExplosive childCube = Instantiate(_cubePrefab, spawnPositions[i], Quaternion.identity);
            childCube.Initialize(_cubesSpawnChance, _minSpawnCubes, _maxSpawnCudes);
        }
        
        Destroy(gameObject);
    }

    private void ApplyExplosion()
    {
        Vector3 explosionPosition = transform.position - (Vector3.up * (transform.localScale.x * ChildScale));
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Rigidbody rb))
            {
                rb.AddExplosionForce(
                    _explosionPower, 
                    explosionPosition, 
                    _explosionRadius, 
                    _explosionUpwardsModif, 
                    ForceMode.Force);
            }
        }
    }

    private Vector3[] GetSpawnPositions()
    {
        Vector3[] positions = new Vector3 [6];
        Vector3 position = transform.position;
        float parentScale = transform.localScale.x;
        
        positions[0] = position + (Vector3.left * (ChildScale * parentScale));
        positions[1] = position + (Vector3.right * (ChildScale * parentScale));
        positions[2] = position + (Vector3.forward * (ChildScale * parentScale));
        positions[3] = position + (Vector3.back * (ChildScale * parentScale));
        positions[4] = position + (Vector3.up * (ChildScale * parentScale));
        positions[5] = position + (Vector3.down * (ChildScale * parentScale));

        return positions;
    }
}
