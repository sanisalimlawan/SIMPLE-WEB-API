﻿using System.ComponentModel.DataAnnotations;

namespace SIMPLE_WEB_API.Data.Entities
{
    public class Category 
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty ;
    }
}
