using System;

namespace HSystemsCore;

[AttributeUsage(AttributeTargets.Class)]
public class SystemsRenderingOrderAttribute : Attribute
{
    public SystemsRenderingOrderEnum orderType = SystemsRenderingOrderEnum.Rendering;
    public byte order = 0;

    public SystemsRenderingOrderAttribute(SystemsRenderingOrderEnum _orderType, byte p)
    {
        orderType = _orderType;
        order = p;
    }

    public SystemsRenderingOrderAttribute(string _orderType, byte p)
    {
        Enum.TryParse(_orderType, out SystemsRenderingOrderEnum temp);
        orderType = temp;
        order = p;
    }

    public SystemsRenderingOrderAttribute(SystemsRenderingOrderEnum _orderType)
    {
        orderType = _orderType;
    }

    public SystemsRenderingOrderAttribute(string _orderType)
    {
        Enum.TryParse(_orderType, out SystemsRenderingOrderEnum temp);
        orderType = temp;
    }
}

public enum SystemsRenderingOrderEnum
{
    PreRendering,
    Rendering,
    PostRendering,
    Postprocessing,
    UI,
    PostUI
}