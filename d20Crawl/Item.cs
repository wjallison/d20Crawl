using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d20Crawl
{
    public class Item
    {
        public int baseValue;
        //public int mass;
        public string name;
    }

    public class Weapon : Item
    {
        public int numDice; // n of ndm + p
        public int diceNum; // m of above
        public int bonus; // p of above


        public WeaponType type;
        public enum WeaponType
        {
            SpikedChain,
            Falchion,


            HandCrossbow
        }

        public bool melee;
        public bool twoHanded;

        public Weapon() { }
        public Weapon(WeaponType typ, int _bonus = 0)
        {
            type = typ;
            bonus = _bonus;
            switch (type)
            {
                case WeaponType.SpikedChain:
                    name = "Spiked Chain" + (_bonus == 0 ? "" : " + " + _bonus.ToString());
                    baseValue = 25;
                    numDice = 2;
                    diceNum = 4;
                    //bonus = _bonus;
                    melee = true;
                    twoHanded = true;
                    break;
                case WeaponType.Falchion:
                    name = "Falchion" + (_bonus == 0 ? "" : " + " + _bonus.ToString());
                    baseValue = 75;
                    numDice = 1;
                    diceNum = 6;
                    //bonus = _bonus;
                    melee = true;
                    break;



                case WeaponType.HandCrossbow:
                    name = "Hand Crossbow" + (_bonus == 0 ? "" : " + " + _bonus.ToString());
                    baseValue = 100;
                    numDice = 1;
                    diceNum = 4;
                    melee = false;
                    twoHanded = false;
                    //bonus = _bonus;
                    break;
            }
        }
    }



    public class Armor : Item
    {
        public int slot; //0 head, 1 chest, 2 legs
        public int bonus;

        public ArmorType type;
        public enum ArmorType
        {
            Padded,
            Leather
        }

        public Armor() { }
        public Armor(ArmorType typ, int _bonus = 0)
        {
            type = typ;
            switch (type)
            {
                case ArmorType.Padded:
                    name = "Padded" + (_bonus == 0 ? "" : " + " + _bonus.ToString());
                    bonus = 1 + _bonus;
                    baseValue = 2;
                    break;
                case ArmorType.Leather:
                    name = "Leather" + (_bonus == 0 ? "" : " + " + _bonus.ToString());
                    bonus = 2 + _bonus;
                    baseValue = 10;
                    break;
            }
        }
    }

    public class UsableItem : Item
    {

    }
}
