using System;
using FactorySystem;

public class FactoryMap : FactorySystem<RecycleObject, MapType>
{
    protected override string Label => "Map";

    protected override MapType TranslateStringKeyToID(string primaryKey)
    {
        return Enum.Parse<MapType>(primaryKey);
    }
}