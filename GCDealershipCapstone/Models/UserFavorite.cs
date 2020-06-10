using System;
using System.Collections.Generic;

namespace GCDealershipCapstone.Models
{
    public partial class UserFavorite
    {
        public int Id { get; set; }
        public int? CarId { get; set; }
        public string? UserId { get; set; }
    }
}
