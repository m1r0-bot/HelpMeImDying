
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLocationTexture : MonoBehaviour
{
    //public float spid = 1f;
    //List<Vector2> partPos = new List<Vector2>();

    Vector2 []partPos = new Vector2[100];                     
    Vector2 Resolution = new Vector2(1280, 720);

    

    

    void Start()
    {
        
        for(int i = 0; i < partPos.Length; i++)                                           //Generating Starting Random Positions for each particle between 0 and 1;
        {
            partPos[i] = new Vector2(Random.value * Resolution.x, Random.value * Resolution.y);
        }

    }

    
     void Update()
    {
       
        Texture2D particles = new Texture2D(1280, 720, TextureFormat.RGBA32, false);      //Create empty Texture

        for (int i = 0; i < partPos.Length; i++)
        {

            Color color = new Color(0.1f, 0.1f, 0.1f);                                    //Set GrayScale Value for each particle on a Texture (more particles on one pixel == whiter color);
            if (particles.GetPixel((int)partPos[i].x, (int)partPos[i].y) != null) 
            { 
                color += particles.GetPixel((int)partPos[i].x, (int)partPos[i].y);
            }
            particles.SetPixel((int)partPos[i].x, (int)partPos[i].y, new Color(1.0f, 1.0f, 1.0f));


            
            partPos[i] += Direction(partPos[i]);                                          // Update the Position of all particles;

            partPos[i] = checkPosOf(partPos[i]);                                          
        }
        
        particles.Apply();                                                                //Applie new particle position on a texture;
        
        Shader.SetGlobalTexture("_PartPos", particles);                                   //Sending Texture Of all particle Positions to Shader;
        
        
            
        
        
    }

    Vector2 Direction(Vector2 uv)
    {
                          
        float scale = 16f;                                                                //direction of each particle according to perlin noise value
        return new Vector2(
                        Mathf.Cos(Mathf.PerlinNoise(uv.x/scale, uv.y/scale) * Mathf.PI * 2f),
                        Mathf.Sin(Mathf.PerlinNoise(uv.x/scale, uv.y/scale) * Mathf.PI * 2f) );
    }
    Vector2 checkPosOf(Vector2 coord)
    {
        if (coord.x >= Resolution.x) coord.x -= Resolution.x;                             //Check if the position is within the texture coordinates and if not, correct them! otherwise, unity will crash
        else if (coord.x < 0) coord.x += Resolution.x;

        if (coord.y >= Resolution.y) coord.y -= Resolution.y;
        else if (coord.y < 0) coord.y += Resolution.y;

        return coord;
    }


}
