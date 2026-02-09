using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Cube : MonoBehaviour
{
    private const float ChildScale = 0.5f;
    
    [Range(0f, 1f)]
    [SerializeField] private float _cubesSpawnChance;
    
    public float CubesSpawnChance => _cubesSpawnChance;

    public void Initialize(Cube parentCube)
    {
        _cubesSpawnChance = parentCube.CubesSpawnChance * ChildScale;
        gameObject.transform.localScale = parentCube.transform.localScale * ChildScale;

        if (gameObject.TryGetComponent(out MeshRenderer meshRenderer))
        {
            meshRenderer.material.color = UtilsRandom.GetRandomColor();
        }
    }
}
