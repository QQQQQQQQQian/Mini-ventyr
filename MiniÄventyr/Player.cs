
namespace MiniÄventyr
{
    public abstract class Player
    {
       public string Name { get; set; } 
        public string Role { get; set; }
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public int Damage { get; set; }
       public int Gold { get; set; }

        public List<string> Inventory = new List<string>();
        public Player(string name, string role, int hp, int maxHp, int damage, int gold)
        {
            Name = name;
            Role = role;
            HP = hp;
            MaxHP = maxHp;
            Damage = damage;
            Gold = gold;
            Inventory = new List<string>();
        }
        public DateTime LastSkillTime;
        public int SkillCooldown = 60; // Cooldown in seconds   
        protected bool CanUseSkill()
        {
            TimeSpan timeSinceLastUse = DateTime.Now - LastSkillTime; // Calculate time since last skill use
            return timeSinceLastUse.TotalSeconds >= SkillCooldown; // Check if cooldown period has passed
        }
        protected void UpdateLastSkillTime()
        {
            LastSkillTime = DateTime.Now;
        }
        public virtual void Status()
        {
            Console.WriteLine($"Player: {Name}, Role: {Role}, HP: {HP}/{MaxHP}, Damage: {Damage}, Gold: {Gold}");
        }
        public void Attack(Enemy enemy, int dmg)
        {
            if(enemy.HP <= 0)
            {
                return;
                
            }
            enemy.HP -= dmg;


            if (enemy.HP <=0)
            {
                enemy.HP = 0;
                

                HandleEnemyDefeated(enemy); // Handle loot and gold reward

            }
            else
            {
                Console.WriteLine($"{Name} attacks {enemy.Name} for {dmg} damage! Now {enemy.Name} has {enemy.HP} HP.");
            }
            
        }
        public virtual void Rest(Player player)
        {
            if(player.HP == player.MaxHP)
            {
                Console.WriteLine($"{player.Name} is already at full health!");
                return;
            }
            int restoredHP = 20;
            int beforeHP = player.HP;

            player.HP += restoredHP;

            if (player.HP > player.MaxHP)
            {
                player.HP = player.MaxHP;
                
            }
            int actualRestored = player.HP - beforeHP;
            Console.WriteLine($"{player.Name} rested and restored {actualRestored} HP. Current HP : {player.HP}/{player.MaxHP}");

        }
        public void AddToInventory(string item)
        {
            Inventory.Add(item);
        }
        public void ShowInventory()
        {
            Console.WriteLine("=== Inventory ===");
            if (Inventory.Count == 0)
            {
                Console.WriteLine($"{Name}'s inventory is empty.");
                return;
            }
            Console.WriteLine($"{Name}'s Inventory:");
            foreach (var item in Inventory)
            {
                Console.WriteLine($"- {item}");
            }
        }
        public void HandleEnemyDefeated(Enemy enemy)
        {
            Random rand = new Random();
            string lootItem = enemy.Loot[rand.Next(enemy.Loot.Count)];
            AddToInventory(lootItem);

            Gold += enemy.GoldReward;
            Console.WriteLine($"{enemy.Name} has been defeated!");

            Console.WriteLine($"{enemy.DeathQuote()}");
            Console.WriteLine($"{Name} found \"{lootItem}\" and gained {enemy.GoldReward} gold. Current gold: {Gold}.");
        }

    }
  
}
