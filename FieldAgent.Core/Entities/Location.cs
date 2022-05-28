namespace FieldAgent.Core.Entities
{
    public class Location
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }

        public int AgencyId { get; set; }
        public Agency Agency { get; set; }
    }
}
