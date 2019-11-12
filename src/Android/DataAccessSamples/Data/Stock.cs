using System;
using SQLite;


namespace DataAccessSamples.Data
{
    [Table("Items")]
    public class Stock
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        [MaxLength(8)]
        public string Symbol { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return Name ?? base.ToString();
        }
    }

}