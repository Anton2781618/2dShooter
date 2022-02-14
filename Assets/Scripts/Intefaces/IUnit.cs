
public interface IUnit 
{
    int Health{get; set;}

    //объект (предмет) который можно удерживать в руках
    Iholding Heands{get; set;}

    //объект инвентарь
    IInvent Inventory{get; set;}

    //объект с помощью которого можно двигаться и поорачиваться
    IUnitMovemant Movemant{get; set;}


    void TakeDamage(int damage);
    void Healing(int damage);
    void Die();
}
