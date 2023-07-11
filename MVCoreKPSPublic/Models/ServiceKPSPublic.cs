using ServiceReference1;

namespace MVCoreKPSPublic.Models
{
    public class ServiceKPSPublic
    {
        public async Task<bool> OnGetService(Parametters parametters)
        {
            bool result = false;
            var client = new ServiceReference1.KPSPublicSoapClient(KPSPublicSoapClient.EndpointConfiguration.KPSPublicSoap);
            var response = await client.TCKimlikNoDogrulaAsync(parametters.TCKimlikNo,parametters.Ad,parametters.Soyad,parametters.DogumYili);
            return result = response.Body.TCKimlikNoDogrulaResult;
        }
    }
}
