using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniÄventyr
{
    class Mage : Player, IHealable
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
            if (HP > MaxHP)
            { HP = MaxHP; }
            Mana += restoredMana;
            if (Mana > MaxMana)
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
