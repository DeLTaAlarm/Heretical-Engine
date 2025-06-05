using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDataCore;

public class Entity
{
    public uint Id;
    public EntityUID uid;
    public Dictionary<Type, BaseComponent> components;
}
