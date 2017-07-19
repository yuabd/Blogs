using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Studio.Models
{
    public class MagnetFile
    {
        [Key]
        public string Hash { get; set; }
        
        public string MagnetHash { get; set; }

        public string Name { get; set; }

        public long Size { get; set; }

        [ForeignKey("MagnetHash")]
        public Magnet Magnet { get; set; }
    }
}