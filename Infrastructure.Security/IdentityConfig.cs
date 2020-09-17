using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Claims;
using Microsoft.Owin;
using System.Net;
using Infrastructure.Security.Util;
using System.Net.Mail;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Infrastructure.Security.Entities;
using System.Collections.Generic;
using System.Linq;
using Core.Entities.Enumerations.General;

namespace Infrastructure.Security
{
    public interface IEmail 
    {
        string UserName { get; set; }
        string Password { get; set; }
        string Adress { get; set; }
        string DisplayName { get; set; }
        string ServerSmpt { get; set; }
        int Port { get; set; }
        string Credentials { get; set; }
        bool EnableSsl { get; set; }
    }
    public class EmailService : IIdentityMessageService
    {
        public IEmail EmailAuth { get; set; }
        public EmailService(IEmail email)
        {
            EmailAuth = email;
        }
        public async Task SendAsync(IdentityMessage message)
        {
            await configSendSmtp(message);
        }

        private async Task configSendSmtp(IdentityMessage message)
        {
            using (MailMessage MailSetup = new MailMessage())
            {
                NetworkCredential loginInfo = new NetworkCredential(EmailAuth.UserName, EmailAuth.Password);
                MailSetup.Subject = message.Subject;
                MailSetup.To.Add(message.Destination);
                MailSetup.From = new MailAddress(EmailAuth.Adress,EmailAuth.DisplayName);
                MailSetup.Body = message.Body;
                MailSetup.IsBodyHtml = true;
                using (SmtpClient SMTP = new SmtpClient(EmailAuth.ServerSmpt))
                {
                    SMTP.Port = EmailAuth.Port;
                    SMTP.EnableSsl = true;
                    SMTP.Credentials = loginInfo;
                    await SMTP.SendMailAsync(MailSetup);
                }
            }
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            return Task.FromResult(0);
        }
    }

    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            PermisosInitiales = new HashSet<InitialPermissionUser>();
        }
        public DateTime FechaDesactivacion { get; set; }
        public string NombreCompleto { get; set; }
        public string Identificacion{ get; set; }
        public long TerceroId{ get; set; }
        public string Estado { get; set; }
        public virtual ICollection<InitialPermissionUser> PermisosInitiales { get; set; }
        public InitialPermissionUser PermisoActivo => ObtenerPermisoActivo();

        private InitialPermissionUser ObtenerPermisoActivo()
        {
            return PermisosInitiales.FirstOrDefault(t=>t.State==StatesGeneralEnumeration.Activo.Value);
        }

        public ClaimsIdentity GenerateUserIdentity(ApplicationUserManager manager)
        {
            var userIdentity = manager.CreateIdentity(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }

        public Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        {
            return Task.FromResult(GenerateUserIdentity(manager));
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            return userIdentity;
        }
    }
    
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {

        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = false,
                RequireUppercase = false,
            };
            InicializarCorreo();
            RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            
            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            MaxFailedAccessAttemptsBeforeLockout = 5;
        }
        public void InicializarCorreo(IEmail email)
        {
            EmailService = new EmailService(email);
        }
        public void InicializarCorreo()
        {
            EmailAuth email = new EmailAuth()
            {
                UserName = "cleanarchitecture@hotmail.com",
                Adress = "cleanarchitecture@hotmail.com",
                Password = "FalseLogin123#",
                Port= 587,
                ServerSmpt= "smtp.live.com",
            };
            EmailService = new EmailService(email);
        }
        /// <summary>
        /// 
        /// manager.UserLockoutEnabledByDefault = Convert.ToBoolean(ConfigurationManager.AppSettings["UserLockoutEnabledByDefault"].ToString());
        /// manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(Double.Parse(ConfigurationManager.AppSettings["DefaultAccountLockoutTimeSpan"].ToString()));
        /// manager.MaxFailedAccessAttemptsBeforeLockout = Convert.ToInt32(ConfigurationManager.AppSettings["MaxFailedAccessAttemptsBeforeLockout"].ToString());
        /// Configure validation logic for passwords
        /// </summary>
        /// <param name="options"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true
            };
          

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;
            manager.InicializarCorreo();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        internal IdentityResult ResetPassword(string v, object code, object password)
        {
            throw new NotImplementedException();
        }
    }
}