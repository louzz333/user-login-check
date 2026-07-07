using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Test2.Data;
using Test2.Models;

namespace Test2.Pages
{
    public class IndexModel : PageModel
    {
        public string Name =" ";
        private readonly ILogger<IndexModel> _logger;
        private readonly AppDbContext _db;

        public IndexModel(ILogger<IndexModel> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        public void OnGet()
        {
            Name = "User";
        }
        [BindProperty]
        public string EnteredName { get; set; } = "";
        [BindProperty]
        public string NewUserName { get; set; } = "";
        [BindProperty]
        public string NewPass { get; set; } = "";
        [TempData]
        public bool? LoginSuccess { get; set; }

        public void OnPost()
        {
            Name = EnteredName;
        }
        public void OnPostAddUser()
        {
            var newUser = new User();
            newUser.UserName = NewUserName;
            newUser.Pass = NewPass;

            _db.Users.Add(newUser);
            _db.SaveChanges();

        }

        public IActionResult OnPostCheck()
        {
            var match = _db.Users.FirstOrDefault(u => u.UserName == NewUserName && u.Pass == NewPass);
            LoginSuccess = match != null;
            return RedirectToPage();
        }
    }
}
