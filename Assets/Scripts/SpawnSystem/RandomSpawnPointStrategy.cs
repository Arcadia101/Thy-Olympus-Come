using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Platformer
{
    public class RandomSpawnPointStrategy : ISpawnPointStrategy
    {
        List<Transform> unusedSpawnpoints;
        private Transform[] spawnPoints;

        public RandomSpawnPointStrategy(Transform[] spawnPoints)
        {
            this.spawnPoints = spawnPoints;
            unusedSpawnpoints = new List<Transform>(spawnPoints);
        }
        
        public Transform NextSpawnPoint()
        {
            if (!unusedSpawnpoints.Any())
            {
                unusedSpawnpoints = new List<Transform>(spawnPoints);
            }

            var randomIndex = Random.Range(0, unusedSpawnpoints.Count);
            Transform result = unusedSpawnpoints[randomIndex];
            unusedSpawnpoints.RemoveAt(randomIndex);
            return result;
        }
    }
}