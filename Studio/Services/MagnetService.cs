using Studio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Studio.Services
{
    public class MagnetService : DbAccess
    {
        public Magnet GetMagnet(string hash)
        {
            return db.Magnets.FirstOrDefault(m => m.Hash == hash);
        }

        public IQueryable<Magnet> GetMagnets()
        {
            return db.Magnets;
        }

        public void InsertMagnet(Magnet model)
        {
            db.Magnets.Add(model);

            db.SaveChanges();
        }

        public void DeleteMagnet(string id)
        {
            var magnet = db.Magnets.FirstOrDefault(m => m.Hash == id);

            db.Magnets.Remove(magnet);

            db.SaveChanges();
        }

        public void InsertMagnetFile(MagnetFile model)
        {
            db.MagnetFiles.Add(model);

            db.SaveChanges();
        }
    }
}