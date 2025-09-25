using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Repository
{
    public class GameModel
    {
        public int Id { get; set; }
        public int HeroId { get; set; }
        public HeroModel Hero { get; set; } = default!;
        public int MonstersKilled { get; set; }
    }
}
