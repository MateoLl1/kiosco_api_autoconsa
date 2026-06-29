

namespace Automotores.Kiosco.Models;

public partial class SI_TURNO_KIOSCO
{
    public decimal TkCodigo { get; set; }

    public decimal AgCodigo { get; set; }

    public decimal? ClCodigo { get; set; }

    public decimal? AsgCodigo { get; set; }

    public string TkTurno { get; set; } = null!;

    public string TkTipo { get; set; } = null!;

    public string TkEstado { get; set; } = null!;

    public decimal? UsCodiLlamo { get; set; }

    public DateTime TkFechCrea { get; set; }

    public DateTime? TkFechLlam { get; set; }

    public DateTime? TkFechAten { get; set; }

    public decimal? TkTimeEspe { get; set; }
}
