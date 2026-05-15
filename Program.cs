using GestaoDePedidoDeRestaurante.Modelos;
using GestaoDePedidoDeRestaurante.Servicos;

try
{
   /*var cardapio = CardapioService.CarregarCardapio();
   Cliente cliente = new Cliente()
   {
       Nome = "Mariany"
   };
   Pedido pedido = new(cliente, 2);
   var item = CardapioService.BuscarItemPorCodigo(cardapio, 8);
   pedido.AdicionarPedido(item);
   pedido.ExibirResumo();*/
   System.Console.WriteLine("cep:");
   string? cep = Console.ReadLine();
   Endereco? endereco = await LocalizacaoService.BuscarEndereco(cep);
   endereco.PreencherEndereco();


}
catch(Exception e)
{
    System.Console.WriteLine(e.Message);
}
