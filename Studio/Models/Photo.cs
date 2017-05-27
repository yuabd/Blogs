using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Studio.Models
{
	public class Photo
	{
		public int ID { get; set; }

		public string PhotoName { get; set; }

		public string Description { get; set; }

		public DateTime DateCreated { get; set; }

		public string Author { get; set; }

		public string Guid { get; set; }

		public string Phone { get; set; }

		public string Email { get; set; }

		public int Count { get; set; }
	}

	public class PhotoEntity 
	{
		public int ID { get; set; }

		public string PhotoName { get; set; }

		public string Description { get; set; }

		public DateTime DateCreated { get; set; }

		public string Author { get; set; }

		public string Guid { get; set; }

		public string Photos { get; set; }

		public string Photo { get; set; }

		public List<PhotoDetailEntity> PhotoDetailEntities { get; set; }

		public string Email { get; set; }

		public string Phone { get; set; }

		public int Count { get; set; }
	}

	public class PhotoDetailEntity
	{
		public int ID { get; set; }

		public int PhotoID { get; set; }

		public string PictureUrl { get; set; }

		public string IsDefault { get; set; }

		public string Name { get; set; }
	}

	public class PhotoDetail 
	{
		public int ID { get; set; }

		public int PhotoID { get; set; }

		public string PictureUrl { get; set; }

		public string IsDefault { get; set; }

		public string Name { get; set; }
	}

	public class PhotoVote
	{
		public int ID { get; set; }

		public int PhotoID { get; set; }

		public string IP { get; set; }
	}

}