using MonoTorrent;
using MonoTorrent.Client;
using MonoTorrent.Common;
using Studio.Models;
using Studio.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Studio.Controllers
{
    public class TorrentController : Controller
    {
        // GET: Torrent
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Magnet2Torrent(string hash)
        {
            //using (MagnetService ms = new MagnetService())
            //{
            //    var hashId = hash.Replace("magnet:?xt=urn:btih:", "");
            //    var magnet = ms.GetMagnet(hash);
            //    if (magnet == null)
            //    {
            //        var save = HttpContext.Server.MapPath("/Content/Bt/" + DateTime.Now.ToString("yyyyMM") + "/" + hashId + ".torrent");

            //        var torrentDefaults = new TorrentSettings();
            //        var manager = new TorrentManager(new MagnetLink(hash), save, torrentDefaults, save);

            //        magnet = new Models.Magnet()
            //        {
            //            Count = 0,
            //            CreatedBy = manager.Torrent.CreatedBy,
            //            MagnetLink = hash,
            //            DateCreated = manager.Torrent.CreationDate,
            //            FileName = manager.Torrent.Name,
            //            FilePath = save,
            //            FileSize = manager.Torrent.Size,
            //            Hash = hashId,
            //            Keywords = "",
            //            TotalFiles = manager.Torrent.Files.Count()
            //        };

            //        magnet.MagnetFiles = new List<MagnetFile>();
            //        foreach (var item in manager.Torrent.Files)
            //        {
            //            magnet.MagnetFiles.Add(new Models.MagnetFile()
            //            {
            //                Hash = item.MD5.ToString(),
            //                MagnetHash = magnet.Hash,
            //                Name = item.Path,
            //                Size = item.Length
            //            });
            //        }

            //        ms.InsertMagnet(magnet);
            //    }

            //    return View(magnet);
            //}

            return View();
        }

        public ActionResult Torrent2Magnet()
        {
            var MonoTorrent = Torrent.Load(""); //路径载入
            ViewBag.s = "magnet:?xt=urn:btih:" + BitConverter.ToString(MonoTorrent.InfoHash.ToArray()).Replace("-", "");

            return View();
        }
    }
}