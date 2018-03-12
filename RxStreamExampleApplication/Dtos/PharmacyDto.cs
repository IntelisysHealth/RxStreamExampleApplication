using System;

namespace RxStream.Dtos
{
        public class PharmacyDto
        {
            public int Id { get; set; }
            public Int64 MedicationRequestHeaderId { get; set; }
            public string MedicationCount { get; set; }
            public string Ncpdp { get; set; }
            public string Npi { get; set; }
            public string Name { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Zipcode { get; set; }
            public string Phone { get; set; }
            public decimal MemberLiability { get; set; }
        }
}