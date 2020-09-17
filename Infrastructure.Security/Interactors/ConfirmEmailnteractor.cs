using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;

namespace Infrastructure.Security.Interactors
{
    public class ConfirmEmailnteractor
    {
        private readonly ApplicationUserManager _manager;
        private readonly ApplicationDbContext _context;
        private readonly UserStore<ApplicationUser> _userStore;
        public ConfirmEmailnteractor(ApplicationUserManager manager)
        {
            _context = new ApplicationDbContext();
            _userStore = new UserStore<ApplicationUser>(_context);
            _manager = manager;
        }

        public Task<IdentityResult> ConfirmedEmail(string userId, string code)
        {
            return _manager.ConfirmEmailAsync(userId, code);
        }
    }
}
