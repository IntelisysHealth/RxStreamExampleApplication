using System;

namespace RxStreamExampleApplication.Dtos
{
    /// <summary>
    /// Line item for Prescriptions
    /// </summary>
    public class PrescriptionItemDto
    {
        public string PrescriptionNumber { get; set; }

        /// <summary>
        /// National Drug Code
        /// </summary>
        public string Ndc { get; set; }

        /// <summary>
        /// Quantity
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Number Of Refills
        /// </summary>
        public int NumberOfRefills { get; set; }

        /// <summary>
        /// DAW (Dispense As Written) allows substitution
        /// </summary>
        public bool DispenseAsWritten { get; set; }

        /// <summary>
        /// CoPay Amount usually a whole dollar amount
        /// </summary>
        public decimal CoPayAmount { get; set; }

        /// <summary>
        /// CoInsurance usually a percentage
        /// </summary>
        public decimal CoInsurance { get; set; }

        public Guid MedicationId { get; set; }
        public decimal? Price { get; set; }
        public Guid TenantId { get; set; }
        public Guid PrescriptionHeaderId { get; set; }
    }
}