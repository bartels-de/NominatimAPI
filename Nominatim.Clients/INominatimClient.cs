using Nominatim.Models;

namespace Nominatim.Clients
{
    public interface INominatimClient
    {
        public Task<RequestResponseModel> SearchAsync(StructuredQuerySearchModel searchModel);
    }
}
