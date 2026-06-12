namespace GestaoDePedidoDeRestaurante.Servicos;
public class Cliente
{
    public string? Nome { get; set; }
    public string? Telefone { get; set; }

    public Cliente(string nome, string telefone)
    {
        Nome = nome;
        Telefone = telefone;
    }
    

}