using System.Globalization;

namespace GestaoDePedidoDeRestaurante.Modelos;
public class PedidoDelivery : Pedido
{
    public Endereco? Endereco { get; set; }
    public decimal Taxa { get; set; }
    public override void ExibirResumo()
    {
        Console.WriteLine($"Pedido📝: {Codigo}");
        foreach (var item in Pedidos)
        {
            Console.WriteLine($"-{item.Nome} R${item.Preco.ToString("F2", CultureInfo.InvariantCulture)}");
        }
        Console.WriteLine($"Total: R${CalcularTotal().ToString("F2", CultureInfo.InvariantCulture)} + Taxa de entrega: R${Taxa.ToString("F2", CultureInfo.InvariantCulture)}");
    }
    
}