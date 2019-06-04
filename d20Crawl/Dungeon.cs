using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d20Crawl
{
    public class Dungeon
    {
        public List<Room> rooms = new List<Room>();
        public int level;
        public string name;
        public int reward;

        public Dungeon(int lvl)
        {
            level = lvl;

            for (int i = 0; i < 5; i++)
            {
                rooms.Add(new Room(lvl));
            }
            rooms.Add(new Room(lvl, true));
        }
    }

    public class Room
    {
        public List<Item> loot = new List<Item>();
        public List<NPC> enemies = new List<NPC>();
        public int encounterLevels;
        //TODO: descriptions
        public string description;


        public Room(int lvl, bool bossRoom = false)
        {
            int points = lvl * 5;
            List<NPC> availableNPCS = new List<NPC>();
            for(int i = 0; i < _globals.npcArchetypes.Count; i++)
            {
                if(_globals.npcArchetypes[i].HD <= lvl + 1)
                {
                    availableNPCS.Add(_globals.npcArchetypes[i]);
                }
            }

            Random r = new Random();
            while(points > 0)
            {
                int rSelect = r.Next(0, availableNPCS.Count);
                if(availableNPCS[rSelect].HD <= points)
                {
                    enemies.Add(new NPC(availableNPCS[rSelect]));
                    points -= availableNPCS[rSelect].HD;
                }
            }



        }
    }
}
