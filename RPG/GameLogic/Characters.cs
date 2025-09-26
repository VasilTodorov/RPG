using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.GameLogic
{
    public class Character
    {        
        public int Strength { get ; protected set; }
        public int Agility { get; protected set; }
        public int Intelligence { get; protected set; }
        public int Range { get; protected set; }
        //public string Symbol { get; protected set; }
        public int Damage { get;set; }
        public int Health { get;set; }
        public int Mana { get;set; }
        public bool Alive { get; private set; }
        public (int X, int Y) Position;

        public Character(int strength, int agilty, int intelligence, int range)
        {
            Strength = strength;
            Agility = agilty;
            Intelligence = intelligence;
            Range = range;
            Alive = true;
            //Symbol = symbol;
            SetUp();
        }
        public virtual void SetUp()
        {
            this.Health = this.Strength * 5;
            this.Mana = this.Intelligence * 3;
            this.Damage = this.Agility * 2;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Health = 0;
                Alive = false;
            }
        }
    }
    public record HeroTemplate(int Strength, int Agility, int Intelligence, int Range, string Symbol);
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
        public string Symbol { get; protected set; }
        public int BonusStrength { get; protected set; }
        public int BonusAgility { get; protected set; }
        public int BonusIntelligence { get; protected set; }
        public Hero(Profession profession) : this(profession,0,0,0)
        {}
        public Hero(Profession profession,int bonusStrength, int bonusAgility, int bonusIntelligence)
            : base(
                  Templates[profession].Strength,
                  Templates[profession].Agility,
                  Templates[profession].Intelligence,
                  Templates[profession].Range
                  )
        {
            HeroProfesion = profession;
            AddStrength(bonusStrength);
            AddAgility(bonusAgility);
            AddIntelligence(bonusIntelligence);            
            Symbol = Templates[profession].Symbol;
            SetUp();
        }
        public void AddStrength(int bonusStrength)
        {            
            BonusStrength += bonusStrength;
            Strength += bonusStrength;
        }
        public void AddIntelligence(int bonusIntelligence)
        {            
            BonusIntelligence += bonusIntelligence;
            Intelligence += bonusIntelligence;
        }
        public void AddAgility(int bonusAgility)
        {            
            BonusAgility += bonusAgility;
            Agility += bonusAgility;
        }        
                

    }
    
    public class Monster : Character
    {
        public static Random rnd = new Random();
        public string Symbol { get; protected set; }
        
        public Monster() : base(rnd.Next(1, 4), rnd.Next(1, 4), rnd.Next(1, 4), 1)
        {
            Symbol = "◙";
        }
        
        public void Attack(Hero hero)
        {

        }
    }

}
