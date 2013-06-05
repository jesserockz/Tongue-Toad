using UnityEngine;
using System.Collections;

public class TripMode : MonoBehaviour {

    public static int animationSpeed = 0;
    public static int shellDropMultiplier = 1;
    public static int pointsMultiplier = 2;
    public static int comboMultiplier = 3;
    public static int movementMultiplier = 4;
    public static float[] bonuses = new float[] { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f };

    float[] timesLeft = new float[] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
    public bool blur = false;

    public int toadsLicked = 0;


    public void lickToad()
    {
        if (toadsLicked == 0)
        {
            toadsLicked++;
            int i = Random.Range(0, bonuses.Length);
            timesLeft[i] = 2.0f;
        }
    }

    public void Update()
    {
        for(int i=0;i<timesLeft.Length;i++)
        {
            float f = timesLeft[i];
            f -= Time.deltaTime;
            if (f <= 0.0f)
            {
                bonuses[i] = 1.0f;
            }
        }
    }
   
}
