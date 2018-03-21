using System;
using System.Collections.Generic;

namespace RxStreamExampleApplication.Dtos
{
    public class PrescriptionDto
    {
        public string ClientId { get; set; }
        public string CustomInput1 { get; set; }
        public string CustomInput2 { get; set; }
        public string DocZip { get; set; }
        public string PatZip { get; set; }
        public List<PrescriptionItemDto> LineItems { get; set; }
        public string PharmacyId { get; set; }

        public PrescriptionDto()
        {
            LineItems = new List<PrescriptionItemDto>();
        }
    }
}