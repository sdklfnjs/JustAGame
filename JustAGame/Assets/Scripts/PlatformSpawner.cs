using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject _platformPrefab;
    [SerializeField] private float _verticaloffset = 0.5f;

    private float? _lastpointpositionY = null;
    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        Transform randomSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        float spawnPointPositionY = _lastpointpositionY == null ? randomSpawnPoint.position.y : (float)_lastpointpositionY + _verticaloffset;

        randomSpawnPoint.position = new Vector3(randomSpawnPoint.position.x, spawnPointPositionY, randomSpawnPoint.position.z);
        _lastpointpositionY = spawnPointPositionY;

        Instantiate(_platformPrefab, randomSpawnPoint.position, Quaternion.identity);
    }
}
