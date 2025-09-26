// See https://aka.ms/new-console-template for more information
using RPG.GameLogic;
using RPG.Repository;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace RPG
{
    public enum Screen { MainMenu, CharacterSelect, InGame, Exit };
    public class Game
    {
        public static Screen MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome!");
            Console.WriteLine();
            Console.Write("Press any key to play.");
            Console.ReadKey(true);
            Console.WriteLine();

            return Screen.CharacterSelect;
        }
        public static Hero.Profession CharacterSelect()
        {
            Console.Clear();
            Console.WriteLine("Choose character type:");
            Console.WriteLine("1) Warrior");
            Console.WriteLine("2) Archer");
            Console.WriteLine("3) Mage");
            Console.Write("Your pick: ");

            while (true)
            {
                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1": return Hero.Profession.Warrior;
                    case "2": return Hero.Profession.Archer;
                    case "3": return Hero.Profession.Mage;
                    default:
                        Console.WriteLine("Invalid choice. Please enter 1, 2, or 3:");
                        break;
                }
            }
        }
        public static (Hero hero, Screen next) PointAllocation(Hero.Profession profession)
        {
            var hero = new Hero(profession);
            int remainingPoints = 3;

            Console.WriteLine("Would you like to buff up your stats before starting?");
            Console.Write("Response Y/N: ");
            string? response = Console.ReadLine()?.Trim().ToUpper();

            if (response == "Y")
            {
                while (remainingPoints > 0)
                {
                    Console.Clear();
                    Console.WriteLine($"Distribute your points (Remaining: {remainingPoints})");
                    Console.WriteLine($"1) Strength: {hero.Strength}");
                    Console.WriteLine($"2) Agility: {hero.Agility}");
                    Console.WriteLine($"3) Intelligence: {hero.Intelligence}");
                    Console.Write("Choose a stat to increase (1-3): ");

                    string? choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            hero.AddStrength(1);
                            remainingPoints--;
                            break;
                        case "2":
                            hero.AddAgility(1);
                            remainingPoints--;
                            break;
                        case "3":
                            hero.AddIntelligence(1);
                            remainingPoints--;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Press Enter to try again.");
                            Console.ReadLine();
                            break;
                    }
                }
            }
            hero.SetUp();
            
            Console.Clear();
            Console.WriteLine("Final Hero Stats:");
            Console.WriteLine($"Strength: {hero.Strength}");
            Console.WriteLine($"Agility: {hero.Agility}");
            Console.WriteLine($"Intelligence: {hero.Intelligence}");
            Console.WriteLine($"Health: {hero.Health}, Mana: {hero.Mana}, Damage: {hero.Damage}");
            Console.Write("Press any key to continue.");
            Console.ReadKey(true);
            return (hero, Screen.InGame);
        }       
        public static (int kills, Screen next) InGame(Hero hero)
        {
            Console.OutputEncoding = Encoding.UTF8;
            using var db = new RPGContext();
            var heroRepo = new HeroRepository(db);
            var gameRepo = new GameRepository(db);

            int heroID = heroRepo.Save(hero);
            int gameID = gameRepo.CreateNewGame(heroID);
            
            GameBoard newGame = new GameBoard(hero);

            while(hero.Alive)
            {             
                newGame.CreateMonster();
                Console.Clear();
                Console.WriteLine($"Health: {hero.Health}   Mana: {hero.Mana}");
                Console.WriteLine();
                newGame.DrawBoard();
                newGame.ConsolePrintBoard();                

                bool acted = false;
                while (!acted)
                {
                    Console.WriteLine("Choose action");
                    Console.WriteLine("1) Attack");
                    Console.WriteLine("2) Move");
                    string? input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            if (!HeroAttackConsole(newGame))
                            {                                
                                break; 
                            }
                            acted = true;
                            break;

                        case "2":
                            Console.Write("Enter move (WASD/QEZX): ");
                            if (HeroMovementConsole(newGame,Console.ReadLine() ?? ""))
                                acted = true;
                            break;

                        default:
                            Console.WriteLine("Invalid choice.Please enter 1 or 2.");
                            break;
                    }
                }

                newGame.MonstersTurn();
            }

            gameRepo.UpdateMonstersKilled(gameID, newGame.KillCount);
            return (newGame.KillCount, Screen.Exit);
        }
        public static void ExitPage(Hero hero,int killCount)
        {
            Console.Clear();
            Console.WriteLine($"Game Over!");
            Console.WriteLine($"Hero: {hero.HeroProfesion}");
            Console.WriteLine($"Final Stats - Strength: {hero.Strength}, Agility: {hero.Agility}, Intelligence: {hero.Intelligence}");
            Console.WriteLine($"Final Kill Count: {killCount}");
            Console.WriteLine();
            Console.WriteLine("Thank you for playing!");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey(true);
            Environment.Exit(0);
            
        }
        public static void Run()
        {
            Screen current = Screen.MainMenu;
            Hero? hero = null;
            int kills = 0;

            while (true)
            {
                switch (current)
                {
                    case Screen.MainMenu:
                        current = MainMenu();
                        break;
                    case Screen.CharacterSelect:
                        var result = PointAllocation(CharacterSelect());
                        hero = result.hero;
                        current = result.next;
                        break;
                    case Screen.InGame:
                        var gameResult = InGame(hero!);
                        kills = gameResult.kills;
                        current = gameResult.next;
                        break;
                    case Screen.Exit:
                        if (hero is null)
                        {
                            Console.WriteLine("Error: No hero found. Returning to Main Menu...");
                            current = Screen.MainMenu;
                            break;
                        }
                        ExitPage(hero,kills);
                        break;
                }
            }
        }
        public static bool HeroAttackConsole(GameBoard board)
        {
            var targets = board.HeroAttackTargets();

            if (targets.Count == 0)
            {
                Console.WriteLine("No available targets in your range");
                return false;
            }

            Console.WriteLine("Available targets:");
            for (int i = 0; i < targets.Count; i++)
            {
                Console.WriteLine($"{i}) Target with remaining health: {targets[i].Health}");
            }

            while (true)
            {
                Console.Write("Which one to attack? ");
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int choice) && choice >= 0 && choice < targets.Count)
                {
                    board.HeroAttackChoice(targets, choice);
                    board.DrawBoard();
                    board.ConsolePrintBoard();
                    return true;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter a number between 0 and " + (targets.Count - 1));
                }
            }
        }
        public static bool HeroMovementConsole(GameBoard board, string key)
        {
            key = key.Trim().ToUpper();

            (int X, int Y) direction = key switch
            {
                "W" => (-1, 0), //UP
                "S" => (1, 0),  //Down
                "A" => (0, -1), //Left
                "D" => (0, 1),  //Right
                "Q" => (-1, -1),//LeftDiagonalUp
                "E" => (-1, 1), //RightDiagonalUp
                "Z" => (1, -1), //RightDiagonalDown
                "X" => (1, 1),  //LeftDiagonalDown
                _ => (0, 0)
            };

            if (direction == (0, 0) && key != "W" && key != "S" && key != "A" &&
                key != "D" && key != "Q" && key != "E" && key != "Z" && key != "X")
            {
                Console.WriteLine("Invalid choice, Please enter a key between (WASD/QEZX)");
                return false;
            }
            bool result = board.HeroMovement(direction);
            if (!result)
            {
                Console.WriteLine("Path is invalid, Please choose a diffrent direction");
            }
            return result;
        }
        
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Run();                        
        }
    }
}




