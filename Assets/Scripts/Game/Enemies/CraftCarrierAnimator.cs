using UnityEngine;
using System.Collections;

public class CraftCarrierAnimator : MonoBehaviour {

    private AnimationState idle, spawn;
    private string idleAnim = "";
    private string spawnAnim = "Spawn";
    private string[] idleString = { "IdleMain", "IdleMain", "IdleMain", "Idle1", "Idle2", "Idle3" };

    bool playedDeath = false;
    // Use this for initialization
    void Start()
    {
        //idleAnim = idleString[Random.Range(0, idleString.Length)];
        //deathAnim = deathString[Random.Range(0, deathString.Length)];

        //idle = animation[idleAnim];
        spawn = animation[spawnAnim];

        
        spawn.wrapMode = WrapMode.Once;
        spawn.speed = 0.6f;
        
        animation.Play("Spawn");
        animation.PlayQueued("Idle2");
        animation.PlayQueued("Idle1");
    }

    // Update is called once per frame
    void Update()
    {
        if (!animation.isPlaying)
        {
            animation.Play(idleString[Random.Range(0,idleString.Length)]);
        }
    }
}
