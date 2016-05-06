using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvadersLibrary
{  
    public interface IProjectile
    {
        //private int width;
        //private int height;



       Rectangle BoundingBox { get; set; }

         bool Alive { get; set; }

         void Move();
       

    }
}
