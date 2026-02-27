using System;
using System.Collections.Generic;

namespace Automotores.Kiosco.Modelos;

public partial class SI_CLIENTE
{
    public decimal ClCodigo { get; set; }

    public string? ClNombre { get; set; }

    public string? ClApellido { get; set; }

    public string? ClDireccion { get; set; }

    public string? ClTelefono1 { get; set; }

    public string? ClTelefono2 { get; set; }

    public string? ClTipoId { get; set; }

    public string ClId { get; set; } = null!;

    public DateTime? ClFechNac { get; set; }

    public string? ClContacto { get; set; }

    public string? ClTelcont1 { get; set; }

    public string? ClTelcont2 { get; set; }

    public decimal? UgCodigo { get; set; }

    public decimal? UsCodigo { get; set; }

    public DateTime? ClFechMovi { get; set; }

    public double? ClCupo { get; set; }

    public double? ClValusado { get; set; }

    public string? ClMail { get; set; }

    public short? SgCodigo { get; set; }

    public bool ClContEspe { get; set; }

    public bool ClEliminado { get; set; }

    public decimal PlCodigo { get; set; }

    public double? ClPorcDesc { get; set; }

    public double? ClPorcDescServ { get; set; }

    public decimal? ZcCodigo { get; set; }

    public short? ClActualizado { get; set; }

    public string? ClTitulo { get; set; }

    public string? ClIdCont { get; set; }

    public double? ClDescRepuMost { get; set; }

    public short? ClIva { get; set; }

    public string? ClTelfTrab { get; set; }

    public string? ClTelfTrabExt { get; set; }

    public string? ClFactMult { get; set; }

    public string? ClReprLega { get; set; }

    public decimal? UgsCodigo { get; set; }

    public string? ClCallPrin { get; set; }

    public string? ClCallSecu { get; set; }

    public string? ClDireNume { get; set; }

    public string? ClLugaRefe { get; set; }

    public decimal? RrCodigo { get; set; }

    public decimal? UsCodigoModi { get; set; }

    public DateTime? ClFechModi { get; set; }

    public string? ClDireTrab { get; set; }

    public string? ClReprLegaApel { get; set; }

    public string? ClReprLegaCedu { get; set; }

    public string? ClEmprTrab { get; set; }

    public bool? ClNoTrab { get; set; }

    public bool? ClFactIndi { get; set; }

    public string? ClDirePrin { get; set; }

    public string? ClDireSecu { get; set; }

    public string? ClDireFactAuto { get; set; }

    public decimal? ClCodiEstaCivi { get; set; }

    public decimal? ClCodiOrigIngr { get; set; }

    public bool ClExpoHabi { get; set; }

    public decimal? ClCodiNaci { get; set; }

    /// <summary>
    /// Eliminar este campo la información esta en el campo CL_CODI_ORIG_INGR
    /// </summary>
    public decimal? ClTipoClie { get; set; }

    public decimal? ClActiEcon { get; set; }

    public decimal? ClClApoderado { get; set; }

    public decimal? ClClCodigo { get; set; }

    public decimal? ClCodiRangSuel { get; set; }

    public decimal? ClCodiSpe { get; set; }

    public int? ClEdad { get; set; }

    public string? ClMailEmpr { get; set; }

    public decimal? ClNiveEstu { get; set; }

    public decimal? ClNumeCarg { get; set; }

    public bool? ClObliConta { get; set; }

    public int? ClPersPoliExpu { get; set; }

    public decimal? ClProfActu { get; set; }

    public bool? ClSepaBie { get; set; }

    public decimal? UgCodiDomi { get; set; }

    public decimal? UgsCodiDomi { get; set; }

    public bool? ClActuClie { get; set; }

    public bool ClMicrEmpr { get; set; }

    public bool ClAgenRete { get; set; }

    public bool ClExpoHabiRecu { get; set; }

    public bool ClEcoValor { get; set; }

    public bool ClInstPubl { get; set; }

    public bool ClRimpe { get; set; }

    public bool ClNegoPopu { get; set; }

    public bool ClGranCont { get; set; }

    public bool ClInstPublFina { get; set; }
}
