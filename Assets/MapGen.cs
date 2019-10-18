using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    public GameObject[] skyIsland;
    public GameObject[] top;
    public GameObject[] earth;
    public GameObject[] subEarth;
    public GameObject[] bottom;
    public GameObject[,] tileGrid;
    public int width;
    public int height;
    public int seed;
    public float scale;
    public bool guaranteeFlatSection;

    private void Start()
    {
        tileGrid = new GameObject[width, height];

        //Generate Perlin Biomes
        for (int i = 0; i < width; i++)
        {
            //determine the mountain height at a given level
            int mountainHeight = -1;
            if (i >= width / 2 - 15 && i <= width / 2 + 15 && guaranteeFlatSection)
            {
                mountainHeight = 99;
            }

            for (int j = 0; j < height; j++) 
            {
                float x = i/(float)width * scale;
                float y = j/(float)height * scale;
                float perlin = Mathf.PerlinNoise(x + seed, y + seed);
                Vector2 pos = new Vector2(transform.position.x + i, transform.position.y - j);
                if (j < 50)
                {
                    if (perlin < 0.15f)
                    {
                        tileGrid[i, j] = skyIsland[0];
                        Instantiate(skyIsland[0], pos, Quaternion.identity);
                    }
                    else
                    {
                        tileGrid[i, j] = null;
                    }
                }
                else if (j >= 50 && j < 150)
                {
                    if (mountainHeight == -1)
                    {
                        //find height of previous mountain
                        int previousMountain = 50;
                        if (i > 0)
                        {
                            for (previousMountain = 50; previousMountain < 150; previousMountain++)
                            {
                                if (tileGrid[i - 1, previousMountain] == top[0])
                                {
                                    break;
                                }
                            }
                        }
                        
                        mountainHeight = (150 - (int)Mathf.Ceil(100 * perlin) + previousMountain) / 2;
                        Debug.Log("Perlin value: " + perlin);
                        Debug.Log("mountain value: " + mountainHeight);
                    }
                    
                    if (j == mountainHeight)
                    {
                        tileGrid[i, j] = top[0];
                        Instantiate(top[0], pos, Quaternion.identity);
                    }
                    else if (j > mountainHeight)
                    {
                        if (perlin < 0.33f)
                        {
                            tileGrid[i, j] = top[1];
                            Instantiate(top[1], pos, Quaternion.identity);
                        }
                        else if (perlin < 0.66f)
                        {
                            tileGrid[i, j] = top[2];
                            Instantiate(top[2], pos, Quaternion.identity);
                        }
                        else
                        {
                            tileGrid[i, j] = top[2];
                            Instantiate(top[2], pos, Quaternion.identity);
                        }
                    }
                    
                }
                else if (j >= 150 && j < 200)
                {
                    //int rand = Random.Range(0, earth.Length);
                    if (perlin < 0.33f)
                    {
                        tileGrid[i, j] = earth[0];
                        Instantiate(earth[0], pos, Quaternion.identity);
                    } 
                    else if (perlin < 0.66f)
                    {
                        tileGrid[i, j] = earth[1];
                        Instantiate(earth[1], pos, Quaternion.identity);
                    } 
                    else
                    {
                        tileGrid[i, j] = earth[1];
                        Instantiate(earth[1], pos, Quaternion.identity);
                    }
                }
                else if (j >= 200 && j < 250)
                {
                    //int rand = Random.Range(0, earth.Length);
                    if (perlin < 0.33f)
                    {
                        tileGrid[i, j] = subEarth[0];
                        Instantiate(subEarth[0], pos, Quaternion.identity);
                    }
                    else if (perlin < 0.66f)
                    {
                        tileGrid[i, j] = subEarth[1];
                        Instantiate(subEarth[1], pos, Quaternion.identity);
                    }
                    else
                    {
                        tileGrid[i, j] = subEarth[2];
                        Instantiate(subEarth[2], pos, Quaternion.identity);
                    }
                }
                else
                {
                    //int rand = Random.Range(0, earth.Length);

                    if (perlin < 0.33f)
                    {
                        tileGrid[i, j] = null;
                        //Instantiate(bottom[0], pos, Quaternion.identity);
                    }
                    else if (perlin < 0.66f)
                    {
                        tileGrid[i, j] = bottom[0];
                        Instantiate(bottom[0], pos, Quaternion.identity);
                    }
                    else
                    {
                        tileGrid[i, j] = bottom[1];
                        Instantiate(bottom[1], pos, Quaternion.identity);
                    }
                }

    
            }
        }
    }
}
