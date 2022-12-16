using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawningRate;
    [SerializeField] private GameObject ZombiePrefab;
    [SerializeField] private Transform[] SpawnPoints;
    public Player player;

    private float LastSpawnTime;

    void Update()
    {
        if (player == null) return;
        if (LastSpawnTime + spawningRate < Time.time)
        {
            var randomSpownPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length - 1)];
            Instantiate(ZombiePrefab, randomSpownPoint.position, Quaternion.identity);
            LastSpawnTime = Time.time;
            spawningRate *= 0.98f;
        } 
    }
}
