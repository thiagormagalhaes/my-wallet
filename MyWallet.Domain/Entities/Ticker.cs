﻿namespace MyWallet.Domain.Entities
{
    public class Ticker : Entity
    {
        public string Code { get; private set; }
        public int CompanyId { get; private set; }
        public decimal? Price { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public virtual Company Company { get; private set; }
        public virtual IList<Earning> Earnings { get; private set; } = new List<Earning>();

        protected Ticker() { }

        public Ticker(string code)
        {
            Code = code;
        }

        public void UpdatePrice(decimal price)
        {
            Price = price;
            UpdateDate = DateTime.Now;
        }
    }
}