using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemoryGame.DAL;
using MemoryGame.Models;

namespace MemoryGame.Service
{
   public class LoginService
    {
        public List<Login> GetAllUser()
        {
            return MemoryGameContext.Instance().Logins.ToList();
        }
    }
}
