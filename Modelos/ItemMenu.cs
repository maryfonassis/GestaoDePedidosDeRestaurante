using System.Globalization;
using System.Text.Json.Serialization;
namespace GestaoDePedidoDeRestaurante.Modelos;
public class ItemMenu
{
    [JsonPropertyName("nome")]
    public required string Nome { get; set; }
    [JsonPropertyName("preco")]
    public decimal Preco { get; set; }
    [JsonPropertyName("codigo")]
    public int Codigo { get; set; }
    [JsonPropertyName("categoria")]
    public required string Categoria { get; set; }

    public override string ToString()
    {
        return ($"-{Nome} | Codigo: {Codigo} | Preço: R${Preco.ToString("F2", CultureInfo.InvariantCulture)}| Categoria: {Categoria}");
    }

}