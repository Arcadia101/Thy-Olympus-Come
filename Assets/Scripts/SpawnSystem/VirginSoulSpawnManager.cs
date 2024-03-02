using System;
using UnityEngine;
using Utilities;

namespace Platformer
{
    public class VirginSoulSpawnManager : EntitySpawnManager
    {
        [SerializeField] private CollectibleData[] virginSoulData;
        [SerializeField] private float spawnInterval = 1f;

        private EntitySpawner<VirginSoul> spawner;

        private CountdownTimer spawnTimer;
        private int counter;

        protected override void Awake()
        {
            base.Awake();

            spawner = new EntitySpawner<VirginSoul>(new EntityFactory<VirginSoul>(virginSoulData),
                spawnPointStrategy);

            spawnTimer = new CountdownTimer(spawnInterval);
            spawnTimer.OnTimerStop += () =>
            {
                if (counter++ >= spawnPoints.Length)
                {
                    spawnTimer.Stop();
                    return;
                }
                Spawn();
                spawnTimer.Start();
            };
        }

        private void Start() => spawnTimer.Start();

        private void Update() => spawnTimer.Tick(Time.deltaTime);
        
        public override void Spawn() => spawner.Spawn();
    }
}