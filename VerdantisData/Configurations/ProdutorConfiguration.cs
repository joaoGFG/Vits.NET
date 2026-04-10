using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VerdantisModel;

namespace VerdantisData.Configurations;

public class ProdutorConfiguration : IEntityTypeConfiguration<ProdutorModel>
{
    public void Configure(EntityTypeBuilder<ProdutorModel> builder)
    {
        builder.ToTable("VITS_ORC_USUARIO");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnName("VITS_ID_USUARIO");

        builder.Property(p => p.Nome)
            .HasColumnName("VITS_NOME_USUARIO")
            .IsRequired();

        builder.Property(p => p.DataCadastro)
            .HasColumnName("VITS_DATA_CADASTRO")
            .IsRequired();

        builder.Property(p => p.TipoUsuarioId)
            .HasColumnName("ID_TIPO_USUARIO")
            .IsRequired();

        builder.Property(p => p.Senha)
            .HasColumnName("VITS_SENHA_USUARIO")
            .IsRequired()
            .HasMaxLength(100); 
    }
}