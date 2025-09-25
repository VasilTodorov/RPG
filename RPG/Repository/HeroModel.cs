using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Repository
{
    public class HeroModel
    {
        public int Id { get; set; }
        public string Profession { get; set; } = string.Empty; // "Mage", "Warrior", "Archer"
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public int BonusStrength { get; set; }
        public int BonusAgility { get; set; }
        public int BonusIntelligence { get; set; }
        public int Range { get; set; }
        public string Symbol { get; set; } = string.Empty ;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
