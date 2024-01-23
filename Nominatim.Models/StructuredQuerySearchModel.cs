using System.Text;

namespace Nominatim.Models
{
    public record StructuredQuerySearchModel
    {
        public string Amenity { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }

        public override string ToString()
        {
            /*
             The '+' character is commonly used as a space substitute in URL queries. 
             In many web applications, spaces are URL-encoded as '%20'. 
             However, using '+' as a space substitute is a convention used in query strings.

             Example:
                So, instead of searching for "123 Main Street", I would search for "123+Main+Street". 
                The Nominatim API interprets the '+' as a space when processing the query.
             */
            var nonNullProperties = GetType().GetProperties()
                .Where(prop => prop.GetValue(this) != null)
                .Select(prop => prop.GetValue(this)?.ToString());

            return string.Join('+', nonNullProperties);
        }
    }
}
