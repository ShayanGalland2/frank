public class Enemy
{
    public int Hp { get; set; }
    public int AttackPower { get; set; }

    public Enemy(int hp, int attackPower)
    {
        Hp = hp;
        AttackPower = attackPower;
    }
}
