using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class T5 : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public float seed;
        public float frequency;
        public float amplitude;
    }

    public int x;
    public int z;
    public float scale;
    float offSetx;
    float offSetz;
    public Wave[] waves;
    float temp;
    public bool steep;
    public float power;
    public float terrace;

    public float[,] HeightMap;
    private float Rng;

    // Use this for initialization
    void Start()
    {
        offSetx = -this.gameObject.transform.position.x;
        offSetz = -this.gameObject.transform.position.z;
        Rng = Random.Range(0f, 10000f);
        Thread newThread = new Thread(GenerateNoiseMap);
        newThread.Start();
        //float [,] HeightMap = GenerateNoiseMap(x, z, Random.Range(0f, 10000f),waves);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            this.gameObject.GetComponent<Terrain>().terrainData.SetHeights(0, 0, HeightMap);
            Start();
        }
    }



    public void GenerateNoiseMap( )
    {
        int mapDepth = x;
        int mapWidth = z;
        
        
        
        float[,] noiseMap = new float[mapDepth, mapWidth];
        temp = scale * 2 / mapDepth;
        for (int z = 0; z < mapDepth; z++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                
                
                float tempZ = (z + offSetz + Rng) / scale;
                float tempX = (x + offSetx + Rng) / scale;

                float noise = 0f;
                float normalization = 0f;
                foreach (Wave wave in waves)
                {
                    noise += wave.amplitude * Mathf.PerlinNoise(wave.frequency * tempX, wave.frequency * tempZ);
                    normalization += wave.amplitude;
                }
                noise /= normalization;

                if (steep)
                {
                    //make mountains steeper
                    noiseMap[z, x] = Mathf.Pow(noise, power);
                }
                else
                {
                    //make terraces
                    noiseMap[z, x] = Mathf.Round(noise * terrace) / terrace;
                }

                if (x < 10 || z < 10)
                {
                    noiseMap[z, x] = 0;
                }
            }
        }

        HeightMap = noiseMap;
    }

   
    /*
    public void TerrainType(Noise)
    {
        if (this.GetComponent<Terrain>().terrainData.)
        {

        }
    }
    */

}
