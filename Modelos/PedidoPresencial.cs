using GestaoDePedidoDeRestaurante.Servicos;

namespace GestaoDePedidoDeRestaurante.Modelos;
public class PedidoPresencial : Pedido
{
    public int Mesa { get; set; }

    public PedidoPresencial()
    {
    }

    public PedidoPresencial(Cliente cliente, int codigo) : base(cliente, codigo)
    {
        Codigo = codigo;
    }


    
    
}