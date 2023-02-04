namespace Report.Domain.Interfaces;

public interface ISoftDeletableEntity
{
    public bool IsDeleted { get; set; }
}