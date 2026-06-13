using System.Linq.Expressions;
using System.Reflection.Metadata;
using GestaoDePedidoDeRestaurante.Modelos;
using GestaoDePedidoDeRestaurante.Servicos;

Queue<Pedido> filaPedido = new Queue<Pedido>();
Stack<Pedido> pedidosCancelados = new Stack<Pedido>();
Cliente[] clientes = new Cliente[50];

bool[,] mesas = new bool[5,5];

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
        Console.WriteLine("9 - Mostrar Mesas");
        Console.WriteLine("10 - Pedidos por Valor Total");
        Console.WriteLine("11 - Sair");

        Console.Write("Escolha uma opção: ");
        int opcao = int.Parse(Console.ReadLine()!);

        switch(opcao)
        {
            case 1:
                Console.Write("Nome do cliente: ");
                string nome = Console.ReadLine()!;

                Console.Write("Telefone: ");
                string telefone = Console.ReadLine()!;

                Cliente cliente = new(nome, telefone);
                clientes[indiceClientes++] = cliente;

                Console.Write("CEP: ");
                string cep = Console.ReadLine()!;

                Endereco endereco = await LocalizacaoService.BuscarEndereco(cep);
                endereco.PreencherEndereco();

                Pedido delivery = new PedidoDelivery(cliente, codigo++, endereco);

                Console.Write("Digite o codigo do item: ");
                int codigoItem = int.Parse(Console.ReadLine()!);

                var item = CardapioService.BuscarItemPorCodigo(cardapio, codigoItem);
                delivery.AdicionarPedido(item);

                filaPedido.Enqueue(delivery);
                Console.WriteLine("Pedido Feito!");
                break;

            case 2:
                Console.Write("Nome do cliente: ");
                nome = Console.ReadLine()!;

                Console.Write("Telefone: ");
                telefone = Console.ReadLine()!;

                Cliente clientePresencial = new(nome, telefone);
                clientes[indiceClientes++] = clientePresencial;

                Console.Write("Linha da mesa (0-4): ");
                int linha = int.Parse(Console.ReadLine()!);

                Console.Write("Coluna da mesa (0-4): ");
                int coluna = int.Parse(Console.ReadLine()!);

                if (mesas[linha,coluna])
                {
                    Console.WriteLine("Mesa ocupada!");
                    break;
                }

                mesas[linha,coluna] = true;

                Pedido presencial = new PedidoPresencial(clientePresencial, codigo++, (linha * 5) + coluna + 1);

                Console.Write("Digite o codigo do item: ");
                codigoItem = int.Parse(Console.ReadLine()!);

                item = CardapioService.BuscarItemPorCodigo(cardapio, codigoItem);
                presencial.AdicionarPedido(item);

                filaPedido.Enqueue(presencial);
                Console.WriteLine("Pedido presencial registrado.");
                break;

            case 3:
                Console.WriteLine("**** FILA DE ATENDIMENTO ****");
                foreach(var pedidoFila in filaPedido)
                {
                    Console.WriteLine($"Código: {pedidoFila.Codigo}");
                    Console.WriteLine($"Cliente: {pedidoFila.Cliente.Nome}");
                    Console.WriteLine($"Total: {pedidoFila.CalcularTotal()}");
                    Console.WriteLine("-----------");
                }
                break;

            case 4:
                if(filaPedido.Count > 0)
                {
                    Pedido pedidoAtendido = filaPedido.Dequeue();
                    Console.WriteLine("Pedido atendido!");
                    Console.WriteLine($"Cliente: {pedidoAtendido.Cliente.Nome}");
                    Console.WriteLine($"Código: {pedidoAtendido.Codigo}");
                }
                else
                {
                    Console.WriteLine("Fila vazia!");
                }
                break;

            case 5:
                Console.Write("Digite o codigo do pedido: ");
                int pedidoCodigo = int.Parse(Console.ReadLine()!);

                var cancelado = filaPedido.FirstOrDefault(p => p.Codigo == pedidoCodigo);

                if(cancelado != null)
                {
                    pedidosCancelados.Push(cancelado);
                    filaPedido = new Queue<Pedido>(filaPedido.Where(p => p.Codigo != pedidoCodigo));
                }
                break;

            case 6:
                Console.WriteLine("Pedidos cancelados (pilha):");
                foreach (var p in pedidosCancelados)
                    p.ExibirResumo();
                break;

            case 7:
                foreach (var c in clientes)
                    if(c != null)
                        Console.WriteLine(c.Nome + " " + c.Telefone);
                break;

            case 8:
                CardapioService.CardapioPorGrupos(cardapio);
                break;

            case 9:
                Console.WriteLine("MATRIZ DE MESAS");
                for(int i=0;i<5;i++)
                {
                    for(int j=0;j<5;j++)
                        Console.Write(mesas[i,j] ? "[X]" : "[O]");
                    Console.WriteLine();
                }
                break;

            case 10:
                Console.WriteLine("Pedidos ordenados por valor total:");
                foreach(var p in filaPedido.OrderByDescending(x => x.Total))
                    Console.WriteLine($"Código: {p.Codigo} | Cliente: {p.Cliente.Nome} | Total: {p.Total}");
                break;

            case 11:
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