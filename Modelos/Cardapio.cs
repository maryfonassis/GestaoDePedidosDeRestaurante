using System.Text.Json.Serialization;

namespace GestaoDePedidoDeRestaurante.Modelos;
public class Cardapio
{
    [JsonPropertyName("cardapio")]
    public required List<ItemMenu> cardapio { get; set; } 
}