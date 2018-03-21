using System;

namespace RxStreamExampleApplication.Dtos
{
    public class PrescriptionItemDto
    {
        public string PrescriptionNumber { get; set; }
        public string Ndc { get; set; }
        public int Quantity { get; set; }
        public int NumberOfRefills { get; set; }
        public bool DispenseAsWritten { get; set; }
        public decimal CoPayAmount { get; set; }
        public decimal CoInsurance { get; set; }
    }
}