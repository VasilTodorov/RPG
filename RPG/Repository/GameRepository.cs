using RPG.GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Repository
{
    public class GameRepository
    {
        private readonly RPGContext _context;
        public GameRepository(RPGContext context) => _context = context;

        public int CreateNewGame(int heroId)
        {
            var game = new GameModel
            {
                HeroId = heroId,
                MonstersKilled = 0
            };
            _context.Games.Add(game);
            _context.SaveChanges();
            return game.Id;
        }

        public void UpdateMonstersKilled(int gameId, int count)
        {
            var game = _context.Games.FirstOrDefault(g => g.Id == gameId);
            if (game != null)
            {
                game.MonstersKilled = count;
                _context.SaveChanges();
            }
        }

    }
}
