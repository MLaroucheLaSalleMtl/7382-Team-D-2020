
using UnityEngine;

public static class PlayerData 
{
    //Here is a file that contains all the necessary data to the player.

    //Statistics
    //REMARKS: NEED TO UPDATE THEIR TEXT.NAME TOO OTHERWISE WILL BREAK
    private static int deathBySpikes = 0;
    private static int deathBySaw = 0;
    private static int deathByArrow = 0 ;
    private static int deathByFall = 0;
    private static int numGamOpentime = 0;
    
    public static int TotalDeaths => deathByFall + deathByArrow + deathBySaw + deathBySpikes;

    //TODO: Add test conditions for setters;
    public static int DeathBySpikes { get => deathBySpikes; set => deathBySpikes = value < 0? 0: value; }
    public static int DeathBySaw { get => deathBySaw; set => deathBySaw = value < 0 ? 0 : value; }
    public static int DeathByArrow { get => deathByArrow; set => deathByArrow = value < 0 ? 0 : value; }
    public static int DeathByFall { get => deathByFall; set => deathByFall = value < 0 ? 0 : value; }
    public static int NumGamOpentime { get=> numGamOpentime; set => numGamOpentime = value < 0 ? 0 : value; }

    public static void LoadInitialData()
    { 
        deathBySaw = PlayerPrefs.GetInt(nameof(deathBySaw));
        deathBySpikes = PlayerPrefs.GetInt(nameof(deathBySpikes));
        deathByArrow = PlayerPrefs.GetInt(nameof(deathByArrow));
        deathByFall = PlayerPrefs.GetInt(nameof(deathByFall));
        numGamOpentime = PlayerPrefs.GetInt(nameof(numGamOpentime));
    }

    public static void SaveData()
    {
        PlayerPrefs.SetInt(nameof(deathBySaw), deathBySaw);
        PlayerPrefs.SetInt(nameof(deathBySpikes), deathBySpikes);
        PlayerPrefs.SetInt(nameof(deathByArrow), deathByArrow);
        PlayerPrefs.SetInt(nameof(deathByFall), deathByFall);
        PlayerPrefs.SetInt(nameof(numGamOpentime), numGamOpentime);
    } 
}
