﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.ViewModel
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int? CategoryId { get; set; }
        public CategoryViewModel Category { get; set; }
        public string Tags { get; set; }
        public string ImageUrl { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
    }
    
    public class AddBookViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }
        public string Tags { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile ImageFile { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Description { get; set; }
    }
    
    public class EditBookViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }
        public string Tags { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile ImageFile { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
