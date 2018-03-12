namespace RxStreamExampleApplication.Dtos
{
    /// <summary>
    /// Initial estimate to display the different total costs by pharmacy
    /// </summary>
    public class EstimateDto
    {
        /// <summary>
        /// Pharmacy Id either the NPI or NCPDP
        /// </summary>
        public string PharmacyId { get; set; }

        /// <summary>
        /// Total cost for all the medications
        /// </summary>
        public string TotalCost { get; set; }

        /// <summary>
        /// If All medications where found at this pharmacy
        /// </summary>
        public string AllItemsFound { get; set; }
    }
}