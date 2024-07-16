using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemoryGame.Models;
using MemoryGame.DAL;


namespace MemoryGame.Service
{
    public class LevelSevice
    {
        public List<Level> GetAllLevel()
        {
            return MemoryGameContext.Instance().Levels.ToList();
        }
    }
}
