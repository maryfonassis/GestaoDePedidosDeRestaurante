using GestaoDePedidoDeRestaurante.Servicos;

namespace GestaoDePedidoDeRestaurante.Modelos;
public class PedidoPresencial : Pedido
{
    public int Mesa { get; set; }

    public PedidoPresencial(Cliente cliente, int codigo, int mesa) : base(cliente, codigo)
    {
        Codigo = codigo;
        Cliente = cliente;
        Mesa = mesa;
    }


    
    
}