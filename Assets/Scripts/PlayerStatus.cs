using System;

public class PlayerStatus
{

    private int highscore;
    private int coins;
    private int cash;
    private int coinUpgrade;
    private int magnetUpgrade;
    private int gasUpgrade;
    private int dashUpgrade;

    public PlayerStatus(int highscore, int coins, int cash, int coinUpgrade, int magnetUpgrade, int gasUpgrade, int dashUpgrade)
    {
        this.Highscore = highscore;
        this.Coins = coins;
        this.Cash = cash;
        this.CoinUpgrade = coinUpgrade;
        this.MagnetUpgrade = magnetUpgrade;
        this.GasUpgrade = gasUpgrade;
        this.DashUpgrade = dashUpgrade;
    }

    public PlayerStatus()
    {
        // Empty constructor.
    }
    public int Highscore { get => highscore; set => highscore = value; }
    public int Coins { get => coins; set => coins = value; }
    public int Cash { get => cash; set => cash = value; }
    public int CoinUpgrade { get => coinUpgrade; set => coinUpgrade = value; }
    public int MagnetUpgrade { get => magnetUpgrade; set => magnetUpgrade = value; }
    public int GasUpgrade { get => gasUpgrade; set => gasUpgrade = value; }
    public int DashUpgrade { get => dashUpgrade; set => dashUpgrade = value; }

    public PlayerStatus Initialize()
    {
        Highscore=0;
        Coins=0;
        Cash=0;

        CoinUpgrade=1;
        MagnetUpgrade=0;
        GasUpgrade=0;
        DashUpgrade=0;

        return this;
    }
}
