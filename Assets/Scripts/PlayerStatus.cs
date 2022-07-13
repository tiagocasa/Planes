using System.Collections.Generic;

public class PlayerStatus
{
    private List<int> playerSkins;

    private string username;
    private int avatarId;
    private int highscore;
    private int coins;
    private int cash;
    private int coinUpgrade;
    private int magnetUpgrade;
    private int gasUpgrade;
    private int dashUpgrade;
    private string skinNameSelected;

    public PlayerStatus(string username, int highscore, int coins, int cash, int coinUpgrade, int magnetUpgrade, int gasUpgrade, int dashUpgrade, string skinNameSelected, List<int> playerSkins, int avatarId)
    {
        this.username = username;
        this.highscore = highscore;
        this.coins = coins;
        this.cash = cash;
        this.coinUpgrade = coinUpgrade;
        this.magnetUpgrade = magnetUpgrade;
        this.gasUpgrade = gasUpgrade;
        this.dashUpgrade = dashUpgrade;
        this.skinNameSelected = skinNameSelected; ;
        this.playerSkins = playerSkins;
        this.avatarId = avatarId;
    }

    public PlayerStatus()
    {
        // Empty constructor.
    }
    public string Username { get => username; set => username = value; }
    public int Highscore { get => highscore; set => highscore = value; }
    public int Coins { get => coins; set => coins = value; }
    public int Cash { get => cash; set => cash = value; }
    public int CoinUpgrade { get => coinUpgrade; set => coinUpgrade = value; }
    public int MagnetUpgrade { get => magnetUpgrade; set => magnetUpgrade = value; }
    public int GasUpgrade { get => gasUpgrade; set => gasUpgrade = value; }
    public int DashUpgrade { get => dashUpgrade; set => dashUpgrade = value; }
    public string SkinNameSelected { get => skinNameSelected; set => skinNameSelected = value; }
    public List<int> PlayerSkins { get => playerSkins; set => playerSkins = value; }
    public int AvatarId { get => avatarId; set => avatarId = value; }

    public PlayerStatus Initialize()
    {
        username = "Guest";
        Highscore = 0;
        Coins = 0;
        Cash = 0;

        CoinUpgrade = 0;
        MagnetUpgrade = 0;
        GasUpgrade = 0;
        DashUpgrade = 0;

        SkinNameSelected = "Default";

        PlayerSkins = new List<int>();
        PlayerSkins.Add(0);
        
        return this;
    }
}
