using AuthDemoProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthDemoProject.Controllers
{
    [Authorize]
    public class PetController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProcessAddPetForm(/*some kind of viewModel here possibly*/)
        {
            // let's assume ModelState is valid and the person provided a name and a species for a new pet
            // they are adding to the database and all the other functionality of this method for handling the form
            // is there cool?

            Pet newPet = new Pet
            {
                Name = "Max",
                Species = "Dog",
                // this ApplicationUserId is what is going to associate this new pet with the user who is creating the pet
                ApplicationUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            // Then you would go on to ya know add this thing to the database via context and so on

            return View();
        }
    }
}
