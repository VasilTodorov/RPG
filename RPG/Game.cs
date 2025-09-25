// See https://aka.ms/new-console-template for more information
using RPG.GameLogic;
using RPG.Repository;
using System.Security.Cryptography;
using System.Text;

namespace RPG
{
    public enum GameState { MainMenu, PAUSED, CharacterSelect, Exit };
    public class Game
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Hello, RPG!");
            //string m = "\u25D9"; 
            Hero hero = new Hero(Hero.Profession.Archer);
            hero.AddAgility(1);
            hero.AddStrength(2);
            hero.Position.X = 2;
            hero.SetUp();
            Console.WriteLine($"My Hero has Base Strength = {hero.Strength}");
            Console.WriteLine($"My Hero has Bonus Strength = {hero.BonusStrength}");
            Console.WriteLine($"My Hero has Base Agility = {hero.Agility}");
            Console.WriteLine($"My Hero has Bonus Agility = {hero.BonusAgility}");
            Console.WriteLine($"My Hero has Base Inteligence = {hero.Inteligence}");
            Console.WriteLine($"My Hero has Bonus Inteligence = {hero.BonusIntelligence}");
            Console.WriteLine("**********************************************");
            Console.WriteLine($"My Hero has Health = {hero.Health}");
            Console.WriteLine($"My Hero has Mana = {hero.Mana}");
            Console.WriteLine($"My Hero has Damage = {hero.Damage}");
            Console.WriteLine($"My Hero has Range = {hero.Range}");
            Console.WriteLine($"My Hero has Symbol = {hero.Symbol}");
            Console.WriteLine($"My Hero Profesion is = {hero.HeroProfesion.ToString()}");

            for (int i=0; i<10; i++)
            {
                for(int j=0; j<10; j++)
                {
                    if(i==2 & j == 4)
                        Console.Write("*");
                    else
                        Console.Write("▒");
                }
                Console.WriteLine();
            }
            Console.WriteLine("**********************************************");
            //Console.WriteLine(Path.GetFullPath("rpg.db"));
            using var db = new RPGContext();
            var repo = new HeroRepository(db);
            //repo.Save(hero);
            Console.WriteLine("Saving Hero ...");
            Console.WriteLine("**********************************************");
            //var loadedHero = repo.Load(1);
            //if(loadedHero != null )
            //{
            //    loadedHero.SetUp();
            //    Console.WriteLine("Loading hero with id 2");
            //    Console.WriteLine($"My loadedHero has Health = {loadedHero.Health}");
            //    Console.WriteLine($"My loadedHero has Mana = {loadedHero.Mana}");
            //    Console.WriteLine($"My loadedHero has Damage = {loadedHero.Damage}");
            //    Console.WriteLine($"My loadedHero has Range = {loadedHero.Range}");
            //    Console.WriteLine($"My loadedHero has Symbol = {loadedHero.Symbol}");
            //    Console.WriteLine($"My loadedHero Profesion is = {loadedHero.HeroProfesion.ToString()}");
            //}
            //int[,] test = new int[10,2];
            //test[9, 1] = 3;
            //Console.WriteLine($"Test[9,1] = {test[9, 1]}");
            //Console.WriteLine($"Test[1,1] = {test[1, 1]}");
            Console.WriteLine("**********************************************");
            GameBoard newGame = new GameBoard(hero);
            newGame.CreateMonster();
            newGame.MonstersTurn();

            newGame.DrawBoard();
            newGame.ConsolePrintBoard();
            Console.WriteLine("**********************************************");
            newGame.CreateMonster();
            newGame.MonstersTurn();

            newGame.DrawBoard();
            newGame.ConsolePrintBoard();
            Console.WriteLine($"My Hero has Health = {hero.Health}");
            Console.WriteLine("**********************************************");
            newGame.CreateMonster();
            newGame.MonstersTurn();

            newGame.DrawBoard();
            newGame.ConsolePrintBoard();
            Console.WriteLine($"My Hero has Health = {hero.Health}");
            Console.WriteLine("**********************************************");
            newGame.CreateMonster();
            newGame.MonstersTurn();

            newGame.DrawBoard();
            newGame.ConsolePrintBoard();
            Console.WriteLine($"My Hero has Health = {hero.Health}");
            Console.WriteLine("**********************************************");
            newGame.CreateMonster();
            newGame.MonstersTurn();

            newGame.DrawBoard();
            newGame.ConsolePrintBoard();
            Console.WriteLine($"My Hero has Health = {hero.Health}");
            Console.WriteLine("**********************************************");
            newGame.CreateMonster();
            newGame.MonstersTurn();

            newGame.DrawBoard();
            newGame.ConsolePrintBoard();
            Console.WriteLine($"My Hero has Health = {hero.Health}");

            while (true)
            {
                Console.Write("Enter move (WASD/QEZX): ");
                var input = Console.ReadLine();
                if (input == null || !newGame.HeroMovement(input))
                    Console.WriteLine("Invalid move!");
                Console.WriteLine("**********************************************");
                newGame.DrawBoard();
                newGame.ConsolePrintBoard();
            }

        }
    }
}




