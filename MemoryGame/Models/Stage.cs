using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame.Models
{
    public class Stage
    {
        public int StageId { get; set; }
        public string StageTitle { get; set; }
        public ICollection<Login> Logins { get; set; }
    }
}
