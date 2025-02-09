using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GamePlay.Enemy
{
    public class TargetSpawner : MonoBehaviour
    {
        [SerializeField] private List<Target> targets = new();
        [SerializeField] private Transform rightUpFront;
        [SerializeField] private Transform leftDownBack;
        [SerializeField] private Vector2 spawnInterval = new(3, 8);
        
        private Coroutine _spawnCoroutine;

        private void OnEnable()
        {
            
            ContinuouslySpawn();
        }
        
        private void OnDisable()
        {
            StopSpawn();
        }

        private void ContinuouslySpawn()
        {
            _spawnCoroutine = StartCoroutine(DoContinuouslySpawn());
        }
        
        private void StopSpawn()
        {
            StopCoroutine(_spawnCoroutine);
        }
        
        private IEnumerator DoContinuouslySpawn()
        {
            while (true)
            {
                SpawnTarget();
                yield return new WaitForSeconds(Random.Range(spawnInterval.x, spawnInterval.y));
            }
        }
        private void SpawnTarget()
        {
            var target = Instantiate(targets[Random.Range(0, targets.Count)], transform, true);
            target.transform.position = new Vector3(Random.Range(leftDownBack.position.x, rightUpFront.position.x),
                Random.Range(leftDownBack.position.y, rightUpFront.position.y),
                Random.Range(leftDownBack.position.z, rightUpFront.position.z));
        }
    }
}