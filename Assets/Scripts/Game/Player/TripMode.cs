using UnityEngine;
using System.Collections;

public class TripMode : MonoBehaviour {

    public const int attackSpeed = 0;
    public const int shellDropMultiplier = 1;
    public const int pointsMultiplier = 2;
    public const int comboMultiplier = 3;
    public const int movementMultiplier = 4;
    public const int enemySpeed = 5;

    public static float[] bonuses;

    float[] timesLeft = new float[] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f};
    bool[] activated = new bool[] { false, false, false, false, false, false };
    public bool blur = false;

    bool cameraSpinning = false;

    public int toadsLicked = 0;

    private Player player;

    public void Start()
    {
        player = GetComponent<Player>();
        bonuses = new float[] { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f };
    }


    public void lickToad()
    {
        player.setTripping(true);
        if (toadsLicked == 0 || toadsLicked == 1)
        {
            toadsLicked++;
            if (toadsLicked == 2)
            {
                blur = true;
            }
            int i = Random.Range(0, bonuses.Length);
            switch (i)
            {
                case attackSpeed:
                case movementMultiplier:
                    bonuses[i] *= 1.5f;
                    break;
                case shellDropMultiplier:
                case pointsMultiplier:
                    bonuses[i] *= 2;
                    break;
                case comboMultiplier:
                    bonuses[i] *= 3;
                    break;
                case enemySpeed:
                    bonuses[i] *= 0.8f;
                    break;
            }
            timesLeft[i] += 10.0f;
            activated[i] = true;
        }
        else if (toadsLicked == 2)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControl>().activateSpin();
            cameraSpinning = true;
        }
    }

    public void OnGUI()
    {
        GUI.Label(new Rect(400, 5, 300, 30),  " Attack Speed: " + bonuses[0] + " Time: " + timesLeft[0]);
        GUI.Label(new Rect(400, 35, 300, 30), "   Shell Drop: " + bonuses[1] + " Time: " + timesLeft[1]);
        GUI.Label(new Rect(400, 65, 300, 30), " Points Multi: " + bonuses[2] + " Time: " + timesLeft[2]);
        GUI.Label(new Rect(400, 95, 300, 30), "  Combo Multi: " + bonuses[3] + " Time: " + timesLeft[3]);
        GUI.Label(new Rect(400, 125, 300, 30), "   Move Multi: " + bonuses[4] + " Time: " + timesLeft[4]);
        GUI.Label(new Rect(400, 155, 300, 30), "  Enemy Speed: " + bonuses[5] + " Time: " + timesLeft[5]);
        GUI.Label(new Rect(400, 185, 300 , 30), "Toads Licked : " + toadsLicked);
    }

    public void finishCameraSpin()
    {
        cameraSpinning = false;
    }

    public void Update()
    {
        int deactivated = 0;
        for(int i=0;i<timesLeft.Length;i++)
        {
            if (activated[i])
            {

                float f = timesLeft[i];
                f -= Time.deltaTime;
                timesLeft[i] = f; 

                if (f <= 0.0f)
                {
                    bonuses[i] = 1.0f;
                    timesLeft[i] = 0.0f;
                    activated[i] = false;
                    toadsLicked--;
                }
                
            }else
                deactivated++;
        }
        if (deactivated == timesLeft.Length && !cameraSpinning)
        {
            player.setTripping(false);
            toadsLicked = 0;

            blur = false;

        }
    }
   
}
