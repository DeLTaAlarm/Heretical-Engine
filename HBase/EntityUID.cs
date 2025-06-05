using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDataCore
{
    public struct EntityUID
    {
        public uint id;
        public uint generation;

        public EntityUID(uint _id, uint _generation)
        {
            id = _id;
            generation = _generation;
        }
    }
}
