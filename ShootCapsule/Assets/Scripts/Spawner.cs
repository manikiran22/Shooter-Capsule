using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Wave[] wave;


    public Enenmy enemy;

    //current wave is the wave of enemies that is right now in the game area
    //waveNumber is the number tag of the wave (Wave-1, Wave-2, etc)
    Wave currentWave;
    int currentWaveNumber;

    //no of enemies that are remaining to spawn though the enemy count is given
    //
    int enemiesRemainigToSpawn;
    float nextSpawnTime;


    private void Start()
    {
        NextWave();
    }


    private void Update()
    {
        if (enemiesRemainigToSpawn > 0 && Time.time > nextSpawnTime)
        {
            enemiesRemainigToSpawn--;
            nextSpawnTime = Time.time + currentWave.timeBetweenSpwan;

            Enenmy spawnedEnemy = Instantiate(enemy, Vector3.zero, Quaternion.identity);
        }
    }

    void NextWave()
    {
        currentWaveNumber++;
        currentWave = wave[currentWaveNumber - 1];

        enemiesRemainigToSpawn = currentWave.enemyCount;
    }

    //here first we created the class of wave and gave it 2 inputs for spawing the wave.
    //then we created an array of that class Wave
    //so now with the system.serializable present the array are represented in the form of subsections in the inspector
    
    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
        public float timeBetweenSpwan;
    }

}
