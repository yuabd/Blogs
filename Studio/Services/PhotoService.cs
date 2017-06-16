using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Studio.Models;
using Newtonsoft.Json;

namespace Studio.Services
{
    public class PhotoService : DbAccess
    {
        public PhotoService()
        {
        }

        //private SiteDataContext db = new SiteDataContext();
        //public int InsertPicture(PhotoEntity param)
        //{
        //    var obj = 1;
        //    try
        //    {
        //        var picture = new Photo();
        //        picture.Author = param.Author;
        //        picture.DateCreated = DateTime.Now;
        //        picture.Description = param.Description;
        //        var guid = Guid.NewGuid();
        //        picture.Guid = guid.ToString();
        //        picture.PhotoName = param.PhotoName;
        //        picture.Phone = param.Phone;
        //        picture.Email = param.Email;
        //        picture.Count = 0;

        //        db.Photos.Add(picture);
        //        db.SaveChanges();

        //        if (HandlePicture(param, picture.ID) != 1)
        //        {
        //            obj = -2;
        //        }

        //        obj = picture.ID;
        //    }
        //    catch (Exception e)
        //    {
        //        obj = -1;
        //        //throw
        //    }

        //    return obj;
        //}

        //public int InsertPhotoDetail(PhotoDetailEntity param)
        //{
        //    int obj = 1;
        //    try
        //    {
        //        var picture = new PhotoDetail();
        //        picture.IsDefault = "N";
        //        picture.Name = param.Name;
        //        picture.PhotoID = param.PhotoID;
        //        picture.PictureUrl = param.PictureUrl;
        //        //picture. = param.PhotoName;

        //        db.PhotoDetails.Add(picture);
        //        db.SaveChanges();

        //        obj = picture.ID;
        //    }
        //    catch (Exception e)
        //    {
        //        obj = -1;
        //        //throw
        //    }

        //    return obj;
        //}

        //public int InsertPhotoVote(int id, string ip)
        //{
        //    int obj = 1;
        //    if (db.PhotoVotes.Any(m => m.PhotoID == id && m.IP == ip))
        //    {
        //        return -2;
        //    }

        //    try
        //    {
        //        var photo = db.Photos.Find(id);
        //        photo.Count += 1;

        //        var photovote = new PhotoVote();
        //        photovote.PhotoID = id;
        //        photovote.IP = ip;

        //        db.PhotoVotes.Add(photovote);
        //        db.SaveChanges();

        //        obj = 1;
        //    }
        //    catch (Exception e)
        //    {
        //        obj = -1;
        //    }

        //    return obj;
        //}

        //private int HandlePicture(PhotoEntity param, int id)
        //{
        //    var obj = 1;
        //    var photo = JsonConvert.DeserializeObject<List<PhotoDetail>>(param.Photos);

        //    try
        //    {
        //        var ids = photo.Select(q => q.ID).ToList();
        //        var phos = db.PhotoDetails.Where(m => m.PhotoID == id && !ids.Contains(m.ID)).ToList();

        //        foreach (var item in phos)
        //        {
        //            var pho = db.PhotoDetails.Find(item.ID);
        //            if (pho == null)
        //            {
        //                continue;
        //            }
        //            db.PhotoDetails.Remove(pho);
        //        }
        //        db.SaveChanges();

        //        foreach (var item in photo)
        //        {
        //            var pho = db.PhotoDetails.Find(item.ID);
        //            if (pho == null)
        //            {
        //                continue;
        //            }
        //            pho.IsDefault = item.IsDefault;
        //            //pho.Type = PictureType.ArticleImage;
        //            pho.PhotoID = id;
        //            pho.PictureUrl = item.PictureUrl;
        //            pho.Name = item.Name;
        //        }

        //        db.SaveChanges();
        //    }
        //    catch (Exception e)
        //    {
        //        obj = -1;
        //    }
        //    return obj;
        //}

        ////
        //public PhotoEntity GetPhotoByID(int id)
        //{
        //    var photo = (from l in db.Photos
        //                 where l.ID == id
        //                 select new PhotoEntity()
        //                 {
        //                     Author = l.Author,
        //                     DateCreated = l.DateCreated,
        //                     Description = l.Description,
        //                     Guid = l.Guid,
        //                     ID = l.ID,
        //                     PhotoName = l.PhotoName,
        //                     Count = l.Count
        //                 }).FirstOrDefault();

        //    photo.PhotoDetailEntities = (from l in db.PhotoDetails
        //                                 where l.PhotoID == photo.ID
        //                                 select new PhotoDetailEntity()
        //                                 {
        //                                     ID = l.ID,
        //                                     IsDefault = l.IsDefault,
        //                                     Name = l.Name,
        //                                     PhotoID = l.PhotoID,
        //                                     PictureUrl = l.PictureUrl
        //                                 }).ToList();

        //    var dePhoto = photo.PhotoDetailEntities.FirstOrDefault(m => m.IsDefault == "Y");

        //    if (dePhoto != null)
        //    {
        //        photo.Photo = dePhoto.PictureUrl;
        //    }

        //    return photo;
        //}

        //public List<PhotoEntity> GetPhotoList()
        //{
        //    var photo = (from l in db.Photos
        //                 //where l.ID == id
        //                 select new PhotoEntity()
        //                 {
        //                     Author = l.Author,
        //                     DateCreated = l.DateCreated,
        //                     Description = l.Description,
        //                     Guid = l.Guid,
        //                     ID = l.ID,
        //                     PhotoName = l.PhotoName,
        //                     Count = l.Count
        //                 }).ToList();

        //    foreach (var item in photo)
        //    {
        //        item.Photo = (from l in db.PhotoDetails
        //                      where l.PhotoID == item.ID && l.IsDefault == "Y"
        //                      select l.PictureUrl).FirstOrDefault();
        //    }

        //    return photo;
        //}
    }
}