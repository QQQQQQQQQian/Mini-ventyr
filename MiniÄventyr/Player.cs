
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

        public Player(string name, string role, int hp, int maxHp, int damage, int gold)
        {
            Name = name;
            Role = role;
            HP = hp;
            MaxHP = maxHp;
            Damage = damage;
            Gold = gold;
        }
        public DateTime LastSkillTime;
        public int SkillCooldown = 60; // Cooldown in seconds   
        protected bool CanUseSkill()
        {
            TimeSpan timeSinceLastUse = DateTime.Now - LastSkillTime;
            return timeSinceLastUse.TotalSeconds >= SkillCooldown;
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
                Console.WriteLine($"{enemy.Name} has been defeated!");
                Console.WriteLine($"{enemy.DeathQuote()}");
                Gold += enemy.GoldReward; // Reward for defeating an enemy
                Console.WriteLine($"{Name} gained {enemy.GoldReward} gold, now has {Gold} Gold.");
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
       
    }
    class Warrior : Player
    {
        public int PowerStrikeDamage { get; set; } = 30;

        public Warrior(string name) : base(name, "Warrior", 100, 100, 15, 50) { }


        public void PowerStrike(Enemy enemy)
        {
            TimeSpan cooldown = DateTime.Now - LastSkillTime;

            if (cooldown.TotalSeconds < SkillCooldown)
            {
                int remaining = SkillCooldown - (int)cooldown.TotalSeconds;
                Console.WriteLine($"Power Strike is on cooldown. Please wait {remaining} seconds.");
                return;
            }

            enemy.HP -= PowerStrikeDamage;
            if (enemy.HP < 0) enemy.HP = 0;
            Console.WriteLine($"{Name} uses Power Strike on {enemy.Name} for {PowerStrikeDamage} damage! Now {enemy.Name}s HP is {enemy.HP}");

            if (enemy.HP <= 0)
            {
                Console.WriteLine($"{enemy.Name} has been defeated!");
                Console.WriteLine(enemy.DeathQuote());
                Gold += enemy.GoldReward; // Reward for defeating an enemy
                Console.WriteLine($"{Name} gained {enemy.GoldReward} gold, now has {Gold} Gold.");
            }
            UpdateLastSkillTime();
        }

    }







     class Mage : Player
     {
            public int Mana = 50;
            public int MaxMana = 50;
            public int HealAmount = 15;
            public int ManaCost = 10;
            


        public Mage(string name) : base(name, "Mage", 80, 80, 20, 30) { }
            
        public override void Status()
        {
            base.Status();
            Console.WriteLine($"Mana: {Mana}/{MaxMana}");
        }
        public override void Rest(Player player)
        {
            if (player.HP == player.MaxHP && Mana == MaxMana)
            {
                Console.WriteLine($"{player.Name} is already at full health and mana!");
                return;
            }
            int restoredHP = 15;
            int restoredMana = 10;
            HP += restoredHP;
            if(HP > MaxHP)
            {  HP = MaxHP; }
            Mana += restoredMana;
            if(Mana > MaxMana)
            {
                Mana = MaxMana;
            }
            Console.WriteLine($"{player.Name} rested and restored {restoredHP} HP and {restoredMana} MP and now has {HP} HP, {Mana} MP");
            Console.ReadKey();
        }
        public void Heal(Player target)
        {
            TimeSpan cooldown = DateTime.Now - LastSkillTime;
            if (cooldown.TotalSeconds < SkillCooldown)
            {
                int remaining = SkillCooldown - (int)cooldown.TotalSeconds;
                Console.WriteLine($"Heal is on cooldown. Please wait {remaining} seconds.");
                return;
            }
            if (Mana < ManaCost)
            {
                Console.WriteLine("Not enough mana to heal.");
                return;
            }
            if (target.HP >= target.MaxHP)
            {

                Console.WriteLine($"{target.Name} is already at full health!");
                return;
            }
            int healAmount = Math.Min(HealAmount, target.MaxHP - target.HP);
            target.HP += healAmount;
            Mana -= ManaCost;
            UpdateLastSkillTime();
            Console.WriteLine($"{Name} heals {target.Name} for {healAmount} HP. {target.Name}'s HP is now {target.HP}/{target.MaxHP}. Mana left: {Mana}");
        }

     }
}
