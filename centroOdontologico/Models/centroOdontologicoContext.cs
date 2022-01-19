using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace centroOdontologico.Models
{
    public partial class centroOdontologicoContext : DbContext
    {
        public centroOdontologicoContext()
        {
        }

        public centroOdontologicoContext(DbContextOptions<centroOdontologicoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Citas> Citas { get; set; } = null!;
        public virtual DbSet<Ciudades> Ciudades { get; set; } = null!;
        public virtual DbSet<DetalleCitas> DetalleCitas { get; set; } = null!;
        public virtual DbSet<Doctores> Doctores { get; set; } = null!;
        public virtual DbSet<Especialidades> Especialidades { get; set; } = null!;
        public virtual DbSet<Pacientes> Pacientes { get; set; } = null!;
        public virtual DbSet<Procedimientos> Procedimientos { get; set; } = null!;
        public virtual DbSet<Roles> Roles { get; set; } = null!;
        public virtual DbSet<Seguros> Seguros { get; set; } = null!;
        public virtual DbSet<Usuarios> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Citas>(entity =>
            {
                entity.HasKey(e => e.IdCita)
                    .HasName("PK__citas__814F3126BB8DEEDF");

                entity.ToTable("citas");

                entity.Property(e => e.IdCita).HasColumnName("idCita");

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.FechaCita)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaCita");

                entity.Property(e => e.IdDoctor).HasColumnName("idDoctor");

                entity.Property(e => e.IdPaciente).HasColumnName("idPaciente");

                entity.Property(e => e.IdSeguro).HasColumnName("idSeguro");

                entity.HasOne(d => d.IdDoctorNavigation)
                    .WithMany(p => p.Citas)
                    .HasForeignKey(d => d.IdDoctor)
                    .HasConstraintName("FK__citas__idDoctor__4CA06362");

                entity.HasOne(d => d.IdPacienteNavigation)
                    .WithMany(p => p.Citas)
                    .HasForeignKey(d => d.IdPaciente)
                    .HasConstraintName("FK__citas__idPacient__4BAC3F29");

                entity.HasOne(d => d.IdSeguroNavigation)
                    .WithMany(p => p.Citas)
                    .HasForeignKey(d => d.IdSeguro)
                    .HasConstraintName("FK__citas__idSeguro__4D94879B");
            });

            modelBuilder.Entity<Ciudades>(entity =>
            {
                entity.HasKey(e => e.IdCiudad)
                    .HasName("PK__ciudades__AEA2A71B3EA1AD56");

                entity.ToTable("ciudades");

                entity.Property(e => e.IdCiudad).HasColumnName("idCiudad");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<DetalleCitas>(entity =>
            {
                entity.HasKey(e => e.IdDetalleCita)
                    .HasName("PK__detalleC__031271114C8A434D");

                entity.ToTable("detalleCitas");

                entity.Property(e => e.IdDetalleCita).HasColumnName("idDetalleCita");

                entity.Property(e => e.IdCita).HasColumnName("idCita");

                entity.Property(e => e.IdProcedimiento).HasColumnName("idProcedimiento");

                entity.Property(e => e.Valor)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("valor");

                entity.HasOne(d => d.IdCitaNavigation)
                    .WithMany(p => p.DetalleCitas)
                    .HasForeignKey(d => d.IdCita)
                    .HasConstraintName("FK__detalleCi__idCit__5070F446");

                entity.HasOne(d => d.IdProcedimientoNavigation)
                    .WithMany(p => p.DetalleCitas)
                    .HasForeignKey(d => d.IdProcedimiento)
                    .HasConstraintName("FK__detalleCi__idPro__5165187F");
            });

            modelBuilder.Entity<Doctores>(entity =>
            {
                entity.HasKey(e => e.IdDoctor)
                    .HasName("PK__doctores__418956C3666E634D");

                entity.ToTable("doctores");

                entity.Property(e => e.IdDoctor).HasColumnName("idDoctor");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("apellidos");

                entity.Property(e => e.Cedula)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("cedula");

                entity.Property(e => e.Correo)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("correo");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.IdCiudad).HasColumnName("idCiudad");

                entity.Property(e => e.IdEspecialidad).HasColumnName("idEspecialidad");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("nombres");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("telefono");

                entity.HasOne(d => d.IdCiudadNavigation)
                    .WithMany(p => p.Doctores)
                    .HasForeignKey(d => d.IdCiudad)
                    .HasConstraintName("FK__doctores__idCiud__4316F928");

                entity.HasOne(d => d.IdEspecialidadNavigation)
                    .WithMany(p => p.Doctores)
                    .HasForeignKey(d => d.IdEspecialidad)
                    .HasConstraintName("FK__doctores__idEspe__440B1D61");
            });

            modelBuilder.Entity<Especialidades>(entity =>
            {
                entity.HasKey(e => e.IdEspecialidad)
                    .HasName("PK__especial__E8AB160087E66ECB");

                entity.ToTable("especialidades");

                entity.Property(e => e.IdEspecialidad).HasColumnName("idEspecialidad");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Pacientes>(entity =>
            {
                entity.HasKey(e => e.IdPaciente)
                    .HasName("PK__paciente__F48A08F292B558B5");

                entity.ToTable("pacientes");

                entity.Property(e => e.IdPaciente).HasColumnName("idPaciente");

                entity.Property(e => e.Cedula)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("cedula");

                entity.Property(e => e.Correo)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("correo");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.Edad)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("edad");

                entity.Property(e => e.IdCiudad).HasColumnName("idCiudad");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(400)
                    .IsUnicode(false)
                    .HasColumnName("nombres");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("telefono");

                entity.HasOne(d => d.IdCiudadNavigation)
                    .WithMany(p => p.Pacientes)
                    .HasForeignKey(d => d.IdCiudad)
                    .HasConstraintName("FK__pacientes__idCiu__403A8C7D");
            });

            modelBuilder.Entity<Procedimientos>(entity =>
            {
                entity.HasKey(e => e.IdProcedimiento)
                    .HasName("PK__procedim__46A68D83F9CBE0FF");

                entity.ToTable("procedimientos");

                entity.Property(e => e.IdProcedimiento).HasColumnName("idProcedimiento");

                entity.Property(e => e.Costo)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("costo");

                entity.Property(e => e.Detalle)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("detalle");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__roles__3C872F768D404D98");

                entity.ToTable("roles");

                entity.Property(e => e.IdRol).HasColumnName("idRol");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Seguros>(entity =>
            {
                entity.HasKey(e => e.IdSeguro)
                    .HasName("PK__seguros__01109A4020AF035B");

                entity.ToTable("seguros");

                entity.Property(e => e.IdSeguro).HasColumnName("idSeguro");

                entity.Property(e => e.Institucion)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("institucion");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("tipo");
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__usuarios__645723A608005332");

                entity.ToTable("usuarios");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.Clave)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("clave");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro");

                entity.Property(e => e.IdRol).HasColumnName("idRol");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Usuario)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("usuario");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK__usuarios__idRol__398D8EEE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
