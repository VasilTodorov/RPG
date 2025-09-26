using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.GameLogic
{
    public class GameBoard
    {
        private enum BoardSpace { Empty, Monster, Hero };
        private Hero hero;
        private List<Monster> monsters;
        private BoardSpace[,] board;
        private string[,] canvasBoard;
        private int emptySpace;
        public int KillCount { get;private set; }
        public int Size { get;}
        public string Symbol { get; }
        
        public GameBoard(Hero _hero,int _size)
        {
            Size = _size;
            Symbol = "▒";
            board = new BoardSpace[Size, Size];
            canvasBoard = new string[Size, Size];

            for (int i = 0 ; i < Size * Size; i++)
            {
                board[i / Size, i % Size] = BoardSpace.Empty;
                canvasBoard[i / Size, i % Size] = string.Empty;
            }
                

            emptySpace = Size * Size - 1;

            this.hero = _hero;
            hero.Position = (1, 1);
            board[1,1] = BoardSpace.Hero;
            KillCount = 0;
            monsters = new List<Monster>(); 
        }
        public GameBoard(Hero _hero) : this(_hero, 10)
        {
            
        }
        public void CreateMonster()
        {            
            if (emptySpace > 0)
            {
                Monster newMonster = new Monster();
                do
                {
                    int position = Monster.rnd.Next(0, Size * Size);
                    newMonster.Position = (position / Size, position % Size);
                } while (board[newMonster.Position.X, newMonster.Position.Y] != BoardSpace.Empty);
                emptySpace--;
                monsters.Add(newMonster);
                board[newMonster.Position.X, newMonster.Position.Y] = BoardSpace.Monster;                
            }
        }
        public void DrawBoard()
        {            
            canvasBoard[hero.Position.X, hero.Position.Y] = hero.Symbol;
            foreach (Monster monster in monsters)
            {
                canvasBoard[monster.Position.X, monster.Position.Y] = monster.Symbol;
            }
            for(int i = 0; i < Size * Size; i++)
                if (board[i/Size, i%Size] == BoardSpace.Empty) canvasBoard[i / Size, i % Size] = Symbol;
        }
        public void ConsolePrintBoard()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                    Console.Write(canvasBoard[i, j]);
                Console.WriteLine();
            }
        }
        public void MonstersTurn()
        {
            foreach (Monster monster in monsters)
            {
                int Mx = monster.Position.X;
                int My = monster.Position.Y;

                int Hx = hero.Position.X;
                int Hy = hero.Position.Y;

                int dx = Hx - Mx;
                int dy = Hy - My;

                if(Math.Abs(dx) <= 1 & Math.Abs(dy) <= 1)
                {
                    if(hero.Alive)
                        hero.TakeDamage(monster.Damage);
                }
                else
                {
                    int stepX = dx == 0 ? 0 : (dx > 0 ? 1 : -1);
                    int stepY = dy == 0 ? 0 : (dy > 0 ? 1 : -1);

                    (int newX, int newY) newPosition = (Mx + stepX, My + stepY);
                    if (board[Mx + stepX, My + stepY] == BoardSpace.Empty)
                    {
                        board[Mx, My] = BoardSpace.Empty;
                        board[Mx + stepX, My + stepY] = BoardSpace.Monster;
                        monster.Position = newPosition;
                    }
                    else if(board[Mx + stepX, My] == BoardSpace.Empty)
                    {
                        board[Mx, My] = BoardSpace.Empty;
                        board[Mx + stepX, My] = BoardSpace.Monster;

                        newPosition.newY = My;
                        monster.Position = newPosition;
                    }
                    else if (board[Mx , My + stepY] == BoardSpace.Empty)
                    {
                        board[Mx, My] = BoardSpace.Empty;
                        board[Mx, My + stepY] = BoardSpace.Monster;

                        newPosition.newX = Mx;
                        monster.Position = newPosition;
                    }                    
                }
            }
        }
        public bool HeroMovement((int X, int Y) direction)
        {
            if (!(Math.Abs(direction.X) <= 1 & Math.Abs(direction.Y) <= 1))
                return false;
            if(
                direction.X + hero.Position.X < 0 || direction.X + hero.Position.X >= Size ||
                direction.Y + hero.Position.Y < 0 || direction.Y + hero.Position.Y >= Size ||
                board[direction.X + hero.Position.X, direction.Y + hero.Position.Y] != BoardSpace.Empty)
                { return false; }
            board[hero.Position.X, hero.Position.Y] = BoardSpace.Empty;
            board[direction.X + hero.Position.X, direction.Y + hero.Position.Y] = BoardSpace.Hero;
            hero.Position = (direction.X + hero.Position.X, direction.Y + hero.Position.Y);

            return true;

        }
        public List<Monster> HeroAttackTargets()
        {
            List<Monster> monstersTargets = new List<Monster>();
            int range = hero.Range;
            foreach (Monster monster in monsters)
            {
                int Mx = monster.Position.X;
                int My = monster.Position.Y;

                int Hx = hero.Position.X;
                int Hy = hero.Position.Y;

                int dx = Hx - Mx;
                int dy = Hy - My;

                if (Math.Abs(dx) <= range & Math.Abs(dy) <= range)
                    monstersTargets.Add(monster);
            }
            return monstersTargets;
        }
        public void HeroAttackChoice(List<Monster> targets, int choice)
        {
            targets[choice].TakeDamage(hero.Damage);
            if(!targets[choice].Alive)
            {
                KillCount++;
                monsters.Remove(targets[choice]);
                board[targets[choice].Position.X, targets[choice].Position.Y] = BoardSpace.Empty;
            }
        }
        
    }
}
