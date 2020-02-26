
using UnityEngine;

public class CurrentSessionPlayerData : MonoBehaviour
{
    [HideInInspector] public CurrentSessionPlayerData Instance = null;
    
    [Tooltip("If this number goes negative, it is intended.")]
    [HideInInspector]public static int Life;

    private void Awake()
    {
        if (!(Instance is null) && Instance != this)
        {
            Destroy(this.gameObject);
        }
        Instance = this;
    } //Singleton



    private void Start()
    {
        Life = 10;
    }


}
