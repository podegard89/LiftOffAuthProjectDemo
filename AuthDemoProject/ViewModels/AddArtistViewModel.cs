using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AuthDemoProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AuthDemoProject.ViewModels
{
    public class AddArtistViewModel
    {
        [Required(ErrorMessage = "Please enter a valid artist name")]
        public string Name { get; set; }
    }
}


