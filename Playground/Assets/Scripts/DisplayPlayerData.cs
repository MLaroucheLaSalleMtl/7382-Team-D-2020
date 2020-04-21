
using UnityEngine;
using UnityEngine.UI;

public class DisplayPlayerData : MonoBehaviour
{

    [SerializeField] private Text[] texts = null;

    private void Awake()
    {
        PlayerData.LoadInitialData();
    }
    private void OnEnable()
    {
        SetValues();
    }

    private void SetValues()
    {
        foreach (Text txt in texts)
        {
            string temp = "";

            switch (txt.name)
            {
                case nameof(PlayerData.DeathByArrow):
                    temp = PlayerData.DeathByArrow.ToString();
                    break;

                case nameof(PlayerData.DeathByFall):
                    temp = PlayerData.DeathByFall.ToString();
                    break;

                case nameof(PlayerData.DeathBySaw):
                    temp = PlayerData.DeathBySaw.ToString();
                    break;

                case nameof(PlayerData.DeathBySpikes):
                    temp = PlayerData.DeathBySpikes.ToString();
                    break;

                case nameof(PlayerData.TotalDeaths):
                    temp = PlayerData.TotalDeaths.ToString();
                    break;

                case nameof(PlayerData.NumGamOpentime):
                    temp = PlayerData.NumGamOpentime.ToString();
                    break;
            }
            txt.text = temp;
        }
    }
}
