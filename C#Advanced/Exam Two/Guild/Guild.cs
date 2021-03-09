using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Guild
{
    public class Guild
    {
        private List<Player> data;
        private Guild()
        {
            this.data = new List<Player>();
        }
        public Guild(string name,int capacity)
            :this()
        {
            this.Name = name;
            this.Capacity = capacity;
        }
        public string Name { get; set; }

        public int Capacity { get; set; }

        public int Count => this.data.Count;

        public void AddPlayer(Player player)
        {
            if (this.data.Count + 1 <= this.Capacity)
            {
                this.data.Add(player);
            }
        }
        public bool RemovePlayer(string name)
        {
            Player player = this.data.FirstOrDefault(n => n.Name == name);
            if (player!=null)
            {
                this.data.Remove(player);
                return true;
            }
            return false;
        }
        public void PromotePlayer(string name)
        {
            Player player = this.data.FirstOrDefault(n => n.Name == name);
            if (player.Rank == "Trial")
            {
                player.Rank = "Member";
            }
        }
        public void DemotePlayer(string name)
        {
            Player player = this.data.FirstOrDefault(n => n.Name == name);
            if (player.Rank == "Member")
            {
                player.Rank = "Trial";
            }
        }
        public Player[] KickPlayersByClass(string classText)
        {
            Player[] arr;
            arr = this.data.Where(x => x.ClassText == classText).ToArray();
            this.data = this.data.Where(x => x.ClassText != classText).ToList();
            return arr;
        }
        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Players in the guild: {this.Name}");
            foreach (var player in this.data)
            {
                sb.AppendLine(player.ToString());
            }
            return sb.ToString().TrimEnd();
        }
    }
}
