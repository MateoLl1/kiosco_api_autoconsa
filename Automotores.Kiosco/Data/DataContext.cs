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

    public virtual DbSet<SI_BAHIA> SI_BAHIA { get; set; }

    public virtual DbSet<SI_CLIENTE> SI_CLIENTE { get; set; }


    public virtual DbSet<SI_AGEND_TECN> SI_AGEND_TECN { get; set; }

    public virtual DbSet<SI_MODELO> SI_MODELO { get; set; }

    public virtual DbSet<SI_STICKER> SI_STICKER { get; set; }

    public virtual DbSet<SI_ASIG_TURNO> SI_ASIG_TURNO { get; set; }

    public virtual DbSet<SI_TURNO> SI_TURNO { get; set; }
    
    public virtual DbSet<SI_TURNERO_MEDIA> SI_TURNERO_MEDIA { get; set; }

    public virtual DbSet<SEG_PARAMETRO_USUARIO> SEG_PARAMETRO_USUARIO { get; set; }

    public virtual DbSet<SEG_USUARIO> SEG_USUARIO { get; set; }

    public virtual DbSet<SI_DISP_VEND> SI_DISP_VEND { get; set; }


    
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

        modelBuilder.Entity<SI_AGEND_TECN>(entity =>
        {
            entity.HasKey(e => e.AtCodigo).HasFillFactor(80);

            entity.ToTable("SI_AGEND_TECN");

            entity.HasIndex(e => new { e.AtStatus, e.IoCodigo, e.CmcCodigo, e.AtEstaCc2 }, "AT_CODIGO_NEW").HasFillFactor(80);

            entity.HasIndex(e => e.AtCodiRela, "AT_CODI_RELA").HasFillFactor(80);

            entity.HasIndex(e => e.FsCodigo, "FS_CODIGO").HasFillFactor(80);

            entity.HasIndex(e => e.StCodigo, "IX_ST_CODIGO").HasFillFactor(80);

            entity.HasIndex(e => e.TlCodigo, "TL_CODIGO").HasFillFactor(80);

            entity.HasIndex(e => new { e.BhCodigo, e.AtStatus }, "bh_codigo").HasFillFactor(80);

            entity.HasIndex(e => new { e.AtHoraInicio, e.AtHoraFin }, "fechas").HasFillFactor(80);

            entity.HasIndex(e => e.HtCodigo, "ht_codigo").HasFillFactor(80);

            entity.HasIndex(e => e.AtEstaCc2, "indAgen_Tecn").HasFillFactor(80);

            entity.Property(e => e.AtCodigo)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("AT_CODIGO");
            entity.Property(e => e.AtCodiRela)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("AT_CODI_RELA");
            entity.Property(e => e.AtEstaCc)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("AT_ESTA_CC");
            entity.Property(e => e.AtEstaCc1)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("AT_ESTA_CC1");
            entity.Property(e => e.AtEstaCc2).HasColumnName("AT_ESTA_CC2");
            entity.Property(e => e.AtEstado)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("AT_ESTADO");
            entity.Property(e => e.AtFechCanc)
                .HasColumnType("datetime")
                .HasColumnName("AT_FECH_CANC");
            entity.Property(e => e.AtFechCc1)
                .HasColumnType("datetime")
                .HasColumnName("AT_FECH_CC1");
            entity.Property(e => e.AtFechConf)
                .HasColumnType("datetime")
                .HasColumnName("AT_FECH_CONF");
            entity.Property(e => e.AtFechCrea)
                .HasColumnType("datetime")
                .HasColumnName("AT_FECH_CREA");
            entity.Property(e => e.AtFechFina)
                .HasColumnType("datetime")
                .HasColumnName("AT_FECH_FINA");
            entity.Property(e => e.AtFechGest)
                .HasColumnType("datetime")
                .HasColumnName("AT_FECH_GEST");
            entity.Property(e => e.AtFechHoja)
                .HasColumnType("datetime")
                .HasColumnName("AT_FECH_HOJA");
            entity.Property(e => e.AtFechLleg)
                .HasColumnType("datetime")
                .HasColumnName("AT_FECH_LLEG");
            entity.Property(e => e.AtFecha)
                .HasColumnType("datetime")
                .HasColumnName("AT_FECHA");
            entity.Property(e => e.AtFrecuencia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AT_FRECUENCIA");
            entity.Property(e => e.AtHoraFin)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AT_HORA_FIN");
            entity.Property(e => e.AtHoraInicio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AT_HORA_INICIO");
            entity.Property(e => e.AtHoraTaxi)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AT_HORA_TAXI");
            entity.Property(e => e.AtIntePrea)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("AT_INTE_PREA");
            entity.Property(e => e.AtInteStat).HasColumnName("AT_INTE_STAT");
            entity.Property(e => e.AtKilome)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("AT_KILOME");
            entity.Property(e => e.AtMotiAnu)
                .HasComment("1	NO CONTESTA\r\n2	REAGENDA\r\n3	NO EXISTE INTERÉS\r\n4	ELIGE OTRO TALLER")
                .HasColumnName("AT_MOTI_ANU");
            entity.Property(e => e.AtObservacion)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("AT_OBSERVACION");
            entity.Property(e => e.AtPreeTrab)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AT_PREE_TRAB");
            entity.Property(e => e.AtStatus)
                .HasComment("1 CITA O AGENDADO\r\n2 CONFIRMADO \r\n3 CONCESIONARIO \r\n4 ATENDIDO \r\n5 CANCELADO \r\n6 NO LLEGO \r\n7 TERMINADO\r\n8 REPUESTOS\r\n9 AUTORIZACION\r\n10 LAVADORA\r\n11 CAMBIO AREA")
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("AT_STATUS");
            entity.Property(e => e.AtTaxi).HasColumnName("AT_TAXI");
            entity.Property(e => e.AtTiempo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AT_TIEMPO");
            entity.Property(e => e.AtTiempoReal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AT_TIEMPO_REAL");
            entity.Property(e => e.AtUbicCrea)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasComment("--NOMENCLATURA\r\n NULL  CUANDO SOLO SE HISO LA CITA O ES REAGENDA\r\n T HOJA DE TRABAJO SE ABRIO EN BAHIA\r\n C HOJA DE TRABAJO SE ABRIO EN LA CENTRAL  ATENDIMIENTO.")
                .HasColumnName("AT_UBIC_CREA");
            entity.Property(e => e.BhCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("BH_CODIGO");
            entity.Property(e => e.ClCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("CL_CODIGO");
            entity.Property(e => e.CmCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("CM_CODIGO");
            entity.Property(e => e.CmcCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("CMC_CODIGO");
            entity.Property(e => e.EecCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("EEC_CODIGO");
            entity.Property(e => e.FaCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("FA_CODIGO");
            entity.Property(e => e.FaCodigoPrea)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("FA_CODIGO_PREA");
            entity.Property(e => e.FltCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("FLT_CODIGO");
            entity.Property(e => e.FsCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("FS_CODIGO");
            entity.Property(e => e.FsCodigoPrea)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("FS_CODIGO_PREA");
            entity.Property(e => e.HtCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("HT_CODIGO");
            entity.Property(e => e.IoCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("IO_CODIGO");
            entity.Property(e => e.IsCodigo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IS_CODIGO");
            entity.Property(e => e.KiCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("KI_CODIGO");
            entity.Property(e => e.PeCodiCita)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("PE_CODI_CITA");
            entity.Property(e => e.PeCodiLlam)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("PE_CODI_LLAM");
            entity.Property(e => e.PfCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("PF_CODIGO");
            entity.Property(e => e.PmCodigo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PM_CODIGO");
            entity.Property(e => e.ReCodigo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("RE_CODIGO");
            entity.Property(e => e.StCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ST_CODIGO");
            entity.Property(e => e.TccCodigo).HasColumnName("TCC_CODIGO");
            entity.Property(e => e.TlCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("TL_CODIGO");
            entity.Property(e => e.UsCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("US_CODIGO");
            entity.Property(e => e.UsCodigoCarc)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("US_CODIGO_CARC");
            entity.Property(e => e.UsCodigoGest)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("US_CODIGO_GEST");
        });

        modelBuilder.Entity<SI_MODELO>(entity =>
        {
            entity.HasKey(e => e.MoCodigo).HasFillFactor(80);

            entity.ToTable("SI_MODELO", tb => tb.HasTrigger("tr_Update_Modelo"));

            entity.HasIndex(e => e.MoNombre, "MO_NOMBRE_IX").HasFillFactor(80);

            entity.HasIndex(e => e.TiCodigo, "TI_CODIGO_IX").HasFillFactor(80);

            entity.Property(e => e.MoCodigo)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("MO_CODIGO");
            entity.Property(e => e.CaCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("CA_CODIGO");
            entity.Property(e => e.MoCilindraje)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MO_CILINDRAJE");
            entity.Property(e => e.MoClase)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MO_CLASE");
            entity.Property(e => e.MoCodigoAlt)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MO_CODIGO_ALT");
            entity.Property(e => e.MoCombus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MO_COMBUS");
            entity.Property(e => e.MoDescrip)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("MO_DESCRIP");
            entity.Property(e => e.MoDispPreInst).HasColumnName("MO_DISP_PRE_INST");
            entity.Property(e => e.MoEje)
                .HasDefaultValue(0, "DF__SI_MODELO__MO_EJ__405268A3")
                .HasColumnName("MO_EJE");
            entity.Property(e => e.MoEliminado).HasColumnName("MO_ELIMINADO");
            entity.Property(e => e.MoEstado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MO_ESTADO");
            entity.Property(e => e.MoFechMovi)
                .HasColumnType("datetime")
                .HasColumnName("MO_FECH_MOVI");
            entity.Property(e => e.MoGmOb)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MO_GM_OB");
            entity.Property(e => e.MoImagen)
                .HasColumnType("image")
                .HasColumnName("MO_IMAGEN");
            entity.Property(e => e.MoImportado)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("MO_IMPORTADO");
            entity.Property(e => e.MoKmat)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MO_KMAT");
            entity.Property(e => e.MoLinkFicha)
                .IsUnicode(false)
                .HasColumnName("MO_LINK_FICHA");
            entity.Property(e => e.MoNoPreag).HasColumnName("MO_NO_PREAG");
            entity.Property(e => e.MoNombFich)
                .HasMaxLength(350)
                .IsUnicode(false)
                .HasColumnName("MO_NOMB_FICH");
            entity.Property(e => e.MoNombHomo)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("MO_NOMB_HOMO");
            entity.Property(e => e.MoNombre)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("MO_NOMBRE");
            entity.Property(e => e.MoNombreAlt)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("MO_NOMBRE_ALT");
            entity.Property(e => e.MoNombreGm)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("MO_NOMBRE_GM");
            entity.Property(e => e.MoNombreImpo)
                .HasMaxLength(50)
                .HasColumnName("MO_NOMBRE_IMPO");
            entity.Property(e => e.MoPasaje)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MO_PASAJE");
            entity.Property(e => e.MoPath)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("MO_PATH");
            entity.Property(e => e.MoPbv)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("", "DF_SI_MODELO_MO_PBV")
                .HasColumnName("MO_PBV");
            entity.Property(e => e.MoPorcIva)
                .HasDefaultValue(true)
                .HasColumnName("MO_PORC_IVA");
            entity.Property(e => e.MoPreciov)
                .HasColumnType("money")
                .HasColumnName("MO_PRECIOV");
            entity.Property(e => e.MoPreciovS).HasColumnName("MO_PRECIOV_S");
            entity.Property(e => e.MoProced)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MO_PROCED");
            entity.Property(e => e.MoPuntos)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MO_PUNTOS");
            entity.Property(e => e.MoRueda)
                .HasDefaultValue(0, "DF__SI_MODELO__MO_RU__41468CDC")
                .HasColumnName("MO_RUEDA");
            entity.Property(e => e.MoSoat).HasColumnName("MO_SOAT");
            entity.Property(e => e.MoTipo)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("MO_TIPO");
            entity.Property(e => e.MoTipoClase)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MO_TIPO_CLASE");
            entity.Property(e => e.MoTonela)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MO_TONELA");
            entity.Property(e => e.OrCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("OR_CODIGO");
            entity.Property(e => e.PaCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("PA_CODIGO");
            entity.Property(e => e.TiCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("TI_CODIGO");
            entity.Property(e => e.UsCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("US_CODIGO");
        });

        modelBuilder.Entity<SI_STICKER>(entity =>
        {
            entity.HasKey(e => e.StCodigo).HasFillFactor(80);

            entity.ToTable("SI_STICKER");

            entity.HasIndex(e => e.MoCodigo, "IX_MO_CODIGO").HasFillFactor(80);

            entity.HasIndex(e => e.AiCodigo, "IX_SI_STICKER").HasFillFactor(80);

            entity.HasIndex(e => e.StNumeChas, "IX_SI_STICKER_2").HasFillFactor(80);

            entity.HasIndex(e => e.StSticker, "st_sticker").HasFillFactor(80);

            entity.Property(e => e.StCodigo)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ST_CODIGO");
            entity.Property(e => e.AiCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("AI_CODIGO");
            entity.Property(e => e.AñCodigo).HasColumnName("AÑ_CODIGO");
            entity.Property(e => e.CoCodigo).HasColumnName("CO_CODIGO");
            entity.Property(e => e.MoCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("MO_CODIGO");
            entity.Property(e => e.OrCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("OR_CODIGO");
            entity.Property(e => e.StChevy)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ST_CHEVY");
            entity.Property(e => e.StDisco)
                .HasMaxLength(50)
                .HasColumnName("ST_DISCO");
            entity.Property(e => e.StFechMovi)
                .HasColumnType("datetime")
                .HasColumnName("ST_FECH_MOVI");
            entity.Property(e => e.StNumeChas)
                .HasMaxLength(50)
                .HasColumnName("ST_NUME_CHAS");
            entity.Property(e => e.StNumeMoto)
                .HasMaxLength(50)
                .HasColumnName("ST_NUME_MOTO");
            entity.Property(e => e.StPlaca)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ST_PLACA");
            entity.Property(e => e.StSticker)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ST_STICKER");
            entity.Property(e => e.UsCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("US_CODIGO");
        });


        modelBuilder.Entity<SI_ASIG_TURNO>(entity =>
        {
            entity.HasKey(e => e.AsgCodigo).HasFillFactor(80);

            entity.ToTable("SI_ASIG_TURNO");

            entity.Property(e => e.AsgCodigo)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ASG_CODIGO");
            entity.Property(e => e.AgCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("AG_CODIGO");
            entity.Property(e => e.AsgCita)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ASG_CITA");
            entity.Property(e => e.AsgEstado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ASG_ESTADO");
            entity.Property(e => e.AsgFechAsig)
                .HasColumnType("datetime")
                .HasColumnName("ASG_FECH_ASIG");
            entity.Property(e => e.AsgFechMovi)
                .HasColumnType("datetime")
                .HasColumnName("ASG_FECH_MOVI");
            entity.Property(e => e.AsgLlegada)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ASG_LLEGADA");
            entity.Property(e => e.AsgModulo)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ASG_MODULO");
            entity.Property(e => e.AsgTime)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ASG_TIME");
            entity.Property(e => e.AsgTimeEspe)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ASG_TIME_ESPE");
            entity.Property(e => e.CiCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("CI_CODIGO");
            entity.Property(e => e.ClCodiAseg)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("CL_CODI_ASEG");
            entity.Property(e => e.TuId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("TU_ID");
            entity.Property(e => e.UsCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("US_CODIGO");
        });

        modelBuilder.Entity<SI_TURNO>(entity =>
        {
            entity.HasKey(e => e.TuCodigo).HasFillFactor(80);

            entity.ToTable("SI_TURNO");

            entity.Property(e => e.TuCodigo)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("TU_CODIGO");
            entity.Property(e => e.AgCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("AG_CODIGO");
            entity.Property(e => e.TuCitaLato)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("TU_CITA_LATO");
            entity.Property(e => e.TuConCita)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("TU_CON_CITA");
            entity.Property(e => e.TuFlota)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("TU_FLOTA");
            entity.Property(e => e.TuSinCita)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("TU_SIN_CITA");
        });


        modelBuilder.Entity<SI_TURNERO_MEDIA>(entity =>
        {
            entity.HasKey(e => e.TmCodigo);

            entity.ToTable("SI_TURNERO_MEDIA");

            entity.Property(e => e.TmCodigo).HasColumnName("TM_CODIGO");
            entity.Property(e => e.AgCodigo).HasColumnName("AG_CODIGO");
            entity.Property(e => e.TmBucket)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TM_BUCKET");
            entity.Property(e => e.TmEstado)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("TM_ESTADO");
            entity.Property(e => e.TmFecha)
                .HasColumnType("datetime")
                .HasColumnName("TM_FECHA");
            entity.Property(e => e.TmModificacion)
                .HasColumnType("datetime")
                .HasColumnName("TM_MODIFICACION");
            entity.Property(e => e.TmObjId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TM_OBJ_ID");
            entity.Property(e => e.TmOrden).HasColumnName("TM_ORDEN");
            entity.Property(e => e.TmTipo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TM_TIPO");
            entity.Property(e => e.UsCodigo).HasColumnName("US_CODIGO");
        });


        modelBuilder.Entity<SEG_PARAMETRO_USUARIO>(entity =>
        {
            entity.HasKey(e => e.PuCodigo).HasFillFactor(90);

            entity.ToTable("SEG_PARAMETRO_USUARIO");

            entity.HasIndex(e => new { e.UsCodigo, e.AgCodigo }, "IX_SEG_PARAMETRO_USUARIO")
                .IsUnique()
                .HasFillFactor(90);

            entity.Property(e => e.PuCodigo)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("PU_CODIGO");
            entity.Property(e => e.AgCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("AG_CODIGO");
            entity.Property(e => e.PuAgenDefa)
                .HasComment("Agencia del usuario por defecto")
                .HasColumnName("PU_AGEN_DEFA");
            entity.Property(e => e.PuBahia)
                .HasMaxLength(100)
                .HasColumnName("PU_BAHIA");
            entity.Property(e => e.PuCaja)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("PU_CAJA");
            entity.Property(e => e.PuDatabase)
                .HasMaxLength(100)
                .HasColumnName("PU_DATABASE");
            entity.Property(e => e.PuModulo)
                .HasMaxLength(20)
                .HasColumnName("PU_MODULO");
            entity.Property(e => e.PuPassword)
                .HasMaxLength(100)
                .HasColumnName("PU_PASSWORD");
            entity.Property(e => e.PuTransito)
                .HasMaxLength(20)
                .HasColumnName("PU_TRANSITO");
            entity.Property(e => e.PuUbicgeo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("PU_UBICGEO");
            entity.Property(e => e.PuUsuario)
                .HasMaxLength(100)
                .HasColumnName("PU_USUARIO");
            entity.Property(e => e.UsCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("US_CODIGO");

            entity.HasOne(d => d.UsCodigoNavigation).WithMany(p => p.SegParametroUsuarios)
                .HasForeignKey(d => d.UsCodigo)
                .HasConstraintName("FK_SEG_PARAMETRO_USUARIO_seg_usuarios");
        });

        modelBuilder.Entity<SEG_USUARIO>(entity =>
        {
            entity.HasKey(e => e.UsCodigo).HasFillFactor(80);

            entity.ToTable("seg_usuarios");

            entity.HasIndex(e => e.UsRuc, "indBusquedaRUC");

            entity.Property(e => e.UsCodigo)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("us_codigo");
            entity.Property(e => e.CcCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("cc_codigo");
            entity.Property(e => e.GrCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("gr_codigo");
            entity.Property(e => e.UsAcc).HasColumnName("US_ACC");
            entity.Property(e => e.UsAgenPremium)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("US_AGEN_PREMIUM");
            entity.Property(e => e.UsAjusBloq).HasColumnName("us_ajus_bloq");
            entity.Property(e => e.UsBloqOrdeComp)
                .HasComment("Si el usuario tiene permiso de desbloquear ordenes de compra")
                .HasColumnName("us_bloq_orde_comp");
            entity.Property(e => e.UsCambIva)
                .HasDefaultValue(false, "DF_seg_usuarios_US_CAMB_IVA")
                .HasColumnName("US_CAMB_IVA");
            entity.Property(e => e.UsCobroDeduc).HasColumnName("us_cobro_deduc");
            entity.Property(e => e.UsCodiCrea)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("US_CODI_CREA");
            entity.Property(e => e.UsCodiModi)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("US_CODI_MODI");
            entity.Property(e => e.UsComp).HasColumnName("US_COMP");
            entity.Property(e => e.UsCompRepu)
                .HasDefaultValue((short)0, "DF_seg_usuarios_us_comp_repu")
                .HasColumnName("us_comp_repu");
            entity.Property(e => e.UsCondInte).HasColumnName("us_cond_inte");
            entity.Property(e => e.UsContAcci)
                .HasDefaultValue(0m, "DF_seg_usuarios_US_CONT_ACCI")
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("US_CONT_ACCI");
            entity.Property(e => e.UsDescAuto).HasColumnName("US_DESC_AUTO");
            entity.Property(e => e.UsDescMoi).HasColumnName("US_DESC_MOI");
            entity.Property(e => e.UsDescRep).HasColumnName("US_DESC_REP");
            entity.Property(e => e.UsDescTarjCred).HasColumnName("us_desc_tarj_cred");
            entity.Property(e => e.UsDescrip)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("us_descrip");
            entity.Property(e => e.UsDescuento)
                .HasDefaultValue(0m, "DF_seg_usuarios_us_descuento")
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("us_descuento");
            entity.Property(e => e.UsDevoAntiEfec).HasColumnName("us_devo_anti_efec");
            entity.Property(e => e.UsExepCaja).HasColumnName("US_EXEP_CAJA");
            entity.Property(e => e.UsExepcion).HasColumnName("us_exepcion");
            entity.Property(e => e.UsFactGaraGm).HasColumnName("US_FACT_GARA_GM");
            entity.Property(e => e.UsFactSinDeduc).HasColumnName("us_fact_sin_deduc");
            entity.Property(e => e.UsFechCrea)
                .HasColumnType("datetime")
                .HasColumnName("US_FECH_CREA");
            entity.Property(e => e.UsFechModi)
                .HasColumnType("datetime")
                .HasColumnName("US_FECH_MODI");
            entity.Property(e => e.UsFlota)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("US_FLOTA");
            entity.Property(e => e.UsImprHtPant).HasColumnName("us_impr_HT_pant");
            entity.Property(e => e.UsLinkId)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("us_link_id");
            entity.Property(e => e.UsLinkPass)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("us_link_pass");
            entity.Property(e => e.UsLinkReun)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("us_link_reun");
            entity.Property(e => e.UsLiquComp).HasColumnName("us_liqu_comp");
            entity.Property(e => e.UsLogin)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("us_login");
            entity.Property(e => e.UsMail)
                .HasMaxLength(100)
                .HasColumnName("us_mail");
            entity.Property(e => e.UsModiDiar).HasColumnName("US_MODI_DIAR");
            entity.Property(e => e.UsMontDesc).HasColumnName("US_MONT_DESC");
            entity.Property(e => e.UsNombre)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("us_nombre");
            entity.Property(e => e.UsPassword)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("us_password");
            entity.Property(e => e.UsPc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("US_PC");
            entity.Property(e => e.UsPermIntranet).HasColumnName("US_PERM_INTRANET");
            entity.Property(e => e.UsPermOtProv).HasColumnName("US_PERM_OT_PROV");
            entity.Property(e => e.UsPermSph).HasColumnName("US_PERM_SPH");
            entity.Property(e => e.UsPermTorre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("us_perm_torre");
            entity.Property(e => e.UsPermTranAuto).HasColumnName("us_perm_tran_auto");
            entity.Property(e => e.UsPorDescAuto).HasColumnName("US_POR_DESC_AUTO");
            entity.Property(e => e.UsPorDescMoi).HasColumnName("US_POR_DESC_MOI");
            entity.Property(e => e.UsPorDescRep).HasColumnName("US_POR_DESC_REP");
            entity.Property(e => e.UsPorcAjusPrec).HasColumnName("US_PORC_AJUS_PREC");
            entity.Property(e => e.UsPrecio).HasColumnName("US_PRECIO");
            entity.Property(e => e.UsReimpresion)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("US_REIMPRESION");
            entity.Property(e => e.UsRuc)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("US_RUC");
            entity.Property(e => e.UsSeguHoja)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("US_SEGU_HOJA");
            entity.Property(e => e.UsStatus)
                .HasComment("0:Activo |1:Cambio de Clave |2:Bloqueado |3:Inactivo")
                .HasDefaultValue((short)1, "DF_seg_usuarios_us_status")
                .HasColumnName("us_status");
            entity.Property(e => e.UsSupervisor).HasColumnName("US_SUPERVISOR");
            entity.Property(e => e.UsTasa).HasColumnName("US_TASA");
            entity.Property(e => e.UsTipoIden)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasDefaultValue("N", "DF__seg_usuar__us_ti__3136CE34")
                .HasColumnName("us_tipo_iden");
            entity.Property(e => e.UsTipoPrecio).HasColumnName("US_TIPO_PRECIO");
            entity.Property(e => e.UsTopCons)
                .HasDefaultValue((short)11, "DF_seg_usuarios_US_TOP_CONS")
                .HasColumnName("US_TOP_CONS");
            entity.Property(e => e.UsTranHc).HasColumnName("US_TRAN_HC");
        });


        modelBuilder.Entity<SI_DISP_VEND>(entity =>
        {
            entity.HasKey(e => e.DvCodigo).HasFillFactor(80);

            entity.ToTable("SI_DISP_VEND");

            entity.Property(e => e.DvCodigo)
                .ValueGeneratedOnAdd()
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("DV_CODIGO");
            entity.Property(e => e.AgCodigo)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("AG_CODIGO");
            entity.Property(e => e.DvBandAten).HasColumnName("DV_BAND_ATEN");
            entity.Property(e => e.DvEstado)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("DV_ESTADO");
            entity.Property(e => e.DvFechLogin)
                .HasColumnType("datetime")
                .HasColumnName("DV_FECH_LOGIN");
            entity.Property(e => e.DvFechUltiAten)
                .HasColumnType("datetime")
                .HasColumnName("DV_FECH_ULTI_ATEN");
            entity.Property(e => e.DvFecha)
                .HasColumnType("datetime")
                .HasColumnName("DV_FECHA");
            entity.Property(e => e.DvNumAten)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("DV_NUM_ATEN");
            entity.Property(e => e.DvPrioridad).HasColumnName("DV_PRIORIDAD");
            entity.Property(e => e.DvTipo).HasColumnName("DV_TIPO");
            entity.Property(e => e.GnCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("GN_CODIGO");
            entity.Property(e => e.TaCodigo)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("TA_CODIGO");
            entity.Property(e => e.TeCodigo)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("TE_CODIGO");
            entity.Property(e => e.UsCodiCrea)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("US_CODI_CREA");
            entity.Property(e => e.UsCodigo)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("US_CODIGO");
        });




        // ANTES DE ESTO DEBE PONER LOS NUEVOS MODELOES 

        OnModelCreatingPartial(modelBuilder);
    }



    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
