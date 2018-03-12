using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RxStreamExampleApplication.Dtos
{
    public class SummaryResponseDto
    {
        public string PharmacyId { get; set; }
        public string TotalCost { get; set; }
        public string AllItemsFound { get; set; }
    }
}
