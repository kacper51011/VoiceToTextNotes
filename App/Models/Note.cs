using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Models
{
    [Table("Notes")]
    public class Note
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("content")]
        public string Content { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set;}

    }
}
