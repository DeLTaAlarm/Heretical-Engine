using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSystemsCore;

[AttributeUsage(AttributeTargets.Class)]
public class SystemsOrderAttribute : Attribute
{
    public SystemsOrderEnum orderType = SystemsOrderEnum.Standart;
    public byte order = 0;

    public SystemsOrderAttribute(SystemsOrderEnum _orderType, byte p)
    {
        orderType = _orderType;
        order = p;
    }

    public SystemsOrderAttribute(string _orderType, byte p)
    {
        Enum.TryParse(_orderType, out SystemsOrderEnum temp);
        orderType = temp;
        order = p;
    }

    public SystemsOrderAttribute(SystemsOrderEnum _orderType)
    {
        orderType = _orderType;
    }

    public SystemsOrderAttribute(string _orderType)
    {
        Enum.TryParse(_orderType, out SystemsOrderEnum temp);
        orderType = temp;
    }
}

public enum SystemsOrderEnum
{
    Input,
    PostInput,
    Standart,
    PrePhysics,
    Physics,
    PostPhysics,
    Rendering
}