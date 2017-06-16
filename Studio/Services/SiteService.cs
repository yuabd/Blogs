using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Studio.Models;
using Studio.Models.Admin;

namespace Studio.Services
{
    public class SiteHelp
    {
        public SiteHelp()
        {

        }

        //public void InsertCase(Case _case, HttpPostedFileBase file)
        //{
        //    using (SiteService ss = new SiteService())
        //    {
        //        ss.InsertCase(_case, file);
        //    }
        //}

        //public Case GetCase(int caseID)
        //{
        //    using (SiteService ss = new SiteService())
        //    {
        //        return ss.GetCase(caseID);
        //    }
        //}

        //public void UpdateCase(Case _case, HttpPostedFileBase file)
        //{
        //    using (SiteService ss = new SiteService())
        //    {
        //        ss.UpdateCase(_case, file);
        //    }
        //}

        //public void Delete(int caseID)
        //{
        //    using (SiteService ss = new SiteService())
        //    {
        //        ss.Delete(caseID);
        //    }
        //}

        //public List<Case> GetCases()
        //{
        //    using (SiteService ss = new SiteService())
        //    {
        //        return ss.GetCases();
        //    }
        //}

        //public List<Case> GetCases(int? categoryID)
        //{
        //    using (SiteService ss = new SiteService())
        //    {
        //        return ss.GetCases(categoryID);
        //    }
        //}

        //public List<Case> GetCases(int categoryID)
        //{
        //    using (SiteService ss = new SiteService())
        //    {
        //        return ss.GetCases(categoryID);
        //    }
        //}

        //public void UploadCasePicture(Case _case, HttpPostedFileBase file)
        //{
        //    using (SiteService ss = new SiteService())
        //    {
        //        ss.UploadCasePicture(_case, file);
        //    }
        //}

        ////case category
        //public void InsertCaseCategory(CaseCategory caseCategory)
        //{
        //    using (SiteService ss = new SiteService())
        //    {
        //        ss.InsertCaseCategory(caseCategory);
        //    }
        //}

        //public CaseCategory GetCaseCategory(int caseCategoryID)
        //{
        //    using (SiteService ss = new SiteService())
        //    {
        //        return ss.GetCaseCategory(caseCategoryID);
        //    }
        //}

        //public void UpdateCaseCategory(CaseCategory caseCategory)
        //{
        //    using (SiteService ss = new SiteService())
        //    {
        //        ss.UpdateCaseCategory(caseCategory);
        //    }
        //}

        //public void DeleteCaseCategory(int caseCategoryID)
        //{
        //    using (SiteService ss = new SiteService())
        //    {
        //        ss.DeleteCaseCategory(caseCategoryID);
        //    }
        //}

        //public List<CaseCategory> GetCaseCategories()
        //{
        //    using (SiteService ss = new SiteService())
        //    {
        //        return ss.GetCaseCategories();
        //    }
        //}


        //public BaseObject<int> InsertPicture(Picture picture)
        //{
        //    using (SiteService ss = new SiteService())
        //    {
        //        return ss.InsertPicture(picture);
        //    }
        //}

		public BaseObject InsertLink(Links links)
		{
			using (SiteService ss = new SiteService())
			{
				return ss.InsertLink(links);
			}
		}

		public Links GetLink(int id)
		{
			using (SiteService ss = new SiteService())
			{
				return ss.GetLink(id);
			}
		}

		public BaseObject UpdateLink(Links link)
		{
			using (SiteService ss = new SiteService())
			{
				return ss.UpdateLink(link);
			}
		}

		public BaseObject DeleteLink(int id)
		{
			using (SiteService ss = new SiteService())
			{
				return ss.DeleteLink(id);
			}
		}

		public List<Links> GetLinks()
		{
			using (SiteService ss = new SiteService())
			{
				return ss.GetLinks();
			}
		}

    }
	public class SiteService : DbAccess
	{
		//Case 
		//public void InsertCase(Case _case, HttpPostedFileBase file)
		//{
		//	db.Cases.Add(_case);
		//	Save();

		//	if (file != null)
		//	{
		//		UploadCasePicture(_case, file);
		//	}

  //          Save();
		//}

		//public Case GetCase(int caseID)
		//{
		//	return db.Cases.Find(caseID);
		//}

		//public void UpdateCase(Case _case, HttpPostedFileBase file)
		//{
		//	var c = GetCase(_case.CaseID);

		//	c.DateCreated = _case.DateCreated;
		//	c.CategoryID = _case.CategoryID;
		//	c.Description = _case.Description;
		//	c.Name = _case.Name;
		//	c.ShortDescription = _case.ShortDescription;
		//	c.Website = _case.Website;

		//	if (file != null)
		//	{
		//		UploadCasePicture(c, file);
		//	}

  //          Save();
		//}

		//public void Delete(int caseID)
		//{
		//	var c = GetCase(caseID);
		//	db.Cases.Remove(c);

  //          Save();
		//}

		//public List<Case> GetCases()
		//{
		//	return db.Cases.OrderByDescending(m => m.DateCreated).ToList();
		//}

  //      public List<Case> GetCases(int? categoryID)
		//{
		//	return db.Cases.Where(m => m.CategoryID == categoryID).OrderByDescending(m => m.DateCreated).ToList();
		//}

  //      public List<Case> GetCases(int categoryID)
		//{
  //          return db.Cases.Where(m => m.CategoryID == categoryID).OrderByDescending(m => m.DateCreated).ToList();
		//}

		//public void UploadCasePicture(Case _case, HttpPostedFileBase file)
		//{
  //          var fileName = string.Format("{0}-{1}", _case.CaseID, Studio.Helpers.Utilities.GenerateSlug(_case.Name, 43));

  //          _case.PictureFile = Studio.Helpers.IO.UploadImageFile(file.InputStream, _case.PictureFolder, fileName, 800, 600, Studio.ImageHelper.ImageSaveType.Cut);

		//	//var picture = string.Format("{0}-{1}.jpg", _case.CaseID, Studio.Helpers.Utilities.GenerateSlug(_case.Name, 43));

		//	//Studio.Helpers.IO.DeleteFile(_case.PictureFolder, _case.PictureFile);
		//	//_case.PictureFile = picture;

  //          //var filePath = HttpContext.Current.Server.MapPath(_case.PictureFolder + "/" + _case.PictureFile);

  //          //file.SaveAs(filePath);
		//	//Studio.Helpers.IO.UploadImageFile(file.InputStream, _case.PictureFolder, _case.PictureFile, 630);
		//}

		////case category
		//public void InsertCaseCategory(CaseCategory caseCategory)
		//{
		//	db.CaseCategories.Add(caseCategory);

  //          Save();
		//}

		//public CaseCategory GetCaseCategory(int caseCategoryID)
		//{
		//	return db.CaseCategories.Find(caseCategoryID);
		//}

		//public void UpdateCaseCategory(CaseCategory caseCategory)
		//{
		//	var c = GetCaseCategory(caseCategory.CategoryID);

		//	c.CategoryDescription = caseCategory.CategoryDescription;
		//	c.CategoryName = caseCategory.CategoryName;

  //          Save();
		//}

		//public void DeleteCaseCategory(int caseCategoryID)
		//{
		//	var c = GetCaseCategory(caseCategoryID);

		//	db.CaseCategories.Remove(c);
  //          Save();
		//}

		//public List<CaseCategory> GetCaseCategories()
		//{
		//	return db.CaseCategories.ToList();
		//}


		//public BaseObject<int> InsertPicture(Picture picture)
		//{
		//	BaseObject<int> obj = new BaseObject<int>();
		//	try
		//	{
		//		db.Pictures.Add(picture);
		//		db.SaveChanges();

		//		obj.Tag = 1;
		//		obj.Result = picture.ID;
		//	}
		//	catch (Exception e)
		//	{
		//		obj.Tag = -1;
		//		obj.Message = e.Message;
		//	}

		//	return obj;
		//}

		public BaseObject InsertLink(Links links)
		{
			BaseObject obj = new BaseObject();
			try
			{
				links.DateCreated = DateTime.Now;
				db.Links.Add(links);
				db.SaveChanges();

				obj.Tag = 1;
				obj.Message = "添加成功!";
			}
			catch (Exception e)
			{
				obj.Tag = -1;
				obj.Message = e.Message;
			}

			return obj;
		}

		public Links GetLink(int id)
		{
			var link = (from l in db.Links
						where l.ID == id
						select l).FirstOrDefault();

			return link;
		}

		public BaseObject UpdateLink(Links link)
		{
			BaseObject obj = new BaseObject();
			var l = db.Links.Find(link.ID);

			if (l == null)
			{
				obj.Tag = -1;
				obj.Message = "该记录不存在！";
				return obj;
			}

			try
			{
				l.Contact = link.Contact;
				l.Description = link.Description;
				l.Email = link.Email;
				l.Name = link.Name;
				l.LinkUrl = link.LinkUrl;
				l.PictureFile = link.PictureFile;
				l.SortOrder = link.SortOrder;
				//l.DateCreated = DateTime.Now;

				db.SaveChanges();

				obj.Tag = 1;
			}
			catch (Exception)
			{
				obj.Tag = -1;
				obj.Message = "修改失败！";
			}

			return obj;
		}

		public BaseObject DeleteLink(int id)
		{
			BaseObject obj = new BaseObject();
			var link = db.Links.Find(id);
			if (link == null)
			{
				obj.Tag = -1;
				obj.Message = "该记录不存在!";
				return obj;
			}

			db.Links.Remove(link);
			db.SaveChanges();

			obj.Tag = 1;

			return obj;
		}

		public List<Links> GetLinks()
		{
			var list = (from l in db.Links
						orderby l.SortOrder descending
						select l).ToList();

			return list;
		}

		public void Save()
		{
			db.SaveChanges();
		}
	}
}