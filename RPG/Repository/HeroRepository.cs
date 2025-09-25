using RPG.GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Repository
{
    public class HeroRepository
    {
        private readonly RPGContext _context;
        public HeroRepository(RPGContext context) => _context = context;

        public void Save(Hero hero)
        {
            var model = HeroMapper.ToModel(hero);
            _context.Heroes.Add(model);
            _context.SaveChanges();
        }

        public Hero? Load(int id)
        {
            var model = _context.Heroes.First(h => h.Id == id);
            if (model == null)
            {
                return null; 
            }
            return HeroMapper.ToDomain(model);
        }
    }
}
