

namespace Automotores.Kiosco.Models;

public partial class SEG_USUARIO
{
    public decimal UsCodigo { get; set; }

    public string UsNombre { get; set; } = null!;

    public string? UsDescrip { get; set; }

    public string UsLogin { get; set; } = null!;

    public string UsPassword { get; set; } = null!;

    public decimal GrCodigo { get; set; }

    public bool UsDescRep { get; set; }

    public double? UsPorDescRep { get; set; }

    public bool UsDescMoi { get; set; }

    public double? UsPorDescMoi { get; set; }

    public double? UsMontDesc { get; set; }

    public bool UsDescAuto { get; set; }

    public double UsPorDescAuto { get; set; }

    public short UsPrecio { get; set; }

    public short UsTipoPrecio { get; set; }

    public bool UsModiDiar { get; set; }

    public string? UsSeguHoja { get; set; }

    public bool? UsCambIva { get; set; }

    public short? UsTopCons { get; set; }

    public decimal? UsContAcci { get; set; }

    public short? UsCondInte { get; set; }

    /// <summary>
    /// 0:Activo |1:Cambio de Clave |2:Bloqueado |3:Inactivo
    /// </summary>
    public short? UsStatus { get; set; }

    public short? UsExepcion { get; set; }

    public decimal? UsReimpresion { get; set; }

    public decimal? UsDescuento { get; set; }

    public short? UsLiquComp { get; set; }

    public short? UsAjusBloq { get; set; }

    public short? UsCompRepu { get; set; }

    public short? UsTasa { get; set; }

    public short? UsAcc { get; set; }

    public short? UsFactGaraGm { get; set; }

    public string? UsPermTorre { get; set; }

    public short? UsCobroDeduc { get; set; }

    public short? UsFactSinDeduc { get; set; }

    public string? UsRuc { get; set; }

    public short? UsImprHtPant { get; set; }

    /// <summary>
    /// Si el usuario tiene permiso de desbloquear ordenes de compra
    /// </summary>
    public short? UsBloqOrdeComp { get; set; }

    public short? UsDescTarjCred { get; set; }

    public short? UsDevoAntiEfec { get; set; }

    public string? UsMail { get; set; }

    public bool? UsPermIntranet { get; set; }

    public decimal? CcCodigo { get; set; }

    public bool? UsPermTranAuto { get; set; }

    public short? UsSupervisor { get; set; }

    public int? UsPermSph { get; set; }

    public bool? UsComp { get; set; }

    public int? UsPermOtProv { get; set; }

    public string? UsAgenPremium { get; set; }

    public string? UsFlota { get; set; }

    public string? UsPc { get; set; }

    public string? UsLinkReun { get; set; }

    public string? UsLinkId { get; set; }

    public string? UsLinkPass { get; set; }

    public string UsTipoIden { get; set; } = null!;

    public bool? UsTranHc { get; set; }

    public bool? UsExepCaja { get; set; }

    public double? UsPorcAjusPrec { get; set; }

    public decimal? UsCodiCrea { get; set; }

    public DateTime? UsFechCrea { get; set; }

    public decimal? UsCodiModi { get; set; }

    public DateTime? UsFechModi { get; set; }

    public virtual ICollection<SEG_PARAMETRO_USUARIO> SegParametroUsuarios { get; set; } = new List<SEG_PARAMETRO_USUARIO>();
}
