using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Mirror;
using Mirror.Websocket;
using UnityEngine;
using Random = UnityEngine.Random;

public class T5 : NetworkBehaviour
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

    public float[,] heightMap;
    public float rng;
    public float lastRng;

    // Use this for initialization
    void Start()
    {
        offSetx = -gameObject.transform.position.x;
        offSetz = -gameObject.transform.position.z;
        Thread newThread = new Thread(GenerateNoiseMap);
        newThread.Start();

        //float [,] HeightMap = GenerateNoiseMap(x, z, Random.Range(0f, 10000f),waves);

    }


    [Command]
    public void CmdSendMap()
    {
        if (isServer) RpcSendMap(rng,lastRng);
    }
    
    [ClientRpc]
    public void RpcChangeMap(float _Rng)
    {
        
            lastRng = rng;
            rng = _Rng;
            gameObject.GetComponent<Terrain>().terrainData.SetHeights(0, 0, heightMap);
            Start();
        
    }

    [ClientRpc]
    public void RpcSendMap(float _Rng, float _LastRng)
    {
        
            lastRng = _LastRng;
            rng = _Rng;
            GenerateNoiseMap(lastRng);
            gameObject.GetComponent<Terrain>().terrainData.SetHeights(0, 0, heightMap);
            Start();
        
    }

    public void ChangeMap()
    {
        if (isServer)
        {
            lastRng = rng;
            rng = Random.Range(0f, 10000f);
            //gameObject.GetComponent<Terrain>().terrainData.SetHeights(0, 0, heightMap);
            RpcChangeMap(rng);
        }
    }
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) ChangeMap();
        
    }


    public void GenerateNoiseMap()
    {
        int mapDepth = x;
        int mapWidth = z;
        
        
        
        float[,] noiseMap = new float[mapDepth, mapWidth];
        temp = scale * 2 / mapDepth;
        for (int z = 0; z < mapDepth; z++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                
                
                float tempZ = (z + offSetz + rng) / scale;
                float tempX = (x + offSetx + rng) / scale;

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

        heightMap = noiseMap;
    }

    public void GenerateNoiseMap( float Rng)
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

        heightMap = noiseMap;
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
