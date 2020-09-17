using System;

namespace Application.WebApi.ViewModels.General
{
    public class UserBasicViewModel
    {
        public string NombreCompleto { get; set; }
        public string Identificacion { get; set; }
        public long TerceroId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string UserName { get; set; }
    }

    public class UserViewModel: UserBasicViewModel
    {
        public Guid ApplicationID { get; set; }
        public string UserId { get; set; }
        public string NewPassword { get; set; }
        public decimal PasswordFormat { get; set; }
        public string PasswordSalt { get; set; }
        public string MobilePin { get; set; }
        public decimal IsApproved { get; set; }
        public decimal IsLockedout { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime FechaDesactivacion { get; set; }
        public string Estado { get; set; }
        public object UrlConfirmar { get; internal set; }
    }
}