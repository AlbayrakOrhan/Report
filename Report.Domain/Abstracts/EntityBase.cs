using System.ComponentModel.DataAnnotations.Schema;
using MediatR;
using Report.Domain.Events;

namespace Report.Domain.Abstracts;

public abstract class EntityBase
{
    protected EntityBase()
    {
        DomainEvents = new List<INotification>();
    }

    public virtual Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }

    [NotMapped] public List<INotification> DomainEvents { get; }

    public void AddPaymentRequestCreatedEvent()
    {
        DomainEvents.Add(new ReportRequestCreatedEvent(Id));
    }

    public void SetId(Guid id)
    {
        Id = id;
    }
}