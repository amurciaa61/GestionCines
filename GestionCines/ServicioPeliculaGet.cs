using Newtonsoft.Json;
using RestSharp;
using System.Collections.ObjectModel;


namespace GestionCines
{
    internal class ServicioPeliculaGet
    {
        internal ObservableCollection<Pelicula> ObtenerCartelera()
        {
            var client = new RestClient(Properties.Settings.Default.endpoint);
            var request = new RestRequest("peliculas", Method.GET);
            var response = client.Execute(request);
            return JsonConvert.DeserializeObject<ObservableCollection<Pelicula>>(response.Content);
        }
    }
}
