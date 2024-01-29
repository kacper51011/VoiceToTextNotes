using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Models
{
    public class NoteRow
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content {  get; set; }
        public string DisplayedTimeCreated { get; set; }
    }
}
