﻿namespace CounterStrike.Models.Guns
{
    public class Pistol : Gun
    {
        public Pistol(string name, int bulletsCount)
            : base(name, bulletsCount)
        {
        }

        protected override int FireRate
        {
            get
            {
                return 1;
            }
        }
    }
}
