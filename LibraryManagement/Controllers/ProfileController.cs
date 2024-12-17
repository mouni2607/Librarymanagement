using LibraryManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.ViewModels;
using System.Threading.Tasks;

public class ProfileController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ProfileController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    // GET: Profile/Index
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User); // Ensure we're getting the ApplicationUser
        if (user == null)
        {
            return NotFound();
        }

        var model = new ProfileViewModel
        {
            UserId = user.Id,
            FullName = user.FullName ?? "Not set", // Default if null
            Email = user.Email ?? "Not set",       // Default if null
            Phone = user.PhoneNumber ?? "Not set"  // Default if null
        };

        return View(model);
    }

    // GET: Profile/Update
    public async Task<IActionResult> Update()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }

        var model = new ProfileViewModel
        {
            UserId = user.Id,
            FullName = user.FullName ?? "Not set",  // Default if null
            Email = user.Email ?? "Not set",        // Default if null
            Phone = user.PhoneNumber ?? "Not set"   // Default if null
        };

        return View(model);
    }

    // POST: Profile/Update
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(ProfileViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByIdAsync(model.UserId); // Get the user by ID
            if (user != null)
            {
                user.FullName = model.FullName;
                user.PhoneNumber = model.Phone;

                // Handle Email only if changed
                if (user.Email != model.Email)
                {
                    user.Email = model.Email;
                }

                var result = await _userManager.UpdateAsync(user); // Update the user
                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Profile updated successfully."; // Success message
                    return RedirectToAction("Index"); // Redirect to Index page
                }
                else
                {
                    // Display each error in result.Errors
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description); // Add errors to ModelState
                    }
                }
            }
        }

        // Return the view with any validation errors
        return View(model);
    }
}
