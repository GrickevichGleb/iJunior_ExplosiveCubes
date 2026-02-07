using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class CubeExplosive : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [Space]
    [Range(0f, 1f)]
    [SerializeField] private float _cubesSpawnChance;
    [SerializeField] private int _minSpawnCubes;
    [SerializeField] private int _maxSpawnCudes;

    private float _childScale = 0.5f;
    private void Start()
    {
        
    }
    
    private void Update()
    {
        
    }

    public void Initialize(float cubesSpawnChance, int minSpawnCubes, int maxSpawnCubes)
    {
        _cubesSpawnChance = cubesSpawnChance;
        _minSpawnCubes = minSpawnCubes;
        _maxSpawnCudes = maxSpawnCubes;
    }

    public void Explode()
    {
        if (UserUtils.CheckChance(_cubesSpawnChance))
        {
            ExplodeCube();
            Debug.Log($"{gameObject.name} exploded");
        }
    }

    private void ExplodeCube()
    {
        SpawnCubes();
        
        Destroy(gameObject);
 
        ApplyExplosion();
    }

    private void SpawnCubes()
    {
        int cubesToSpawn = UserUtils.GetRandomNumber(_minSpawnCubes, _maxSpawnCudes);
        Vector3[] spawnPositions = GetSpawnPositions();
        
        for (int i = 0; i < cubesToSpawn; i++)
        {
            GameObject childCube = Instantiate(_cubePrefab, spawnPositions[i], Quaternion.identity);
            childCube.transform.localScale = transform.localScale * _childScale;
            childCube.GetComponent<CubeExplosive>().
                Initialize
                    (_cubesSpawnChance * _childScale, _minSpawnCubes, _maxSpawnCudes);
        }
    }

    private void ApplyExplosion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 4);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Rigidbody rb))
            {
                rb.AddExplosionForce(120f, transform.position, 4f, 1f, ForceMode.Force);
            }
        }
    }

    private Vector3[] GetSpawnPositions()
    {
        Vector3[] positions = new Vector3 [6];
        Vector3 position = transform.position;
        
        positions[0] = position + (Vector3.left * 0.55f);
        positions[1] = position + (Vector3.right * 0.55f);
        positions[2] = position + (Vector3.forward * 0.55f);
        positions[3] = position + (Vector3.back * 0.55f);
        positions[4] = position + (Vector3.up * 0.55f);
        positions[5] = position + (Vector3.down * 0.55f);

        return positions;
    }
}
