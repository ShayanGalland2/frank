using System;

namespace LesChroniquesDeLArcaTech
{
    public class CombatSystem
    {
        private int playerHp;
        private int playerAttackPower;
        private Enemy enemy;
        private Random random = new Random();

        public CombatSystem(int playerHp, int playerAttack, Enemy enemy)
        {
            this.playerHp = playerHp;
            this.playerAttackPower = playerAttack;
            this.enemy = enemy;
        }

        public bool StartCombat()
        {
            DisplayHealthBars();


            bool playerTurn = random.Next(0, 2) == 0;

            while (playerHp > 0 && enemy.Hp > 0)
            {
                if (playerTurn)
                {
                    Console.WriteLine("Player's turn. Choose an action: A) Attack  R) Run");
                    string action = Console.ReadLine();
                    if (action.ToUpper() == "A")
                    {
                        PerformPlayerAttack();
                        if (enemy.Hp <= 0)
                        {
                            Console.WriteLine("Enemy defeated!");
                            return true;
                        }
                    }
                    else if (action.ToUpper() == "R")
                    {
                        Console.WriteLine("Player runs away!");
                        return false;
                    }
                    else if (action.ToUpper() != "R" || action.ToUpper() != "A")
                    {
                        Console.WriteLine("Learn how to press buttons!");
                    }

                    playerTurn = false;

                }
                else
                {
                    PerformEnemyAttack(ref playerTurn);
                }
            }

            return playerHp > 0;
        }

        private void DisplayHealthBars()
        {
            int playerHealthToShow = Math.Max(0, playerHp);
            int enemyHealthToShow = Math.Max(0, enemy.Hp);

            Console.WriteLine($"Player HP: [{new string('|', playerHealthToShow / 10)}] {playerHealthToShow}/100");
            Console.WriteLine($"Enemy HP:  [{new string('|', enemyHealthToShow / 10)}] {enemyHealthToShow}/100");

        }

        private void PerformPlayerAttack()
        {
            Console.WriteLine("Choose your attack: 1) Quick Strike  2) Heavy Blow  3) Magic Blast");
            string choice = Console.ReadLine();
            int damage = 0;
            string attackName = "";

            switch (choice)
            {
                case "1":
                    damage = 10; // Dégâts pour Quick Strike
                    attackName = "Quick Strike";
                    break;
                case "2":
                    damage = 20; // Dégâts pour Heavy Blow
                    attackName = "Heavy Blow";
                    break;
                case "3":
                    damage = 15; // Dégâts pour Magic Blast
                    attackName = "Magic Blast";
                    break;
                // Vous pouvez ajouter plus d'attaques ici
                default:
                    Console.WriteLine("Learn how to press buttons!");
                    break;

            }

            Console.WriteLine($"Player uses {attackName}, causing {damage} damage.");
            enemy.Hp -= damage;
            Console.WriteLine($"Enemy HP is now {enemy.Hp}.");

            // Afficher les barres de santé après l'attaque
            DisplayHealthBars();
        }

        private void PerformEnemyAttack(ref bool playerTurn)
        {
            Console.WriteLine("Enemy's turn.");
            Console.WriteLine("Enemy attacks!");
            playerHp -= enemy.AttackPower;
            Console.WriteLine($"Player HP is now {playerHp}.");

            // Afficher les barres de santé après l'attaque
            DisplayHealthBars();

            if (playerHp <= 0)
            {
                Console.WriteLine("Player is defeated!");
            }
            else
            {
                playerTurn = true; // Le tour passe au joueur
            }
        }

    }
}