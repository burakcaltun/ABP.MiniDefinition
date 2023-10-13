using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiniDefinition.Dto
{

    public class CityDto
    {
        public long Id { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public bool? IsPassive { get; set; }
        public DateTime DatePassive { get; set; }
        public bool? ApprovalStatus { get; set; }
        public UNKNOWN_TYPE ProcessID { get; set; }
        public long? CountryId { get; set; }
        public CountryDto Country { get; set; }

        // jhipster-needle-dto-add-field - JHipster will add fields here, do not remove
    }
}
