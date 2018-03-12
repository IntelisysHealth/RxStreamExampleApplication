namespace RxStreamExampleApplication.Dtos
{
    /// <summary>
    /// Final Quote before sending the prescription off. 
    /// </summary>
    public class FinalQuoteDto
    {
        /// <summary>
        /// Pharmacy Id either the NPI or NCPDP
        /// </summary>
        public string PharmacyId { get; set; }

        /// <summary>
        /// Prescription Id
        /// </summary>
        public string PrescriptionId { get; set; }

        /// <summary>
        /// If you need to append Pharmacy notes
        /// </summary>
        public bool HasPharmacyNotes { get; set; }

        /// <summary>
        /// Note to append
        /// </summary>
        public string PharmacyNotes { get; set; }
    }
}