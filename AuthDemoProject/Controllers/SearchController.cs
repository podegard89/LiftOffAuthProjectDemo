/*using AuthDemoProject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthDemoProject.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        private SongRepository _repo;

        public SearchController(SongRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            ViewBag.currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View();
        }

        public IActionResult Results(string searchType, string searchTerm)
        {

        }


    }
}*/
