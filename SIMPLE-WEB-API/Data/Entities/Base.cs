using System.ComponentModel.DataAnnotations;

namespace SIMPLE_WEB_API.Data.Entities
{
    public class Base
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string? LastModefiedby { get; set; }
        protected Base() 
        {
            CreatedDate = DateTime.UtcNow;
            IsDeleted = false;
        }
    }
}
