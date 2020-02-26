using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

    //Here is a file that contains all the necessary data to the game.
    //i.e. saves, achievements, collections, etc...

    //Level Tracker
    private static int numLevelUnlocked;

    //Statistics
    private static int deathBySpikes;
    private static int deathBySaw;
    private static int deathByLaser;
    private static int deathByFall;
    private static int deathBySuicide;

    public static int NumLevelUnlocked  => numLevelUnlocked;
    
    public static int TotalDeaths => deathByFall + deathByLaser + deathBySaw + deathBySpikes + deathBySuicide;


    //TODO: Add test conditions for setters;
    public static int DeathBySpikes { get => deathBySpikes; set => deathBySpikes = value; }
    public static int DeathBySaw { get => deathBySaw; set => deathBySaw = value; }
    public static int DeathByLaser { get => deathByLaser; set => deathByLaser = value; }
    public static int DeathByFall { get => deathByFall; set => deathByFall = value; }
    public static int DeathBySuicide { get => deathBySuicide; set => deathBySuicide = value; }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }


    private void Start()
    {
        LoadInitialData();
    }

    private void LoadInitialData()
    {
        numLevelUnlocked = PlayerPrefs.GetInt("NumLevelsUnlocked");

        deathBySaw = PlayerPrefs.GetInt("SawKills");
        deathBySpikes = PlayerPrefs.GetInt("SpikeSkills");
        deathByLaser = PlayerPrefs.GetInt("LaserSkill");
        deathByFall = PlayerPrefs.GetInt("FallKills");
        deathBySuicide = PlayerPrefs.GetInt("Suicide");
    }
}
