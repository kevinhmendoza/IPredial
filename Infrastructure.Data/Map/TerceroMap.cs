﻿using Core.Entities.General;
using System.Data.Entity.ModelConfiguration;

namespace Infrastructure.Data.Map
{
    public class TerceroMap : EntityTypeConfiguration<Tercero>
    {
        public TerceroMap()
        {
            HasKey(t => t.Id);
            Property(t => t.Nombres).IsRequired().HasMaxLength(50);
            Property(t => t.Telefono).IsRequired();
            Property(t => t.Apellidos).IsRequired();
            Property(t => t.Identificacion).IsRequired().HasMaxLength(20);
            HasIndex(u => new { u.Identificacion , u.State}).IsUnique(); 
            Property(t => t.Direccion).IsRequired();
        }
    }
}
