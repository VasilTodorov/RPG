// See https://aka.ms/new-console-template for more information
using RPG;
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
            Hero hero = new Hero(Hero.Profession.Mage);
            hero.AddAgility(1);
            hero.AddStrength(2);
            hero.Position.X = 2;
            hero.SetUp();
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
                    Console.Write("▒");
                }
                Console.WriteLine();
            }
    
        }
    }
}




