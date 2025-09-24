using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Character
    {        
        public int Strength { get ; protected set; }
        public int Agility { get; protected set; }
        public int Inteligence { get; protected set; }
        public int Range { get; init; }
        public string Symbol { get; init; }
        public int Damage { get;set; }
        public int Health { get;set; }
        public int Mana { get;set; }
        public (int X, int Y) Position;

        public Character(int strength, int agilty, int inteligence, int range, string symbol)
        {
            Strength = strength;
            Agility = agilty;
            Inteligence = inteligence;
            Range = range;
            Symbol = symbol;
            SetUp();
        }
        public void SetUp()
        {
            this.Health = this.Strength * 5;
            this.Mana = this.Inteligence * 3;
            this.Damage = this.Agility * 2;
        }
    }
    public record HeroTemplate(int Strength, int Agility, int Inteligence, int Range, string Symbol);
    public class Hero : Character
    {
        public enum Profession { Mage, Warrior, Archer };
        public Profession HeroProfesion { get; }
        private static readonly Dictionary<Profession, HeroTemplate> Templates = new()
        {
            { Profession.Mage, new HeroTemplate(2, 1, 3, 3, "*") },
            { Profession.Warrior, new HeroTemplate(3, 3, 0, 1, "@") },
            { Profession.Archer, new HeroTemplate(2, 4, 0, 2, "#") }
        };
        public Hero(Profession profession) : base(
        Templates[profession].Strength,
        Templates[profession].Agility,
        Templates[profession].Inteligence,
        Templates[profession].Range,
        Templates[profession].Symbol)
        {
            HeroProfesion = profession;
        }

        public void AddStrength(int addition)
        {            
            this.Strength += addition;            
        }

        public void AddInteligence(int addition)
        {
            this.Inteligence += addition;            
        }

        public void AddAgility(int addition)
        {
            this.Agility += addition;            
        }
        //TODO save in database
        public void SaveHero()
        {
            throw new NotImplementedException();
        }
    }
    
    public class Monster : Character
    {
        static Random rnd = new Random();
        public Monster() : base(rnd.Next(1, 4), rnd.Next(1, 4), rnd.Next(1, 4), 1, "◙")
        {            
        }

    }

}
