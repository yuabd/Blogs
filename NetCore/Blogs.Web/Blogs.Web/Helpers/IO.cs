using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Blogs.Web.Helpers
{
	public class IO
	{
		public static string UploadImageFile(Stream fileStream, string folderPath, string filename)
		{
			string saveUrl = DateTime.Now.ToString("yyyyMM") + @"\" + filename + ".jpg";
			string FileServerPaht = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderPath , saveUrl);
			//System.Web.HttpContext.Current.Server.MapPath(folderPath + "/Source/" + saveUrl);
			//string FileServerPaht_s = folderPath.Replace(".", "_s.");

			string directory = Path.GetDirectoryName(FileServerPaht);
			if (directory != null && !Directory.Exists(directory))
			{
				Directory.CreateDirectory(directory);
			}

			if (File.Exists(FileServerPaht))
			{
				File.Delete(FileServerPaht);
			}

			using (var stream = new FileStream(FileServerPaht, FileMode.Create))
			{
				//再把文件保存的文件夹中
				fileStream.CopyTo(stream);
			}

			return Path.Combine(folderPath, saveUrl);
		}

		/// <summary>
		/// 根据本地文件【FileLocalPath】，按照宽度【width】高度【height】剪裁模式【mode】(模式字母全部大写：CUT)保存至服务器【FileServerPaht】
		/// </summary>
		/// <param name="FileLocalPath">本地文件完整路径</param>
		/// <param name="FileServerPaht">服务器完整路径</param>
		/// <param name="width">宽度</param>
		/// <param name="height">高度</param>
		/// <param name="mode">模式</param>
		//private static void MakeThumbnail(string FileLocalPath, string FileServerPaht, int width, int height, string mode)
		//{
		//	System.Drawing.Image originalImage = System.Drawing.Image.FromFile(FileLocalPath);

		//	int towidth = width;
		//	int toheight = height;

		//	int x = 0;
		//	int y = 0;
		//	int ow = originalImage.Width;
		//	int oh = originalImage.Height;

		//	switch (mode)
		//	{
		//		case "HW"://指定高宽缩放（可能变形） 
		//			break;
		//		case "W"://指定宽，高按比例 
		//			toheight = originalImage.Height * width / originalImage.Width;
		//			break;
		//		case "H"://指定高，宽按比例 
		//			towidth = originalImage.Width * height / originalImage.Height;
		//			break;
		//		case "CUT"://指定高宽裁减（不变形） 
		//			if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
		//			{
		//				oh = originalImage.Height;
		//				ow = originalImage.Height * towidth / toheight;
		//				y = 0;
		//				x = (originalImage.Width - ow) / 2;
		//			}
		//			else
		//			{
		//				ow = originalImage.Width;
		//				oh = originalImage.Width * height / towidth;
		//				x = 0;
		//				y = 0;
		//			}
		//			break;
		//		case "None":
		//			toheight = originalImage.Height;
		//			towidth = originalImage.Width;
		//			break;
		//		default:
		//			break;
		//	}

		//	//新建一个bmp图片 
		//	System.Drawing.Image bitmap = new Bitmap(towidth, toheight);

		//	//新建一个画板 
		//	System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

		//	//设置高质量插值法 
		//	g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

		//	//设置高质量,低速度呈现平滑程度 
		//	g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

		//	//清空画布并以透明背景色填充 
		//	g.Clear(System.Drawing.Color.Transparent);

		//	//在指定位置并且按指定大小绘制原图片的指定部分 
		//	g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
		//	new System.Drawing.Rectangle(x, y, ow, oh),
		//	System.Drawing.GraphicsUnit.Pixel);

		//	try
		//	{
		//		string directory = Path.GetDirectoryName(FileServerPaht);
		//		if (directory != null && !Directory.Exists(directory))
		//		{
		//			Directory.CreateDirectory(directory);
		//		}
		//		//以jpg格式保存缩略图 
		//		bitmap.Save(FileServerPaht, System.Drawing.Imaging.ImageFormat.Jpeg);
		//	}
		//	catch (System.Exception e)
		//	{
		//		throw e;
		//	}
		//	finally
		//	{
		//		originalImage.Dispose();
		//		bitmap.Dispose();
		//		g.Dispose();
		//		bitmap = null;
		//	}
		//}

		//public static void DeleteFile(string folderPath, string filename)
		//{
		//	string fullPath = HttpContext.Current.Server.MapPath(folderPath + "/" + filename);
		//	try
		//	{
		//		File.Delete(fullPath);  //avoid failure if file doesn't exist
		//	}
		//	catch
		//	{
		//		//TODO:handle errors here
		//	}
		//}

		public static class ImageSaveType
		{
			/// <summary>
			/// 指定高宽裁减（不变形） 
			/// </summary>
			public const string Cut = "CUT";
			/// <summary>
			/// 指定高，宽按比例 
			/// </summary>
			public const string Height = "H";
			/// <summary>
			/// 指定宽，高按比例 
			/// </summary>
			public const string Width = "W";
			/// <summary>
			/// 指定高宽缩放（可能变形） 
			/// </summary>
			public const string WidthHeight = "HW";
			/// <summary>
			/// 原图
			/// </summary>
			public const string Original = "None";
		}
	}
}
