using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeExploder : MonoBehaviour
{
    private const float DefaultSideBias = 0.5f;
    
    [SerializeField] private float _explosionPower = 220f;
    [SerializeField] private float _explosionRadius = 3f;
    [SerializeField] private float _explosionUpwardsModif = 0.8f;
    
    public void ApplyExplosion(GameObject parentCube, Cube[] childrenCubes)
    {
        Transform parentCubeTransform = parentCube.transform;
        Vector3 explosionPosition = 
            parentCubeTransform.position - (Vector3.up * (parentCubeTransform.localScale.x * DefaultSideBias));

        Destroy(parentCube);
        
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

    public void CreateExplosion(GameObject cubeObject)
    {
        Transform objectTransform = cubeObject.transform;
        Vector3 explosionPosition =
            objectTransform.position - (Vector3.up * (objectTransform.localScale.x * DefaultSideBias));
        float explosionScale = 1f / objectTransform.localScale.x;
        
        Destroy(cubeObject);
        
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, _explosionRadius  * explosionScale);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Rigidbody rb))
            {
                rb.AddExplosionForce(
                    _explosionPower * explosionScale, 
                    explosionPosition, 
                    _explosionRadius * explosionScale, 
                    _explosionUpwardsModif, 
                    ForceMode.Force);
            }
        }
    }
}
