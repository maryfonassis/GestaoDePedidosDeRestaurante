using System.Globalization;
using GestaoDePedidoDeRestaurante.Enums;
using GestaoDePedidoDeRestaurante.Servicos;

namespace GestaoDePedidoDeRestaurante.Modelos;
public class Pedido
{

    public int Codigo { get; set; }
    public  List<ItemMenu> Pedidos { get; set; } = new();
    public DateTime Data { get; set; } = DateTime.Now;
    public Status StatusPedido { get; set; } = Status.Recebido;
    public Cliente Cliente { get; set; }
    public decimal Total { get; set; }

     public Pedido(Cliente cliente, int codigo)
    {
        Cliente = cliente;
        Codigo = codigo;
    }

    public Pedido()
    {
    }

    public void AdicionarPedido(ItemMenu pedido)
    {
        Pedidos.Add(pedido);
    }
     public void RemoverPedido(ItemMenu pedido)
    {
        Pedidos.Remove(pedido);
    }
    public decimal CalcularTotal()
    {
        Total = Pedidos.Sum(item => item.Preco);
        return Total;
    }
    public virtual void ExibirResumo()
    {
        Console.WriteLine($"Pedido📝: {Codigo}");
        foreach (var item in Pedidos)
        {
            Console.WriteLine($"-{item.Nome} R${item.Preco.ToString("F2", CultureInfo.InvariantCulture)}");
        }
        Console.WriteLine($"Total: R${CalcularTotal().ToString("F2", CultureInfo.InvariantCulture)}");
    }
    public void AlterarEstado(string estado)
    {
        StatusPedido = Enum.Parse<Status>(estado);
    }



    


}