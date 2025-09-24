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
        public (int x, int y) Position;

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
    public class Hero : Character
    {
        public Hero(int strength, int agilty, int inteligence, int range, string symbol) 
            : base(strength, agilty, inteligence, range, symbol)
        {}

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
    }
    public class Mage : Hero
    {
        public Mage() : base(2, 1, 3, 3, "*")
        {
            
        }
    }
    public class Warrior : Hero
    {
        public Warrior() : base(3, 3, 0, 1, "@")
        {

        }
    }
    public class Archer : Hero
    {
        public Archer() : base(2, 4, 0, 2, "#")
        {

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
