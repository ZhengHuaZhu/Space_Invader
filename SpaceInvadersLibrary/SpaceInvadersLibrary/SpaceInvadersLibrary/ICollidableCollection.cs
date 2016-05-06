using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvadersLibrary
{
    public interface ICollidableCollection
    {
        
       ICollidable this[int row, int col] { get;}

        int Length { get; }
    }
}
