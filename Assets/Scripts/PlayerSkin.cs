using UnityEngine;

public class PlayerSkin
{
    private int id;

    public PlayerSkin(int id)
    {
        this.id = id;
    }

    public int Id { get => id; set => id = value; }
}
