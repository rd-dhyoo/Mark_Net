using MarkNet.Core.Entities.Configs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarkNet.Infrastructure.SchemaDefinitions.Configs
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
