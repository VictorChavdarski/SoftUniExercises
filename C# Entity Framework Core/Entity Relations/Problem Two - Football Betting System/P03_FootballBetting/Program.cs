namespace P03_FootballBetting
{
    using Microsoft.EntityFrameworkCore;
    using P03_FootballBetting.Data;
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            var context = new FootballBettingContext();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

        }
    }
}
