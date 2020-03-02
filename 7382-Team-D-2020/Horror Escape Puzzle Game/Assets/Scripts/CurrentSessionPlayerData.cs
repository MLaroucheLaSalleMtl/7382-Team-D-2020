
public static class CurrentSessionPlayerData
{
    
    private static int life = 5; //default 5 lifes per session, num can go negative

    public static int Life
    {
        get => life;
        set => life = value > life? life : value;
    }
}
