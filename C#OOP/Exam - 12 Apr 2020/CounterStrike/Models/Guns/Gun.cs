namespace CounterStrike.Models.Guns
{
    using CounterStrike.Models.Guns.Contracts;
    using CounterStrike.Utilities.Messages;
    using System;

    public abstract class Gun : IGun
    {
        private string name;
        private int bulletsCount;

        protected Gun(string name, int bulletsCount)
        {
            Name = name;
            BulletsCount = bulletsCount;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidGunName);
                }
                this.name = value;
            }
        }

        public int BulletsCount
        {
            get
            {
                throw new System.NotImplementedException();
            }
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidGunBulletsCount);
                }
                this.bulletsCount = value;
            }
        }

        abstract protected int FireRate { get; }
        public int Fire()
        {
            if (BulletsCount - FireRate >= 0)
            {
                BulletsCount -= FireRate;
                return FireRate;
            }
            else
            {
                int resultingBullets = BulletsCount;
                BulletsCount = 0;
                return resultingBullets;
            }
        }

    }
}
