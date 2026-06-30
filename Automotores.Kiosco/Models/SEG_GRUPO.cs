
namespace Automotores.Kiosco.Models;

public partial class SEG_GRUPO
{
    public decimal GrCodigo { get; set; }

    public string GrNombre { get; set; } = null!;

    public string? GrDescrip { get; set; }

    public bool? GrAproba { get; set; }

    public int GxCodigo { get; set; }
}
