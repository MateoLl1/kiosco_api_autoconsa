using System;
using System.Collections.Generic;
using Automotores.Kiosco.Models;
using Microsoft.EntityFrameworkCore;

namespace Automotores.Kiosco.Data;

public partial class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<SI_AGENCIA> SI_AGENCIA { get; set; }

    public virtual DbSet<SI_BAHIA> SiAgSI_BAHIAenda { get; set; }

    public virtual DbSet<SI_BAHIA> SI_BAHIA { get; set; }

    public virtual DbSet<SI_CLIENTE> SI_CLIENTE { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SI_AGENCIA>(entity =>
        {
            entity.HasKey(e => e.AgCodigo).HasFillFactor(80);

            entity.ToTable("SI_AGENCIA");

            entity.Property(e => e.AgCodigo)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("AG_CODIGO");
            entity.Property(e => e.AgAutoFactGara).HasColumnName("AG_AUTO_FACT_GARA");
            entity.Property(e => e.AgAutoHoja).HasColumnName("AG_AUTO_HOJA");
            entity.Property(e => e.AgAutoLato).HasColumnName("AG_AUTO_LATO");
            entity.Property(e => e.AgCaliRies).HasColumnName("AG_CALI_RIES");
            entity.Property(e => e.AgCargGere)
                .HasMaxLength(150)
                .HasColumnName("AG_CARG_GERE");
            entity.Property(e => e.AgCargResp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AG_CARG_RESP");
            entity.Property(e => e.AgChevy).HasColumnName("AG_CHEVY");
            entity.Property(e => e.AgClavDeal)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("AG_CLAV_DEAL");
            entity.Property(e => e.AgCodiUaf)
                .HasMaxLength(15)
                .IsFixedLength()
                .HasColumnName("AG_CODI_UAF");
            entity.Property(e => e.AgCupoAcc).HasColumnName("AG_CUPO_ACC");
            entity.Property(e => e.AgDealer)
                .HasMaxLength(100)
                .HasColumnName("AG_DEALER");
            entity.Property(e => e.AgDireccion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("AG_DIRECCION");
            entity.Property(e => e.AgDistCateLead).HasColumnName("AG_DIST_CATE_LEAD");
            entity.Property(e => e.AgEliminado).HasColumnName("AG_ELIMINADO");
            entity.Property(e => e.AgFactAseg).HasColumnName("AG_FACT_ASEG");
            entity.Property(e => e.AgFechMovi)
                .HasColumnType("datetime")
                .HasColumnName("AG_FECH_MOVI");
            entity.Property(e => e.AgFifo).HasColumnName("AG_FIFO");
            entity.Property(e => e.AgGerente)
                .HasMaxLength(150)
                .HasColumnName("AG_GERENTE");
            entity.Property(e => e.AgHabilitar)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ag_habilitar");
            entity.Property(e => e.AgImagMapa)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("AG_IMAG_MAPA");
            entity.Property(e => e.AgImpreAutoTalle).HasColumnName("AG_IMPRE_AUTO_TALLE");
            entity.Property(e => e.AgInfo)
                .HasMaxLength(255)
                .HasColumnName("AG_INFO");
            entity.Property(e => e.AgLatitud).HasColumnName("AG_LATITUD");
            entity.Property(e => e.AgLeadLiviano)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("AG_LEAD_LIVIANO");
            entity.Property(e => e.AgLeadPesado)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("AG_LEAD_PESADO");
            entity.Property(e => e.AgLineCor).HasColumnName("AG_LINE_COR");
            entity.Property(e => e.AgLongitud).HasColumnName("AG_LONGITUD");
            entity.Property(e => e.AgMailAnalGara)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("AG_MAIL_ANAL_GARA");
            entity.Property(e => e.AgMailBackOffi)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("Para registro de mails de usuarios back office de la agencia")
                .HasColumnName("AG_MAIL_BACK_OFFI");
            entity.Property(e => e.AgMailBo)
                .HasMaxLength(600)
                .IsUnicode(false)
                .HasColumnName("AG_MAIL_BO");
            entity.Property(e => e.AgMailCaja)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("AG_MAIL_CAJA");
            entity.Property(e => e.AgMailCont)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("AG_MAIL_CONT");
            entity.Property(e => e.AgMailCredDir)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("AG_MAIL_CRED_DIR");
            entity.Property(e => e.AgMailFi)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("AG_MAIL_FI");
            entity.Property(e => e.AgMailLogi)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("AG_MAIL_LOGI");
            entity.Property(e => e.AgMailServ)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("AG_MAIL_SERV");
            entity.Property(e => e.AgMailVehi)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("AG_MAIL_VEHI");
            entity.Property(e => e.AgMensProf)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("AG_MENS_PROF");
            entity.Property(e => e.AgMontoRese).HasColumnName("AG_MONTO_RESE");
            entity.Property(e => e.AgNick)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("AG_NICK");
            entity.Property(e => e.AgNombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AG_NOMBRE");
            entity.Property(e => e.AgNumeTecn).HasColumnName("AG_NUME_TECN");
            entity.Property(e => e.AgOrden).HasColumnName("AG_ORDEN");
            entity.Property(e => e.AgOrdenSoliRepu).HasColumnName("AG_ORDEN_SOLI_REPU");
            entity.Property(e => e.AgPassTurn)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("AG_PASS_TURN");
            entity.Property(e => e.AgPedgmDest)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("AG_PEDGM_DEST");
            entity.Property(e => e.AgPedgmSoli)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("AG_PEDGM_SOLI");
            entity.Property(e => e.AgPedgmText)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("AG_PEDGM_TEXT");
            entity.Property(e => e.AgPlaca)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("AG_PLACA");
            entity.Property(e => e.AgPrefijo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("AG_PREFIJO");
            entity.Property(e => e.AgPromo).HasColumnName("AG_PROMO");
            entity.Property(e => e.AgRespId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("AG_RESP_ID");
            entity.Property(e => e.AgResponsable)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AG_RESPONSABLE");
            entity.Property(e => e.AgRutaImg)
                .HasMaxLength(2500)
                .HasColumnName("AG_RUTA_IMG");
            entity.Property(e => e.AgTelefono1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AG_TELEFONO1");
            entity.Property(e => e.AgTelefono2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AG_TELEFONO2");
            entity.Property(e => e.AgTorre)
                .HasDefaultValue((short)0, "DF_SI_AGENCIA_AG_TORRE")
                .HasColumnName("AG_TORRE");
            entity.Property(e => e.AgTranAuto)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("AG_TRAN_AUTO");
            entity.Property(e => e.AgTransNomb)
                .HasMaxLength(100)
                .HasColumnName("AG_TRANS_NOMB");
            entity.Property(e => e.AgTransRuc)
                .HasMaxLength(13)
                .HasColumnName("AG_TRANS_RUC");
            entity.Property(e => e.AgTurno).HasColumnName("AG_TURNO");
            entity.Property(e => e.AgUrlShort)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("AG_URL_SHORT");
            entity.Property(e => e.AgUsuaDeal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AG_USUA_DEAL");
            entity.Property(e => e.ClCodiUsad)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("CL_CODI_USAD");
            entity.Property(e => e.SuCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("SU_CODIGO");
            entity.Property(e => e.UsCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("US_CODIGO");
        });

        modelBuilder.Entity<SI_AGENDA>(entity =>
        {
            entity.HasKey(e => e.AaCodigo)
                .HasName("PK_SI_AGEND_ACCE")
                .HasFillFactor(80);

            entity.ToTable("SI_AGENDA");

            entity.HasIndex(e => new { e.AaTipoAgen, e.HtCodigo }, "IX_TIPO_AGENDA").HasFillFactor(80);

            entity.Property(e => e.AaCodigo)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("AA_CODIGO");
            entity.Property(e => e.AaCaliClie).HasColumnName("AA_CALI_CLIE");
            entity.Property(e => e.AaComeClie)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("AA_COME_CLIE");
            entity.Property(e => e.AaConfirmado).HasColumnName("AA_CONFIRMADO");
            entity.Property(e => e.AaFechCanc)
                .HasColumnType("datetime")
                .HasColumnName("AA_FECH_CANC");
            entity.Property(e => e.AaFechConf)
                .HasColumnType("datetime")
                .HasColumnName("AA_FECH_CONF");
            entity.Property(e => e.AaFechIngr)
                .HasColumnType("datetime")
                .HasColumnName("AA_FECH_INGR");
            entity.Property(e => e.AaFechLleg)
                .HasColumnType("datetime")
                .HasColumnName("AA_FECH_LLEG");
            entity.Property(e => e.AaFechaFin)
                .HasColumnType("datetime")
                .HasColumnName("AA_FECHA_FIN");
            entity.Property(e => e.AaFechaFinEsti)
                .HasColumnType("datetime")
                .HasColumnName("AA_FECHA_FIN_ESTI");
            entity.Property(e => e.AaFechaInicio)
                .HasColumnType("datetime")
                .HasColumnName("AA_FECHA_INICIO");
            entity.Property(e => e.AaIdCale)
                .HasMaxLength(1000)
                .HasColumnName("AA_ID_CALE");
            entity.Property(e => e.AaKiloIni).HasColumnName("AA_KILO_INI");
            entity.Property(e => e.AaMails)
                .HasMaxLength(300)
                .HasComment("Contiene los mails de los asistentes a  la reunion de la agenda")
                .HasColumnName("AA_MAILS");
            entity.Property(e => e.AaObservacion)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("AA_OBSERVACION");
            entity.Property(e => e.AaProcAgen).HasColumnName("AA_PROC_AGEN");
            entity.Property(e => e.AaPuntualidad).HasColumnName("AA_PUNTUALIDAD");
            entity.Property(e => e.AaSaliConc).HasColumnName("AA_SALI_CONC");
            entity.Property(e => e.AaStatus)
                .HasComment("0. Sin Estatus 1.Entregado/Terminado 2. No Asistio")
                .HasColumnName("AA_STATUS");
            entity.Property(e => e.AaTiemAgen).HasColumnName("AA_TIEM_AGEN");
            entity.Property(e => e.AaTipoAgen)
                .HasComment("1. Agenda Entrega Vendedores 2. Agenda Accesorios")
                .HasColumnName("AA_TIPO_AGEN");
            entity.Property(e => e.AaValiCedu).HasColumnName("AA_VALI_CEDU");
            entity.Property(e => e.AaValiLice).HasColumnName("AA_VALI_LICE");
            entity.Property(e => e.AgCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("AG_CODIGO");
            entity.Property(e => e.AtCodigo)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("AT_CODIGO");
            entity.Property(e => e.BhCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("BH_CODIGO");
            entity.Property(e => e.ClCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("CL_CODIGO");
            entity.Property(e => e.GuCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("GU_CODIGO");
            entity.Property(e => e.HcCodigo)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("HC_CODIGO");
            entity.Property(e => e.HtCodigo)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("HT_CODIGO");
            entity.Property(e => e.OiCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("OI_CODIGO");
            entity.Property(e => e.PrCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("PR_CODIGO");
            entity.Property(e => e.RfoCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("RFO_CODIGO");
            entity.Property(e => e.StCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ST_CODIGO");
            entity.Property(e => e.StdCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("STD_CODIGO");
            entity.Property(e => e.TeCodiAgen)
                .HasComment("Asesor servicio en entrega de vehículo")
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("TE_CODI_AGEN");
            entity.Property(e => e.TeCodiAgenAsis)
                .HasComment("Asesor servicio asiste a entrega de vehículo")
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("TE_CODI_AGEN_ASIS");
            entity.Property(e => e.TeCodigo)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("TE_CODIGO");
            entity.Property(e => e.UsCodiCall)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("US_CODI_CALL");
            entity.Property(e => e.UsCodiLogi)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("US_CODI_LOGI");
            entity.Property(e => e.UsCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("US_CODIGO");
        });

        modelBuilder.Entity<SI_BAHIA>(entity =>
        {
            entity.HasKey(e => e.BhCodigo).HasFillFactor(80);

            entity.ToTable("SI_BAHIA");

            entity.Property(e => e.BhCodigo)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("BH_CODIGO");
            entity.Property(e => e.AgCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("AG_CODIGO");
            entity.Property(e => e.BhBahiaTipo)
                .HasComment("1 BAHIAS BLOQUEASAS PARA CALL CENTER\r\n0 DAFAULT")
                .HasDefaultValue(false, "DF_SI_BAHIA_BH_BAHIA_TIPO")
                .HasColumnName("BH_BAHIA_TIPO");
            entity.Property(e => e.BhDescripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("BH_DESCRIPCION");
            entity.Property(e => e.BhNombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("BH_NOMBRE");
            entity.Property(e => e.BhOrden)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("BH_ORDEN");
            entity.Property(e => e.BhStatus).HasColumnName("BH_STATUS");
            entity.Property(e => e.BhTipo)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("BH_TIPO");
            entity.Property(e => e.TeCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("TE_CODIGO");
            entity.Property(e => e.TlCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("TL_CODIGO");
        });

        modelBuilder.Entity<SI_CLIENTE>(entity =>
        {
            entity.HasKey(e => e.ClCodigo)
                .HasName("PK_SI_CLIENTE_1")
                .HasFillFactor(80);

            entity.ToTable("SI_CLIENTE", tb => tb.HasTrigger("tr_Update_Cliente"));

            entity.HasIndex(e => e.ClId, "IX_SI_CLIENTE").HasFillFactor(80);

            entity.HasIndex(e => e.ClNombre, "IX_SI_CLIENTE_1").HasFillFactor(80);

            entity.HasIndex(e => e.ClApellido, "IX_SI_CLIENTE_2").HasFillFactor(80);

            entity.HasIndex(e => e.SgCodigo, "IX_SI_CLIENTE_3").HasFillFactor(80);

            entity.Property(e => e.ClCodigo)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("CL_CODIGO");
            entity.Property(e => e.ClActiEcon)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("CL_ACTI_ECON");
            entity.Property(e => e.ClActuClie)
                .HasDefaultValue(false, "DF_SI_CLIENTE_CL_ACTU_CLIE")
                .HasColumnName("CL_ACTU_CLIE");
            entity.Property(e => e.ClActualizado).HasColumnName("CL_ACTUALIZADO");
            entity.Property(e => e.ClAgenRete).HasColumnName("CL_AGEN_RETE");
            entity.Property(e => e.ClApellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CL_APELLIDO");
            entity.Property(e => e.ClCallPrin)
                .HasMaxLength(100)
                .HasColumnName("CL_CALL_PRIN");
            entity.Property(e => e.ClCallSecu)
                .HasMaxLength(100)
                .HasColumnName("CL_CALL_SECU");
            entity.Property(e => e.ClClApoderado)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("CL_CL_APODERADO");
            entity.Property(e => e.ClClCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("CL_CL_CODIGO");
            entity.Property(e => e.ClCodiEstaCivi)
                .HasDefaultValueSql("(NULL)", "DF__SI_CLIENT__CL_CO__11E561C4")
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("CL_CODI_ESTA_CIVI");
            entity.Property(e => e.ClCodiNaci)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("CL_CODI_NACI");
            entity.Property(e => e.ClCodiOrigIngr)
                .HasDefaultValueSql("(NULL)", "DF__SI_CLIENT__CL_CO__12D985FD")
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("CL_CODI_ORIG_INGR");
            entity.Property(e => e.ClCodiRangSuel)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("CL_CODI_RANG_SUEL");
            entity.Property(e => e.ClCodiSpe)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("CL_CODI_SPE");
            entity.Property(e => e.ClContEspe).HasColumnName("CL_CONT_ESPE");
            entity.Property(e => e.ClContacto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CL_CONTACTO");
            entity.Property(e => e.ClCupo).HasColumnName("CL_CUPO");
            entity.Property(e => e.ClDescRepuMost).HasColumnName("CL_DESC_REPU_MOST");
            entity.Property(e => e.ClDireFactAuto)
                .HasMaxLength(200)
                .HasColumnName("CL_DIRE_FACT_AUTO");
            entity.Property(e => e.ClDireNume)
                .HasMaxLength(20)
                .HasColumnName("CL_DIRE_NUME");
            entity.Property(e => e.ClDirePrin)
                .HasMaxLength(50)
                .HasColumnName("CL_DIRE_PRIN");
            entity.Property(e => e.ClDireSecu)
                .HasMaxLength(50)
                .HasColumnName("CL_DIRE_SECU");
            entity.Property(e => e.ClDireTrab)
                .HasMaxLength(150)
                .IsFixedLength()
                .HasColumnName("CL_DIRE_TRAB");
            entity.Property(e => e.ClDireccion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("CL_DIRECCION");
            entity.Property(e => e.ClEcoValor).HasColumnName("CL_ECO_VALOR");
            entity.Property(e => e.ClEdad)
                .HasComputedColumnSql("(datediff(year,[CL_FECH_NAC],getdate()))", false)
                .HasColumnName("CL_EDAD");
            entity.Property(e => e.ClEliminado).HasColumnName("CL_ELIMINADO");
            entity.Property(e => e.ClEmprTrab)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("CL_EMPR_TRAB");
            entity.Property(e => e.ClExpoHabi).HasColumnName("CL_EXPO_HABI");
            entity.Property(e => e.ClExpoHabiRecu).HasColumnName("CL_EXPO_HABI_RECU");
            entity.Property(e => e.ClFactIndi).HasColumnName("CL_FACT_INDI");
            entity.Property(e => e.ClFactMult)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("CL_FACT_MULT");
            entity.Property(e => e.ClFechModi)
                .HasColumnType("datetime")
                .HasColumnName("CL_FECH_MODI");
            entity.Property(e => e.ClFechMovi)
                .HasColumnType("datetime")
                .HasColumnName("CL_FECH_MOVI");
            entity.Property(e => e.ClFechNac)
                .HasColumnType("datetime")
                .HasColumnName("CL_FECH_NAC");
            entity.Property(e => e.ClGranCont).HasColumnName("CL_GRAN_CONT");
            entity.Property(e => e.ClId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CL_ID");
            entity.Property(e => e.ClIdCont)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("CL_ID_CONT");
            entity.Property(e => e.ClInstPubl).HasColumnName("CL_INST_PUBL");
            entity.Property(e => e.ClInstPublFina).HasColumnName("CL_INST_PUBL_FINA");
            entity.Property(e => e.ClIva).HasColumnName("CL_IVA");
            entity.Property(e => e.ClLugaRefe)
                .HasMaxLength(150)
                .HasColumnName("CL_LUGA_REFE");
            entity.Property(e => e.ClMail)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("CL_MAIL");
            entity.Property(e => e.ClMailEmpr)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("CL_MAIL_EMPR");
            entity.Property(e => e.ClMicrEmpr).HasColumnName("CL_MICR_EMPR");
            entity.Property(e => e.ClNegoPopu).HasColumnName("CL_NEGO_POPU");
            entity.Property(e => e.ClNiveEstu)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("CL_NIVE_ESTU");
            entity.Property(e => e.ClNoTrab).HasColumnName("CL_NO_TRAB");
            entity.Property(e => e.ClNombre)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("CL_NOMBRE");
            entity.Property(e => e.ClNumeCarg)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("CL_NUME_CARG");
            entity.Property(e => e.ClObliConta).HasColumnName("CL_OBLI_CONTA");
            entity.Property(e => e.ClPersPoliExpu).HasColumnName("CL_PERS_POLI_EXPU");
            entity.Property(e => e.ClPorcDesc)
                .HasDefaultValue(0.0, "DF_SI_CLIENTE_CL_PORC_DESC")
                .HasColumnName("CL_PORC_DESC");
            entity.Property(e => e.ClPorcDescServ)
                .HasDefaultValue(0.0, "DF_SI_CLIENTE_CL_PORC_DESC_SERV")
                .HasColumnName("CL_PORC_DESC_SERV");
            entity.Property(e => e.ClProfActu)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("CL_PROF_ACTU");
            entity.Property(e => e.ClReprLega)
                .HasMaxLength(100)
                .HasColumnName("CL_REPR_LEGA");
            entity.Property(e => e.ClReprLegaApel)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("CL_REPR_LEGA_APEL");
            entity.Property(e => e.ClReprLegaCedu)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("CL_REPR_LEGA_CEDU");
            entity.Property(e => e.ClRimpe).HasColumnName("CL_RIMPE");
            entity.Property(e => e.ClSepaBie).HasColumnName("CL_SEPA_BIE");
            entity.Property(e => e.ClTelcont1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CL_TELCONT1");
            entity.Property(e => e.ClTelcont2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CL_TELCONT2");
            entity.Property(e => e.ClTelefono1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CL_TELEFONO1");
            entity.Property(e => e.ClTelefono2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue(" ", "DF_SI_CLIENTE_CL_TELEFONO2")
                .HasColumnName("CL_TELEFONO2");
            entity.Property(e => e.ClTelfTrab)
                .HasMaxLength(50)
                .HasColumnName("CL_TELF_TRAB");
            entity.Property(e => e.ClTelfTrabExt)
                .HasMaxLength(10)
                .HasColumnName("CL_TELF_TRAB_EXT");
            entity.Property(e => e.ClTipoClie)
                .HasComment("Eliminar este campo la información esta en el campo CL_CODI_ORIG_INGR")
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("CL_TIPO_CLIE");
            entity.Property(e => e.ClTipoId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CL_TIPO_ID");
            entity.Property(e => e.ClTitulo)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("CL_TITULO");
            entity.Property(e => e.ClValusado).HasColumnName("CL_VALUSADO");
            entity.Property(e => e.PlCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("PL_CODIGO");
            entity.Property(e => e.RrCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("RR_CODIGO");
            entity.Property(e => e.SgCodigo).HasColumnName("SG_CODIGO");
            entity.Property(e => e.UgCodiDomi)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("UG_CODI_DOMI");
            entity.Property(e => e.UgCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("UG_CODIGO");
            entity.Property(e => e.UgsCodiDomi)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("UGS_CODI_DOMI");
            entity.Property(e => e.UgsCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("UGS_CODIGO");
            entity.Property(e => e.UsCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("US_CODIGO");
            entity.Property(e => e.UsCodigoModi)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("US_CODIGO_MODI");
            entity.Property(e => e.ZcCodigo)
                .HasDefaultValue(0m, "DF_SI_CLIENTE_ZC_CODIGO")
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ZC_CODIGO");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
