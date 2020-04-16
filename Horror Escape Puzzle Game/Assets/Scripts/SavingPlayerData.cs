
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

    [RuntimeInitializeOnLoadMethod]
    public static void LoadInitialData()
    {
        Debug.Log("===LoadInitData===");
        deathBySaw = PlayerPrefs.GetInt(nameof(deathBySaw));
        deathBySpikes = PlayerPrefs.GetInt(nameof(deathBySpikes));
        deathByArrow = PlayerPrefs.GetInt(nameof(deathByArrow));
        deathByFall = PlayerPrefs.GetInt(nameof(deathByFall));
        numGamOpentime = PlayerPrefs.GetInt(nameof(numGamOpentime));

        numGamOpentime++;
        Debug.Log(numGamOpentime);
    }

    public static void SaveData()
    {
        PlayerPrefs.SetInt(nameof(deathBySaw).ToString(), deathBySaw);
        PlayerPrefs.SetInt(nameof(deathBySpikes).ToString(), deathBySpikes);
        PlayerPrefs.SetInt(nameof(deathByArrow).ToString(), deathByArrow);
        PlayerPrefs.SetInt(nameof(deathByFall).ToString(), deathByFall);
        PlayerPrefs.SetInt(nameof(numGamOpentime).ToString(), numGamOpentime);
    } 
}

public class SavingPlayerData: MonoBehaviour
{
#if UNITY_EDITOR
    private void OnDestroy()
    {
        PlayerData.SaveData();
    }
#endif
    private void OnApplicationQuit()
    {
        PlayerData.SaveData();
    }
}