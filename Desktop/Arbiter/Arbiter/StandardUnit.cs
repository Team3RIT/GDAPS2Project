using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace Arbiter
{
    //Margaret
    class StandardUnit : Unit
    {
        public StandardUnit(int x, int y, Texture2D icn, Player own)
            : base(x, y, icn, own)
        {
            rank = 2;
        }
        
    }
}
