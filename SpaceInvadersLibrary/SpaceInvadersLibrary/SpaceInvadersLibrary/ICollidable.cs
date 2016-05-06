using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvadersLibrary
{
      public interface ICollidable
    {
          Rectangle BoundingBox
        {
            get;
            set;
        }

          int Points { get; set; }

          bool Alive { get; set; }
    }
}