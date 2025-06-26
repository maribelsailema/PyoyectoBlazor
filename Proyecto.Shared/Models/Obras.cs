public class Obra
{
    public int IdObra { get; set; }
    public string Cedula { get; set; } = string.Empty;
    public string TipoObra { get; set; } = string.Empty;
    public DateOnly Fecha { get; set; }
    public byte[]? Pdf { get; set; }
}
