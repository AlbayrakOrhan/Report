using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Report.Domain.Abstracts;
using Report.Domain.Entities;

namespace Report.Domain.EntityConfigurations;

public class ReportRequestConfiguration : EntityConfigurationBase<ReportRequest>
{
    protected override void Configure(EntityTypeBuilder<ReportRequest> eb)
    {
        eb.Property(x => x.ReportPath).IsRequired(false);
    }
}