using System.Text.Json;
using GestaoDePedidoDeRestaurante.Modelos;

namespace GestaoDePedidoDeRestaurante.Servicos;
public class LocalizacaoService
{
    public static async Task<Endereco?> BuscarEndereco(string cep)
    {
        string url = $"https://viacep.com.br/ws/{cep}/json/";
        using (HttpClient client = new HttpClient())
        {
            string json = await client.GetStringAsync(url);
            return JsonSerializer.Deserialize<Endereco>(json);
        }

    }
}