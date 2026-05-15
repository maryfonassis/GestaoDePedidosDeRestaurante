using System.Text.Json.Serialization;

namespace GestaoDePedidoDeRestaurante.Modelos;
public class Endereco
{
    [JsonPropertyName("cep")]
    public string? CEP { get; set; }
    [JsonPropertyName("localidade")]
    public string? Cidade { get; set; }
    [JsonPropertyName("estado")]
    public string? Estado { get; set; }

    [JsonPropertyName("logradouro")]
    public string? logradouro { get; set; }

    [JsonPropertyName("bairro")]
    public string? Bairro { get; set; }
    public int? Numero { get; set; }

    public void ExibirEndereco()
    {
        System.Console.WriteLine($"CEP: {CEP}");
        System.Console.WriteLine($"Cidade: {Cidade}");
        System.Console.WriteLine($"Estado: {Estado}");
        System.Console.WriteLine($"Bairro: {Bairro}");
        System.Console.WriteLine($"Logradouro: {logradouro}");
        System.Console.WriteLine($"Número: {Numero}");
    }
    public void CompletarEndereco()
    {
        if (string.IsNullOrWhiteSpace(Bairro))
        {
            System.Console.WriteLine("Digite seu Bairro: ");
            Bairro = Console.ReadLine();
        }
         if (string.IsNullOrWhiteSpace(logradouro))
        {
            System.Console.WriteLine("Digite seu Logradouro: ");
            Bairro = Console.ReadLine();
        }
        if (Numero == null)
        {
            System.Console.WriteLine("Digite seu Número: ");
            Numero = int.Parse(Console.ReadLine()!);
        }
    }
    public void PreencherEndereco()
    {
        ExibirEndereco();
        System.Console.WriteLine();
        CompletarEndereco();
        System.Console.WriteLine();
        ExibirEndereco();
    }





}