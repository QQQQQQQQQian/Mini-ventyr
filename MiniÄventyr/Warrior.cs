using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniÄventyr
{
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
                HandleEnemyDefeated(enemy);
            }
            UpdateLastSkillTime(); // Update the last skill use time
        }

    }
}
