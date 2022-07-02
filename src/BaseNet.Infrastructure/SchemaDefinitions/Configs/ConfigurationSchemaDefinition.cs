using BaseNet.Core.Entities.Configs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseNet.Infrastructure.SchemaDefinitions.Configs
{
    public class ConfigSchemaDefinition<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, IConfigEntity, new()
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(col => col.Id);
            builder.HasData(new TEntity()
            {
                Id = 1
            });
        }
    }
}
