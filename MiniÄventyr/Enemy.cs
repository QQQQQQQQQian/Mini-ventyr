using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniÄventyr
{
    public class Enemy: IAttackable
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public int Damage { get; set; }
        public int Level { get; set; }
        public int GoldReward { get; set; } 
        public List<string> Loot { get; set; }
        public Enemy(string name, int hp,  int damage, int level, int goldReward)
        {
            Name = name;
            HP = hp;
            MaxHP = hp;
            Level = level;
            Damage = damage;
            GoldReward = goldReward;
            Loot = new List<string>();// Initialize loot list
        }
        public virtual string DeathQuote()
        {
            return $"{Name} says: It's not end...";
        }
        public void DisplayStatus()
        {
                        Console.WriteLine($"{Name} - HP: {HP}/{MaxHP},Level: {Level}, Damage: {Damage}, GoldReward:{GoldReward}");
        }
        
        public void EnemyAttack(Player player)
        {
            player.HP -= Damage;
            if (player.HP > 0)
            {
                Console.WriteLine($"{Name} attacks {player.Name} for {Damage} damage! Now {player.Name}'s HP is {player.HP}/{player.MaxHP}");
            }

            else
            {
                player.HP = 0;
                Console.WriteLine($"{Name} delivers a fatal blow! {player.Name}'s HP dropped to 0!");
            }
        }
       

    }
    public class Rat : Enemy
    {
        public Rat() : base("Rat", 20, 5, 1, 10)
        {
            Loot = new List<string> { "Rat Tail", "Cheese", "Small Claw" }; // Example loot item
        }
        public override string DeathQuote()
        {
            return "Rat squeaks before dying: Squeak... squeak...";
        }
    }
    public class Goblin : Enemy
    {
        public Goblin() : base("Goblin", 30, 8, 2, 15)
        {
            Loot = new List<string> { "Goblin Ear" ,"Rusty Dagger"}; 
        }
        public override string DeathQuote()
        {
            return "Goblin snarls before dying: Grrr... I'll be back!";
        }
    }
    public class Skeleton : Enemy
    {
        public Skeleton() : base("Skeleton", 40, 10, 3, 20)
        {
            Loot = new List<string> { "Bone", "Rusty Sword" };
        }
        public override string DeathQuote()
        {
           return "Skeleton rattles before dying: Clack... clack...";
        }
    }
    public class Bandit : Enemy
    {
        public Bandit() : base("Bandit", 50, 12, 4, 25)
        {
            Loot = new List<string> { "Bandit Dagger" ,"Silver Coin"}; 
        }
        public override string DeathQuote()
        {
            return $"Bandit groans before dying : You got lucky this time...";
        }
    }
    public class Dragon : Enemy
    {
        public Dragon() : base("Dragon", 70, 20, 5, 35)
        {
            Loot = new List<string> { "Dragon Scale", "Treasure Chest" }; 
        }
        public override string DeathQuote()
        {
            return $"Dragon roars before dying: you may have won this time...";
        }
    }


}
