using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stdlib.src.endstone.actor
{
    public class Player : Mob
    {
        private Player(IntPtr ptr)
        {
            _ptr = ptr;
        }
        public static Player CreatInstance(IntPtr ptr)
        {
            return new Player(ptr);
        }
    }
}
