using System;
using System.Collections.Generic;

namespace Automotores.Kiosco.Models;

public partial class SEG_PARAMETRO_USUARIO
{
    public decimal PuCodigo { get; set; }

    public decimal? AgCodigo { get; set; }

    public decimal? PuUbicgeo { get; set; }

    public decimal? PuCaja { get; set; }

    public string? PuTransito { get; set; }

    public string? PuModulo { get; set; }

    public string? PuBahia { get; set; }

    public decimal? UsCodigo { get; set; }

    public string? PuDatabase { get; set; }

    public string? PuUsuario { get; set; }

    public string? PuPassword { get; set; }

    /// <summary>
    /// Agencia del usuario por defecto
    /// </summary>
    public bool? PuAgenDefa { get; set; }

    public virtual SEG_USUARIO? UsCodigoNavigation { get; set; }
}
