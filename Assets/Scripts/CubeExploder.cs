using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeExploder : MonoBehaviour
{
    private const float DefaultSideBias = 0.5f;
    
    [SerializeField] private float _explosionPower = 220f;
    [SerializeField] private float _explosionRadius = 3f;
    [SerializeField] private float _explosionUpwardsModif = 0.8f;
    
    public void ApplyExplosion(Transform parentCube, Cube[] childrenCubes)
    {
        Vector3 explosionPosition = parentCube.position - (Vector3.up * (parentCube.localScale.x * DefaultSideBias));

        foreach (Cube cube in childrenCubes)
        {
            if (cube.TryGetComponent(out Rigidbody rb))
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
}
