using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d20Crawl
{
    public class Character
    {
        public int numDice; // n of ndm + p
        public int diceNum; // m of above
        public int bonus; // p of above
        public string name;
        public int toHitBonus;
        public int armorClass;
        public int maxHP;
        public int hp;

        public bool toHitAtAdvantage = false;
        public bool toHitAtDisadvantage = false;

        public int combatLine;

        public int RollToHit()
        {
            Random r = new Random();
            return r.Next(1, 21) + toHitBonus;
        }

        public int RollToHit(bool advantage, bool disadvantage)
        {
            Random r = new Random();
            int r1 = 0, r2 = 0;
            r1 = r.Next(1, 21);
            r2 = r.Next(1, 21);
            if (advantage)
            {
                return (r1 > r2 ? r1 + toHitBonus : r2 + toHitBonus);
            }
            else if (disadvantage)
            {
                return (r1 > r2 ? r2 + toHitBonus : r1 + toHitBonus);
            }
            else
            {
                return r1 + toHitBonus;
            }
        }

        public int rollDamage()
        {
            Random r = new Random();
            int sum = 0;
            for (int i = 0; i < numDice; i++)
            {
                sum += r.Next(1, diceNum + 1);
            }
            return sum + bonus;
        }

        public void TakeDamage(int damage)
        {
            hp -= damage;
        }

        public void Heal(int healPoints)
        {
            if (healPoints < maxHP - hp)
            {
                hp += healPoints;
            }
            else { hp = maxHP; }
        }

        public void SetLine(int line)
        {
            combatLine = line;
        }
    }

    public class NPC : Character
    {
        public int HD;
        public int hdNum, hdBonus;
        //maxHP in Character
        //hp in Character
        //armorClass in Character
        //name in Character
        public string attackName;
        public int toHitMod;
        public int atkNumDie;
        public int atkDieNumber;
        public int atkBonus;

        public NPC() { }
        public NPC(string _name, int _hd, int _hdNum, int _hdBonus, int _ac, int _toHitMod,
            string _atkName, int _atkNumDie, int _atkDieNum, int _atkBonus)
        {
            name = _name;
            HD = _hd;
            hdNum = _hdNum;
            hdBonus = _hdBonus;
            Random r = new Random();
            int sum = 0;
            for (int i = 0; i < HD; i++)
            {
                sum += r.Next(1, _hdNum + 1);
            }
            maxHP = sum + _hdBonus;
            hp = maxHP;
            armorClass = _ac;
            toHitMod = _toHitMod;
            attackName = _atkName;
            atkNumDie = _atkNumDie;
            atkDieNumber = _atkDieNum;
            atkBonus = _atkBonus;
        }
        public NPC(NPC archetype)
        {
            name = archetype.name;
            HD = archetype.HD;
            hdNum = archetype.hdNum;
            hdBonus = archetype.hdBonus;
            Random r = new Random();
            int sum = 0;
            for (int i = 0; i < HD; i++)
            {
                sum += r.Next(1, hdNum + 1);
            }
            maxHP = sum + hdBonus;
            hp = maxHP;
            armorClass = archetype.armorClass;
            toHitMod = archetype.toHitMod;
            attackName = archetype.attackName;
            atkNumDie = archetype.atkNumDie;
            atkDieNumber = archetype.atkDieNumber;
            atkBonus = archetype.atkBonus;
        }

        //public enum Monster
        //{
        //    Badger,
        //    Ankheg
        //}
    }

    public class AC : Character
    {
        public int str = 0, strBonus;
        public int dex = 0, dexBonus;
        public int mind = 0, mindBonus;
        public int Physical = 0;
        public int Subterfuge = 0;
        public int Knowledge = 0;
        public int Communication = 0;

        public Weapon weapon;
        public Armor[] armorSet = new Armor[3];//0 head, 1 chest, 2 legs
        public List<string> kills = new List<string>();
        public int encounterLevels = 0;
        public int level;

        //public StatusEffect statusEffect;
        public IDictionary<string, int> magic = new Dictionary<string, int>();

        public Job job;
        public Race race;

        public enum Race
        {
            Human = 0,
            Elf,
            Dwarf,
            Halfling
        }
        public enum Job
        {
            Warrior = 0,
            Rogue,
            Hunter,
            Mage,
            Cleric
        }

        public AC() { }
        public AC(string _name, Job _job, Race _race)
        {
            name = _name;
            job = _job;
            race = _race;
            level = 1;

            Random r = new Random();
            List<int> stats = new List<int>();
            for (int i = 0; i < 3; i++)
            {
                //rolls.Clear();
                List<int> rolls = new List<int>();
                int teemp = 10;
                int tempInd = 0;
                for (int j = 0; j < 4; j++)
                {
                    rolls.Add(r.Next(1, 7));
                    if (rolls[j] < teemp)
                    {
                        tempInd = j;
                        teemp = rolls[j];
                    }
                }
                rolls.RemoveAt(tempInd);
                stats.Add(rolls[0] + rolls[1] + rolls[2]);
            }

            List<int> orderedStats = new List<int>();
            int teeemp = 0;
            int teeempInd = 0;
            for (int i = 0; i < 3; i++)
            {
                if (stats[i] > teeemp) { teeemp = stats[i]; teeempInd = i; }
            }
            orderedStats.Add(stats[teeempInd]);
            stats.RemoveAt(teeempInd);
            if (stats[0] > stats[1])
            {
                orderedStats.Add(stats[0]);
                orderedStats.Add(stats[1]);
            }
            else { orderedStats.Add(stats[1]); orderedStats.Add(stats[0]); }


            switch (race)
            {
                case Race.Human:
                    Physical++;
                    Subterfuge++;
                    Knowledge++;
                    Communication++;
                    break;
                case Race.Elf:
                    mind += 2;
                    break;
                case Race.Dwarf:
                    str += 2;
                    break;
                case Race.Halfling:
                    dex += 2;
                    break;
            }
            switch (job)
            {
                case Job.Warrior:
                    Physical += 3;
                    str += orderedStats[0];
                    dex += orderedStats[1];
                    mind += orderedStats[2];

                    weapon = new Weapon(Weapon.WeaponType.SpikedChain);
                    armorSet[1] = new Armor(Armor.ArmorType.Leather);

                    break;
                case Job.Rogue:
                    Subterfuge += 3;
                    dex += orderedStats[0];
                    mind += orderedStats[1];
                    str += orderedStats[2];

                    weapon = new Weapon(Weapon.WeaponType.HandCrossbow);
                    break;


                case Job.Mage:
                    Knowledge += 3;
                    mind += orderedStats[0];
                    dex += orderedStats[1];
                    str += orderedStats[2];

                    break;
                case Job.Cleric:
                    Communication += 3;
                    str += orderedStats[0];
                    mind += orderedStats[1];
                    dex += orderedStats[2];

                    break;
            }


            initHP();
        }

        public void SortOutBonuses()
        {
            strBonus = (str - 10) / 2;
            dexBonus = (dex - 10) / 2;
            mindBonus = (mind - 10) / 2;

            numDice = weapon.numDice;
            diceNum = weapon.diceNum;
            if (weapon.melee)
            {
                if (job == Job.Rogue)
                {
                    toHitBonus = dexBonus;
                    bonus = weapon.bonus + dexBonus;
                }
                else
                {
                    toHitBonus = strBonus;
                    bonus = weapon.bonus + strBonus;
                }

            }
            else
            {
                bonus = weapon.bonus;
                toHitBonus = dexBonus;
            }

            int armorBonus = 0;
            for (int i = 0; i < 3; i++)
            {
                if (armorSet[i] != null)
                {
                    armorBonus += armorSet[i].bonus;
                }
            }

            armorClass = 10 + dexBonus + armorBonus;

        }

        public void initHP()
        {
            Random r = new Random();
            maxHP = str + r.Next(1, 7);
            hp = maxHP;
        }

        public void LevelUp()
        {
            Random r = new Random();
            maxHP += r.Next(1, 7);
        }



        //public int EvaluateCombatAbility()
        //{

        //}
    }
}
