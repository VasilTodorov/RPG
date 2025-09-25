using RPG.GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Repository
{
    public static class HeroMapper
    {
        public static HeroModel ToModel(Hero hero)
        {
            return new HeroModel
            {
                Profession = hero.HeroProfesion.ToString(),
                Strength = hero.Strength,
                Agility = hero.Agility,
                Intelligence = hero.Inteligence,
                BonusStrength = hero.BonusStrength,
                BonusAgility = hero.BonusAgility,
                BonusIntelligence = hero.BonusIntelligence,
                Range = hero.Range,
                Symbol = hero.Symbol
            };
        }

        public static Hero ToDomain(HeroModel model)
        {
            var profession = Enum.Parse<Hero.Profession>(model.Profession);            
            return new Hero(profession, model.BonusStrength, model.BonusAgility, model.BonusIntelligence);
        }
    }
}
