﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.RegularExpressions;

namespace d20Crawl
{
    class Program
    {
        static void Main(string[] args)
        {
            #region init

            #region NPCTableInit
            System.IO.TextReader tr = new System.IO.StreamReader(@"MonsterTable.txt");

            //List<NPC> npcList = new List<NPC>();
            string line;
            while ((line = tr.ReadLine()) != null)
            {
                /*extract:
                 * name
                 * hit die (a of adb + c)
                 * hit die number (b of adb + c)
                 * hit die bonus (c of adb + c)
                 * AC
                 * Attack name
                 * Attack to-hit mod
                 * Attack number of die
                 * Attack die number
                 * Attack bonus
                 * 
                 * 
                 * 
                 */

                if (line != "" && !line.Contains('½'))
                {
                    NumberFormatInfo nfi = NumberFormatInfo.CurrentInfo;
                    string name = line.Split(':')[0];
                    string pattern = @"\d*[d]\d*(\s*["
                                        + Regex.Escape(nfi.PositiveSign + nfi.NegativeSign)
                                        + @"]\s*\d*)*";
                    Regex rx = new Regex(pattern);
                    MatchCollection matches = rx.Matches(line);
                    int hd = Convert.ToInt16(matches[0].ToString().Split('d')[0]);
                    int numHD = Convert.ToInt16(matches[0].ToString().Split('d')[1]);
                    int bonusHD = Convert.ToInt32(matches[0].ToString().Split('+')[1]);


                    int atkNum = Convert.ToInt16(matches[1].ToString().Split('d')[0]);
                    int atkDice = Convert.ToInt16(matches[1].ToString().Split('d')[1]);
                    int atkBonus = Convert.ToInt32(matches[1].ToString().Split('+')[1]);

                    string pattern2 = @"[AC]\s*\d*";
                    Regex rx2 = new Regex(pattern2);
                    int ac = Convert.ToInt32(rx2.Match(line).ToString().Split(' ')[1]);

                    string pattern3 = @"\w*\s+["
                                        + Regex.Escape(nfi.PositiveSign + nfi.NegativeSign)
                                        + @"]\d*";
                    Regex rx3 = new Regex(pattern3);
                    string atkName = rx3.Match(line).ToString().Split(' ')[0];
                    int atkToHitBonus = Convert.ToInt32(rx3.Match(line).ToString().Split('+')[1]);

                    _globals.npcArchetypes.Add(new NPC(name, hd, numHD, bonusHD, ac, atkToHitBonus, atkName, atkNum, atkDice, atkBonus));
                }
            }
            #endregion

            #region weaponTableInit



            #endregion



            #region armorTableInit



            #endregion



            #region magicTableInit



            #endregion



            List<AC> initACs = new List<AC>();
            initACs.Add(new AC("WarriorOne", AC.Job.Warrior, AC.Race.Dwarf));
            
            initACs.Add(new AC("WarriorTwo", AC.Job.Warrior, AC.Race.Human));
            initACs.Add(new AC("RogueOne:AStarWarsStory", AC.Job.Rogue, AC.Race.Halfling));

            for(int i = 0; i < 3; i++)
            {
                initACs[i].takeTurn += character_turn;
            }

            Party party = new Party(initACs);

            Dungeon dungeon = new Dungeon(1);

            /*Order:
             * 
             * BEFORE ENTERING ROOM:
             * opportunity to heal, change gear, etc
             * 
             * UPON ENTERING ROOM:
             * Describe size of room, ex. narrow, wide, etc
             * Describe visible NPCs
             * Set party order
             * 
             * 
             * COMBAT BEGINS
             * Roll for starting turn timer
             * 
             * PC TURN
             * "Wat do?"
             * Target:
             * 0: Self
             * 1: Party member 1
             * ...
             * n: NPC 1
             * ...
             *
             * 
             * NPC TURN
             * [NPC] attacks [target] using [attack name]
             * 
             * PLAYER INCAP
             * [PC] is totally dead, but for this game is just knocked out!
             * 
             * NPC INCAP
             * The [NPC] dies!
             * 
             * 
             * COMBAT ENDS
             * Allocate EXP
             * Allocate loot
             * 
             * Give choice: continue or go back?
             * 
             * 
             * 
             * */

            //int roomCt = 0;


            

            
            while (dungeon.activeRoom <= dungeon.rooms.Count)
            {
                bool beforeEntryLoop = true;
                while (beforeEntryLoop)
                {
                    //BEFORE ENTERING ROOM
                    //TODO: Actions for before entering room
                    Console.WriteLine("Do something before entering the next room? ({ENTER} key to continue");
                    Console.WriteLine("0: ");

                    System.ConsoleKeyInfo ch = Console.ReadKey();
                    switch (ch.Key)
                    {
                        case ConsoleKey.Enter:
                            beforeEntryLoop = false;
                            break;
                        case ConsoleKey.D0:
                            break;
                    }
                }
                //UPON ENTERING ROOM:
                //Describe the room
                DescribeRoom(dungeon.rooms[dungeon.activeRoom]);

                //Decide party order
                //TODO: Enable keeping order from previous room

                for (int i = 0; i < party.roster.Count; i++)
                {
                    Console.WriteLine(party.roster[i].name + ", where will you stand?");
                    Console.WriteLine("0: Front");
                    Console.WriteLine("1: Mid");
                    Console.WriteLine("2: Back");

                    try
                    {
                        party.roster[i].SetLine(Convert.ToInt16(Console.ReadKey().KeyChar));
                    }
                    catch { party.roster[i].SetLine(0); }
                }

                //Roll for initiative




                //Console.WriteLine()

            }

           


            #endregion

            while (true)
            {
                //Console.WriteLine()
            }



            Console.ReadKey();
        }

        static void character_turn(object sender)
        {

        }

        public void DescribeParty(Party party)
        {
            for(int i = 0; i < party.roster.Count; i++)
            {

            }
        }

        public static void DescribeRoom(Room room)
        {
            Console.WriteLine("Your party enters the room.");
            Console.WriteLine(room.description);
            Console.WriteLine(room.width + " people can stand side by side.");

            for(int i = 0; i < room.enemies.Count; i++)
            {
                DescribeNPC(room.enemies[i]);
            }
        }

        public static void DescribeNPC(NPC npc)
        {
            Console.WriteLine("A " + npc.name + " faces you.");
        }
    }
}
