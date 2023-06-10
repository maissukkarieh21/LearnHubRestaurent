using Azure.Identity;
using LearnHubRestaurent.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace LearnHubRestaurent.Controllers
{
    public class AuthController : Controller
    {
        private readonly ModelContext _context;

        private readonly IWebHostEnvironment _webHostEnviroment;

        public AuthController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }
        public IActionResult Register()
        {
            return View();
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Register([Bind("Fname,Lname,ImageFile")] Customerr customerr,string username , string pass)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string wwwRootPath = _webHostEnviroment.WebRootPath;
        //        string fileName = Guid.NewGuid().ToString() + "_" + customerr.ImageFile.FileName;
        //        string path = Path.Combine(wwwRootPath + "/Images/", fileName);
        //        using (var fileStream = new FileStream(path, FileMode.Create))

        //        {
        //            await customerr.ImageFile.CopyToAsync(fileStream);
        //        }
        //        customerr.ImagePath = fileName;
        //        _context.Add(customerr);
        //        await _context.SaveChangesAsync();

        //        UserLogin rlogin = new UserLogin();

        //        if (!IsPasswordValid(pass))
        //        {
        //            ModelState.AddModelError("pass", "Password must contain at least 8 characters, including an uppercase letter and a symbol.");
        //            return View(customerr);
        //        }
        //        else
        //        {
        //            rlogin.UserName = username;
        //            rlogin.Password = pass;
        //            rlogin.RoleId = 6;
        //            rlogin.CustomerId = customerr.Id;
        //            _context.Add(rlogin);
        //            await _context.SaveChangesAsync();
        //            return RedirectToAction(nameof(Register));
        //        }  
        //    }
        //    return View(customerr);

        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Fname,Lname,ImageFile")] Customerr customerr, string username, string pass)
        {
            if (ModelState.IsValid)
            {
                // Validate password
                if (!IsPasswordValid(pass))
                {
                    ModelState.AddModelError("pass", "Password must contain at least 8 characters, including an uppercase letter and a symbol.");
                    return View(customerr);
                }

                string wwwRootPath = _webHostEnviroment.WebRootPath;
                string fileName = Guid.NewGuid().ToString() + "_" + customerr.ImageFile.FileName;
                string path = Path.Combine(wwwRootPath + "/Images/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await customerr.ImageFile.CopyToAsync(fileStream);
                }

                customerr.ImagePath = fileName;
                _context.Add(customerr);
                await _context.SaveChangesAsync();

                UserLogin rlogin = new UserLogin();
                rlogin.UserName = username;
                rlogin.Password = pass;
                rlogin.RoleId = 6;
                rlogin.CustomerId = customerr.Id;
                _context.Add(rlogin);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Register));
            }

            return View(customerr);
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login([Bind("UserName,Password")] UserLogin login)
        {
            var auth = _context.UserLogins.Where(x=> x.UserName == login.UserName && x.Password==login.Password).SingleOrDefault();
            if (auth != null)
            {
                switch (auth.RoleId)
                {
                    case 6:
                        HttpContext.Session.SetString("CName", auth.UserName);
                        return RedirectToAction("Profile", "Customerrs");
                        case 5: return RedirectToAction("Index", "Admin");
                }
            }
            return View();
        }

        private bool IsPasswordValid(string pass)
        {
            var hasUpperCase = false;
            var hasSymbol = false;

            foreach (char c in pass)
            {
                if (char.IsUpper(c))
                    hasUpperCase = true;

                if (char.IsSymbol(c) || char.IsPunctuation(c))
                    hasSymbol = true;
            }

            return pass.Length >= 8 && hasUpperCase && hasSymbol;
        }

    }
}
