using System.Linq.Expressions;
using System.Reflection.Metadata;
using GestaoDePedidoDeRestaurante.Modelos;
using GestaoDePedidoDeRestaurante.Servicos;

Queue<Pedido> filaPedido = new Queue<Pedido>();
Stack<Pedido> pedidosCancelados = new Stack<Pedido>();
Cliente[] clientes = new Cliente[50];
var cardapio = CardapioService.CarregarCardapio();
int indiceClientes = 0;
int codigo = 111;


try
{
    bool rodar = true;
    while (rodar)
    {
        
        Console.WriteLine("*******RESTAURANTE******* ");
            Console.WriteLine("1 - Novo Pedido Delivery");
            Console.WriteLine("2 - Novo Pedido Presencial");
            Console.WriteLine("3 - Mostrar Fila");
            Console.WriteLine("4 - Atender Pedido");
            Console.WriteLine("5 - Cancelar Pedido");
            Console.WriteLine("6 - Mostrar Cancelados");
            Console.WriteLine("7 - Mostrar Clientes");
            Console.WriteLine("8 - Mostrar Cardápio");
            Console.WriteLine("9 - Sair");

        System.Console.Write("Escolha uma opção: ");
        int opcao = int.Parse(Console.ReadLine()!);

        switch(opcao)
        {
            case 1:
                Console.Write("Nome do cliente: ");
                string nome = Console.ReadLine()!;

                Console.Write("Telefone: ");
                string telefone = Console.ReadLine()!;
                Cliente cliente = new(nome, telefone);
                clientes[indiceClientes] = cliente;
                indiceClientes++;

                Console.Write("CEP: ");
                string cep = Console.ReadLine()!;
                Endereco endereco = await LocalizacaoService.BuscarEndereco(cep);
                endereco.PreencherEndereco();

                Pedido delivery = new PedidoDelivery(cliente, codigo, endereco);
                codigo++;
    
                Console.Write("Digite o codigo do item: ");
                int codigoItem = int.Parse(Console.ReadLine()!);
                var pedido = CardapioService.BuscarItemPorCodigo(cardapio, codigoItem);
                delivery.AdicionarPedido(pedido);
                filaPedido.Append(delivery);
                Console.WriteLine("Pedido Feito!");
                break;
            case 2:

                Console.Write("Nome do cliente: ");
                nome = Console.ReadLine()!;

                Console.Write("Telefone: ");
                telefone = Console.ReadLine()!;
                Cliente clientePresencial = new(nome, telefone);
                clientes[indiceClientes] = clientePresencial;
                indiceClientes++;
                System.Console.Write("Número da mesa: ");

                int mesa = int.Parse(Console.ReadLine()!);
                Pedido presencial = new PedidoPresencial(clientePresencial, codigo, mesa);
                codigo++;
    
                Console.Write("Digite o codigo do item: ");
                codigoItem = int.Parse(Console.ReadLine()!);
                pedido = CardapioService.BuscarItemPorCodigo(cardapio, codigoItem);
                presencial.AdicionarPedido(pedido);
                filaPedido.Append(presencial);
                Console.WriteLine("Pedido presencial registrado.");
                break;

            case 3:

                Console.WriteLine(
                    "**** FILA ****");

                foreach(var pedidoFila in filaPedido)
                {
                    Console.WriteLine(
                        $"Código: {pedidoFila.Codigo}");

                    Console.WriteLine(
                        $"Cliente: {pedidoFila.Cliente.Nome}");

                    Console.WriteLine(
                        $"Total: {pedidoFila.Total}");

                    Console.WriteLine("-----------");
                 }

                 break;
            case 4:
                if(filaPedido.Count > 0)
                {
                    Pedido pedidoAtendido =
                    filaPedido.Dequeue();

                    Console.WriteLine(
                    "Pedido atendido!");

                Console.WriteLine(
                  $"Cliente: " +
                 $"{pedidoAtendido.Cliente.Nome}");

                Console.WriteLine(
                     $"Código: " +
                 $"{pedidoAtendido.Codigo}");
                }

                else
                {
                    Console.WriteLine(
                        "Fila vazia!");
                }
                break;
            case 5:
                System.Console.WriteLine("Digite o codigo do pedido: ");
                int pedidoCodigo = int.Parse(Console.ReadLine()!);
                pedidosCancelados.Push(filaPedido.FirstOrDefault(pedido => pedido.Codigo == pedidoCodigo));
                var filaPedidoTemp = new Queue<Pedido>(filaPedido.Where(pedido => pedido.Codigo != pedidoCodigo));
                filaPedido = filaPedidoTemp;
                break;

            case 6:
                Console.WriteLine("Pedidos cancelados:");
                foreach (var item in pedidosCancelados)
                {
                   item.ExibirResumo();
                }
                break;
            case 7:
                foreach (var item in clientes)
                {
                    System.Console.WriteLine(item.Nome + " " + item.Telefone);
                }


                break;
            case 8:
                CardapioService.CardapioPorGrupos(cardapio);
                break;
            case 9:
                rodar = false;
                break;
            default:
                Console.WriteLine("Opção inválida.");
                break;
        }
    }
    




}
catch(Exception e)
{
    Console.WriteLine(e.Message);
}
