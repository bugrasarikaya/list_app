﻿using System.ComponentModel.DataAnnotations.Schema;
namespace list_api.Models {
	public class Product {
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		public string Name { get; set; } = null!;
		public string? Description { get; set; }
	}
}