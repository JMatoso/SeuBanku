using SeuBanku.Entities;
using SeuBanku.Helpers.Paging;

#nullable disable

namespace SeuBanku.Models
{
    public class CommonModel
    {
        public Account Account { get; set; }
        public Metadata Metadata { get; set; }
        public ApplicationUser User { get; set; }
        public List<Extract> Extracts { get; set; }
        public List<Service> Services { get; set; }
    }
}
