using UnityEngine;
using System.Collections;

public interface IArmor
{
    string Name { get; }
    int PhysicalDefence { get; }
}

public interface IHead : IArmor
{

}

public interface IBody : IArmor
{

}

public interface ITrinket : IArmor
{

}

//public abstract class Armor
//{
//    public abstract string Name { get; }

//    public abstract int PhysicalDefence { get; }
//}

public class ArmorWaifCap : IHead
{
    public string Name
    {
        get
        {
            return "Waif Cap";
        }
    }
    public int PhysicalDefence
    {

        get
        {
            return 2;
        }
    }
}

public class ArmorFrayedShirt : IBody
{
    public string Name
    {
        get
        {
            return "Frayed Shirt";
        }
    }
    public int PhysicalDefence
    {

        get
        {
            return 4;
        }
    }
}

public class ArmorLuckyPendant : ITrinket
{
    public string Name
    {
        get
        {
            return "Lcuky Pendant";
        }
    }
    public int PhysicalDefence
    {

        get
        {
            return 1;
        }
    }
}

public class ArmorThickBand : IHead
{
    public string Name
    {
        get
        {
            return "Thick Band";
        }
    }
    public int PhysicalDefence
    {

        get
        {
            return 3;
        }
    }
}

public class ArmorLeatherJacket : IArmor
{
    public string Name
    {
        get
        {
            return "Leather Jacket";
        }
    }
    public int PhysicalDefence
    {

        get
        {
            return 6;
        }
    }
}

public class ArmorSpikedCollar : ITrinket
{
    public string Name
    {
        get
        {
            return "Spiked Collar";
        }
    }
    public int PhysicalDefence
    {

        get
        {
            return 3;
        }
    }
}