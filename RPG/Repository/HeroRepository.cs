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

        public int Save(Hero hero)
        {
            var heroModel = HeroMapper.ToModel(hero);
            _context.Heroes.Add(heroModel);
            _context.SaveChanges();

            return heroModel.Id;
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
