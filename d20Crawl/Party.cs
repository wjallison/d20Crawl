using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d20Crawl
{
    public class Party
    {
        public List<Item> inventory = new List<Item>();
        public List<AC> roster = new List<AC>();

        public int partyThreat;


        public Party() { }
        public Party(List<AC> partyList)
        {
            roster = partyList;

            AssignLines();
        }

        public int CharRollToAttack(int index, int targetAC)
        {
            if (roster[index].toHitAtAdvantage)
            {
                if (roster[index].RollToHit() >= targetAC || roster[index].RollToHit() >= targetAC)
                {
                    return roster[index].rollDamage();
                }
            }
            if (roster[index].RollToHit() >= targetAC)
            {
                return roster[index].rollDamage();
            }
            return 0;
        }

        public void AssignLines()
        {
            List<int> threatVals = new List<int>();
            int sum = 0;
            for (int i = 0; i < roster.Count; i++)
            {
                threatVals.Add(roster[i].numDice * roster[i].diceNum + roster[i].bonus + roster[i].armorClass * roster[i].maxHP);
                sum += threatVals[i];
            }
            partyThreat = sum;

            List<int> squishinessVals = new List<int>();
            int maxInd = 0, maxSquish = 0, maxMaxInd = 0, maxMaxSquish = 0; ;
            List<AC> temp = new List<AC>();
            for (int i = 0; i < roster.Count; i++)
            {
                temp.Add(roster[i]);
            }
            for (int i = 0; i < roster.Count; i++)
            {
                squishinessVals.Add(roster[i].armorClass * roster[i].maxHP);
                if (squishinessVals[i] > maxSquish)
                {
                    maxSquish = squishinessVals[i];
                    maxMaxSquish = squishinessVals[i];
                    maxInd = i;
                    maxMaxInd = i;
                }
            }
            roster[maxMaxInd].combatLine = 0;
            temp.RemoveAt(maxMaxInd);
            squishinessVals.Clear();
            while (temp.Count > 0)
            {
                squishinessVals.Clear();
                for (int i = 0; i < temp.Count; i++)
                {
                    squishinessVals.Add(temp[i].armorClass * temp[i].maxHP);
                    if (squishinessVals[i] > maxSquish) { maxSquish = squishinessVals[i]; maxInd = i; }
                }

                if (maxSquish > maxMaxSquish * .6)
                {
                    temp[maxInd].SetLine(0);
                }
                else { temp[maxInd].SetLine(1); }
            }

        }//Right now only assigns to front two lines
    }
}
