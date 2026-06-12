using System.Globalization;
using System.Text.Json;
using GestaoDePedidoDeRestaurante.Modelos;

namespace GestaoDePedidoDeRestaurante.Servicos;
public class CardapioService
{
    public static List<ItemMenu> CarregarCardapio()
    {
        string json = File.ReadAllText("Dados/cardapio.json");
        var cardapio = JsonSerializer.Deserialize<Cardapio>(json);
        if (cardapio != null)
            return cardapio.cardapio;
        else throw new NullReferenceException();
    }
    public static void ExibirCardapio(List<ItemMenu> cardapio)
    {
        Console.WriteLine("-----CARDAPIO🥘----");
        foreach (var item in cardapio)
        {
            Console.WriteLine(item.ToString());
        }
    }
    public static ItemMenu BuscarItemPorCodigo(List<ItemMenu> cardapio, int codigo)
    {
        var itemCodigo = cardapio.FirstOrDefault(item => item.Codigo.Equals(codigo));
        if (itemCodigo != null)
            return itemCodigo;
        else throw new NullReferenceException();
    }
     public static void BuscarItemPorNome(List<ItemMenu> cardapio, string nome)
    {
        var itemNome = cardapio.Where(item => item.Nome.Contains(nome)).ToList();
         foreach (var item in itemNome)
        {
            Console.WriteLine(item.ToString());
        }
    }
     public static void BuscarItemPorCategoria(List<ItemMenu> cardapio, string categoria)
    {
        var itemCategoria = cardapio.Where(item => item.Categoria.Equals(categoria)).ToList();
         foreach (var item in itemCategoria)
        {
            Console.WriteLine(item.ToString());
        }
    }
    public static void CardapioPorGrupos(List<ItemMenu> cardapio)
    {
        var cardapioGrupo = cardapio.GroupBy(item => item.Categoria).ToList();
        Console.WriteLine("-----CARDAPIO🥘----");

        foreach (var item in cardapioGrupo)
        {
            Console.WriteLine(item.Key.ToUpper());
            foreach (var menu in item)
            {
                Console.WriteLine($"-{menu.Nome} | Codigo: {menu.Codigo} | Preço: R${menu.Preco.ToString("F2", CultureInfo.InvariantCulture)}");
            }
        }
    }
}