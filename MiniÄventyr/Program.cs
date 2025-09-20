using System.Dynamic;
using System.IO.IsolatedStorage;
using System.Threading.Channels;

namespace MiniÄventyr
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Enemy[] enemies = new Enemy[]
            {
               new Rat(),
               new Goblin(),
               new Skeleton(),
               new Bandit(),
               new Dragon()

            };


            Console.WriteLine("Welcome to the Mini Adventure Game!");
            Console.WriteLine("Entry your name: ");
            string playerName = Console.ReadLine();


            Console.WriteLine("Choose your class: 1. Warrior   2. Mage");
            int choice;
            while(!int.TryParse(Console.ReadLine(), out choice)||(choice !=1 && choice != 2) )
            {
                Console.WriteLine("Invalid choice, please choose again: 1. Warrior   2. Mage");

            }
            Player player;



            if (choice == 1)
            {
                player = new Warrior(playerName);
            }
            else
            {
                player = new Mage(playerName);
            }
            Console.WriteLine($"Welcome {player.Name} the {player.Role}!");
            Console.WriteLine("Press any key to start your adventure!");
            Console.ReadKey();



            bool running = true;
            Random rand = new Random();

            while (running && player.HP > 0)
            {
                Console.Clear();

                Console.WriteLine("=== Main Menu === ");
                Console.WriteLine("1.Adventure");
                Console.WriteLine("2.Rest");
                Console.WriteLine("3.Status");
                Console.WriteLine("4.Exit");

                int action;
                while (true)
                {
                    Console.WriteLine("Choose action(1-4): ");
                    if(int.TryParse(Console.ReadLine(), out action) && action >=1 && action <=4)
                    {
                        break;
                    }
                    Console.WriteLine("Invalid inout. Please enter a number between 1 and 4.");

                }
                switch (action)
                {
                    case 1:
                        Adventure(player, enemies,rand);
                        break;
                    case 2:
                        player.Rest(player);
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case 3:
                        player.Status();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case 4:
                        running = false;
                        Console.WriteLine("Thanks for playing! Hope to see you again soon!");
                        break;

                    default:
                        Console.WriteLine("Invalid action. Please choose again.");
                        Console.ReadKey();
                        break;
                }

            }
        }
        static void Adventure(Player player, Enemy[] enemies, Random rand)
        {
            Console.Clear();
            Console.WriteLine($"Welcome {player.Name} the {player.Role} to your adventure!");
            Console.WriteLine("Now you need to choose an enemy to fight...");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            int fightChoice;
            while (true)
            {
                Console.WriteLine("Do you want to: 1. choose an enemy  2. Random enemy");
                if (int.TryParse(Console.ReadLine(), out fightChoice) && (fightChoice == 1 || fightChoice == 2)) 
                break;
                Console.WriteLine("Invalid input, please choose again");
            }
            Enemy enemy;
            if (fightChoice == 1)
            {
                Console.WriteLine("Choose an enemy to fight: ");
                for (int i = 0; i < enemies.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {enemies[i].Name} (HP: {enemies[i].HP}, Damage: {enemies[i].Damage}, GoldReward: {enemies[i].GoldReward})");
                }

                int enemyIndex;
                while(!int.TryParse(Console.ReadLine(), out enemyIndex) || enemyIndex < 1 || enemyIndex > enemies.Length)
                {
                    Console.WriteLine("Invalid choice, returning to menu");
                    return;
                }
                enemy = GetRandomEnemy(new Enemy[] { enemies[enemyIndex - 1] },rand);

            }
            else 
            {
                enemy = GetRandomEnemy(enemies, rand);
               
                Console.WriteLine($"A wild {enemy.Name} appears!");

            }
            
            Console.WriteLine($"You have chosen to fight {enemy.Name}!");
            enemy.DisplayStatus();
            Console.WriteLine("Press any key to start the fight!");
            Console.ReadKey();

            bool inCombat = true;
            while (inCombat && player.HP > 0 && enemy.HP > 0)
            {
                Console.WriteLine("=== Combat Menu ===");
                Console.WriteLine("1. Attack");
                Console.WriteLine("2. Use Skill");
                Console.WriteLine("3. Run");
              

                int combatAction;
                Console.WriteLine("Choose action: ");
                if (!int.TryParse(Console.ReadLine(), out combatAction))
                {
                    Console.WriteLine("Invalid input, please enter a number: ");
                    continue;
                }
                switch (combatAction)
                {
                    case 1:
                        player.Attack(enemy, player.Damage);
                        Console.ReadKey();
                        break;
                    case 2:
                        if (player is Warrior warrior)
                        {
                            warrior.PowerStrike(enemy);
                        }
                        else if (player is Mage mage)
                        {
                            mage.Heal(player);
                        }
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.WriteLine("You fled the battle!");
                        inCombat = false;
                        Console.WriteLine("Press any key to return to the main menu.");
                        Console.ReadKey();
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please choose again.");

                        continue;

                }
                if (enemy.HP > 0)
                {
                    enemy.EnemyAttack(player);
                }


                if (enemy.HP <= 0)
                {
                    if(fightChoice == 2)
                    {
                        if (enemy is Dragon)
                        {
                            Console.WriteLine("Congratulations! You have defeated the final boss and completed the game!");
                            inCombat = false;
                            Environment.Exit(0);
                            return;
                        }
                        
                        minRandomLevel = enemy.Level + 1 ; // Ensure next random enemy is at least as strong as the chosen one
                        Console.WriteLine("You will meet a stronger enemy next random!");
                    }
                   
                    inCombat = false;

                }
                if (player.HP <= 0)
                {
                    Console.WriteLine("You have been defeated! Game Over!");
                    inCombat = false;
                    return;
                }
                
           
                     
                
            }
            Console.WriteLine("Combat ended. Press any key to return to the main menu.");
            Console.ReadKey();
        }
        static int minRandomLevel = 1;
        static Enemy GetRandomEnemy(Enemy[] enemies, Random rand)
        {  
            var possibleEnemies = enemies.Where(e => e.Level >= minRandomLevel).ToArray();
            if (possibleEnemies.Length == 0)
            {
                possibleEnemies = enemies; // Fallback to all enemies if none meet the criteria
            }
            Enemy template = enemies[rand.Next(enemies.Length)];
            switch (template.Name)
            {
                case "Rat":
                    return new Rat();
                case "Goblin":
                    return new Goblin();
                case "Skeleton":
                    return new Skeleton();
                case "Bandit":
                    return new Bandit();
                case "Dragon":
                    return new Dragon();
                default:
                    return new Enemy("Unknown", 10, 2, 1, 5);
            }
        }
    }
}
