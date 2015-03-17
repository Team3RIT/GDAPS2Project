﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

// Margaret
namespace Arbiter
{
    class Structure:Piece
    {
        //this class allows towers and blocked spaces to be stored on the board, which is a Piece array. It's a little hacky.
        public Structure(int x, int y, Texture2D icn):base(x,y,icn) //will be owned by a non-player
        {
            rank = 5; //ridiculously high, will not be captured
            GameVariables.structures.Add(this);
        }
        
    }
}
