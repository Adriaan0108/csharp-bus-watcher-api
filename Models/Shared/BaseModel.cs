using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using csharp_bus_watcher_api.Helpers;

namespace csharp_bus_watcher_api.Models.Shared;

public class BaseModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; } = DateTimeHelper.GetSouthAfricanTime();

    public DateTime? DeletedAt { get; set; }

    public void SetDeletedAt()
    {
        DeletedAt = DateTimeHelper.GetSouthAfricanTime();
    }
}