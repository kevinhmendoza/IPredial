using System;

namespace Infrastructure.Security.Dtos
{
    public class ApplicationUserDto
    {
        public string Email { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string CorreoConfirmado { get { return EmailConfirmed ? "SI" : "NO"; } }
        public string FinBloqueo { get { return LockoutEndDateUtc == null ? "NA" : LockoutEndDateUtc.Value.AddHours((DateTime.Now.Hour - DateTime.UtcNow.Hour)).ToString(); } }
        public string BloqueoHabilitado { get { return LockoutEnabled ? "SI" : "NO"; } }
        public int AccessosFallidos { get { return AccessFailedCount; } }
        public string UserName { get; internal set; }
        public DateTime FechaDesactivacion { get; set; }
        public string NombreCompleto { get; set; }
        public string Identificacion { get; set; }
        public long TerceroId { get; set; }
        public string Estado { get; set; }
    }
}
