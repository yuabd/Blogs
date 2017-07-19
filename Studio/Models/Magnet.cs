using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Studio.Models
{
    public class Magnet
    {
        [Key]
        public string Hash { get; set; }

        public string FilePath { get; set; }

        public int Count { get; set; }

        public DateTime DateCreated { get; set; }

        public string CreatedBy { get; set; }

        public long FileSize { get; set; }

        public string Keywords { get; set; }

        public int TotalFiles { get; set; }

        public string MagnetLink { get; set; }

        public string FileName { get; set; }

        public virtual ICollection<MagnetFile> MagnetFiles { get; set; }
    }
}