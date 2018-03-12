using System;
using System.Collections.Generic;

namespace RxStreamExampleApplication.Dtos
{
    /// <summary>
    /// Prescription
    /// </summary>
    public class PrescriptionDto
    {
        
        /// <summary>
        /// Client GUID
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Customer reference value
        /// </summary>
        public string CustomInput1 { get; set; }

        /// <summary>
        /// Customer reference value
        /// </summary>
        public string CustomInput2 { get; set; }


        public string DocZip { get; set; }

        public string PatZip { get; set; }

        /// <summary>
        /// Collection of medications
        /// </summary>
        public List<PrescriptionItemDto> LineItems { get; set; }

        /// <summary>
        /// one or many pharmacies delimited by commas
        /// </summary>
        public string PharmacyId { get; set; }

        /// <summary>
        /// Constructor to instantiate the item collection 
        /// </summary>
        public PrescriptionDto()
        {
            LineItems = new List<PrescriptionItemDto>();
        }
    }
}