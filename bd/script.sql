USE [master]
GO
/****** Object:  Database [TAT004]    Script Date: 28/12/2018 06:29:38 p.m. ******/
CREATE DATABASE [TAT004]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TAT004', FILENAME = N'F:\SLQ2014\MSSQL12.MSSQLSERVER\MSSQL\DATA\TAT004.mdf' , SIZE = 588800KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'TAT004_log', FILENAME = N'F:\SLQ2014\MSSQL12.MSSQLSERVER\MSSQL\DATA\TAT004_log.ldf' , SIZE = 2160960KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TAT004].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TAT004] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TAT004] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TAT004] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TAT004] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TAT004] SET ARITHABORT OFF 
GO
ALTER DATABASE [TAT004] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TAT004] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [TAT004] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TAT004] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TAT004] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TAT004] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TAT004] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TAT004] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TAT004] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TAT004] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TAT004] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TAT004] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TAT004] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TAT004] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TAT004] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TAT004] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TAT004] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TAT004] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TAT004] SET RECOVERY FULL 
GO
ALTER DATABASE [TAT004] SET  MULTI_USER 
GO
ALTER DATABASE [TAT004] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TAT004] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TAT004] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TAT004] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'TAT004', N'ON'
GO
USE [TAT004]
GO
/****** Object:  UserDefinedTableType [dbo].[ClientesTableType]    Script Date: 28/12/2018 06:29:39 p.m. ******/
CREATE TYPE [dbo].[ClientesTableType] AS TABLE(
	[BUKRS] [nvarchar](250) NULL,
	[LAND] [nvarchar](250) NULL,
	[KUNNR] [nvarchar](250) NULL,
	[VKORG] [nvarchar](250) NULL,
	[VTWEG] [nvarchar](250) NULL,
	[SPART] [nvarchar](250) NULL,
	[CLIENTE_N] [nvarchar](250) NULL,
	[ID_US0] [nvarchar](250) NULL,
	[ID_US1] [nvarchar](250) NULL,
	[ID_US2] [nvarchar](250) NULL,
	[ID_US3] [nvarchar](250) NULL,
	[ID_US4] [nvarchar](250) NULL,
	[ID_US5] [nvarchar](250) NULL,
	[ID_US6] [nvarchar](250) NULL,
	[ID_US7] [nvarchar](250) NULL,
	[ID_PROVEEDOR] [nvarchar](250) NULL,
	[BANNER] [nvarchar](250) NULL,
	[BANNERG] [nvarchar](250) NULL,
	[CANAL] [nvarchar](250) NULL,
	[EXPORTACION] [nvarchar](250) NULL,
	[CONTACTO] [nvarchar](250) NULL,
	[CONTACTOE] [nvarchar](250) NULL,
	[MESS] [nvarchar](max) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[presupuestop]    Script Date: 28/12/2018 06:29:39 p.m. ******/
CREATE TYPE [dbo].[presupuestop] AS TABLE(
	[anio] [nchar](4) NULL,
	[pos] [int] NULL,
	[mes] [nchar](2) NULL,
	[version] [nvarchar](50) NULL,
	[pais] [nvarchar](15) NULL,
	[moneda] [nchar](2) NULL,
	[material] [nvarchar](18) NULL,
	[banner] [nchar](6) NULL,
	[concepto] [nvarchar](10) NULL,
	[data] [nvarchar](50) NULL
)
GO
/****** Object:  StoredProcedure [dbo].[CPS_LISTA_CITIES]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CPS_LISTA_CITIES] 
	-- Add the parameters for the stored procedure here
	@ESTADO      NCHAR(30)     = NULL,
	@PREFIX      NVARCHAR(MAX) =''
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM  [CITIES]
	WHERE NAME LIKE '%'+@PREFIX+'%' AND  [STATE_ID]   = (SELECT ID  FROM [STATES] WHERE NAME=  @ESTADO)
END

GO
/****** Object:  StoredProcedure [dbo].[CPS_LISTA_CLI_PRO]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CPS_LISTA_CLI_PRO] 
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT C.KUNNR, C.NAME1, C.PROVEEDOR_ID, P.NOMBRE, I.LANDX, C.VKORG, (C.CANAL + '- ' + A.CDESCRIPCION) AS CANAL, C.CONTAC, C.CONT_EMAIL 
	FROM CLIENTE AS C 
	LEFT JOIN PROVEEDOR AS P 
	ON P.ID = C.PROVEEDOR_ID 
	INNER JOIN PAIS AS I 
	ON I.LAND = C.LAND
	INNER JOIN CANAL AS A
	ON A.CANAL = C.CANAL;
END


GO
/****** Object:  StoredProcedure [dbo].[CPS_LISTA_CLIENTES]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CPS_LISTA_CLIENTES]
	@USUARIO_ID NVARCHAR(16) = NULL,
	@PAIS       NVARCHAR(5) = NULL,
	@PREFIX     NVARCHAR(MAX) =''
	

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	IF @USUARIO_ID IS NULL
	BEGIN
		SELECT TOP 100 * FROM CLIENTE 
		WHERE (KUNNR LIKE '%'+@PREFIX+'%' OR  ISNULL(NAME1,'') LIKE '%'+@PREFIX+'%') 
		AND (LAND=@PAIS OR @PAIS IS NULL) AND ACTIVO=1;
	END
	ELSE
	BEGIN
		SELECT c.* FROM CLIENTE c INNER JOIN USUARIOF u 
		ON c.[VKORG] =u.[VKORG] AND c.[VTWEG] = u.[VTWEG] AND c.[SPART]=u.[SPART] AND c.[KUNNR]=u.[KUNNR]
		INNER JOIN CLIENTEF as cf 
		ON c.[VKORG] = cf.[VKORG] AND c.[VTWEG] = cf.[VTWEG] AND c.[SPART]= cf.[SPART] AND c.[KUNNR]= cf.[KUNNR] --ADD RSG 16.11.2018
		WHERE (c.KUNNR LIKE '%'+@PREFIX+'%' OR  ISNULL(NAME1,'') LIKE '%'+@PREFIX+'%') 
		AND (c.LAND=@PAIS OR @PAIS IS NULL)
		AND u.USUARIO_ID=@USUARIO_ID AND c.ACTIVO=1
		AND cf.ACTIVO = 1;  --ADD RSG 16.11.2018
	END 
END

GO
/****** Object:  StoredProcedure [dbo].[CPS_LISTA_CONTACTOS]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CPS_LISTA_CONTACTOS]
    @KUNNR    NVARCHAR(10),
	@VKORG    NVARCHAR(4) = NULL,
	@VTWEG    NVARCHAR(2) = NULL,
	@PREFIX   NVARCHAR(MAX) = ''
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

		SELECT 
		MAX(ID) AS ID,
		NOMBRE,
		PHONE,
		EMAIL,
		VKORG,
		VTWEG,
		SPART,
		KUNNR,
		ACTIVO,
		DEFECTO,
		CARTA 
		FROM [dbo].[CONTACTOC]
		WHERE (NOMBRE LIKE '%'+@PREFIX+'%' OR  EMAIL LIKE '%'+@PREFIX+'%') 
     	AND VKORG=@VKORG AND KUNNR=@KUNNR AND VTWEG=@VTWEG 
		AND ACTIVO=1
       GROUP BY NOMBRE,PHONE,EMAIL,VKORG,VTWEG,SPaRT,KUNNR,ACTIVO,DEFECTO,CARTA ;
	
END
GO
/****** Object:  StoredProcedure [dbo].[CPS_LISTA_DOCUMENTOP]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CPS_LISTA_DOCUMENTOP] 
	@NUM_DOC       DECIMAL(18,0),
	@SPRAS_ID      NVARCHAR(2),
	@VIGENCIA_DE   DATE,
	@VIGENCIA_AL   DATE

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT 
	dp.NUM_DOC,
	dp.MATNR,
	ISNULL((SELECT DESCRIPCION FROM [dbo].[MATERIALGP] mg WHERE mg.ID=m.[MATERIALGP_ID]),'') AS DESCRIPCION,
	ISNULL(mt.[MAKTX],m.[MAKTX]) AS [MAKTX],
	dp.MONTO,
	m.PUNIT,
	dp.PORC_APOYO,
	dp.MONTO_APOYO,
	(dp.MONTO-dp.MONTO_APOYO) AS RESTA,
	dp.PRECIO_SUG,
	dp.APOYO_EST,
	dp.APOYO_REAL,
	dp.VOLUMEN_EST,
	dp.VOLUMEN_REAL,
	dp.VIGENCIA_DE,
	dp.VIGENCIA_AL
	
	 FROM [MATERIAL] m 
	 LEFT JOIN [MATERIALT] mt ON m.ID=mt.MATERIAL_ID AND mt.SPRAS=@SPRAS_ID
	 INNER JOIN [DOCUMENTOP] dp ON dp.NUM_DOC=@NUM_DOC AND dp.MATNR=m.ID AND dp.VIGENCIA_DE=@VIGENCIA_DE AND dp.VIGENCIA_AL=@VIGENCIA_AL
END

GO
/****** Object:  StoredProcedure [dbo].[CPS_LISTA_MATERIALES]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CPS_LISTA_MATERIALES]
    @SPRAS_ID       NVARCHAR(2),
	@VKORG          NVARCHAR(4),
	@VTWEG          NVARCHAR(2),
	@PREFIX         NVARCHAR(MAX) = NULL 
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	   
		SELECT TOP 100 m.[ID],m.[MTART] ,m.[MATKL_ID]
		,ISNULL(mt.[MAKTX],m.[MAKTX]) AS [MAKTX]
		,ISNULL(mt.[MAKTG],m.[MAKTG]) AS [MAKTG]
		,m.[MEINS],m.[PUNIT],m.[ACTIVO] ,m.[CTGR],m.[BRAND],m.[MATERIALGP_ID]
	    FROM [dbo].[MATERIAL] m 
		LEFT JOIN [dbo].[MATERIALT] mt 	ON m.ID=mt.MATERIAL_ID AND mt.SPRAS=@SPRAS_ID
		INNER JOIN MATERIALVKE mk ON m.ID=mk.MATERIAL_ID AND VKORG=@VKORG AND VTWEG=@VTWEG	
		WHERE  mk.ACTIVO=1 AND m.ACTIVO=1 AND [MATERIALGP_ID] IS NOT NULL
		AND (m.ID LIKE '%'+@PREFIX+'%' OR  ISNULL(m.[MAKTX],'') LIKE '%'+@PREFIX+'%' OR  ISNULL(m.[MAKTG],'') LIKE '%'+@PREFIX+'%') 
	    GROUP BY m.[ID],m.[MTART] ,m.[MAKTX],mt.[MAKTX],m.[MAKTG],mt.[MAKTG],m.[MATKL_ID],m.[MEINS],m.[PUNIT],m.[ACTIVO] ,m.[CTGR],m.[BRAND],m.[MATERIALGP_ID]
		
	
END

GO
/****** Object:  StoredProcedure [dbo].[CPS_LISTA_MATERIALGP_CLIENTE]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CPS_LISTA_MATERIALGP_CLIENTE]
    @SOCIEDAD_ID NVARCHAR(4),
	@VKORG       NVARCHAR(4) ,
	@SPART       NVARCHAR(2) ,
	@KUNNR       NVARCHAR(10) ,
	@aii         INT = NULL ,
	@mii          INT = NULL ,
	@aff          INT = NULL ,
	@mff          INT = NULL 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @CAMPO NVARCHAR(20)=NULL;
	DECLARE @ANIO_PERIODO_I INT;
	DECLARE @ANIO_PERIODO_F INT;

	SELECT @CAMPO=CAMPO FROM CONFDIST_CAT WHERE SOCIEDAD_ID=@SOCIEDAD_ID;
	SET @ANIO_PERIODO_I=CAST(CAST(@aii AS NVARCHAR(4))+RIGHT(CONCAT('00',CAST(@mii AS NVARCHAR(4))),2) AS INT);
    SET @ANIO_PERIODO_F=CAST(CAST(@aff AS NVARCHAR(4))+RIGHT(CONCAT('00',CAST(@mff AS NVARCHAR(4))),2) AS INT);

	
	IF @CAMPO IS NOT NULL
	BEGIN
	    SELECT *  INTO #tempP 
		FROM (SELECT *,CAST((CAST(ANIO AS NVARCHAR(4))+RIGHT(CONCAT('00',CAST(PERIOD AS NVARCHAR(4))),2))AS INT) AS ANIO_PERIODO
		FROM PRESUPSAPP 
		WHERE VKORG=@VKORG AND SPART=@SPART AND (KUNNR=@KUNNR  OR KUNNR_PAY=@KUNNR) AND (GRSLS IS NOT NULL OR NETLB IS NOT NULL)) AS p
		WHERE p.ANIO_PERIODO>=@ANIO_PERIODO_I AND p.ANIO_PERIODO<=@ANIO_PERIODO_F;
			
		SELECT * INTO #tempM FROM MATERIAL   WHERE ACTIVO=1 AND [MATERIALGP_ID] IS NOT NULL;

		IF @CAMPO='GRSLS'
		BEGIN
			SELECT mg.ID AS MATERIALGP_ID,mg.DESCRIPCION AS TXT50,'EN' AS SPRAS_ID FROM #tempP p 
			INNER JOIN #tempM m ON p.MATNR=m.ID 
			INNER JOIN MATERIALGP mg ON m.MATERIALGP_ID=mg.ID
			WHERE p.BUKRS=@SOCIEDAD_ID AND p.GRSLS>0			
			GROUP BY mg.ID ,mg.DESCRIPCION;
		END
		ELSE
		BEGIN
			SELECT mg.ID AS MATERIALGP_ID,mg.DESCRIPCION AS TXT50,'EN' AS SPRAS_ID FROM #tempP p 
			INNER JOIN #tempM m ON p.MATNR=m.ID 
			INNER JOIN MATERIALGP mg ON m.MATERIALGP_ID=mg.ID
			WHERE p.BUKRS=@SOCIEDAD_ID AND p.NETLB>0			
			GROUP BY mg.ID ,mg.DESCRIPCION;
		END

		DROP TABLE #tempP;
		DROP TABLE #tempM;
	END
 
END



                  
GO
/****** Object:  StoredProcedure [dbo].[CPS_LISTA_MATERIALGP_MATERIALES]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CPS_LISTA_MATERIALGP_MATERIALES]
    @SOCIEDAD_ID NVARCHAR(4),
	@VKORG       NVARCHAR(4) ,
	@SPART       NVARCHAR(2) ,
	@KUNNR       NVARCHAR(10) ,
	@SPRAS_ID    NVARCHAR(2),
	@aii         INT = NULL ,
	@mii          INT = NULL ,
	@aff          INT = NULL ,
	@mff          INT = NULL 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @CAMPO NVARCHAR(20)=NULL;
	DECLARE @ANIO_PERIODO_I INT;
	DECLARE @ANIO_PERIODO_F INT;

	SELECT @CAMPO=CAMPO FROM CONFDIST_CAT WHERE SOCIEDAD_ID=@SOCIEDAD_ID;
	SET @ANIO_PERIODO_I=CAST(CAST(@aii AS NVARCHAR(4))+RIGHT(CONCAT('00',CAST(@mii AS NVARCHAR(4))),2) AS INT);
    SET @ANIO_PERIODO_F=CAST(CAST(@aff AS NVARCHAR(4))+RIGHT(CONCAT('00',CAST(@mff AS NVARCHAR(4))),2) AS INT);
	
	IF @CAMPO IS NOT NULL
	BEGIN
		SELECT *  INTO #tempP 
		FROM (SELECT *,CAST((CAST(ANIO AS NVARCHAR(4))+RIGHT(CONCAT('00',CAST(PERIOD AS NVARCHAR(4))),2))AS INT) AS ANIO_PERIODO
		FROM PRESUPSAPP 
		WHERE VKORG=@VKORG AND SPART=@SPART AND (KUNNR=@KUNNR  OR KUNNR_PAY=@KUNNR) AND (GRSLS IS NOT NULL OR NETLB IS NOT NULL)) AS p
		WHERE p.ANIO_PERIODO>=@ANIO_PERIODO_I AND p.ANIO_PERIODO<=@ANIO_PERIODO_F;
		
		SELECT * INTO #tempM FROM MATERIAL   WHERE ACTIVO=1 AND [MATERIALGP_ID] IS NOT NULL;

		IF @CAMPO='GRSLS'
		BEGIN
			SELECT mg.ID AS ID_CAT, m.ID AS MATNR ,p.GRSLS AS VAL,mg.EXCLUIR ,
			ISNULL((SELECT MAKTX FROM MATERIALT mt WHERE mt.MATERIAL_ID=m.ID AND mt.SPRAS=@SPRAS_ID),m.MAKTX)AS [DESC] 
			--0 AS POR 
			FROM #tempP p 
			INNER JOIN #tempM m ON p.MATNR=m.ID 
			INNER JOIN MATERIALGP mg ON m.MATERIALGP_ID=mg.ID
			WHERE p.BUKRS=@SOCIEDAD_ID AND p.GRSLS>0;
		END
		ELSE
		BEGIN
			SELECT mg.ID AS ID_CAT, m.ID AS MATNR ,p.GRSLS AS VAL, mg.EXCLUIR ,
			ISNULL((SELECT MAKTX FROM MATERIALT mt WHERE mt.MATERIAL_ID=m.ID AND mt.SPRAS=@SPRAS_ID),m.MAKTX)AS [DESC] 
			--0 AS POR  
			FROM #tempP p 
			INNER JOIN #tempM m ON p.MATNR=m.ID 
			INNER JOIN MATERIALGP mg ON m.MATERIALGP_ID=mg.ID
			WHERE p.BUKRS=@SOCIEDAD_ID AND p.NETLB>0;
			
		END

		DROP TABLE #tempP;
		DROP TABLE #tempM;
	END
 
END



                  
GO
/****** Object:  StoredProcedure [dbo].[CPS_LISTA_SOCIEDADES]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CPS_LISTA_SOCIEDADES]
	-- Add the parameters for the stored procedure here
	@ACCION       INT            = 0,
	@PREFIX       NVARCHAR(MAX)  = '',
	@SOCIEDAD_ID  NCHAR(4)       = NULL,
	@USUARIO_ID   NVARCHAR(MAX)  = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
  
  IF @ACCION =1 --ACCION_LISTA_SOCIEDADES
  BEGIN
	  SELECT * FROM SOCIEDAD 
	  WHERE ACTIVO=1 
	  AND ([BUKRS] LIKE '%'+@PREFIX+'%' OR ISNULL([BUTXT],'') LIKE '%'+@PREFIX+'%')
	  AND (@SOCIEDAD_ID IS NULL OR [BUKRS]=@SOCIEDAD_ID);
  END

   
  IF @ACCION =2 --ACCION_LISTA_SOCPORUSUARIO
  BEGIN
	  SELECT * FROM SOCIEDAD 
	  WHERE ACTIVO=1 
	  AND [BUKRS] IN (SELECT [SOCIEDAD_ID] fROM [dbo].[USUARIOSOC]  WHERE [USUARIO_ID]=@USUARIO_ID);
  END

END

GO
/****** Object:  StoredProcedure [dbo].[CPS_LISTA_SOLICITUDES]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CPS_LISTA_SOLICITUDES]
@ACCION     INT           = 0,
@PREFIX     NVARCHAR(MAX) ='',
@NUM_DOCI   DECIMAL(18,0)=NULL,
@NUM_DOCF   DECIMAL(18,0)=NULL,
@FECHAI     DATE = NULL,
@FECHAF     DATE = NULL,
@KUNNR      NVARCHAR(10) =NULL,
@USUARIO_ID NVARCHAR(16)=NULL,
@SOCIEDAD_ID NVARCHAR(4)=NULL
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   IF @ACCION=1 --ACCION_LISTA_SOLICITUDES
	BEGIN
	  SELECT *  FROM DOCUMENTO 
	  WHERE  NUM_DOC LIKE '%'+@PREFIX+'%' AND ESTATUS_C IS NULL AND ESTATUS_SAP IS NULL
	  AND (@NUM_DOCI IS NULL OR NUM_DOC>= @NUM_DOCI)
	  AND (@NUM_DOCF IS NULL OR NUM_DOC<=@NUM_DOCF)
	  AND (@FECHAI IS NULL OR FECHAC>=@FECHAI)
	  AND (@FECHAF IS NULL OR FECHAC<=@FECHAF)
	  AND (@KUNNR IS NULL OR PAYER_ID=@KUNNR)
	  AND (@USUARIO_ID IS NULL OR USUARIOD_ID=@USUARIO_ID )
	  AND (@SOCIEDAD_ID IS NULL OR SOCIEDAD_ID=@SOCIEDAD_ID );
	END
	IF @ACCION=2 --ACCION_LISTA_SOLICITUDES_POR_APROBADOR
	BEGIN
	 SELECT d.* FROM DOCUMENTO d
	  INNER JOIN  FLUJO f 
	  ON d.NUM_DOC=f.NUM_DOC AND f.POS=(SELECT MAX(POS) FROM FLUJO f1 WHERE d.NUM_DOC=f1.NUM_DOC  ) AND f.[ESTATUS]='P' AND f.USUARIOA_ID=@USUARIO_ID AND d.ESTATUS_WF ='P' AND d.ESTATUS_C IS NULL 
	 WHERE d.NUM_DOC LIKE '%'+@PREFIX+'%' 
	 AND (@NUM_DOCI IS NULL OR d.NUM_DOC>= @NUM_DOCI)
	 AND (@NUM_DOCF IS NULL OR d.NUM_DOC<=@NUM_DOCF)
	  AND (@SOCIEDAD_ID IS NULL OR d.SOCIEDAD_ID=@SOCIEDAD_ID );
	END
END

GO
/****** Object:  StoredProcedure [dbo].[CPS_LISTA_SOLICITUDES_POR_APROBAR]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CPS_LISTA_SOLICITUDES_POR_APROBAR]
@NUM_DOCI   DECIMAL(18,0)=NULL,
@NUM_DOCF   DECIMAL(18,0)=NULL,
@FECHAI     DATE = NULL,
@FECHAF     DATE = NULL,
@KUNNR      NVARCHAR(10) =NULL,
@USUARIOA_ID NVARCHAR(16) =NULL,
@SOCIEDAD_ID NVARCHAR(4)=NULL,
@USUARIO_ID NVARCHAR(16) =NULL
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	  SELECT 
	  d.NUM_DOC,
	  f.USUARIOA_ID,
	   d.USUARIOD_ID,
	  (SELECT [NOMBRE]+' '+[APELLIDO_P]+' '+ISNULL([APELLIDO_M],'') FROM USUARIO u WHERE u.ID=f.USUARIOA_ID) AS USUARIOA_NOMBRE,
	  (SELECT [NOMBRE]+' '+[APELLIDO_P]+' '+ISNULL([APELLIDO_M],'') FROM USUARIO u WHERE u.ID=d.USUARIOD_ID) AS USUARIOD_NOMBRE,
	  NULL AS USUARIOA_ID_NUEVO
	  FROM DOCUMENTO d
	  INNER JOIN  FLUJO f ON d.NUM_DOC=f.NUM_DOC AND f.POS=(SELECT MAX(POS) FROM FLUJO f1 WHERE d.NUM_DOC=f1.NUM_DOC  ) AND f.ESTATUS='P'
	  WHERE d.ESTATUS_WF ='P' AND d.ESTATUS_C IS NULL
	  AND (@NUM_DOCI IS NULL OR d.NUM_DOC>= @NUM_DOCI)
	  AND (@NUM_DOCF IS NULL OR d.NUM_DOC<=@NUM_DOCF)
	  AND (@FECHAI IS NULL OR d.FECHAC>=@FECHAI)
	  AND (@FECHAF IS NULL OR d.FECHAC<=@FECHAF)
	  AND (@KUNNR IS NULL OR d.PAYER_ID=@KUNNR)
	  AND (@USUARIOA_ID IS NULL OR f.USUARIOA_ID=@USUARIOA_ID )
	  AND (@USUARIO_ID IS NULL OR d.USUARIOD_ID=@USUARIO_ID )
	  AND (@SOCIEDAD_ID IS NULL OR SOCIEDAD_ID=@SOCIEDAD_ID );
END

GO
/****** Object:  StoredProcedure [dbo].[CPS_LISTA_SPRAS]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CPS_LISTA_SPRAS] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT * FROM SPRAS WHERE ID<>'PT';
END

GO
/****** Object:  StoredProcedure [dbo].[CPS_LISTA_STATES]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CPS_LISTA_STATES] 
	-- Add the parameters for the stored procedure here
	@PAIS        NCHAR(2)      = NULL,
	@PREFIX      NVARCHAR(MAX) =''
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [STATES] 
	WHERE NAME LIKE '%'+@PREFIX+'%' AND COUNTRY_ID    = (SELECT ID  FROM [COUNTRIES] WHERE SORTNAME=  @PAIS)
END

GO
/****** Object:  StoredProcedure [dbo].[CPS_LISTA_USUARIOS]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CPS_LISTA_USUARIOS]
@ACCION       INT           = 0,
@PREFIX       NVARCHAR(MAX) = '',
@ID           NVARCHAR(16)  = NULL,
@SOCIEDAD_ID  NVARCHAR(4)   = NULL
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF @ACCION=1 --ACCION_LISTA_USUARIO
	BEGIN
		SELECT *  FROM USUARIO 
		WHERE  (ID LIKE '%'+@PREFIX+'%' OR [NOMBRE] LIKE '%'+@PREFIX+'%' OR [APELLIDO_P] LIKE '%'+@PREFIX+'%' )
		AND ACTIVO = 1 
		AND (@ID IS NULL OR ID=@ID)
		AND (@SOCIEDAD_ID IS NULL OR BUNIT=@SOCIEDAD_ID);
	END
	IF @ACCION=2 --ACCION_LISTA_AUTORIZADOR (FLUJO) 
	BEGIN
		SELECT DISTINCT u.*  FROM USUARIO u 
		INNER JOIN FLUJO f  ON  u.ID=f.USUARIOA_ID
		INNER JOIN DOCUMENTO d ON d.NUM_DOC=f.NUM_DOC AND f.POS=(SELECT MAX(POS) FROM FLUJO f1 WHERE d.NUM_DOC=f1.NUM_DOC  ) AND f.ESTATUS='P'AND d.ESTATUS_WF ='P' AND d.ESTATUS_C IS NULL
	    WHERE (u.ID LIKE '%'+@PREFIX+'%' OR u.[NOMBRE] LIKE '%'+@PREFIX+'%' OR u.[APELLIDO_P] LIKE '%'+@PREFIX+'%') 
		AND (@SOCIEDAD_ID IS NULL OR u.BUNIT=@SOCIEDAD_ID);
	END
	IF @ACCION=3 --ACCION_LISTA_AUTORIZADOR (USUARIOS)
	BEGIN
		SELECT *  FROM USUARIO 
		WHERE  (ID LIKE '%'+@PREFIX+'%' OR [NOMBRE] LIKE '%'+@PREFIX+'%' OR [APELLIDO_P] LIKE '%'+@PREFIX+'%') AND ACTIVO = 1 
		AND PUESTO_ID<>1 AND PUESTO_ID<>2 AND PUESTO_ID<>14 
		AND (@SOCIEDAD_ID IS NULL OR BUNIT=@SOCIEDAD_ID);
	END

END

GO
/****** Object:  StoredProcedure [dbo].[CSP_BANNERSINCANAL]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CSP_BANNERSINCANAL]
@sociedad nvarchar(max)
AS
DECLARE @BANERS TABLE
(
			BANNER  VARCHAR(10)
          , SOCIEDAD VARCHAR(4)
)
INSERT INTO @BANERS 
SELECT PRESUPUESTOP.BANNER, r.SOCIEDAD FROM PRESUPUESTOP 
LEFT JOIN CLIENTE ON PRESUPUESTOP.BANNER = CLIENTE.BANNER --AND PRESUPUESTOP.BANNER = CLIENTE.BANNERG
INNER JOIN REGION AS r ON r.REGION = PRESUPUESTOP.PAIS
WHERE CLIENTE.CANAL IS NULL AND r.SOCIEDAD IN (SELECT val FROM dbo.split(@sociedad,',') WHERE val != '')
GROUP BY PRESUPUESTOP.BANNER, r.SOCIEDAD order by PRESUPUESTOP.BANNER, r.SOCIEDAD asc

--SELECT * FROM @BANERS GROUP BY BANNER, SOCIEDAD order by BANNER, SOCIEDAD asc

DELETE FROM @BANERS WHERE BANNER IN (SELECT BANNERG FROM CLIENTE GROUP BY BANNERG) 

SELECT * FROM @BANERS GROUP BY BANNER, SOCIEDAD order by BANNER, SOCIEDAD asc




GO
/****** Object:  StoredProcedure [dbo].[CSP_CAMBIO]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[CSP_CAMBIO]
@sociedad nvarchar(4)
AS
begin
select T.FCURR, T.TCURR from TCAMBIO as T inner join SOCIEDAD as S on t.FCURR = S.WAERS where S.BUKRS = @sociedad AND T.GDATU = CONVERT(date, GETDATE())
end


GO
/****** Object:  StoredProcedure [dbo].[CSP_CARPETA]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<ROGELIO SÁNCHEZ>
-- Create date: <22-02-2018>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CSP_CARPETA]
	-- Add the parameters for the stored procedure here
	@ID		nvarchar(16),
	@ACCION int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF @ACCION = 1
	BEGIN
		--INSERT USUARIO( ID, PASS, NOMBRE, APELLIDO_P, APELLIDO_M, EMAIL, ACTIVO)
		--VALUES ( @ID, @PASS, @NOMBRE, @APELLIDO_P, @APELLIDO_M, @EMAIL, 'TRUE')
		SELECT C.ID AS CAR_ID, C.URL AS CAR_URL, CT.TXT50 AS CAR_TIT, C.ICON AS ICONO
		FROM USUARIO AS U
		INNER JOIN MIEMBROS AS M
		ON U.ID = M.USUARIO_ID
		INNER JOIN PERMISO_PAGINA AS PP
		ON M.ROL_ID = PP.ROL_ID
		INNER JOIN PAGINA AS P
		ON PP.PAGINA_ID = P.ID
		INNER JOIN CARPETA AS C
		ON p.CARPETA_ID = C.ID
		INNER JOIN CARPETAT AS CT
		ON C.ID = CT.ID
		WHERE U.ID = @ID
		AND PP.PERMISO = 1
		AND CT.SPRAS_ID = U.SPRAS_ID
		group by C.ID, C.URL,  CT.TXT50, C.ICON
	END
	IF @ACCION = 2
	BEGIN 
		SELECT C.ID AS CAR_ID, C.URL AS CAR_URL, CT.TXT50 AS CAR_TIT, C.ICON AS ICONO
		FROM USUARIO AS U
		INNER JOIN MIEMBROS AS M
		ON U.ID = M.USUARIO_ID
		INNER JOIN PERMISO_PAGINA AS PP
		ON M.ROL_ID = PP.ROL_ID
		INNER JOIN PAGINA AS P
		ON PP.PAGINA_ID = P.ID
		INNER JOIN CARPETA AS C
		ON p.CARPETA_ID = C.ID
		INNER JOIN CARPETAT AS CT
		ON C.ID = CT.ID
		WHERE U.ID = @ID
		AND PP.PERMISO = 1
		AND CT.SPRAS_ID = U.SPRAS_ID
		group by C.ID, C.URL,  CT.TXT50, C.ICON
	END
END



GO
/****** Object:  StoredProcedure [dbo].[CSP_CONSULTARPRESUPUESTO]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CSP_CONSULTARPRESUPUESTO]
    @SOCIEDAD NVARCHAR(max)
    ,
    @ANIOC NVARCHAR(2)
    ,
    @ANIOS NVARCHAR(4)
    ,
    @PERIODOC NVARCHAR(3)
    ,
    @PERIODOS NVARCHAR(2)
    ,
    @MONEDAD NVARCHAR(4)
    ,
    @MONEDAA NVARCHAR(4)
    ,
    @CPT NCHAR
    (
        1
    )
AS
    DECLARE @PRESUPUESTOSAP TABLE
        (
            BANNER  VARCHAR(10)
          , PERIODO VARCHAR(3)
          ,
             --GRSLS FLOAT,
             --RECSL FLOAT,
             --INDLB FLOAT,
             --FRGHT FLOAT,
             --PURCH FLOAT,
             --RAWMT FLOAT,
             --PKGMT FLOAT,
             --OVHDV FLOAT,
             --OVHDF FLOAT,
             --DIRLB FLOAT,
             CSHDC FLOAT
          , RECUN  FLOAT
          , OTHTA  FLOAT
          , SPA    FLOAT
          , FREEG  FLOAT
          ,
             --PKGDS FLOAT,
             CONPR FLOAT
          , RSRDV  FLOAT
          , CORPM  FLOAT
          , POP    FLOAT
          , PMVAR  FLOAT
          , ADVER  FLOAT
          ,
             --NETLB FLOAT,
             --SLLBS FLOAT,
             --SLCAS FLOAT,
             --PRCAS FLOAT,
             --NPCAS FLOAT,
             DSTRB FLOAT
          ,
             --ILVAR FLOAT,
             --BILBK FLOAT,
             --OVHVV FLOAT,
             VVX17 FLOAT
          , OHV    FLOAT
            --CONSU FLOAT
        )
	DECLARE @PRESUPUESTOSAP2 TABLE
        (
            BANNER  VARCHAR(10)
          , PERIODO VARCHAR(3)
          , CSHDC FLOAT
          , RECUN  FLOAT
          , OTHTA  FLOAT
          , SPA    FLOAT
          , FREEG  FLOAT
          , CONPR FLOAT
          , RSRDV  FLOAT
          , CORPM  FLOAT
          , POP    FLOAT
          , PMVAR  FLOAT
          , ADVER  FLOAT
          , DSTRB FLOAT
          , VVX17 FLOAT
          , OHV    FLOAT
        )
    DECLARE @PRESUPUESTOCPT TABLE
        (
            BANNER  VARCHAR(10)
          , PERIODO VARCHAR(3)
          , PPTO    FLOAT
        )
    DECLARE @PRESUPUESTOCPT2 TABLE
        (
            BANNER  VARCHAR(10)
          , PERIODO VARCHAR(3)
          , CSHDC   FLOAT
          , RECUN   FLOAT
          , OTHTA   FLOAT
          , SPA     FLOAT
          , FREEG   FLOAT
          , CONPR   FLOAT
          , RSRDV   FLOAT
          , CORPM   FLOAT
          , POP     FLOAT
          , PMVAR   FLOAT
          , ADVER   FLOAT
          , DSTRB   FLOAT
          , VVX17   FLOAT
          , PPTOC   FLOAT
        )
    DECLARE @PRESUPUESTOCPTCANAL TABLE
        (
            CANAL        VARCHAR(10)
          , CDESCRIPCION VARCHAR(50)
          , BANNER       VARCHAR(10)
          , BDESCRIPCION VARCHAR(50)
          , PPTOC        FLOAT
        )
    DECLARE @CPTCANAL TABLE
        (
            CANAL VARCHAR(10)
          , PPTOC FLOAT
        )
    DECLARE @TSOCIEDAD TABLE
        (
            SOCIEDAD VARCHAR(15)
        )
    DECLARE @TPROCETAT TABLE
        (
            BANNER  VARCHAR(10)
          , PERIODO VARCHAR(3)
          , TOTAL   FLOAT
        )
	DECLARE @TPROCETAT2 TABLE
        (
            BANNER  VARCHAR(10)
          , PERIODO VARCHAR(3)
          , TOTAL   FLOAT
        )
    DECLARE @PRESUSAPB TABLE
        (
            BANNER  VARCHAR(10)
          , PERIODO VARCHAR(3)
          , TOTAL   FLOAT
        )
    DECLARE @PRESUSAPF TABLE
        (
            BANNER  VARCHAR(10)
          , PERIODO VARCHAR(3)
          , TOTAL   FLOAT
        )
    DECLARE @CONSUMO TABLE
        (
            BANNER  VARCHAR(10)
          , PERIODO VARCHAR(3)
          , TOTAL1  FLOAT
        )
    DECLARE @REGION TABLE
        (
            REGION nvarchar(6)
        )
        DECLARE @cambio decimal(9,5)
            , @cero     float
            , @usuario  nvarchar(50)
        SELECT
            @cambio = 0
        SELECT
            @cero = 0 BEGIN
        --IF @SOCIEDAD != ''
        --BEGIN
        -- INSERT INTO @TSOCIEDAD SELECT VAL FROM SPLIT(@SOCIEDAD,',')
        -- SELECT * FROM @TSOCIEDAD
        --END
        INSERT INTO @REGION
        SELECT
            REGION
        FROM
            REGION
        WHERE
            SOCIEDAD IN (SELECT val FROM dbo.split(@sociedad,',') WHERE val != '')
        ;
        
        SELECT
            TOP 1 @usuario = IDUSUARIO
        FROM
            USUARIOSAP
        WHERE
            AUTOMATICO = 1
        IF @MONEDAA   != ''
        AND
        @MONEDAD != ''
        BEGIN
            SELECT
                @cambio = UKURS
            FROM
                TCAMBIO
            WHERE
                FCURR     = @MONEDAD
                AND TCURR = @MONEDAA
                AND GDATU = CONVERT(DATE, GETDATE())
            ;
            
            IF @cambio = 0
            BEGIN
                SELECT
                    @cambio = 1
            END
        END
        ELSE
        BEGIN
            SELECT
                @cambio = 1
        END
        IF @CPT != ''
        BEGIN
            IF @PERIODOC != ''
            BEGIN
                INSERT INTO @PRESUPUESTOCPT2
                SELECT
                    C.BANNER
                  , C.MES
                  ,((SUM(C.CSHDC) / @cambio) / 1000)                                                                                                       AS CSHDC
                  ,((SUM(C.RECUN) / @cambio) / 1000)                                                                                                       AS RECUN
                  ,((SUM(C.OTHTA) / @cambio) / 1000)                                                                                                       AS OTHTA
                  ,((SUM(C.SPA)   / @cambio) / 1000)                                                                                                       AS SPA
                  ,((SUM(C.FREEG) / @cambio) / 1000)                                                                                                       AS FREEG
                  ,((SUM(C.CONPR) / @cambio) / 1000)                                                                                                       AS CONPR
                  ,((SUM(C.RSRDV) / @cambio) / 1000)                                                                                                       AS RSRDV
                  ,((SUM(C.CORPM) / @cambio) / 1000)                                                                                                       AS CORPM
                  ,((SUM(C.POP)   / @cambio) / 1000)                                                                                                       AS POP
                  ,((SUM(C.PMVAR) / @cambio) / 1000)                                                                                                       AS PMVAR
                  ,((SUM(C.ADVER) / @cambio) / 1000)                                                                                                       AS ADVER
                  ,((SUM(C.DSTRB) / @cambio) / 1000)                                                                                                       AS DSTRB
                  ,((SUM(C.VVX17) / @cambio) / 1000)                                                                                                       AS VVX17
                  ,((SUM(C.ADVER + C.CONPR + C.OTHTA + SPA + C.POP + C.DSTRB + C.CSHDC + CORPM + C.FREEG + C.PMVAR + C.RECUN + C.RSRDV) / @cambio) / 1000) AS CONSU
                FROM
                    PRESUPUESTOP AS C
                WHERE
                    C.REGION IN
                    (
                        SELECT
                            REGION
                        FROM
                            @REGION
                    )
                    AND C.ANIO = @ANIOC
                    AND C.MES  = @PERIODOC
                GROUP BY
                    C.MES
                  , C.BANNER
                ORDER BY
                    C.BANNER ASC
                ;
                --select * from @PRESUPUESTOCPT2
            END
            ELSE
            BEGIN
                INSERT INTO @PRESUPUESTOCPT2
                SELECT
                    C.BANNER
                  , C.MES
                  ,((SUM(C.CSHDC) / @cambio) / 1000)                                                                                                       AS CSHDC
                  ,((SUM(C.RECUN) / @cambio) / 1000)                                                                                                       AS RECUN
                  ,((SUM(C.OTHTA) / @cambio) / 1000)                                                                                                       AS OTHTA
                  ,((SUM(C.SPA)   / @cambio) / 1000)                                                                                                       AS SPA
                  ,((SUM(C.FREEG) / @cambio) / 1000)                                                                                                       AS FREEG
                  ,((SUM(C.CONPR) / @cambio) / 1000)                                                                                                       AS CONPR
                  ,((SUM(C.RSRDV) / @cambio) / 1000)                                                                                                       AS RSRDV
                  ,((SUM(C.CORPM) / @cambio) / 1000)                                                                                                       AS CORPM
                  ,((SUM(C.POP)   / @cambio) / 1000)                                                                                                       AS POP
                  ,((SUM(C.PMVAR) / @cambio) / 1000)                                                                                                       AS PMVAR
                  ,((SUM(C.ADVER) / @cambio) / 1000)                                                                                                       AS ADVER
                  ,((SUM(C.DSTRB) / @cambio) / 1000)                                                                                                       AS DSTRB
                  ,((SUM(C.VVX17) / @cambio) / 1000)                                                                                                       AS VVX17
                  ,((SUM(C.ADVER + C.CONPR + C.OTHTA + SPA + C.POP + C.DSTRB + C.CSHDC + CORPM + C.FREEG + C.PMVAR + C.RECUN + C.RSRDV) / @cambio) / 1000) AS CONSU
                FROM
                    PRESUPUESTOP AS C
                WHERE
                    C.REGION IN
                    (
                        SELECT
                            REGION
                        FROM
                            @REGION
                    )
                    AND C.ANIO = @ANIOC
                GROUP BY
                    C.MES
                  , C.BANNER
                ORDER BY
                    C.BANNER ASC
                ;
            --select * from @PRESUPUESTOCPT2
            END
            INSERT INTO @PRESUPUESTOCPTCANAL
            SELECT
                A.CANAL
              , L.CDESCRIPCION
              , C.BANNER
              , A.BDESCRIPCION
              , PPTOC AS PPTOC
            FROM
                @PRESUPUESTOCPT2 AS C
                INNER JOIN
                    CLIENTE AS A
                    ON
                        A.BANNER = C.BANNER
                INNER JOIN
                    CANAL AS L
                    ON
                        L.CANAL = A.CANAL
            WHERE
                A.CANAL != ''
            GROUP BY
                C.BANNER
              , A.CANAL
              , PPTOC
              , CDESCRIPCION
              , BDESCRIPCION
            ORDER BY
                A.CANAL ASC
            --select * from @PRESUPUESTOCPTCANAL
            INSERT INTO @PRESUPUESTOCPTCANAL
            SELECT
                A.CANAL
              , L.CDESCRIPCION
              , C.BANNER
              , A.BDESCRIPCION
              , PPTOC AS PPTOC
            FROM
                @PRESUPUESTOCPT2 AS C
                INNER JOIN
                    CLIENTE AS A
                    ON
                        A.BANNERG = C.BANNER
                INNER JOIN
                    CANAL AS L
                    ON
                        L.CANAL = A.CANAL
            WHERE
                A.CANAL != ''
            GROUP BY
                C.BANNER
              , A.CANAL
              , PPTOC
              , CDESCRIPCION
              , BDESCRIPCION
            ORDER BY
                A.CANAL ASC
            --select * from @PRESUPUESTOCPTCANAL
            IF EXISTS
            (
                SELECT
                    TOP 1 *
                FROM
                    @PRESUPUESTOCPTCANAL
            )
            BEGIN
                INSERT INTO @CPTCANAL
                SELECT
                    CANAL      AS CANAL
                  , SUM(PPTOC) AS PPTOC
                FROM
                    @PRESUPUESTOCPTCANAL
                GROUP BY
                    CANAL
            END
                --    SELECT * FROM @PRESUPUESTOCPT2
                --      SELECT * FROM @PRESUPUESTOCPTCANAL
                IF EXISTS
                (
                    SELECT
                        TOP 1 *
                    FROM
                        @PRESUPUESTOCPTCANAL
                )
                BEGIN
                    SELECT DISTINCT
                        [@PRESUPUESTOCPTCANAL].CANAL        AS CANAL
                      , [@PRESUPUESTOCPTCANAL].CDESCRIPCION AS CDESCRIPCION
                      , ISNULL([@CPTCANAL].PPTOC, 0)        AS PPTOC
                      , [@PRESUPUESTOCPT2].BANNER           AS BANNER
                      , [@PRESUPUESTOCPTCANAL].BDESCRIPCION AS BDESCRIPCION
                      , ISNULL([@PRESUPUESTOCPT2].PPTOC, 0) AS PPTO
                      , [@PRESUPUESTOCPT2].PERIODO          AS PERIODO
                      , ISNULL([@PRESUPUESTOCPT2].CSHDC, 0) AS CSHDC
                      , ISNULL([@PRESUPUESTOCPT2].RECUN, 0) AS RECUN
                      , ISNULL([@PRESUPUESTOCPT2].OTHTA, 0) AS OTHTA
                      , ISNULL([@PRESUPUESTOCPT2].SPA, 0)   AS SPA
                      , ISNULL([@PRESUPUESTOCPT2].FREEG, 0) AS FREEG
                      , ISNULL([@PRESUPUESTOCPT2].CONPR, 0) AS CONPR
                      , ISNULL([@PRESUPUESTOCPT2].RSRDV, 0) AS RSRDV
                      , ISNULL([@PRESUPUESTOCPT2].CORPM, 0) AS CORPM
                      , ISNULL([@PRESUPUESTOCPT2].POP, 0)   AS POP
                      , ISNULL([@PRESUPUESTOCPT2].PMVAR, 0) AS PMVAR
                      , ISNULL([@PRESUPUESTOCPT2].ADVER, 0) AS ADVER
                      , ISNULL([@PRESUPUESTOCPT2].DSTRB, 0) AS DSTRB
                      , ISNULL([@PRESUPUESTOCPT2].VVX17, 0) AS VVX17
                      , ISNULL(@cero, 0)                    AS ALLB
                      , ISNULL(@cero, 0)                    AS ALLF
                      , ISNULL(@cero, 0)                    AS PROCESO
                      , ISNULL(@cero, 0)                    AS CONSU
                      , ISNULL(@cero, 0)                    AS TOTAL
                    FROM
                        @PRESUPUESTOCPT2
                        LEFT JOIN
                            @PRESUPUESTOCPTCANAL
                            ON
                                [@PRESUPUESTOCPTCANAL].BANNER = [@PRESUPUESTOCPT2].BANNER
                        LEFT JOIN
                            @CPTCANAL
                            ON
                                [@CPTCANAL].CANAL = [@PRESUPUESTOCPTCANAL].CANAL
                    ORDER BY
                        BANNER ASC
                END
                ELSE
                BEGIN
                    SELECT DISTINCT
                        ''                                  AS CANAL
                      ,''                                   AS CDESCRIPCION
                      , ISNULL(@cero, 0)                    AS PPTOC
                      , [@PRESUPUESTOCPT2].BANNER           AS BANNER
                      ,''                                   AS BDESCRIPCION
                      , ISNULL([@PRESUPUESTOCPT2].PPTOC, 0) AS PPTO
                      , [@PRESUPUESTOCPT2].PERIODO          AS PERIODO
                      , ISNULL([@PRESUPUESTOCPT2].CSHDC, 0) AS CSHDC
                      , ISNULL([@PRESUPUESTOCPT2].RECUN, 0) AS RECUN
                      , ISNULL([@PRESUPUESTOCPT2].OTHTA, 0) AS OTHTA
                      , ISNULL([@PRESUPUESTOCPT2].SPA, 0)   AS SPA
                      , ISNULL([@PRESUPUESTOCPT2].FREEG, 0) AS FREEG
                      , ISNULL([@PRESUPUESTOCPT2].CONPR, 0) AS CONPR
                      , ISNULL([@PRESUPUESTOCPT2].RSRDV, 0) AS RSRDV
                      , ISNULL([@PRESUPUESTOCPT2].CORPM, 0) AS CORPM
                      , ISNULL([@PRESUPUESTOCPT2].POP, 0)   AS POP
                      , ISNULL([@PRESUPUESTOCPT2].PMVAR, 0) AS PMVAR
                      , ISNULL([@PRESUPUESTOCPT2].ADVER, 0) AS ADVER
                      , ISNULL([@PRESUPUESTOCPT2].DSTRB, 0) AS DSTRB
                      , ISNULL([@PRESUPUESTOCPT2].VVX17, 0) AS VVX17
                      , ISNULL(@cero, 0)                    AS ALLB
                      , ISNULL(@cero, 0)                    AS ALLF
                      , ISNULL(@cero, 0)                    AS PROCESO
                      , ISNULL(@cero, 0)                    AS CONSU
                      , ISNULL(@cero, 0)                    AS TOTAL
                    FROM
                        @PRESUPUESTOCPT2
                    ORDER BY
                        BANNER
                END
            END
            ELSE
            BEGIN
                IF @PERIODOS != ''
                BEGIN
        --            INSERT INTO @PRESUPUESTOSAP2
        --            SELECT
        --                S.BANNER
        --              , S.PERIOD
        --                --   ,(SUM(S.GRSLS) / @cambio) AS GRSLS
        --                --   ,(SUM(S.RECSL) / @cambio) AS RECSL
        --                --   ,(SUM(S.INDLB) / @cambio) AS INDLB
        --                --   ,(SUM(S.FRGHT) / @cambio) AS FRGHT
        --                --   ,(SUM(S.PURCH) / @cambio) AS PURCH
        --                --   ,(SUM(S.RAWMT) / @cambio) AS RAWMT
        --                --   ,(SUM(S.PKGMT) / @cambio) AS PKGMT
        --                --   ,(SUM(S.OVHDV) / @cambio) AS OVHDV
        --                --   ,(SUM(S.OVHDF) / @cambio) AS OVHDF
        --                --   ,(SUM(S.DIRLB) / @cambio) AS DIRLB
							 --,SUM(S.CSHDC) AS CSHDC
							 --,SUM(S.RECUN) AS RECUN
							 --,SUM(S.OTHTA) AS OTHTA
							 --,SUM(S.SPA  ) AS SPA
							 --,SUM(S.FREEG) AS FREEG
        --                --   ,(SUM(S.PKGDS) / @cambio) AS PKGDS
							 --,SUM(S.CONPR) AS CONPR
							 --,SUM(S.RSRDV) AS RSRDV
							 --,SUM(S.CORPM) AS CORPM
							 --,SUM(S.POP  ) AS POP
							 --,SUM(S.PMVAR) AS PMVAR
							 --,SUM(S.ADVER) AS ADVER
        --                --   ,(SUM(S.NETLB) / @cambio) AS NETLB
        --                --   ,(SUM(S.SLLBS) / @cambio) AS SLLBS
        --                --   ,(SUM(S.SLCAS) / @cambio) AS SLCAS
        --                --   ,(SUM(S.PRCAS) / @cambio) AS PRCAS
        --                --   ,(SUM(S.NPCAS) / @cambio) AS NPCAS
							 --,SUM(S.DSTRB) AS DSTRB
        --                --   ,(SUM(S.ILVAR) / @cambio) AS ILVAR
        --                --   ,(SUM(S.BILBK) / @cambio) AS BILBK
        --                --   ,(SUM(S.OVHVV) / @cambio) AS OVHVV
							 --,SUM(VVX17) AS VVX17
							 --,SUM(OHV) AS OHV
        --                --,((SUM(S.CSHDC + S.RECUN + S.OTHTA + S.SPA + S.FREEG + S.CONPR + S.RSRDV + S.CORPM + S.POP + S.PMVAR + S.ADVER + S.DSTRB + VVX17 + OHV) / @cambio) / 1000) AS CONSU
        --                --,(SUM(S.GRSLS + S.RECSL + S.RAWMT + S.INDLB + S.FRGHT + S.PURCH + S.PKGMT + S.OVHDV + S.OVHDF + S.DIRLB + S.CSHDC + S.RECUN + S.OTHTA + S.SPA + S.FREEG + S.PKGDS + S.CONPR + S.RSRDV + S.CORPM + S.POP + S.PMVAR + S.ADVER + S.NETLB + S.SLLBS + S.SLCAS + S.PRCAS + S.NPCAS + S.DSTRB + S.ILVAR + S.BILBK + S.OVHVV) / @cambio) AS CONSU
        --            FROM
        --                PRESUPSAPP AS S
        --            WHERE
        --                S.BUKRS      IN (SELECT val FROM dbo.split(@sociedad,',') WHERE val != '')
        --                AND S.ANIO   = @ANIOS
        --                AND S.PERIOD = @PERIODOS
        --                AND S.TYPE   = 'B'
        --                AND S.UNAME  = @usuario
        --            GROUP BY
        --                S.BANNER
        --              , S.PERIOD
        --            ORDER BY
        --                S.BANNER ASC
        --            ;
					--select * from @PRESUPUESTOSAP2
					INSERT INTO @PRESUPUESTOSAP2
						SELECT
						  BANNER
						 ,PERIODO
						 ,[CSHDC  ]
						 ,[RECUN  ]
						 ,[OTHTA  ]
						 ,[SPA    ]
						 ,[FREEG  ]
						 ,[CONPR  ]
						 ,[RSRDV  ]
						 ,[CORPM  ]
						 ,[POP    ]
						 ,[PMVAR  ]
						 ,[ADVER  ]
						 ,[DSTRB  ]
						 ,[VVX17  ]
						 ,[OHV    ]
						FROM (SELECT
							c.BANNER
						   ,d.PERIODO
						   ,t.GALL_ID
						   ,d.MONTO_DOC_ML
						  FROM DOCUMENTO AS d
						  INNER JOIN TALL AS t
							ON d.TALL_ID = t.ID
						  INNER JOIN CLIENTE AS c
							ON c.KUNNR = d.PAYER_ID 
						  INNER JOIN TSOL AS s
							ON d.TSOL_ID = s.ID
						  WHERE d.SOCIEDAD_ID = @sociedad 
							AND d.ESTATUS_C IS NULL	-- --ADD RSG 29.11.2018
							AND d.ESTATUS_WF <> 'B'	-- --ADD RSG 29.11.2018
							AND s.REVERSO = 0 AND d.PERIODO = @PERIODOS) x
						PIVOT
						(
						  SUM(MONTO_DOC_ML)
						  FOR GALL_ID IN (
							[CSHDC  ],
							[RECUN  ],
							[OTHTA  ],
							[SPA    ],
							[FREEG  ],
							[CONPR  ],
							[RSRDV  ],
							[CORPM  ],
							[POP    ],
							[PMVAR  ],
							[ADVER  ],
							[DSTRB  ],
							[VVX17  ],
							[OHV    ]
						  )
						) p
						--select * from @PRESUPUESTOSAP2
						INSERT INTO @PRESUPUESTOSAP2
						SELECT
						  BANNER
						 ,PERIODO
						 ,[CSHDC  ] * -1
						 ,[RECUN  ] * -1
						 ,[OTHTA  ] * -1
						 ,[SPA    ] * -1
						 ,[FREEG  ] * -1
						 ,[CONPR  ] * -1
						 ,[RSRDV  ] * -1
						 ,[CORPM  ] * -1
						 ,[POP    ] * -1
						 ,[PMVAR  ] * -1
						 ,[ADVER  ] * -1
						 ,[DSTRB  ] * -1
						 ,[VVX17  ] * -1
						 ,[OHV    ] * -1
						FROM (SELECT
							c.BANNER
						   ,d.PERIODO
						   ,t.GALL_ID
						   ,d.MONTO_DOC_ML
						  FROM DOCUMENTO AS d
						  INNER JOIN TALL AS t
							ON d.TALL_ID = t.ID
						  INNER JOIN CLIENTE AS c
							ON c.KUNNR = d.PAYER_ID 
						  INNER JOIN TSOL AS s
							ON d.TSOL_ID = s.ID
						  WHERE d.SOCIEDAD_ID = @sociedad
							AND d.ESTATUS_C  IS NULL	-- --ADD RSG 29.11.2018
							AND d.ESTATUS_WF <> 'B'	-- --ADD RSG 29.11.2018
							AND s.REVERSO = 1 AND d.PERIODO = @PERIODOS) x
						PIVOT
						(
						  SUM(MONTO_DOC_ML)
						  FOR GALL_ID IN (
							[CSHDC  ],
							[RECUN  ],
							[OTHTA  ],
							[SPA    ],
							[FREEG  ],
							[CONPR  ],
							[RSRDV  ],
							[CORPM  ],
							[POP    ],
							[PMVAR  ],
							[ADVER  ],
							[DSTRB  ],
							[VVX17  ],
							[OHV    ]
						  )
						) p
						--select * from @PRESUPUESTOSAP
						INSERT INTO @PRESUPUESTOSAP
						SELECT
							 BANNER,
							 PERIODO,
							 ((SUM(CSHDC) / @cambio) / 1000) AS CSHDC,
							 ((SUM(RECUN) / @cambio) / 1000) AS RECUN,
							 ((SUM(OTHTA) / @cambio) / 1000) AS OTHTA,
							 ((SUM(SPA  ) / @cambio) / 1000) AS SPA  ,
							 ((SUM(FREEG) / @cambio) / 1000) AS FREEG,
							 ((SUM(CONPR) / @cambio) / 1000) AS CONPR,
							 ((SUM(RSRDV) / @cambio) / 1000) AS RSRDV,
							 ((SUM(CORPM) / @cambio) / 1000) AS CORPM,
							 ((SUM(POP  ) / @cambio) / 1000) AS POP  ,
							 ((SUM(PMVAR) / @cambio) / 1000) AS PMVAR,
							 ((SUM(ADVER) / @cambio) / 1000) AS ADVER,
							 ((SUM(DSTRB) / @cambio) / 1000) AS DSTRB,
							 ((SUM(VVX17) / @cambio) / 1000) AS VVX17,
							 ((SUM(OHV  ) / @cambio) / 1000) AS OHV  
						FROM @PRESUPUESTOSAP2 GROUP BY BANNER, PERIODO
                    INSERT INTO @PRESUSAPB
                    SELECT
                        BANNER
                      , PERIOD
                      ,((SUM(S.CSHDC + S.RECUN + S.OTHTA + S.SPA + S.FREEG + S.CONPR + S.RSRDV + S.CORPM + S.POP + S.PMVAR + S.ADVER + S.DSTRB + VVX17 + OHV) / @cambio) / 1000) AS TOTAL
                    FROM
                        PRESUPSAPP AS S
                    WHERE
                        S.BUKRS		 = @sociedad
                        AND S.ANIO   = @ANIOS
                        AND S.PERIOD = @PERIODOS
                        AND S.TYPE   = 'B'
                        AND S.UNAME != @usuario
                    GROUP BY
                        S.BANNER
                      , S.PERIOD
                    ORDER BY
                        S.BANNER ASC
                    ;
                    
                    INSERT INTO @TPROCETAT
                    SELECT
                        BANNER
                      , PERIODO
                      , SUM(MONTO_DOC_MD)
                    FROM
                        DOCUMENTO
                        INNER JOIN
                            CLIENTE
                            ON
                                CLIENTE.KUNNR = DOCUMENTO.PAYER_ID
					    INNER JOIN TSOL AS s
							              ON DOCUMENTO.TSOL_ID = s.ID
                    WHERE
                        SOCIEDAD_ID = @sociedad
							AND ESTATUS_C  IS NULL	-- --ADD RSG 29.11.2018
							AND ESTATUS_WF <> 'B'	-- --ADD RSG 29.11.2018
							AND s.REVERSO = 0
                        AND PERIODO = @PERIODOS
                    GROUP BY
                        BANNER
                      , PERIODO
					  INSERT INTO @TPROCETAT
                    SELECT
                        BANNER
                      , PERIODO
                      , SUM(MONTO_DOC_MD) * -1
                    FROM
                        DOCUMENTO
                        INNER JOIN
                            CLIENTE
                            ON
                                CLIENTE.KUNNR = DOCUMENTO.PAYER_ID
					    INNER JOIN TSOL AS s
							              ON DOCUMENTO.TSOL_ID = s.ID
                    WHERE
                        SOCIEDAD_ID = @sociedad
							AND ESTATUS_C  IS NULL	-- --ADD RSG 29.11.2018
							AND ESTATUS_WF <> 'B'	-- --ADD RSG 29.11.2018
							AND s.REVERSO = 1
                        AND PERIODO = @PERIODOS
                    GROUP BY
                        BANNER
                      , PERIODO
					INSERT INTO @TPROCETAT2
					SELECT
						T.BANNER, 
						T.PERIODO, 
						((SUM(T.TOTAL) / @cambio) / 1000) 
					FROM @TPROCETAT AS T
					GROUP BY
						BANNER
						,PERIODO
                    INSERT INTO @PRESUSAPF
                    SELECT
                        BANNER
                      , PERIOD
                      ,((SUM(S.CSHDC + S.RECUN + S.OTHTA + S.SPA + S.FREEG + S.CONPR + S.RSRDV + S.CORPM + S.POP + S.PMVAR + S.ADVER + S.DSTRB + VVX17 + OHV) / @cambio) / 1000) AS TOTAL
                    FROM
                        PRESUPSAPP AS S
                    WHERE
                        S.BUKRS      = @sociedad
                        AND S.ANIO   = @ANIOS
                        AND S.PERIOD = @PERIODOS
                        AND S.TYPE   = 'F'
                    GROUP BY
                        S.BANNER
                      , S.PERIOD
                    ORDER BY
                        S.BANNER ASC
                    ;
                    
                    INSERT INTO @CONSUMO
                    SELECT
                        S.BANNER
                      , S.PERIODO
                      ,(SUM(S.CSHDC + S.RECUN + S.OTHTA + S.SPA + S.FREEG + S.CONPR + S.RSRDV + S.CORPM + S.POP + S.PMVAR + S.ADVER + S.DSTRB + VVX17 + OHV)) AS TOTAL
                    FROM
                        @PRESUPUESTOSAP AS S
                        INNER JOIN
                            @PRESUSAPF AS F
                            ON
                                F.BANNER      = S.BANNER
                                AND F.PERIODO = S.PERIODO
                        INNER JOIN
                            @PRESUSAPB AS B
                            ON
                                B.BANNER      = S.BANNER
                                AND F.PERIODO = S.PERIODO
                    GROUP BY
                        S.BANNER
                      , S.PERIODO
                    ORDER BY
                        S.BANNER ASC
                    ;
                
                END
                ELSE
                BEGIN
        --            INSERT INTO @PRESUPUESTOSAP2
        --            SELECT
        --                S.BANNER
        --              , S.PERIOD
        --                --   ,(SUM(S.GRSLS) / @cambio) AS GRSLS
        --                --   ,(SUM(S.RECSL) / @cambio) AS RECSL
        --                --   ,(SUM(S.INDLB) / @cambio) AS INDLB
        --                --   ,(SUM(S.FRGHT) / @cambio) AS FRGHT
        --                --   ,(SUM(S.PURCH) / @cambio) AS PURCH
        --                --   ,(SUM(S.RAWMT) / @cambio) AS RAWMT
        --                --   ,(SUM(S.PKGMT) / @cambio) AS PKGMT
        --                --   ,(SUM(S.OVHDV) / @cambio) AS OVHDV
        --                --   ,(SUM(S.OVHDF) / @cambio) AS OVHDF
        --                --   ,(SUM(S.DIRLB) / @cambio) AS DIRLB
							 --,SUM(S.CSHDC) AS CSHDC
							 --,SUM(S.RECUN) AS RECUN
							 --,SUM(S.OTHTA) AS OTHTA
							 --,SUM(S.SPA  ) AS SPA
							 --,SUM(S.FREEG) AS FREEG
        --                --   ,(SUM(S.PKGDS) / @cambio) AS PKGDS
							 --,SUM(S.CONPR) AS CONPR
							 --,SUM(S.RSRDV) AS RSRDV
							 --,SUM(S.CORPM) AS CORPM
							 --,SUM(S.POP  ) AS POP
							 --,SUM(S.PMVAR) AS PMVAR
							 --,SUM(S.ADVER) AS ADVER
        --                --   ,(SUM(S.NETLB) / @cambio) AS NETLB
        --                --   ,(SUM(S.SLLBS) / @cambio) AS SLLBS
        --                --   ,(SUM(S.SLCAS) / @cambio) AS SLCAS
        --                --   ,(SUM(S.PRCAS) / @cambio) AS PRCAS
        --                --   ,(SUM(S.NPCAS) / @cambio) AS NPCAS
							 --,SUM(S.DSTRB) AS DSTRB
        --                --   ,(SUM(S.ILVAR) / @cambio) AS ILVAR
        --                --   ,(SUM(S.BILBK) / @cambio) AS BILBK
        --                --   ,(SUM(S.OVHVV) / @cambio) AS OVHVV
							 --,SUM(VVX17) AS VVX17
							 --,SUM(OHV) AS OHV
        --                --,((SUM(S.CSHDC + S.RECUN + S.OTHTA + S.SPA + S.FREEG + S.CONPR + S.RSRDV + S.CORPM + S.POP + S.PMVAR + S.ADVER + S.DSTRB + VVX17 + OHV) / @cambio) / 1000) AS CONSU
        --                --   ,(SUM(S.GRSLS + S.RECSL + S.RAWMT + S.INDLB + S.FRGHT + S.PURCH + S.PKGMT + S.OVHDV + S.OVHDF + S.DIRLB + S.CSHDC + S.RECUN + S.OTHTA + S.SPA + S.FREEG + S.PKGDS + S.CONPR + S.RSRDV + S.CORPM + S.POP + S.PMVAR + S.ADVER + S.NETLB + S.SLLBS + S.SLCAS + S.PRCAS + S.NPCAS + S.DSTRB + S.ILVAR + S.BILBK + S.OVHVV) / @cambio) AS CONSU
        --            FROM
        --                PRESUPSAPP AS S
        --            WHERE
        --                S.BUKRS     = @sociedad
        --                AND S.ANIO  = @ANIOS
        --                AND S.TYPE  = 'B'
        --                AND S.UNAME = @usuario
        --            GROUP BY
        --                S.BANNER
        --              , S.PERIOD
        --            ORDER BY
        --                S.BANNER ASC
        --            ;
                    INSERT INTO @PRESUPUESTOSAP2
						SELECT
						  BANNER
						 ,PERIODO
						 ,[CSHDC  ]
						 ,[RECUN  ]
						 ,[OTHTA  ]
						 ,[SPA    ]
						 ,[FREEG  ]
						 ,[CONPR  ]
						 ,[RSRDV  ]
						 ,[CORPM  ]
						 ,[POP    ]
						 ,[PMVAR  ]
						 ,[ADVER  ]
						 ,[DSTRB  ]
						 ,[VVX17  ]
						 ,[OHV    ]
						FROM (SELECT
							c.BANNER
						   ,d.PERIODO
						   ,t.GALL_ID
						   ,d.MONTO_DOC_ML
						  FROM DOCUMENTO AS d
						  INNER JOIN TALL AS t
							ON d.TALL_ID = t.ID
						  INNER JOIN CLIENTE AS c
							ON c.KUNNR = d.PAYER_ID 
						  INNER JOIN TSOL AS s
							ON d.TSOL_ID = s.ID
						  WHERE d.SOCIEDAD_ID = @sociedad 
							AND d.ESTATUS_C IS NULL	-- --ADD RSG 29.11.2018
							AND d.ESTATUS_WF <> 'B'	-- --ADD RSG 29.11.2018
							AND s.REVERSO = 0 ) x
						PIVOT
						(
						  SUM(MONTO_DOC_ML)
						  FOR GALL_ID IN (
							[CSHDC  ],
							[RECUN  ],
							[OTHTA  ],
							[SPA    ],
							[FREEG  ],
							[CONPR  ],
							[RSRDV  ],
							[CORPM  ],
							[POP    ],
							[PMVAR  ],
							[ADVER  ],
							[DSTRB  ],
							[VVX17  ],
							[OHV    ]
						  )
						) p
						INSERT INTO @PRESUPUESTOSAP2
						SELECT
						  BANNER
						 ,PERIODO
						 ,[CSHDC  ] * -1
						 ,[RECUN  ] * -1
						 ,[OTHTA  ] * -1
						 ,[SPA    ] * -1
						 ,[FREEG  ] * -1
						 ,[CONPR  ] * -1
						 ,[RSRDV  ] * -1
						 ,[CORPM  ] * -1
						 ,[POP    ] * -1
						 ,[PMVAR  ] * -1
						 ,[ADVER  ] * -1
						 ,[DSTRB  ] * -1
						 ,[VVX17  ] * -1
						 ,[OHV    ] * -1
						FROM (SELECT
							c.BANNER
						   ,d.PERIODO
						   ,t.GALL_ID
						   ,d.MONTO_DOC_ML
						  FROM DOCUMENTO AS d
						  INNER JOIN TALL AS t
							ON d.TALL_ID = t.ID
						  INNER JOIN CLIENTE AS c
							ON c.KUNNR = d.PAYER_ID 
						  INNER JOIN TSOL AS s
							ON d.TSOL_ID = s.ID
						  WHERE d.SOCIEDAD_ID = @sociedad 
							AND d.ESTATUS_C IS NULL	-- --ADD RSG 29.11.2018
							AND d.ESTATUS_WF <> 'B'	-- --ADD RSG 29.11.2018
							AND s.REVERSO = 1) x
						PIVOT
						(
						  SUM(MONTO_DOC_ML)
						  FOR GALL_ID IN (
							[CSHDC  ],
							[RECUN  ],
							[OTHTA  ],
							[SPA    ],
							[FREEG  ],
							[CONPR  ],
							[RSRDV  ],
							[CORPM  ],
							[POP    ],
							[PMVAR  ],
							[ADVER  ],
							[DSTRB  ],
							[VVX17  ],
							[OHV    ]
						  )
						) p
						INSERT INTO @PRESUPUESTOSAP
						SELECT
							 BANNER,
							 PERIODO,
							 ((SUM(CSHDC) / @cambio) / 1000) AS CSHDC,
							 ((SUM(RECUN) / @cambio) / 1000) AS RECUN,
							 ((SUM(OTHTA) / @cambio) / 1000) AS OTHTA,
							 ((SUM(SPA  ) / @cambio) / 1000) AS SPA  ,
							 ((SUM(FREEG) / @cambio) / 1000) AS FREEG,
							 ((SUM(CONPR) / @cambio) / 1000) AS CONPR,
							 ((SUM(RSRDV) / @cambio) / 1000) AS RSRDV,
							 ((SUM(CORPM) / @cambio) / 1000) AS CORPM,
							 ((SUM(POP  ) / @cambio) / 1000) AS POP  ,
							 ((SUM(PMVAR) / @cambio) / 1000) AS PMVAR,
							 ((SUM(ADVER) / @cambio) / 1000) AS ADVER,
							 ((SUM(DSTRB) / @cambio) / 1000) AS DSTRB,
							 ((SUM(VVX17) / @cambio) / 1000) AS VVX17,
							 ((SUM(OHV  ) / @cambio) / 1000) AS OHV  
						FROM @PRESUPUESTOSAP2 GROUP BY BANNER, PERIODO
                    INSERT INTO @PRESUSAPB
                    SELECT
                        BANNER
                      , PERIOD
                      ,((SUM(S.CSHDC + S.RECUN + S.OTHTA + S.SPA + S.FREEG + S.CONPR + S.RSRDV + S.CORPM + S.POP + S.PMVAR + S.ADVER + S.DSTRB + VVX17 + OHV) / @cambio) / 1000) AS TOTAL
                    FROM
                        PRESUPSAPP AS S
                    WHERE
                        S.BUKRS      = @sociedad
                        AND S.ANIO   = @ANIOS
                        AND S.TYPE   = 'B'
                        AND S.UNAME != @usuario
                    GROUP BY
                        S.BANNER
                      , S.PERIOD
                    ORDER BY
                        S.BANNER ASC
                    ;
                    
                    INSERT INTO @PRESUSAPF
                    SELECT
                        BANNER
                      , PERIOD
                      ,((SUM(S.CSHDC + S.RECUN + S.OTHTA + S.SPA + S.FREEG + S.CONPR + S.RSRDV + S.CORPM + S.POP + S.PMVAR + S.ADVER + S.DSTRB + VVX17 + OHV) / @cambio) / 1000) AS TOTAL
                    FROM
                        PRESUPSAPP AS S
                    WHERE
                        S.BUKRS    = @sociedad
                        AND S.ANIO = @ANIOS
                        AND S.TYPE = 'F'
                    GROUP BY
                        S.BANNER
                      , S.PERIOD
                    ORDER BY
                        S.BANNER ASC
                    ;
                    
                    INSERT INTO @TPROCETAT
                    SELECT
                        BANNER
                      , PERIODO
                      , SUM(MONTO_DOC_MD)
                    FROM
                        DOCUMENTO AS D
                        INNER JOIN
                            CLIENTE AS C
                            ON
                                C.KUNNR = D.PAYER_ID
						INNER JOIN TSOL AS s
							              ON d.TSOL_ID = s.ID
                    WHERE
                        SOCIEDAD_ID = @sociedad
							AND ESTATUS_C IS NULL	-- --ADD RSG 29.11.2018
							AND ESTATUS_WF <> 'B'	-- --ADD RSG 29.11.2018
							AND s.REVERSO = 0
                    GROUP BY
                        BANNER
                      , PERIODO
					INSERT INTO @TPROCETAT
                    SELECT
                        BANNER
                      , PERIODO
                      , SUM(MONTO_DOC_MD) * -1
                    FROM
                        DOCUMENTO AS D
                        INNER JOIN
                            CLIENTE AS C
                            ON
                                C.KUNNR = D.PAYER_ID
						INNER JOIN TSOL AS s
							              ON d.TSOL_ID = s.ID
                    WHERE
                        SOCIEDAD_ID = @sociedad
							AND ESTATUS_C IS NULL	-- --ADD RSG 29.11.2018
							AND ESTATUS_WF <> 'B'	-- --ADD RSG 29.11.2018
							AND s.REVERSO = 1
                    GROUP BY
                        BANNER
                      , PERIODO
					INSERT INTO @TPROCETAT2
					SELECT
						T.BANNER, 
						T.PERIODO, 
						((SUM(T.TOTAL) / @cambio) / 1000) 
					FROM @TPROCETAT AS T
					GROUP BY
						BANNER
						,PERIODO
                    INSERT INTO @CONSUMO
                    SELECT
                        S.BANNER
                      , S.PERIODO
                      ,(SUM(S.CSHDC + S.RECUN + S.OTHTA + S.SPA + S.FREEG + S.CONPR + S.RSRDV + S.CORPM + S.POP + S.PMVAR + S.ADVER + S.DSTRB + VVX17 + OHV)) AS TOTAL
                    FROM
                        @PRESUPUESTOCPT AS C
                        RIGHT JOIN
                            @PRESUPUESTOSAP AS S
                            ON
                                S.BANNER      = C.BANNER
                                AND S.PERIODO = C.PERIODO
                    GROUP BY
                        S.BANNER
                      , S.PERIODO
                    ORDER BY
                        S.BANNER ASC
                    ;
                
                END
                IF @PERIODOC != ''
                BEGIN
                    INSERT INTO @PRESUPUESTOCPT
                    SELECT
                        c.BANNER
                      , C.MES
                        --   ,(SUM(C.NETLB + C.TOTCS + C.ADVER + C.DIRLB + C.OVHDF + C.OVHDV + C.PKGMT + C.RAWMT + C.CONPR + C.POP + C.DSTRB + C.GRSLS + C.CSHDC + C.FREEG + C.PMVAR + C.PURCH + C.RECUN + C.RSRDV) / @cambio) AS PPTOC
                      ,((SUM(C.ADVER + C.CONPR + C.POP + C.DSTRB + C.CSHDC + C.FREEG + C.PMVAR + C.RECUN + C.RSRDV) / @cambio) / 1000) AS PPTO
                    FROM
                        PRESUPUESTOP AS C
                    WHERE
                        C.REGION IN
                        (
                            SELECT
                                REGION
                            FROM
                                @REGION
                        )
                        AND C.ANIO = @ANIOC
                        AND C.MES  = @PERIODOC
                    GROUP BY
                        C.MES
                      , C.BANNER
                    ORDER BY
                        C.BANNER ASC
                    INSERT INTO @PRESUPUESTOCPTCANAL
                    SELECT
                        A.CANAL
                      , L.CDESCRIPCION
                      , C.BANNER
                      , A.BDESCRIPCION
                        --   ,(SUM(C.NETLB + C.TOTCS + C.ADVER + C.DIRLB + C.OVHDF + C.OVHDV + C.PKGMT + C.RAWMT + C.CONPR + C.POP + C.DSTRB + C.GRSLS + C.CSHDC + C.FREEG + C.PMVAR + C.PURCH + C.RECUN + C.RSRDV) / @cambio) AS PPTOC
                      , PPTO AS PPTOC
                    FROM
                        @PRESUPUESTOCPT AS C
                        INNER JOIN
                            CLIENTE AS A
                            ON
                                A.BANNER = C.BANNER
                        INNER JOIN
                            CANAL AS L
                            ON
                                L.CANAL = A.CANAL
                    WHERE
                        A.CANAL != ''
                    GROUP BY
                        C.BANNER
                      , A.CANAL
                      , PPTO
                      , CDESCRIPCION
                      , BDESCRIPCION
                    ORDER BY
                        A.CANAL ASC
                    INSERT INTO @PRESUPUESTOCPTCANAL
                    SELECT
                        A.CANAL
                      , L.CDESCRIPCION
                      , C.BANNER
                      , A.BDESCRIPCION
                        --   ,(SUM(C.NETLB + C.TOTCS + C.ADVER + C.DIRLB + C.OVHDF + C.OVHDV + C.PKGMT + C.RAWMT + C.CONPR + C.POP + C.DSTRB + C.GRSLS + C.CSHDC + C.FREEG + C.PMVAR + C.PURCH + C.RECUN + C.RSRDV) / @cambio) AS PPTOC
                      , PPTO AS PPTOC
                    FROM
                        @PRESUPUESTOCPT AS C
                        INNER JOIN
                            CLIENTE AS A
                            ON
                                A.BANNERG = C.BANNER
                        INNER JOIN
                            CANAL AS L
                            ON
                                L.CANAL = A.CANAL
                    WHERE
                        A.CANAL != ''
                    GROUP BY
                        C.BANNER
                      , A.CANAL
                      , PPTO
                      , CDESCRIPCION
                      , BDESCRIPCION
                    ORDER BY
                        A.CANAL ASC
                    IF EXISTS
                    (
                        SELECT
                            TOP 1 *
                        FROM
                            @PRESUPUESTOCPTCANAL
                    )
                    BEGIN
                        INSERT INTO @CPTCANAL
                        SELECT
                            CANAL      AS CANAL
                          , SUM(PPTOC) AS PPTOC
                        FROM
                            @PRESUPUESTOCPTCANAL
                        GROUP BY
                            CANAL
                    END
            END 
			ELSE 
			BEGIN
                        INSERT INTO @PRESUPUESTOCPT
                        SELECT
                            c.BANNER
                          , C.MES
                            --   ,(SUM(C.NETLB + C.TOTCS + C.ADVER + C.DIRLB + C.OVHDF + C.OVHDV + C.PKGMT + C.RAWMT + C.CONPR + C.POP + C.DSTRB + C.GRSLS + C.CSHDC + C.FREEG + C.PMVAR + C.PURCH + C.RECUN + C.RSRDV) / @cambio) AS PPTOC
                          ,((SUM(C.ADVER + C.CONPR + C.POP + C.DSTRB + C.CSHDC + C.FREEG + C.PMVAR + C.RECUN + C.RSRDV) / @cambio) / 1000) AS PPTO
                        FROM
                            PRESUPUESTOP AS C
                        WHERE
                            C.REGION IN
                            (
                                SELECT
                                    REGION
                                FROM
                                    @REGION
                            )
                            AND C.ANIO = @ANIOC
                        GROUP BY
                            C.MES
                          , C.BANNER
                        ORDER BY
                            C.BANNER ASC
                        INSERT INTO @PRESUPUESTOCPTCANAL
                        SELECT
                            A.CANAL
                          , L.CDESCRIPCION
                          , C.BANNER
                          , A.BDESCRIPCION
                            --   ,(SUM(C.NETLB + C.TOTCS + C.ADVER + C.DIRLB + C.OVHDF + C.OVHDV + C.PKGMT + C.RAWMT + C.CONPR + C.POP + C.DSTRB + C.GRSLS + C.CSHDC + C.FREEG + C.PMVAR + C.PURCH + C.RECUN + C.RSRDV) / @cambio) AS PPTOC
                          , PPTO AS PPTOC
                        FROM
                            @PRESUPUESTOCPT AS C
                            INNER JOIN
                                CLIENTE AS A
                                ON
                                    A.BANNER = C.BANNER
                            INNER JOIN
                                CANAL AS L
                                ON
                                    L.CANAL = A.CANAL
                        WHERE
                            A.CANAL != ''
                        GROUP BY
                            C.BANNER
                          , A.CANAL
                          , PPTO
                          , CDESCRIPCION
                          , BDESCRIPCION
                        ORDER BY
                            C.BANNER ASC
                        INSERT INTO @PRESUPUESTOCPTCANAL
                        SELECT
                            A.CANAL
                          , L.CDESCRIPCION
                          , C.BANNER
                          , A.BDESCRIPCION
                            --   ,(SUM(C.NETLB + C.TOTCS + C.ADVER + C.DIRLB + C.OVHDF + C.OVHDV + C.PKGMT + C.RAWMT + C.CONPR + C.POP + C.DSTRB + C.GRSLS + C.CSHDC + C.FREEG + C.PMVAR + C.PURCH + C.RECUN + C.RSRDV) / @cambio) AS PPTOC
                          , PPTO AS PPTOC
                        FROM
                            @PRESUPUESTOCPT AS C
                            INNER JOIN
                                CLIENTE AS A
                                ON
                                    A.BANNERG = C.BANNER
                            INNER JOIN
                                CANAL AS L
                                ON
                                    L.CANAL = A.CANAL
                        WHERE
                            A.CANAL != ''
                        GROUP BY
                            C.BANNER
                          , A.CANAL
                          , PPTO
                          , CDESCRIPCION
                          , BDESCRIPCION
                        ORDER BY
                            C.BANNER ASC
                        IF EXISTS
                        (
                            SELECT
                                TOP 1 *
                            FROM
                                @PRESUPUESTOCPTCANAL
                        )
                        BEGIN
                            INSERT INTO @CPTCANAL
                            SELECT
                                CANAL      AS CANAL
                              , SUM(PPTOC) AS PPTOC
                            FROM
                                @PRESUPUESTOCPTCANAL
                            GROUP BY
                                CANAL
                        END
                END
                            --select * from @PRESUPUESTOCPT order by BANNER asc
                            --select * from @PRESUPUESTOSAP order by BANNER asc
                            --select * from @PRESUPUESTOCPTCANAL
                            --select * from @CPTCANAL
                            --SELECT * FROM @PRESUSAPB ORDER BY BANNER ASC
                            --SELECT * FROM @PRESUSAPF ORDER BY BANNER ASC
                            --SELECT * FROM @CONSUMO ORDER BY BANNER ASC
                            --select * from @TPROCETAT2
                            IF EXISTS
                            (
                                SELECT
                                    TOP 1 *
                                FROM
                                    @PRESUPUESTOCPTCANAL
                            )
                            BEGIN
                                SELECT DISTINCT
                                    [@PRESUPUESTOCPTCANAL].CANAL        AS CANAL
                                  , [@PRESUPUESTOCPTCANAL].CDESCRIPCION AS CDESCRIPCION
                                  , ISNULL([@CPTCANAL].PPTOC, 0)        AS PPTOC
                                  , [@PRESUPUESTOCPT].BANNER            AS BANNER
                                  , [@PRESUPUESTOCPTCANAL].BDESCRIPCION AS BDESCRIPCION
                                  , ISNULL([@PRESUPUESTOCPT].PPTO, 0)   AS PPTO
                                  , [@PRESUPUESTOCPT].PERIODO           AS PERIODO
                                    -- ,ISNULL([@PRESUPUESTOSAP].GRSLS, 0) AS GRSLS
                                    -- ,ISNULL([@PRESUPUESTOSAP].RECSL, 0) AS RECSL
                                    -- ,ISNULL([@PRESUPUESTOSAP].INDLB, 0) AS INDLB
                                    -- ,ISNULL([@PRESUPUESTOSAP].FRGHT, 0) AS FRGHT
                                    -- ,ISNULL([@PRESUPUESTOSAP].PURCH, 0) AS PURCH
                                    -- ,ISNULL([@PRESUPUESTOSAP].RAWMT, 0) AS RAWMT
                                    -- ,ISNULL([@PRESUPUESTOSAP].PKGMT, 0) AS PKGMT
                                    -- ,ISNULL([@PRESUPUESTOSAP].OVHDV, 0) AS OVHDV
                                    -- ,ISNULL([@PRESUPUESTOSAP].OVHDF, 0) AS OVHDF
                                    -- ,ISNULL([@PRESUPUESTOSAP].DIRLB, 0) AS DIRLB
                                  , ISNULL([@PRESUPUESTOSAP].CSHDC, 0) AS CSHDC
                                  , ISNULL([@PRESUPUESTOSAP].RECUN, 0) AS RECUN
                                  , ISNULL([@PRESUPUESTOSAP].OTHTA, 0) AS OTHTA
                                  , ISNULL([@PRESUPUESTOSAP].SPA, 0)   AS SPA
                                  , ISNULL([@PRESUPUESTOSAP].FREEG, 0) AS FREEG
                                    -- ,ISNULL([@PRESUPUESTOSAP].PKGDS, 0) AS PKGDS
                                  , ISNULL([@PRESUPUESTOSAP].CONPR, 0) AS CONPR
                                  , ISNULL([@PRESUPUESTOSAP].RSRDV, 0) AS RSRDV
                                  , ISNULL([@PRESUPUESTOSAP].CORPM, 0) AS CORPM
                                  , ISNULL([@PRESUPUESTOSAP].POP, 0)   AS POP
                                  , ISNULL([@PRESUPUESTOSAP].PMVAR, 0) AS PMVAR
                                  , ISNULL([@PRESUPUESTOSAP].ADVER, 0) AS ADVER
                                    -- ,ISNULL([@PRESUPUESTOSAP].NETLB, 0) AS NETLB
                                    -- ,ISNULL([@PRESUPUESTOSAP].SLLBS, 0) AS SLLBS
                                    -- ,ISNULL([@PRESUPUESTOSAP].SLCAS, 0) AS SLCAS
                                    -- ,ISNULL([@PRESUPUESTOSAP].PRCAS, 0) AS PRCAS
                                    -- ,ISNULL([@PRESUPUESTOSAP].NPCAS, 0) AS NPCAS
                                  , ISNULL([@PRESUPUESTOSAP].DSTRB, 0) AS DSTRB
                                    -- ,ISNULL([@PRESUPUESTOSAP].ILVAR, 0) AS ILVAR
                                    -- ,ISNULL([@PRESUPUESTOSAP].BILBK, 0) AS BILBK
                                    -- ,ISNULL([@PRESUPUESTOSAP].OVHVV, 0) AS OVHVV
                                  , ISNULL([@PRESUPUESTOSAP].VVX17, 0) AS VVX17
                                  , ISNULL([@PRESUPUESTOSAP].OHV, 0)   AS OHV
                                    --,ISNULL([@PRESUPUESTOSAP].CONSU, 0) AS ALLB
                                  , ABS(ISNULL([@PRESUSAPB].TOTAL, 0))                                    AS ALLB
                                  , ABS(ISNULL([@PRESUSAPF].TOTAL, 0))                                    AS ALLF
                                  , ISNULL([@TPROCETAT2].TOTAL,0)                                          AS PROCESO
                                  , ISNULL([@CONSUMO].TOTAL1, 0)                                          AS CONSU
                                  , ISNULL([@PRESUPUESTOCPT].PPTO - ABS(ISNULL([@CONSUMO].TOTAL1, 0)), 0) AS TOTAL
                                FROM
                                    @PRESUPUESTOCPT
                                    LEFT JOIN
                                        @PRESUPUESTOSAP
                                        ON
                                            [@PRESUPUESTOSAP].BANNER      = [@PRESUPUESTOCPT].BANNER
                                            AND [@PRESUPUESTOSAP].PERIODO = [@PRESUPUESTOCPT].PERIODO
                                    LEFT JOIN
                                        @PRESUPUESTOCPTCANAL
                                        ON
                                            [@PRESUPUESTOCPTCANAL].BANNER = [@PRESUPUESTOCPT].BANNER
                                    LEFT JOIN
                                        @CPTCANAL
                                        ON
                                            [@CPTCANAL].CANAL = [@PRESUPUESTOCPTCANAL].CANAL
                                    LEFT JOIN
                                        @CONSUMO
                                        ON
                                            [@CONSUMO].BANNER      = [@PRESUPUESTOCPT].BANNER
                                            AND [@CONSUMO].PERIODO = [@PRESUPUESTOCPT].PERIODO
                                    LEFT JOIN
                                        @TPROCETAT2
                                        ON
                                            [@TPROCETAT2].BANNER      = [@PRESUPUESTOCPT].BANNER
                                            AND [@TPROCETAT2].PERIODO = [@PRESUPUESTOCPT].PERIODO
                                    LEFT JOIN
                                        @PRESUSAPB
                                        ON
                                            [@PRESUSAPB].BANNER      = [@PRESUPUESTOCPT].BANNER
                                            AND [@PRESUSAPB].PERIODO = [@PRESUPUESTOCPT].PERIODO
                                    LEFT JOIN
                                        @PRESUSAPF
                                        ON
                                            [@PRESUSAPF].BANNER      = [@PRESUPUESTOCPT].BANNER
                                            AND [@PRESUSAPF].PERIODO = [@PRESUPUESTOCPT].PERIODO
                                ORDER BY
                                    BANNER
                                  , PERIODO ASC
                            END
                            ELSE
                            BEGIN
                                SELECT DISTINCT
                                    ''                                AS CANAL
                                  ,''                                 AS CDESCRIPCION
                                  , ISNULL(@cero, 0)                  AS PPTOC
                                  , [@PRESUPUESTOCPT].BANNER          AS BANNER
                                  ,''                                 AS BDESCRIPCION
                                  , ISNULL([@PRESUPUESTOCPT].PPTO, 0) AS PPTO
                                  , [@PRESUPUESTOCPT].PERIODO         AS PERIODO
                                    -- ,ISNULL([@PRESUPUESTOSAP].GRSLS, 0) AS GRSLS
                                    -- ,ISNULL([@PRESUPUESTOSAP].RECSL, 0) AS RECSL
                                    -- ,ISNULL([@PRESUPUESTOSAP].INDLB, 0) AS INDLB
                                    -- ,ISNULL([@PRESUPUESTOSAP].FRGHT, 0) AS FRGHT
                                    -- ,ISNULL([@PRESUPUESTOSAP].PURCH, 0) AS PURCH
                                    -- ,ISNULL([@PRESUPUESTOSAP].RAWMT, 0) AS RAWMT
                                    -- ,ISNULL([@PRESUPUESTOSAP].PKGMT, 0) AS PKGMT
                                    -- ,ISNULL([@PRESUPUESTOSAP].OVHDV, 0) AS OVHDV
                                    -- ,ISNULL([@PRESUPUESTOSAP].OVHDF, 0) AS OVHDF
                                    -- ,ISNULL([@PRESUPUESTOSAP].DIRLB, 0) AS DIRLB
                                  , ISNULL([@PRESUPUESTOSAP].CSHDC, 0) AS CSHDC
                                  , ISNULL([@PRESUPUESTOSAP].RECUN, 0) AS RECUN
                                  , ISNULL([@PRESUPUESTOSAP].OTHTA, 0) AS OTHTA
                                  , ISNULL([@PRESUPUESTOSAP].SPA, 0)   AS SPA
                                  , ISNULL([@PRESUPUESTOSAP].FREEG, 0) AS FREEG
                                    -- ,ISNULL([@PRESUPUESTOSAP].PKGDS, 0) AS PKGDS
                                  , ISNULL([@PRESUPUESTOSAP].CONPR, 0) AS CONPR
                                  , ISNULL([@PRESUPUESTOSAP].RSRDV, 0) AS RSRDV
                                  , ISNULL([@PRESUPUESTOSAP].CORPM, 0) AS CORPM
                                  , ISNULL([@PRESUPUESTOSAP].POP, 0)   AS POP
                                  , ISNULL([@PRESUPUESTOSAP].PMVAR, 0) AS PMVAR
                                  , ISNULL([@PRESUPUESTOSAP].ADVER, 0) AS ADVER
                                    -- ,ISNULL([@PRESUPUESTOSAP].NETLB, 0) AS NETLB
                                    -- ,ISNULL([@PRESUPUESTOSAP].SLLBS, 0) AS SLLBS
                                    -- ,ISNULL([@PRESUPUESTOSAP].SLCAS, 0) AS SLCAS
                                    -- ,ISNULL([@PRESUPUESTOSAP].PRCAS, 0) AS PRCAS
                                    -- ,ISNULL([@PRESUPUESTOSAP].NPCAS, 0) AS NPCAS
                                  , ISNULL([@PRESUPUESTOSAP].DSTRB, 0) AS DSTRB
                                    -- ,ISNULL([@PRESUPUESTOSAP].ILVAR, 0) AS ILVAR
                                    -- ,ISNULL([@PRESUPUESTOSAP].BILBK, 0) AS BILBK
                                    -- ,ISNULL([@PRESUPUESTOSAP].OVHVV, 0) AS OVHVV
                                  , ISNULL([@PRESUPUESTOSAP].VVX17, 0) AS VVX17
                                  , ISNULL([@PRESUPUESTOSAP].OHV, 0)   AS OHV
                                    --,ISNULL([@PRESUPUESTOSAP].CONSU, 0) AS ALLB
                                  , ABS(ISNULL([@PRESUSAPB].TOTAL, 0))                                    AS ALLB
                                  , ABS(ISNULL([@PRESUSAPF].TOTAL, 0))                                    AS ALLF
                                  , ISNULL([@TPROCETAT2].TOTAL,0)                                          AS PROCESO
                                  , ISNULL([@CONSUMO].TOTAL1, 0)                                          AS CONSU
                                  , ISNULL([@PRESUPUESTOCPT].PPTO - ABS(ISNULL([@CONSUMO].TOTAL1, 0)), 0) AS TOTAL
                                FROM
                                    @PRESUPUESTOSAP
                                    RIGHT JOIN
                                        @PRESUPUESTOCPT
                                        ON
                                            [@PRESUPUESTOSAP].BANNER      = [@PRESUPUESTOCPT].BANNER
                                            AND [@PRESUPUESTOSAP].PERIODO = [@PRESUPUESTOCPT].PERIODO
                                    RIGHT JOIN
                                        @CONSUMO
                                        ON
                                            [@CONSUMO].BANNER      = [@PRESUPUESTOCPT].BANNER
                                            AND [@CONSUMO].PERIODO = [@PRESUPUESTOCPT].PERIODO
                                    RIGHT JOIN
                                        @PRESUSAPB
                                        ON
                                            [@PRESUSAPB].BANNER      = [@PRESUPUESTOCPT].BANNER
                                            AND [@PRESUSAPB].PERIODO = [@PRESUPUESTOCPT].PERIODO
                                    LEFT JOIN
                                        @TPROCETAT2
                                        ON
                                            [@TPROCETAT2].BANNER      = [@PRESUPUESTOCPT].BANNER
                                            AND [@TPROCETAT2].PERIODO = [@PRESUPUESTOCPT].PERIODO
                                    RIGHT JOIN
                                        @PRESUSAPF
                                        ON
                                            [@PRESUSAPF].BANNER      = [@PRESUPUESTOCPT].BANNER
                                            AND [@PRESUSAPF].PERIODO = [@PRESUPUESTOCPT].PERIODO
                                ORDER BY
                                    BANNER
                                  , PERIODO ASC
                            END
                        END
                    END
GO
/****** Object:  StoredProcedure [dbo].[CSP_DOCUMENTOSRECCXSOL]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CSP_DOCUMENTOSRECCXSOL]
	-- Add the parameters for the stored procedure here	
	@SOLICITUD NVARCHAR(10),
	@SPRAS NVARCHAR(2)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	

SELECT	  D.NUM_DOC
		, TS.PADRE
		, TS.REVERSO
		--, CASE WHEN TS.PADRE = 1 THEN CASE WHEN COUNT(R.NUM_DOC)>0 THEN 'expand_more' else 'add' end ELSE '' END as BUTTON
		, D.ESTATUS_WF as ESTATUS
		, F.USUARIOA_ID
		, CASE WHEN D.ESTATUS = 'R' AND F.USUARIOA_ID = 'COKCXG24' THEN 'Edit' ELSE 'Details' END as NUM_DOC_TEXT
		, D.SOCIEDAD_ID
		, D.PAIS_ID
		, D.FECHAD
		, D.HORAC
		, D.PERIODO
		, ISNULL(D.ESTATUS,' ') + ISNULL(D.ESTATUS_C, ' ') + ISNULL(D.ESTATUS_SAP,' ')
     + ISNULL(D.ESTATUS_WF,' ') + ISNULL( F.TIPO,' ') + (CASE WHEN TS.PADRE = 1 THEN 'P' ELSE ' ' END ) 
	 + (CASE WHEN COUNT(R.NUM_DOC)> 0 THEN 'R' ELSE ' ' END) + ISNULL(CASE WHEN REV.REVERSO = 1 THEN '1' ELSE '0' END,' ')
	 AS ESTATUSS
	 , D.PAYER_ID
	 , C.NAME1
	 , C.CANAL
	 , TST.TXT020
	 , TAT.TXT50
	 , D.CONCEPTO
	 , D.MONTO_DOC_MD
	 , DF.FACTURA
	 , DF.FACTURAK
	 , D.USUARIOD_ID
	 , D.DOCUMENTO_SAP
	 ,DS.BLART
	 , DS.KUNNR
	 , DS.DESCR
	 , DS.IMPORTE
	 , DS.CUENTA_C
	 , CTA.CARGO
	 , D.CUENTAP
	 , D.CUENTAPL
	 , D.CUENTACL
	 ,R.NUM_DOC AS NUM_DOC_PADRE
FROM DOCUMENTOREC AS R
INNER JOIN DOCUMENTO AS D
ON R.DOC_REF = D.NUM_DOC
INNER JOIN TSOL AS TS
ON D.TSOL_ID = TS.ID
LEFT JOIN (SELECT F1.NUM_DOC, ESTATUS, F1.USUARIOA_ID, A.TIPO FROM FLUJO AS F1
		INNER JOIN (SELECT NUM_DOC, MAX(POS) AS POS FROM FLUJO GROUP BY NUM_DOC) AS F2
		ON  F1.NUM_DOC = F2.NUM_DOC
		AND F1.POS = F2.POS
		INNER JOIN WORKFP AS W
		ON W.ID = F1.WORKF_ID
		AND W.[VERSION] = F1.WF_VERSION
		AND W.POS = F1.WF_POS
		INNER JOIN ACCION AS A
		ON A.ID = W.ACCION_ID) AS F
	ON D.NUM_DOC = F.NUM_DOC
	INNER JOIN CLIENTE AS C
	ON D.VKORG = C.VKORG AND D.VTWEG = C.VTWEG AND D.SPART = C.SPART AND D.PAYER_ID = C.KUNNR
	INNER JOIN TSOLT AS TST
	ON TS.ID = TST.TSOL_ID
	INNER JOIN TALLT AS TAT
	ON D.TALL_ID = TAT.TALL_ID
	LEFT JOIN (SELECT NUM_DOC, FACTURA, FACTURAK FROM DOCUMENTOF WHERE POS = 1) AS DF
	ON D.NUM_DOC = DF.NUM_DOC
	LEFT JOIN DOCUMENTOSAP AS DS
	ON D.NUM_DOC = DS.NUM_DOC
	LEFT JOIN CUENTA AS CTA
	--ON D.SOCIEDAD_ID = CTA.SOCIEDAD_ID AND D.PAIS_ID = CTA.PAIS_ID AND D.TALL_ID = CTA.TALL_ID
	ON D.SOCIEDAD_ID = CTA.SOCIEDAD_ID AND D.TALL_ID = CTA.TALL_ID
	LEFT JOIN (
	select padre.NUM_DOC, t.REVERSO, hijo.DOCUMENTO_SAP from DOCUMENTO as padre
	 inner JOIN   DOCUMENTO as hijo 
	 on hijo.DOCUMENTO_REF = padre.NUM_DOC
	 inner join tsol as t
	 on hijo.TSOL_ID = t.ID
	 and t.REVERSO = 1
	 and hijo.DOCUMENTO_SAP is not null
	 --group by padre.NUM_DOC 
	) as REV on D.NUM_DOC = REV.NUM_DOC
WHERE R.NUM_DOC=@SOLICITUD AND R.DOC_REF!=0
AND TST.SPRAS_ID = @SPRAS
AND TAT.SPRAS_ID = @SPRAS
GROUP BY D.NUM_DOC, TS.PADRE, TS.REVERSO, D.ESTATUS, F.USUARIOA_ID, D.SOCIEDAD_ID, D.PAIS_ID, D.FECHAD, D.HORAC, D.PERIODO, D.ESTATUS, D.ESTATUS_C
		,D.ESTATUS_SAP, D.ESTATUS_WF, F.TIPO, D.PAYER_ID, C.NAME1, C.CANAL, TST.TXT020, TAT.TXT50	 , D.CONCEPTO	 , D.MONTO_DOC_MD
	 , DF.FACTURA	 , DF.FACTURAK, D.USUARIOD_ID	 , D.DOCUMENTO_SAP	 ,DS.BLART	 , DS.KUNNR	 , DS.DESCR	 , DS.IMPORTE
	 , DS.CUENTA_C, CTA.CARGO, REV.REVERSO,  D.CUENTAP, D.CUENTAPL, D.CUENTACL, R.NUM_DOC
END



GO
/****** Object:  StoredProcedure [dbo].[CSP_DOCUMENTOSRELXSOL]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CSP_DOCUMENTOSRELXSOL]
	-- Add the parameters for the stored procedure here	
	@SOLICITUD NVARCHAR(10),
	@SPRAS NVARCHAR(2)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	

SELECT	  D.NUM_DOC
		, TS.PADRE
		, TS.REVERSO
		--, CASE WHEN TS.PADRE = 1 THEN CASE WHEN COUNT(R.NUM_DOC)>0 THEN 'expand_more' else 'add' end ELSE '' END as BUTTON
		, D.ESTATUS_WF as ESTATUS
		, F.USUARIOA_ID
		, CASE WHEN D.ESTATUS = 'R' AND F.USUARIOA_ID = 'COKCXG24' THEN 'Edit' ELSE 'Details' END as NUM_DOC_TEXT
		, D.SOCIEDAD_ID
		, D.PAIS_ID
		, D.FECHAD
		, D.HORAC
		, D.PERIODO
		, ISNULL(D.ESTATUS,' ') + ISNULL(D.ESTATUS_C, ' ') + ISNULL(D.ESTATUS_SAP,' ')
     + ISNULL(D.ESTATUS_WF,' ') + ISNULL( F.TIPO,' ') + (CASE WHEN TS.PADRE = 1 THEN 'P' ELSE ' ' END ) 
	 + (CASE WHEN COUNT(R.NUM_DOC)> 0 THEN 'R' ELSE ' ' END) + ISNULL(CASE WHEN REV.REVERSO = 1 THEN '1' ELSE '0' END,' ')
	 AS ESTATUSS
	 , D.PAYER_ID
	 , C.NAME1
	 , C.CANAL
	 , TST.TXT020
	 , TAT.TXT50
	 , D.CONCEPTO
	 , D.MONTO_DOC_MD
	 , DF.FACTURA
	 , DF.FACTURAK
	 , D.USUARIOD_ID
	 , D.DOCUMENTO_SAP
	 ,DS.BLART
	 , DS.KUNNR
	 , DS.DESCR
	 , DS.IMPORTE
	 , DS.CUENTA_C
	 , CTA.CARGO
	 , D.CUENTAP
	 , D.CUENTAPL
	 , D.CUENTACL
	 ,D.DOCUMENTO_REF
FROM DOCUMENTO AS D
LEFT JOIN DOCUMENTOREC AS R
ON D.NUM_DOC = R.NUM_DOC
INNER JOIN TSOL AS TS
ON D.TSOL_ID = TS.ID
LEFT JOIN (SELECT F1.NUM_DOC, ESTATUS, F1.USUARIOA_ID, A.TIPO FROM FLUJO AS F1
		INNER JOIN (SELECT NUM_DOC, MAX(POS) AS POS FROM FLUJO GROUP BY NUM_DOC) AS F2
		ON  F1.NUM_DOC = F2.NUM_DOC
		AND F1.POS = F2.POS
		INNER JOIN WORKFP AS W
		ON W.ID = F1.WORKF_ID
		AND W.[VERSION] = F1.WF_VERSION
		AND W.POS = F1.WF_POS
		INNER JOIN ACCION AS A
		ON A.ID = W.ACCION_ID) AS F
	ON D.NUM_DOC = F.NUM_DOC
	INNER JOIN CLIENTE AS C
	ON D.VKORG = C.VKORG AND D.VTWEG = C.VTWEG AND D.SPART = C.SPART AND D.PAYER_ID = C.KUNNR
	INNER JOIN TSOLT AS TST
	ON TS.ID = TST.TSOL_ID
	INNER JOIN TALLT AS TAT
	ON D.TALL_ID = TAT.TALL_ID
	LEFT JOIN (SELECT NUM_DOC, FACTURA, FACTURAK FROM DOCUMENTOF WHERE POS = 1) AS DF
	ON D.NUM_DOC = DF.NUM_DOC
	LEFT JOIN DOCUMENTOSAP AS DS
	ON D.NUM_DOC = DS.NUM_DOC
	LEFT JOIN CUENTA AS CTA
	--ON D.SOCIEDAD_ID = CTA.SOCIEDAD_ID AND D.PAIS_ID = CTA.PAIS_ID AND D.TALL_ID = CTA.TALL_ID
	ON D.SOCIEDAD_ID = CTA.SOCIEDAD_ID AND D.TALL_ID = CTA.TALL_ID
	LEFT JOIN (
	select padre.NUM_DOC, t.REVERSO, hijo.DOCUMENTO_SAP from DOCUMENTO as padre
	 inner JOIN   DOCUMENTO as hijo 
	 on hijo.DOCUMENTO_REF = padre.NUM_DOC
	 inner join tsol as t
	 on hijo.TSOL_ID = t.ID
	 and t.REVERSO = 1
	 and hijo.DOCUMENTO_SAP is not null
	 --group by padre.NUM_DOC 
	) as REV on D.NUM_DOC = REV.NUM_DOC
WHERE D.DOCUMENTO_REF=@SOLICITUD
AND TST.SPRAS_ID = @SPRAS
AND TAT.SPRAS_ID = @SPRAS
GROUP BY D.NUM_DOC, TS.PADRE, TS.REVERSO, D.ESTATUS, F.USUARIOA_ID, D.SOCIEDAD_ID, D.PAIS_ID, D.FECHAD, D.HORAC, D.PERIODO, D.ESTATUS, D.ESTATUS_C
		,D.ESTATUS_SAP, D.ESTATUS_WF, F.TIPO, D.PAYER_ID, C.NAME1, C.CANAL, TST.TXT020, TAT.TXT50	 , D.CONCEPTO	 , D.MONTO_DOC_MD
	 , DF.FACTURA	 , DF.FACTURAK, D.USUARIOD_ID	 , D.DOCUMENTO_SAP	 ,DS.BLART	 , DS.KUNNR	 , DS.DESCR	 , DS.IMPORTE
	 , DS.CUENTA_C, CTA.CARGO, REV.REVERSO,  D.CUENTAP, D.CUENTAPL, D.CUENTACL, D.DOCUMENTO_REF
END



GO
/****** Object:  StoredProcedure [dbo].[CSP_DOCUMENTOSXCOCODE]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,Roberto Mtz>
-- Create date: <Create Date,21/11/18>
-- Description:	<Description,Will get al documents for the company codes belonging to a given user,>
-- =============================================
CREATE PROCEDURE [dbo].[CSP_DOCUMENTOSXCOCODE] 
	-- Add the parameters for the stored procedure here
	@COCODE NCHAR(4),
	@SPRAS NVARCHAR(2)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	

SELECT	  D.NUM_DOC
		, TS.PADRE
		, TS.REVERSO
		, CASE WHEN TS.PADRE = 1 THEN CASE WHEN COUNT(R.NUM_DOC)>0 THEN 'expand_more' else 'add' end ELSE '' END as BUTTON
		, D.ESTATUS_WF as ESTATUS
		, F.USUARIOA_ID
		, CASE WHEN D.ESTATUS = 'R' AND F.USUARIOA_ID = 'COKCXG24' THEN 'Edit' ELSE 'Details' END as NUM_DOC_TEXT
		, D.SOCIEDAD_ID
		, D.PAIS_ID
		, D.FECHAD
		, D.HORAC
		, D.PERIODO
		, ISNULL(D.ESTATUS,' ') + ISNULL(D.ESTATUS_C, ' ') + ISNULL(D.ESTATUS_SAP,' ')
     + ISNULL(D.ESTATUS_WF,' ') + ISNULL( F.TIPO,' ') + (CASE WHEN TS.PADRE = 1 THEN 'P' ELSE ' ' END ) 
	 + (CASE WHEN COUNT(R.NUM_DOC)> 0 THEN 'R' ELSE ' ' END) + ISNULL(CASE WHEN REV.REVERSO = 1 THEN '1' ELSE '0' END,' ')
	 AS ESTATUSS
	 , D.PAYER_ID
	 , C.NAME1
	 , C.CANAL
	 , TST.TXT020
	 , TAT.TXT50
	 , D.CONCEPTO
	 , D.MONTO_DOC_MD
	 , DF.FACTURA
	 , DF.FACTURAK
	 , D.USUARIOD_ID
	 , D.DOCUMENTO_SAP
	 ,DS.BLART
	 , DS.KUNNR
	 , DS.DESCR
	 , DS.IMPORTE
	 , DS.CUENTA_C
	 , CTA.CARGO
	 , D.CUENTAP
	 , D.CUENTAPL
	 , D.CUENTACL
FROM DOCUMENTO AS D
LEFT JOIN DOCUMENTOREC AS R
ON D.NUM_DOC = R.NUM_DOC
INNER JOIN TSOL AS TS
ON D.TSOL_ID = TS.ID
LEFT JOIN (SELECT F1.NUM_DOC, ESTATUS, F1.USUARIOA_ID, A.TIPO FROM FLUJO AS F1
		INNER JOIN (SELECT NUM_DOC, MAX(POS) AS POS FROM FLUJO GROUP BY NUM_DOC) AS F2
		ON  F1.NUM_DOC = F2.NUM_DOC
		AND F1.POS = F2.POS
		INNER JOIN WORKFP AS W
		ON W.ID = F1.WORKF_ID
		AND W.[VERSION] = F1.WF_VERSION
		AND W.POS = F1.WF_POS
		INNER JOIN ACCION AS A
		ON A.ID = W.ACCION_ID) AS F
	ON D.NUM_DOC = F.NUM_DOC
	INNER JOIN CLIENTE AS C
	ON D.VKORG = C.VKORG AND D.VTWEG = C.VTWEG AND D.SPART = C.SPART AND D.PAYER_ID = C.KUNNR
	INNER JOIN TSOLT AS TST
	ON TS.ID = TST.TSOL_ID
	INNER JOIN TALLT AS TAT
	ON D.TALL_ID = TAT.TALL_ID
	LEFT JOIN (SELECT NUM_DOC, FACTURA, FACTURAK FROM DOCUMENTOF WHERE POS = 1) AS DF
	ON D.NUM_DOC = DF.NUM_DOC
	LEFT JOIN DOCUMENTOSAP AS DS
	ON D.NUM_DOC = DS.NUM_DOC
	LEFT JOIN CUENTA AS CTA
	--ON D.SOCIEDAD_ID = CTA.SOCIEDAD_ID AND D.PAIS_ID = CTA.PAIS_ID AND D.TALL_ID = CTA.TALL_ID
	ON D.SOCIEDAD_ID = CTA.SOCIEDAD_ID AND D.TALL_ID = CTA.TALL_ID
	LEFT JOIN (
	select padre.NUM_DOC, t.REVERSO, hijo.DOCUMENTO_SAP from DOCUMENTO as padre
	 inner JOIN   DOCUMENTO as hijo 
	 on hijo.DOCUMENTO_REF = padre.NUM_DOC
	 inner join tsol as t
	 on hijo.TSOL_ID = t.ID
	 and t.REVERSO = 1
	 and hijo.DOCUMENTO_SAP is not null
	 --group by padre.NUM_DOC 
	) as REV on D.NUM_DOC = REV.NUM_DOC
WHERE D.NUM_DOC IN (
SELECT distinct num_doc FROM DOCUMENTO
WHERE 
(D.SOCIEDAD_ID = @COCODE))
AND TST.SPRAS_ID = @SPRAS
AND TAT.SPRAS_ID = @SPRAS
GROUP BY D.NUM_DOC, TS.PADRE, TS.REVERSO, D.ESTATUS, F.USUARIOA_ID, D.SOCIEDAD_ID, D.PAIS_ID, D.FECHAD, D.HORAC, D.PERIODO, D.ESTATUS, D.ESTATUS_C
		,D.ESTATUS_SAP, D.ESTATUS_WF, F.TIPO, D.PAYER_ID, C.NAME1, C.CANAL, TST.TXT020, TAT.TXT50	 , D.CONCEPTO	 , D.MONTO_DOC_MD
	 , DF.FACTURA	 , DF.FACTURAK, D.USUARIOD_ID	 , D.DOCUMENTO_SAP	 ,DS.BLART	 , DS.KUNNR	 , DS.DESCR	 , DS.IMPORTE
	 , DS.CUENTA_C, CTA.CARGO, REV.REVERSO,  D.CUENTAP, D.CUENTAPL, D.CUENTACL
END

GO
/****** Object:  StoredProcedure [dbo].[CSP_DOCUMENTOSXUSER]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CSP_DOCUMENTOSXUSER]
	-- Add the parameters for the stored procedure here	
	@USUARIO NVARCHAR(16),
	@SPRAS NVARCHAR(2)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	

SELECT	  D.NUM_DOC
		, TS.PADRE
		, TS.REVERSO
		, CASE WHEN TS.PADRE = 1 THEN CASE WHEN COUNT(R.NUM_DOC)>0 THEN 'expand_more' else 'add' end ELSE '' END as BUTTON
		, D.ESTATUS_WF as ESTATUS
		, F.USUARIOA_ID
		, CASE WHEN D.ESTATUS = 'R' AND F.USUARIOA_ID = 'COKCXG24' THEN 'Edit' ELSE 'Details' END as NUM_DOC_TEXT
		, D.SOCIEDAD_ID
		, D.PAIS_ID
		, D.FECHAD
		, D.HORAC
		, D.PERIODO
		, ISNULL(D.ESTATUS,' ') + ISNULL(D.ESTATUS_C, ' ') + ISNULL(D.ESTATUS_SAP,' ')
     + ISNULL(D.ESTATUS_WF,' ') + ISNULL( F.TIPO,' ') + (CASE WHEN TS.PADRE = 1 THEN 'P' ELSE ' ' END ) 
	 + (CASE WHEN COUNT(R.NUM_DOC)> 0 THEN 'R' ELSE ' ' END) + ISNULL(CASE WHEN REV.REVERSO = 1 THEN '1' ELSE '0' END,' ')
	 AS ESTATUSS
	 , D.PAYER_ID
	 , C.NAME1
	 , C.CANAL
	 , TST.TXT020
	 , TAT.TXT50
	 , D.CONCEPTO
	 , D.MONTO_DOC_MD
	 , DF.FACTURA
	 , DF.FACTURAK
	 , D.USUARIOD_ID
	 , D.DOCUMENTO_SAP
	 ,DS.BLART
	 , DS.KUNNR
	 , DS.DESCR
	 , DS.IMPORTE
	 , DS.CUENTA_C
	 , CTA.CARGO
	 , D.CUENTAP
	 , D.CUENTAPL
	 , D.CUENTACL
FROM DOCUMENTO AS D
LEFT JOIN DOCUMENTOREC AS R
ON D.NUM_DOC = R.NUM_DOC
INNER JOIN TSOL AS TS
ON D.TSOL_ID = TS.ID
LEFT JOIN (SELECT F1.NUM_DOC, ESTATUS, F1.USUARIOA_ID, A.TIPO FROM FLUJO AS F1
		INNER JOIN (SELECT NUM_DOC, MAX(POS) AS POS FROM FLUJO GROUP BY NUM_DOC) AS F2
		ON  F1.NUM_DOC = F2.NUM_DOC
		AND F1.POS = F2.POS
		INNER JOIN WORKFP AS W
		ON W.ID = F1.WORKF_ID
		AND W.[VERSION] = F1.WF_VERSION
		AND W.POS = F1.WF_POS
		INNER JOIN ACCION AS A
		ON A.ID = W.ACCION_ID) AS F
	ON D.NUM_DOC = F.NUM_DOC
	INNER JOIN CLIENTE AS C
	ON D.VKORG = C.VKORG AND D.VTWEG = C.VTWEG AND D.SPART = C.SPART AND D.PAYER_ID = C.KUNNR
	INNER JOIN TSOLT AS TST
	ON TS.ID = TST.TSOL_ID
	INNER JOIN TALLT AS TAT
	ON D.TALL_ID = TAT.TALL_ID
	LEFT JOIN (SELECT NUM_DOC, FACTURA, FACTURAK FROM DOCUMENTOF WHERE POS = 1) AS DF
	ON D.NUM_DOC = DF.NUM_DOC
	LEFT JOIN DOCUMENTOSAP AS DS
	ON D.NUM_DOC = DS.NUM_DOC
	LEFT JOIN CUENTA AS CTA
	--ON D.SOCIEDAD_ID = CTA.SOCIEDAD_ID AND D.PAIS_ID = CTA.PAIS_ID AND D.TALL_ID = CTA.TALL_ID
	ON D.SOCIEDAD_ID = CTA.SOCIEDAD_ID AND D.TALL_ID = CTA.TALL_ID
	LEFT JOIN (
	select padre.NUM_DOC, t.REVERSO, hijo.DOCUMENTO_SAP from DOCUMENTO as padre
	 inner JOIN   DOCUMENTO as hijo 
	 on hijo.DOCUMENTO_REF = padre.NUM_DOC
	 inner join tsol as t
	 on hijo.TSOL_ID = t.ID
	 and t.REVERSO = 1
	 and hijo.DOCUMENTO_SAP is not null
	 --group by padre.NUM_DOC 
	) as REV on D.NUM_DOC = REV.NUM_DOC
WHERE D.NUM_DOC IN (
SELECT distinct num_doc FROM DOCUMENTO
WHERE 
(USUARIOC_ID = @USUARIO or USUARIOD_ID = @USUARIO)
union
SELECT distinct num_doc FROM FLUJO
WHERE (USUARIOA_ID = @USUARIO or USUARIOD_ID = @USUARIO)
)
AND TST.SPRAS_ID = @SPRAS
AND TAT.SPRAS_ID = @SPRAS
GROUP BY D.NUM_DOC, TS.PADRE, TS.REVERSO, D.ESTATUS, F.USUARIOA_ID, D.SOCIEDAD_ID, D.PAIS_ID, D.FECHAD, D.HORAC, D.PERIODO, D.ESTATUS, D.ESTATUS_C
		,D.ESTATUS_SAP, D.ESTATUS_WF, F.TIPO, D.PAYER_ID, C.NAME1, C.CANAL, TST.TXT020, TAT.TXT50	 , D.CONCEPTO	 , D.MONTO_DOC_MD
	 , DF.FACTURA	 , DF.FACTURAK, D.USUARIOD_ID	 , D.DOCUMENTO_SAP	 ,DS.BLART	 , DS.KUNNR	 , DS.DESCR	 , DS.IMPORTE
	 , DS.CUENTA_C, CTA.CARGO, REV.REVERSO,  D.CUENTAP, D.CUENTAPL, D.CUENTACL
END



GO
/****** Object:  StoredProcedure [dbo].[CSP_DOCUMENTOSXUSER_test]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CSP_DOCUMENTOSXUSER_test]
	-- Add the parameters for the stored procedure here	
	@SPRAS NVARCHAR(2)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	

SELECT	  D.NUM_DOC
		, TS.PADRE
		, TS.REVERSO
		, D.ESTATUS_WF as ESTATUS
		, F.USUARIOA_ID
		, D.SOCIEDAD_ID
		, D.PAIS_ID
		, D.FECHAD
		, D.HORAC
		, D.PERIODO
		, ISNULL(D.ESTATUS,' ') + ISNULL(D.ESTATUS_C, ' ') + ISNULL(D.ESTATUS_SAP,' ')
     + ISNULL(D.ESTATUS_WF,' ') + ISNULL( F.TIPO,' ') + (CASE WHEN TS.PADRE = 1 THEN 'P' ELSE ' ' END ) 
	 + (CASE WHEN COUNT(R.NUM_DOC)> 0 THEN 'R' ELSE ' ' END) + ISNULL(CASE WHEN REV.REVERSO = 1 THEN '1' ELSE '0' END,' ')
	 AS ESTATUSS
	 , D.PAYER_ID
	 , C.NAME1
	 , C.CANAL
	 , TST.TXT020
	 , TAT.TXT50
	 , D.CONCEPTO
	 , D.MONTO_DOC_MD
	 , DF.FACTURA
	 , DF.FACTURAK
	 , D.USUARIOD_ID
	 , D.DOCUMENTO_SAP
	 , DS.BLART
	 , DS.KUNNR
	 , DS.DESCR
	 , DS.IMPORTE
	 , DS.CUENTA_C
	 , CTA.CARGO
	 , D.CUENTAP
	 , D.CUENTAPL
	 , D.CUENTACL
	 , D.TIPO_RECURRENTE
	 , D.DOCUMENTO_REF
FROM DOCUMENTO AS D
LEFT JOIN DOCUMENTOREC AS R
ON D.NUM_DOC = R.NUM_DOC
INNER JOIN TSOL AS TS
ON D.TSOL_ID = TS.ID
LEFT JOIN (SELECT F1.NUM_DOC, ESTATUS, F1.USUARIOA_ID, A.TIPO FROM FLUJO AS F1
		INNER JOIN (SELECT NUM_DOC, MAX(POS) AS POS FROM FLUJO GROUP BY NUM_DOC) AS F2
		ON  F1.NUM_DOC = F2.NUM_DOC
		AND F1.POS = F2.POS
		INNER JOIN WORKFP AS W
		ON W.ID = F1.WORKF_ID
		AND W.[VERSION] = F1.WF_VERSION
		AND W.POS = F1.WF_POS
		INNER JOIN ACCION AS A
		ON A.ID = W.ACCION_ID) AS F
	ON D.NUM_DOC = F.NUM_DOC
	INNER JOIN CLIENTE AS C
	ON D.VKORG = C.VKORG AND D.VTWEG = C.VTWEG AND D.SPART = C.SPART AND D.PAYER_ID = C.KUNNR
	INNER JOIN TSOLT AS TST
	ON TS.ID = TST.TSOL_ID
	INNER JOIN TALLT AS TAT
	ON D.TALL_ID = TAT.TALL_ID
	LEFT JOIN (SELECT NUM_DOC, FACTURA, FACTURAK FROM DOCUMENTOF WHERE POS = 1) AS DF
	ON D.NUM_DOC = DF.NUM_DOC
	LEFT JOIN DOCUMENTOSAP AS DS
	ON D.NUM_DOC = DS.NUM_DOC
	LEFT JOIN CUENTA AS CTA
	--ON D.SOCIEDAD_ID = CTA.SOCIEDAD_ID AND D.PAIS_ID = CTA.PAIS_ID AND D.TALL_ID = CTA.TALL_ID
	ON D.SOCIEDAD_ID = CTA.SOCIEDAD_ID AND D.TALL_ID = CTA.TALL_ID
	LEFT JOIN (
	select padre.NUM_DOC, t.REVERSO, hijo.DOCUMENTO_SAP from DOCUMENTO as padre
	 inner JOIN   DOCUMENTO as hijo 
	 on hijo.DOCUMENTO_REF = padre.NUM_DOC
	 inner join tsol as t
	 on hijo.TSOL_ID = t.ID
	 and t.REVERSO = 1
	 and hijo.DOCUMENTO_SAP is not null
	 --group by padre.NUM_DOC 
	) as REV on D.NUM_DOC = REV.NUM_DOC
	where D.ESTATUS_SAP='P'
AND TST.SPRAS_ID = @SPRAS
AND TAT.SPRAS_ID = @SPRAS
GROUP BY D.NUM_DOC, TS.PADRE, TS.REVERSO, D.ESTATUS, F.USUARIOA_ID, D.SOCIEDAD_ID, D.PAIS_ID, D.FECHAD, D.HORAC, D.PERIODO, D.ESTATUS, D.ESTATUS_C
		,D.ESTATUS_SAP, D.ESTATUS_WF, F.TIPO, D.PAYER_ID, C.NAME1, C.CANAL, TST.TXT020, TAT.TXT50	 , D.CONCEPTO	 , D.MONTO_DOC_MD
	 , DF.FACTURA	 , DF.FACTURAK, D.USUARIOD_ID	 , D.DOCUMENTO_SAP	 ,DS.BLART	 , DS.KUNNR	 , DS.DESCR	 , DS.IMPORTE
	 , DS.CUENTA_C, CTA.CARGO, REV.REVERSO,  D.CUENTAP, D.CUENTAPL, D.CUENTACL, D.TIPO_RECURRENTE, D.DOCUMENTO_REF
END



GO
/****** Object:  StoredProcedure [dbo].[CSP_DOCUMENTOSXUSER2]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CSP_DOCUMENTOSXUSER2]
	-- Add the parameters for the stored procedure here	
	@USUARIO NVARCHAR(16),
	@SPRAS NVARCHAR(2)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	

SELECT	  D.NUM_DOC
		, TS.PADRE
		, TS.REVERSO
		, CASE WHEN TS.PADRE = 1 THEN CASE WHEN COUNT(R.NUM_DOC)>0 THEN 'expand_more' else 'add' end ELSE '' END as BUTTON
		, D.ESTATUS_WF as ESTATUS
		, F.USUARIOA_ID
		, CASE WHEN D.ESTATUS = 'R' AND F.USUARIOA_ID = 'COKCXG24' THEN 'Edit' ELSE 'Details' END as NUM_DOC_TEXT
		, D.SOCIEDAD_ID
		, D.PAIS_ID
		, D.FECHAD
		, D.HORAC
		, D.PERIODO
		, ISNULL(D.ESTATUS,' ') + ISNULL(D.ESTATUS_C, ' ') + ISNULL(D.ESTATUS_SAP,' ')
     + ISNULL(D.ESTATUS_WF,' ') + ISNULL( F.TIPO,' ') + (CASE WHEN TS.PADRE = 1 THEN 'P' ELSE ' ' END ) 
	 + (CASE WHEN COUNT(R.NUM_DOC)> 0 THEN 'R' ELSE ' ' END) + ISNULL(CASE WHEN REV.REVERSO = 1 THEN '1' ELSE '0' END,' ')
	 AS ESTATUSS
	 , D.PAYER_ID
	 , C.NAME1
	 , C.CANAL
	 , TST.TXT020
	 , TAT.TXT50
	 , D.CONCEPTO
	 , D.MONTO_DOC_MD
	 , DF.FACTURA
	 , DF.FACTURAK
	 , D.USUARIOD_ID
	 , D.DOCUMENTO_SAP
	 ,DS.BLART
	 , DS.KUNNR
	 , DS.DESCR
	 , DS.IMPORTE
	 , DS.CUENTA_C
	 , CTA.CARGO
	 , D.CUENTAP
	 , D.CUENTAPL
	 , D.CUENTACL
	 , D.TIPO_RECURRENTE
	 , D.DOCUMENTO_REF
	 ,(SELECT COUNT(*) FROM DOCUMENTO doc WHERE doc.DOCUMENTO_REF=D.NUM_DOC) as nRelacionadas
	 ,(SELECT COUNT(*) FROM DOCUMENTOREC dr where  dr.NUM_DOC=D.NUM_DOC and dr.DOC_REF!=0) as nRecurrentes
	 ,(SELECT TOP 1 ESTATUS FROM(SELECT ESTATUS, MAX(POS) AS POS FROM FLUJO flu where flu.NUM_DOC=D.NUM_DOC and flu.USUARIOA_ID=@USUARIO group by ESTATUS) as ESTATUSWF_USER ORDER BY POS DESC) as ESTATUS_WF_USER
FROM DOCUMENTO AS D
LEFT JOIN DOCUMENTOREC AS R
ON D.NUM_DOC = R.NUM_DOC
INNER JOIN TSOL AS TS
ON D.TSOL_ID = TS.ID
LEFT JOIN (SELECT F1.NUM_DOC, ESTATUS, F1.USUARIOA_ID, A.TIPO FROM FLUJO AS F1
		INNER JOIN (SELECT NUM_DOC, MAX(POS) AS POS FROM FLUJO GROUP BY NUM_DOC) AS F2
		ON  F1.NUM_DOC = F2.NUM_DOC
		AND F1.POS = F2.POS
		INNER JOIN WORKFP AS W
		ON W.ID = F1.WORKF_ID
		AND W.[VERSION] = F1.WF_VERSION
		AND W.POS = F1.WF_POS
		INNER JOIN ACCION AS A
		ON A.ID = W.ACCION_ID) AS F
	ON D.NUM_DOC = F.NUM_DOC
	INNER JOIN CLIENTE AS C
	ON D.VKORG = C.VKORG AND D.VTWEG = C.VTWEG AND D.SPART = C.SPART AND D.PAYER_ID = C.KUNNR
	INNER JOIN TSOLT AS TST
	ON TS.ID = TST.TSOL_ID
	INNER JOIN TALLT AS TAT
	ON D.TALL_ID = TAT.TALL_ID
	LEFT JOIN (SELECT NUM_DOC, FACTURA, FACTURAK FROM DOCUMENTOF WHERE POS = 1) AS DF
	ON D.NUM_DOC = DF.NUM_DOC
	LEFT JOIN DOCUMENTOSAP AS DS
	ON D.NUM_DOC = DS.NUM_DOC
	LEFT JOIN CUENTA AS CTA
	--ON D.SOCIEDAD_ID = CTA.SOCIEDAD_ID AND D.PAIS_ID = CTA.PAIS_ID AND D.TALL_ID = CTA.TALL_ID
	ON D.SOCIEDAD_ID = CTA.SOCIEDAD_ID AND D.TALL_ID = CTA.TALL_ID
	LEFT JOIN (
	select padre.NUM_DOC, t.REVERSO, hijo.DOCUMENTO_SAP from DOCUMENTO as padre
	 inner JOIN   DOCUMENTO as hijo 
	 on hijo.DOCUMENTO_REF = padre.NUM_DOC
	 inner join tsol as t
	 on hijo.TSOL_ID = t.ID
	 and t.REVERSO = 1
	 and hijo.DOCUMENTO_SAP is not null
	 --group by padre.NUM_DOC 
	) as REV on D.NUM_DOC = REV.NUM_DOC
WHERE D.NUM_DOC IN (
SELECT distinct num_doc FROM DOCUMENTO
WHERE 
(USUARIOC_ID = @USUARIO or USUARIOD_ID = @USUARIO)
union
SELECT distinct num_doc FROM FLUJO
WHERE (USUARIOA_ID = @USUARIO or USUARIOD_ID = @USUARIO)
)
AND TST.SPRAS_ID = @SPRAS
AND TAT.SPRAS_ID = @SPRAS
GROUP BY D.NUM_DOC, TS.PADRE, TS.REVERSO, D.ESTATUS, F.USUARIOA_ID, D.SOCIEDAD_ID, D.PAIS_ID, D.FECHAD, D.HORAC, D.PERIODO, D.ESTATUS, D.ESTATUS_C
		,D.ESTATUS_SAP, D.ESTATUS_WF, F.TIPO, D.PAYER_ID, C.NAME1, C.CANAL, TST.TXT020, TAT.TXT50	 , D.CONCEPTO	 , D.MONTO_DOC_MD
	 , DF.FACTURA	 , DF.FACTURAK, D.USUARIOD_ID	 , D.DOCUMENTO_SAP	 ,DS.BLART	 , DS.KUNNR	 , DS.DESCR	 , DS.IMPORTE
	 , DS.CUENTA_C, CTA.CARGO, REV.REVERSO,  D.CUENTAP, D.CUENTAPL, D.CUENTACL, D.TIPO_RECURRENTE, D.DOCUMENTO_REF
END



GO
/****** Object:  StoredProcedure [dbo].[CSP_LISTA_TALL_CUENTA]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CSP_LISTA_TALL_CUENTA] 
    @SPRAS_ID    NCHAR(2),
	@PAIS_ID     NCHAR(2)      = NULL,
	@EJERCICIO   INT,
	@SOCIEDAD_ID NCHAR(4)      = NULL,
	@PREFIX      NVARCHAR(MAX) ='',
    @ACCION      INT           = 2
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF @ACCION=1 --ACCION_LISTA_TALLCONCUENTA
	BEGIN
	SELECT t.[DESCRIPCION] AS [TXT50], t.[ID] AS [TALL_ID]  ,'EN' AS [SPRAS_ID] FROM CUENTA c 
		INNER JOIN TALL t ON c.TALL_ID=t.ID
		WHERE 
		t.[ACTIVO]=1 
		AND c.SOCIEDAD_ID=@SOCIEDAD_ID 
		AND c.PAIS_ID=@PAIS_ID
		AND c.EJERCICIO=@EJERCICIO
		AND t.DESCRIPCION LIKE '%'+@PREFIX+'%'
	END

	IF @ACCION=2 --ACCION_LISTA_TALLTCONCUENTA
	BEGIN
		SELECT tt.* FROM CUENTA c 
		INNER JOIN TALL t ON c.TALL_ID=t.ID
		INNER JOIN TALLT tt ON t.ID=tt.TALL_ID AND tt.SPRAS_ID=@SPRAS_ID
		WHERE 
		t.[ACTIVO]=1 
		AND c.SOCIEDAD_ID=@SOCIEDAD_ID 
		AND c.PAIS_ID=@PAIS_ID
		AND c.EJERCICIO=@EJERCICIO
		AND tt.[TXT50] LIKE '%'+@PREFIX+'%'
	    
	END
   
END

GO
/****** Object:  StoredProcedure [dbo].[CSP_MASIVA_CLIENTES]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL

-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.

-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
-- =============================================
-- --Author:		<Author,,Name>
-- --Create date: <Create Date,,>
-- --Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CSP_MASIVA_CLIENTES]

    @ACCION   INT = 0,
	@CLIENTES dbo.[ClientesTableType] READONLY 
	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF @ACCION=1 --ACCION_MASIVA_CLIENTES_PROCESAR
	BEGIN
	DECLARE @ClientesAux [ClientesTableType]
	DECLARE @ClientesAuxErr [ClientesTableType]

	DECLARE 
	@BUKRS [NVARCHAR](250) = '',
	@LAND [NVARCHAR](250) = '',
	@KUNNR [NVARCHAR](250) = '',
	@VKORG [NVARCHAR](250) = '',
	@VTWEG [NVARCHAR](250) = '',
	@SPART [NVARCHAR](250) = '',
	@CLIENTE_N [NVARCHAR](250) = '',
	@ID_US0 [NVARCHAR](250) = '',
	@ID_US1 [NVARCHAR](250) = '',
	@ID_US2 [NVARCHAR](250) = '',
	@ID_US3 [NVARCHAR](250) = '',
	@ID_US4 [NVARCHAR](250) = '',
	@ID_US5 [NVARCHAR](250) = '',
	@ID_US6 [NVARCHAR](250) = '',
	@ID_US7 [NVARCHAR](250) = '',
	@ID_PROVEEDOR [NVARCHAR](250) = '',
	@BANNER [NVARCHAR](250) = '',
	@BANNERG [NVARCHAR](250) = '',
	@CANAL [NVARCHAR](250) = '',
	@EXPORTACION [NVARCHAR](250) = '',
	@CONTACTO [NVARCHAR](250) = '',
	@CONTACTOE [NVARCHAR](250) = '',
	@MESS [NVARCHAR](MAX) = '',
	@CONT INT,
	@EXISTE INT;

	DECLARE CR CURSOR FAST_FORWARD FOR
	SELECT * FROM @CLIENTES FOR READ ONLY;

	OPEN CR;

	FETCH CR INTO @BUKRS,@LAND,@KUNNR,@VKORG,@VTWEG,@SPART,@CLIENTE_N,
	@ID_US0,@ID_US1,@ID_US2,@ID_US3,@ID_US4,@ID_US5,@ID_US6,@ID_US7,
	@ID_PROVEEDOR,@BANNER,@BANNERG,@CANAL,@EXPORTACION,@CONTACTO,@CONTACTOE,
	@MESS;

	WHILE @@FETCH_STATUS = 0
	BEGIN

	FETCH CR INTO @BUKRS,@LAND,@KUNNR,@VKORG,@VTWEG,@SPART,@CLIENTE_N,
	@ID_US0,@ID_US1,@ID_US2,@ID_US3,@ID_US4,@ID_US5,@ID_US6,@ID_US7,
	@ID_PROVEEDOR,@BANNER,@BANNERG,@CANAL,@EXPORTACION,@CONTACTO,@CONTACTOE,
	@MESS;

	SET @CONT=1;
	SET @MESS='';
	SET @EXISTE=0;
	-------------------------------CoCode
	SET @BUKRS=UPPER(LTRIM(RTRIM(ISNULL(@BUKRS,''))));
	IF NOT EXISTS (SELECT 1 FROM PAIS WHERE SOCIEDAD_ID = @BUKRS AND ACTIVO=1)
	BEGIN
	    SET @BUKRS= @BUKRS+ '?';
		SET @MESS=CAST(@CONT AS NVARCHAR(10))+'. Error con el Co. Code </BR>';
		SET @CONT=@CONT+1;
	END
	-------------------------------Pais
	SET @LAND=UPPER(LTRIM(RTRIM(ISNULL(@LAND,''))));
	IF NOT EXISTS (SELECT 1 FROM PAIS WHERE LAND = @LAND AND ACTIVO=1)
	BEGIN
	    SET @LAND= @LAND+ '?';
		SET @MESS=@MESS+CAST(@CONT AS NVARCHAR(10))+'. Error con el País </BR>';
		SET @CONT=@CONT+1;
	END
	-------------------------------CLIENTE
	IF NOT EXISTS (SELECT 1 FROM CLIENTE WHERE KUNNR = @KUNNR AND ACTIVO=1)
	BEGIN
	    SET @KUNNR= @KUNNR+ '?';
		SET @MESS=@MESS+CAST(@CONT AS NVARCHAR(10))+'. Error con el Cliente </BR>';
		SET @CONT=@CONT+1;
	END
	ELSE
	BEGIN
	    IF EXISTS (SELECT 1 FROM @ClientesAuxErr WHERE KUNNR = @KUNNR  ) OR EXISTS (SELECT 1 FROM @ClientesAux WHERE KUNNR = @KUNNR  )
		BEGIN
			SET @EXISTE=1;
		END
		SELECT @VKORG=VKORG,@VTWEG=VTWEG,@SPART=SPART,
		@CLIENTE_N=ISNULL(@CLIENTE_N,NAME1),
		@ID_PROVEEDOR=ISNULL(@ID_PROVEEDOR,PROVEEDOR_ID),
		@BANNER=ISNULL(@BANNER,BANNER),
		@BANNERG=ISNULL(@BANNERG,BANNERG),
		@CANAL=ISNULL(@CANAL,CANAL)    
		FROM CLIENTE WHERE KUNNR = @KUNNR AND ACTIVO=1;
		
	END
	-------------------------------NOMBRE DEL CLIENTE
	SET @CLIENTE_N=UPPER(@CLIENTE_N);
	IF @CLIENTE_N IS NULL OR @CLIENTE_N=''
	BEGIN
	    SET @CLIENTE_N= @CLIENTE_N+ '?';
		SET @MESS=@MESS+CAST(@CONT AS NVARCHAR(10))+'. Error en el Nombre del Cliente </BR>';
		SET @CONT=@CONT+1;
	END

	--------------------------------Manager
	SET @ID_US0=UPPER(LTRIM(RTRIM(ISNULL(@ID_US0,''))));
	IF NOT EXISTS (SELECT 1 FROM USUARIO WHERE (ID = @ID_US0 AND ACTIVO=1 AND PUESTO_ID NOT IN (1,14)) OR (@ID_US0=''))
	BEGIN
	    SET @ID_US0= @ID_US0+ '?';
		SET @MESS=@MESS+CAST(@CONT AS NVARCHAR(10))+'. Error en el Nivel 0 </BR>';
		SET @CONT=@CONT+1;
	END
	--------------------------------Nivel1
	SET @ID_US1=UPPER(LTRIM(RTRIM(ISNULL(@ID_US1,''))));
	IF NOT EXISTS (SELECT 1 FROM USUARIO WHERE ID = @ID_US1 AND ACTIVO=1 AND PUESTO_ID NOT IN (1,14))
	BEGIN
	    SET @ID_US1= @ID_US1+ '?';
		SET @MESS=@MESS+CAST(@CONT AS NVARCHAR(10))+'. Error en el Nivel 1 </BR>';
		SET @CONT=@CONT+1;
	END
	--------------------------------Nivel2
	SET @ID_US2=UPPER(LTRIM(RTRIM(ISNULL(@ID_US2,''))));
	IF NOT EXISTS (SELECT 1 FROM USUARIO WHERE ID = @ID_US2 AND ACTIVO=1 AND PUESTO_ID NOT IN (1,14))
	BEGIN
	    SET @ID_US2= @ID_US2+ '?';
		SET @MESS=@MESS+CAST(@CONT AS NVARCHAR(10))+'. Error en el Nivel 2 </BR>';
		SET @CONT=@CONT+1;
	END
	--------------------------------Nivel3
	SET @ID_US3=UPPER(LTRIM(RTRIM(ISNULL(@ID_US3,''))));
	IF NOT EXISTS (SELECT 1 FROM USUARIO WHERE (ID = @ID_US3 AND ACTIVO=1 AND PUESTO_ID NOT IN (1,14))OR (@ID_US3=''))
	BEGIN
	    SET @ID_US3= @ID_US3+ '?';
		SET @MESS=@MESS+CAST(@CONT AS NVARCHAR(10))+'. Error en el Nivel 3 </BR>';
		SET @CONT=@CONT+1;
	END
	--------------------------------Nivel4
	SET @ID_US4=UPPER(LTRIM(RTRIM(ISNULL(@ID_US4,''))));
	IF NOT EXISTS (SELECT 1 FROM USUARIO WHERE (ID = @ID_US4 AND ACTIVO=1 AND PUESTO_ID NOT IN (1,14)) OR (@ID_US4=''))
	BEGIN
	    SET @ID_US4= @ID_US4+ '?';
		SET @MESS=@MESS+CAST(@CONT AS NVARCHAR(10))+'. Error en el Nivel 4 </BR>';
		SET @CONT=@CONT+1;
	END
	--------------------------------Nivel5
	SET @ID_US5=UPPER(LTRIM(RTRIM(ISNULL(@ID_US5,''))));
	IF NOT EXISTS (SELECT 1 FROM USUARIO WHERE (ID = @ID_US5 AND ACTIVO=1 AND PUESTO_ID NOT IN (1,14)) OR (@ID_US5=''))
	BEGIN
	    SET @ID_US5= @ID_US5+ '?';
		SET @MESS=@MESS+CAST(@CONT AS NVARCHAR(10))+'. Error en el Nivel 5 </BR>';
		SET @CONT=@CONT+1;
	END
	--------------------------------Nivel6
	SET @ID_US6=UPPER(LTRIM(RTRIM(ISNULL(@ID_US6,''))));
	IF NOT EXISTS (SELECT 1 FROM USUARIO WHERE (ID = @ID_US6 AND ACTIVO=1 AND PUESTO_ID NOT IN (1,14) AND PUESTO_ID=8) OR (@ID_US6=''))
	BEGIN
	    SET @ID_US6= @ID_US6+ '?';
		SET @MESS=@MESS+CAST(@CONT AS NVARCHAR(10))+'. Error en el Nivel 6 </BR>';
		SET @CONT=@CONT+1;
	END
	--------------------------------Nivel7
	SET @ID_US7=UPPER(LTRIM(RTRIM(ISNULL(@ID_US7,''))));
	IF NOT EXISTS (SELECT 1 FROM USUARIO WHERE (ID = @ID_US7 AND ACTIVO=1 AND PUESTO_ID NOT IN (1,14) AND PUESTO_ID=9) OR (@ID_US7=''))
	BEGIN
	    SET @ID_US7= @ID_US7+ '?';
		SET @MESS=@MESS+CAST(@CONT AS NVARCHAR(10))+'. Error en el Nivel 7 </BR>';
		SET @CONT=@CONT+1;
	END

	-------------------------------ID_PROVEEDOR
	SET @ID_PROVEEDOR=ISNULL(@ID_PROVEEDOR,'');
	IF NOT EXISTS (SELECT 1 FROM PROVEEDOR WHERE ID = @ID_PROVEEDOR AND ACTIVO=1) AND (@ID_PROVEEDOR<>'')
	BEGIN
	    SET @ID_PROVEEDOR= @ID_PROVEEDOR+ '?';
		SET @MESS=@MESS+CAST(@CONT AS NVARCHAR(10))+'. Error en el Vendor </BR>';
		SET @CONT=@CONT+1;
	END
	--------------------------------Banner
	SET @BANNER=ISNULL(@BANNER,'')
	--------------------------------Banner Agrupador
	SET @BANNERG=ISNULL(@BANNERG,'')
	--------------------------------CANAL
	SET @CANAL=ISNULL(@CANAL,'');
	IF NOT EXISTS (SELECT 1 FROM CANAL WHERE CANAL = @CANAL) AND ( @CANAL<>'')
	BEGIN
	    SET @CANAL= @CANAL+ '?';
		SET @MESS=@MESS+CAST(@CONT AS NVARCHAR(10))+'. Error en el Canal </BR>';
		SET @CONT=@CONT+1;
	END
	--------------------------------EXPORTACION
	SET @EXPORTACION=UPPER(LTRIM(RTRIM(ISNULL(@EXPORTACION,''))));
	--------------------------------CONTACTO
	SET @CONTACTO=UPPER(LTRIM(RTRIM(ISNULL(@CONTACTO,''))));
	--------------------------------EMAIL
	SET @CONTACTOE=LTRIM(RTRIM(ISNULL(@CONTACTOE,'')));
	IF NOT EXISTS (SELECT 1 WHERE @CONTACTOE LIKE '%_@__%.__%' AND PATINDEX('%[^a-z,0-9,@,.,_,\-]%', @CONTACTOE) = 0)
	BEGIN
	    SET @CONTACTOE= @CONTACTOE+ '?';
		SET @MESS=@MESS+CAST(@CONT AS NVARCHAR(10))+'. Error en el Correo </BR>';
		SET @CONT=@CONT+1;
	END
               


    IF @EXISTE=0
	BEGIN            
		IF @MESS = ''
		BEGIN
			INSERT INTO @ClientesAux VALUES
			(@BUKRS,@LAND,@KUNNR,@VKORG,@VTWEG,@SPART,@CLIENTE_N,
			@ID_US0,@ID_US1,@ID_US2,@ID_US3,@ID_US4,@ID_US5,@ID_US6,@ID_US7,
			@ID_PROVEEDOR,@BANNER,@BANNERG,@CANAL,@EXPORTACION,@CONTACTO,@CONTACTOE,
			@MESS); 
		END
		ELSE
		BEGIN
			INSERT INTO @ClientesAuxErr VALUES
			(@BUKRS,@LAND,@KUNNR,@VKORG,@VTWEG,@SPART,@CLIENTE_N,
			@ID_US0,@ID_US1,@ID_US2,@ID_US3,@ID_US4,@ID_US5,@ID_US6,@ID_US7,
			@ID_PROVEEDOR,@BANNER,@BANNERG,@CANAL,@EXPORTACION,@CONTACTO,@CONTACTOE,
			@MESS); 
		END
	END
	
	END
	CLOSE CR;
	DEALLOCATE CR;

	INSERT INTO @ClientesAuxErr 
	SELECT * FROM @ClientesAux;
	
	SELECT * FROM @ClientesAuxErr;

	END
	
	IF @ACCION=2 --ACCION_MASIVA_CLIENTES_GUARDAR
	BEGIN
	    -- MAS
		UPDATE  CLIENTEF SET ACTIVO=0,FECHAM=GETDATE()
		FROM CLIENTEF  WHERE [KUNNR] IN (SELECT [KUNNR] FROM @CLIENTES);

		INSERT INTO CLIENTEF (
		[VKORG],[VTWEG],[SPART],[KUNNR],[VERSION]
		,[USUARIO0_ID],[USUARIO1_ID],[USUARIO2_ID],[USUARIO3_ID],[USUARIO4_ID],[USUARIO5_ID],[USUARIO6_ID]
        ,[ACTIVO],[FECHAC],[FECHAM],[USUARIO7_ID])
	  SELECT 
	  [VKORG],[VTWEG],[SPART],[KUNNR],(SELECT MAX([VERSION]) FROM  CLIENTEF cf WHERE cf.[KUNNR]=cli.[KUNNR])+1
	  ,[ID_US0],[ID_US1],[ID_US2],[ID_US3],[ID_US4],[ID_US5],[ID_US6]
	  ,1,GETDATE(),GETDATE(),[ID_US7] 
	  FROM @CLIENTES cli WHERE cli.[KUNNR] IN (SELECT [KUNNR] FROM CLIENTEF);

	  -- NUEVOS
	  INSERT INTO CLIENTEF (
		[VKORG],[VTWEG],[SPART],[KUNNR],[VERSION]
		,[USUARIO0_ID],[USUARIO1_ID],[USUARIO2_ID],[USUARIO3_ID],[USUARIO4_ID],[USUARIO5_ID],[USUARIO6_ID]
        ,[ACTIVO],[FECHAC],[FECHAM],[USUARIO7_ID])
	  SELECT 
	  [VKORG],[VTWEG],[SPART],[KUNNR],1
	  ,[ID_US0],[ID_US1],[ID_US2],[ID_US3],[ID_US4],[ID_US5],[ID_US6]
	  ,1,GETDATE(),NULL,[ID_US7] 
	  FROM @CLIENTES cli WHERE cli.[KUNNR] NOT IN (SELECT [KUNNR] FROM CLIENTEF);

	  UPDATE CLIENTE SET 
	  NAME1 = ISNULL(cli.CLIENTE_N,c.NAME1),
	  LAND= ISNULL(cli.LAND,c.LAND),
	  PROVEEDOR_ID=cli.ID_PROVEEDOR,	  
	  BANNER=cli.BANNER,
	  BANNERG=cli.BANNERG,
	  CANAL=cli.CANAL,
	  EXPORTACION=cli.EXPORTACION,
	  CONTAC=cli.CONTACTO,
	  CONT_EMAIL=cli.CONTACTOE
	  FROM CLIENTE c INNER JOIN  @CLIENTES cli  ON cli.[KUNNR]=c.[KUNNR]

	  UPDATE CONTACTOC SET [DEFECTO]=0
	  FROM CONTACTOC c  
	  WHERE c.KUNNR IN (SELECT KUNNR FROM @CLIENTES cli WHERE cli.CONTACTO IS NOT NULL AND (c.[NOMBRE]<> cli.CONTACTO OR c.[EMAIL]<>cli.CONTACTOE ))

	  UPDATE CONTACTOC SET [DEFECTO]=1
	  FROM CONTACTOC c  
	  WHERE c.KUNNR IN (SELECT KUNNR FROM @CLIENTES cli WHERE cli.CONTACTO IS NOT NULL AND c.[NOMBRE]= cli.CONTACTO AND c.[EMAIL]=cli.CONTACTOE )


	  INSERT INTO CONTACTOC (NOMBRE,[EMAIL],[VKORG],[VTWEG],[SPART],[KUNNR],[ACTIVO],[DEFECTO])
	  SELECT CONTACTO,CONTACTOE,VKORG,VTWEG,SPART,KUNNR,1,1
	  FROM @CLIENTES cli 
	  WHERE cli.CONTACTO IS NOT NULL AND KUNNR NOT IN (SELECT KUNNR FROM CONTACTOC c WHERE c.[NOMBRE]= cli.CONTACTO AND c.[EMAIL]=cli.CONTACTOE)




	END


END

GO
/****** Object:  StoredProcedure [dbo].[CSP_PERMISO]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CSP_PERMISO]
	-- Add the parameters for the stored procedure here
	@ID		nvarchar(16),
	@ACCION int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF @ACCION = 1
	BEGIN
		--INSERT USUARIO( ID, PASS, NOMBRE, APELLIDO_P, APELLIDO_M, EMAIL, ACTIVO)
		--VALUES ( @ID, @PASS, @NOMBRE, @APELLIDO_P, @APELLIDO_M, @EMAIL, 'TRUE')

		SELECT U.ID, P.ID AS ID_PAG, P.URL, CT.TXT50 AS TITULO, C.ID AS CAR_ID, C.URL AS CAR_URL, P.ICON AS ICONO
		FROM USUARIO AS U 
		INNER JOIN MIEMBROS AS M
		ON U.ID = M.USUARIO_ID
		INNER JOIN PERMISO_PAGINA AS PP
		ON M.ROL_ID = PP.ROL_ID
		INNER JOIN PAGINA AS P
		ON PP.PAGINA_ID = P.ID
		INNER JOIN CARPETA AS C
		ON P.CARPETA_ID = C.ID		
		INNER JOIN PAGINAT AS CT
		ON P.ID = CT.ID
		WHERE U.ID = @ID
		AND PP.PERMISO = 1
		AND CT.SPRAS_ID = U.SPRAS_ID
	END
	IF @ACCION = 2
	BEGIN 
		SELECT U.ID, P.ID AS ID_PAG, P.URL, CT.TXT50 AS TITULO, C.ID AS CAR_ID, C.URL AS CAR_URL, P.ICON AS ICONO
		FROM USUARIO AS U 
		INNER JOIN MIEMBROS AS M
		ON U.ID = M.USUARIO_ID
		INNER JOIN PERMISO_PAGINA AS PP
		ON M.ROL_ID = PP.ROL_ID
		INNER JOIN PAGINA AS P
		ON PP.PAGINA_ID = P.ID
		INNER JOIN CARPETA AS C
		ON P.CARPETA_ID = C.ID		
		INNER JOIN PAGINAT AS CT
		ON P.ID = CT.ID
		WHERE U.ID = @ID
		AND PP.PERMISO = 1
		AND CT.SPRAS_ID = U.SPRAS_ID
	END
END



GO
/****** Object:  StoredProcedure [dbo].[CSP_PRESU_CLIENT]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CSP_PRESU_CLIENT]
  @CLIENTE NVARCHAR(10),
  @PERIODO NCHAR(2) 
AS
DECLARE @PRESUPUESTOS TABLE(
  DESCRIPCION NVARCHAR(100),
  VALOR FLOAT
  )
DECLARE 
@CLIENTEA NVARCHAR(50),
@BANERG NVARCHAR(50),
@BANNER NVARCHAR(50),
@CANAL  NVARCHAR(50);

BEGIN
SELECT TOP 1 @CLIENTEA = IDUSUARIO FROM USUARIOSAP WHERE AUTOMATICO = 1;
SELECT TOP 1 @BANERG = BANNERG FROM CLIENTE WHERE KUNNR = @CLIENTE
SELECT TOP 1 @CANAL=CANAL FROM CLIENTE WHERE KUNNR = @CLIENTE


SELECT * INTO #tbBannersG FROM (
SELECT DISTINCT BANNERG FROM CLIENTE WHERE CANAL=@CANAL  AND BANNERG IS NOT NULL  
UNION
SELECT DISTINCT BANNER AS BANNERG  FROM CLIENTE WHERE CANAL=@CANAL  AND BANNER IS NOT NULL AND BANNERG IS NULL
) as tmp


IF @BANERG <> '' OR @BANERG IS NOT NULL
BEGIN
	SELECT TOP 1 @BANNER = RTRIM('%'+ SUBSTRING(BANNERG, PATINDEX('%[^0]%', BANNERG+'.'), LEN(BANNERG))) FROM CLIENTE WHERE KUNNR = @CLIENTE
	INSERT INTO @PRESUPUESTOS
	SELECT
	'PRESUPUESTO_CANAL'
	,ISNULL(SUM(C.ADVER + C.CONPR+C.OTHTA + SPA  + C.POP + C.DSTRB + C.CSHDC +CORPM+ C.FREEG + C.PMVAR + C.RECUN + C.RSRDV), 0) AS PPTOC
	FROM PRESUPUESTOP AS C
	WHERE C.BANNER IN (select BANNERG FROM #tbBannersG)
	AND C.MES = @PERIODO
	AND C.ID  = (select max(id) from PRESUPUESTOP  WHERE BANNER IN (select BANNERG FROM #tbBannersG));

	INSERT INTO @PRESUPUESTOS
	SELECT
	'PRESUPUESTO_BANNER',
	ISNULL(SUM(C.ADVER + C.CONPR + C.POP + C.DSTRB + C.CSHDC +CORPM+ C.FREEG + C.PMVAR + C.RECUN + C.RSRDV), 0) AS PPTOC
	FROM PRESUPUESTOP AS C
	WHERE C.BANNER LIKE @BANNER
	AND C.MES = @PERIODO
	AND C.ID  = (select max(id) from PRESUPUESTOP WHERE BANNER LIKE @BANNER );
	END
ELSE
BEGIN
	SELECT TOP 1 @BANNER = RTRIM('%'+ SUBSTRING(BANNER, PATINDEX('%[^0]%', BANNER+'.'), LEN(BANNER))) FROM CLIENTE WHERE KUNNR = @CLIENTE
	INSERT INTO @PRESUPUESTOS
	SELECT 
	'PRESUPUESTO_CANAL'
	,ISNULL(SUM(C.ADVER + C.CONPR+C.OTHTA + SPA  + C.POP + C.DSTRB + C.CSHDC + C.FREEG + C.PMVAR + C.RECUN + C.RSRDV), 0) AS PPTOC
	FROM PRESUPUESTOP AS C
	WHERE C.BANNER IN (select BANNERG FROM #tbBannersG)
	AND C.MES = @PERIODO
	AND C.ID  = (select max(id) from PRESUPUESTOP  WHERE BANNER IN (select BANNERG FROM #tbBannersG));
			
	INSERT INTO @PRESUPUESTOS
	SELECT
	'PRESUPUESTO_BANNER'
	,ISNULL(SUM(C.ADVER + C.CONPR + C.POP + C.DSTRB + C.CSHDC + C.FREEG + C.PMVAR + C.RECUN + C.RSRDV), 0) AS PPTOC
	FROM PRESUPUESTOP AS C
	WHERE C.BANNER LIKE @BANNER
	AND C.MES = @PERIODO
	AND C.ID  = (select max(id) from PRESUPUESTOP WHERE BANNER LIKE @BANNER );
END


INSERT INTO @PRESUPUESTOS
  SELECT
    'PRESUPUESTO_CLIENTE'
   ,ABS(ISNULL(SUM(S.CSHDC + S.RECUN + S.OTHTA + S.SPA + S.FREEG + S.CONPR + S.RSRDV + S.CORPM + S.POP + S.PMVAR + S.ADVER + S.DSTRB + VVX17 + OHV + NETLB + GRSLS), 0)) AS CONSU
  FROM PRESUPSAPP AS S
  WHERE S.KUNNR = @CLIENTE
  AND S.PERIOD = @PERIODO;

INSERT INTO @PRESUPUESTOS
  SELECT
    'PRESUPUESTO_PROCESO'
   ,ISNULL(SUM(S.MONTO_DOC_MD), 0) AS CONSU
  FROM DOCUMENTO AS S
  WHERE S.PAYER_ID = @CLIENTE
  AND S.PERIODO = @PERIODO;

INSERT INTO @PRESUPUESTOS
  SELECT
    'CONSUMO_F'
    ,ISNULL(SUM(S.CSHDC + S.RECUN + S.OTHTA + S.SPA + S.FREEG + S.CONPR + S.RSRDV + S.CORPM + S.POP + S.PMVAR + S.ADVER + S.DSTRB + VVX17 + OHV + NETLB + GRSLS), 0) AS CONSU
  FROM PRESUPSAPP AS S
  WHERE S.TYPE = 'F'
  AND S.KUNNR = @CLIENTE
  AND S.PERIOD = @PERIODO;

INSERT INTO @PRESUPUESTOS
  SELECT
    'CONSUMO_B'
    ,ISNULL(SUM(S.CSHDC + S.RECUN + S.OTHTA + S.SPA + S.FREEG + S.CONPR + S.RSRDV + S.CORPM + S.POP + S.PMVAR + S.ADVER + S.DSTRB + VVX17 + OHV + NETLB + GRSLS), 0) AS CONSU
  FROM PRESUPSAPP AS S
  WHERE S.TYPE = 'B'
    AND S.UNAME = @CLIENTEA
    AND S.KUNNR = @CLIENTE
    AND S.PERIOD = @PERIODO;

INSERT INTO @PRESUPUESTOS
  SELECT
    'CONSUMO_BM'
    ,ISNULL(SUM(S.CSHDC + S.RECUN + S.OTHTA + S.SPA + S.FREEG + S.CONPR + S.RSRDV + S.CORPM + S.POP + S.PMVAR + S.ADVER + S.DSTRB + VVX17 + OHV + NETLB + GRSLS), 0) AS CONSU
  FROM PRESUPSAPP AS S
  WHERE S.TYPE = 'B'
    AND S.UNAME != @CLIENTEA
  AND S.KUNNR = @CLIENTE
  AND S.PERIOD = @PERIODO;

INSERT INTO @PRESUPUESTOS
  SELECT
    'PRESUPUESTO_CONTABLE_C'
   ,ISNULL(SUM(D.MONTO_DOC_MD), 0) AS CONSU
  FROM DOCUMENTO AS D
  WHERE D.ESTATUS_SAP = 'C'
  AND D.ESTATUS_WF = 'A'
  AND D.PAYER_ID IN (SELECT
      KUNNR
    FROM CLIENTE
    WHERE KUNNR = @CLIENTE)
  AND D.PERIODO = @PERIODO;
  
--PRESUPUESTO: POR REGISTRAR EN SAP------------------------------------------------
INSERT INTO @PRESUPUESTOS --
  ----SELECT
  ----  'PRESUPUESTO_CONTABLE_A'
  ---- ,ISNULL(SUM(D.MONTO_DOC_MD), 0) AS CONSU
  ----FROM DOCUMENTO AS D
  ----WHERE D.ESTATUS_SAP = 'P'
  ----AND D.ESTATUS_WF = 'A'
  ----AND D.PAYER_ID IN (SELECT
  ----    KUNNR
  ----  FROM CLIENTE
  ----  WHERE KUNNR = @CLIENTE)
  ----AND D.PERIODO = @PERIODO;
    SELECT
    'PRESUPUESTO_CONTABLE_A',
	(
select isnull(sum(monto_doc_md),0)
--num_doc, monto_doc_md, D.estatus,estatus_c,estatus_sap,estatus_wf, tsol_id
from documento AS D
INNER JOIN TSOL AS T
ON D.TSOL_ID = T.ID
where payer_id = @CLIENTE
and periodo = @PERIODO
and D.estatus != 'C'
and D.estatus_C is null
and ((D.estatus = 'A' and D.estatus_WF = 'A' and D.estatus_sap != 'X')
or (D.estatus_sap = 'P' and D.estatus_wf = 'A'))
and DOCUMENTO_REF is null
)-
(
select isnull(sum(monto_doc_md),0)
--num_doc, monto_doc_md, D.estatus,estatus_c,estatus_sap,estatus_wf, tsol_id
from documento AS D
INNER JOIN TSOL AS T
ON D.TSOL_ID = T.ID
where payer_id = @CLIENTE
and periodo = @PERIODO
and D.estatus != 'C'
and D.estatus_C is null
and ((D.estatus = 'A' and D.estatus_WF = 'A' and D.estatus_sap != 'X')
or (D.estatus_sap = 'P' and D.estatus_wf = 'A'))
and T.reverso = 1) as CONTU;

------------------------------------------------------------------------------
--PRESUPUESTO: EN PROCESO TAT-------------------------------------------------
INSERT INTO @PRESUPUESTOS 
  ----SELECT
  ----  'PRESUPUESTO_CONTABLE_P'
  ---- ,ISNULL(SUM(D.MONTO_DOC_MD), 0) AS CONSU
  ----FROM DOCUMENTO AS D
  ----WHERE D.ESTATUS_WF = 'P'
  ----AND D.PAYER_ID IN (SELECT
  ----    KUNNR
  ----  FROM CLIENTE
  ----  WHERE KUNNR = @CLIENTE)
  ----AND D.PERIODO = @PERIODO;
    SELECT
    'PRESUPUESTO_CONTABLE_P',
   (
select isnull(sum(monto_doc_md),0)
--num_doc, monto_doc_md, D.estatus,estatus_c,estatus_sap,estatus_wf, tsol_id
from documento AS D
INNER JOIN TSOL AS T
ON D.TSOL_ID = T.ID
where payer_id = @CLIENTE
and periodo = @PERIODO
and D.estatus != 'C'
and D.estatus_C is null
and ((D.estatus = 'N' ))
and DOCUMENTO_REF is null
)-
(
select isnull(sum(monto_doc_md),0)
--num_doc, monto_doc_md, D.estatus,estatus_c,estatus_sap,estatus_wf, tsol_id
from documento AS D
INNER JOIN TSOL AS T
ON D.TSOL_ID = T.ID
where payer_id = @CLIENTE
and periodo = @PERIODO
and D.estatus != 'C'
and D.estatus_C is null
and ((D.estatus = 'N' ))
and T.reverso = 1) as CONTU;

--------------------------------------------------------------------------------
  INSERT INTO @PRESUPUESTOS
  SELECT
    'PRESUPUESTO_CONTABLE_T'
   ,ISNULL(SUM(D.VALOR), 0) AS CONSU
  FROM @PRESUPUESTOS AS D
  WHERE D.DESCRIPCION LIKE '%PRESUPUESTO_CONTABLE%';

  -->reversa
  INSERT INTO @PRESUPUESTOS
  SELECT
    'PRESUPUESTO_CONTABLE_CR'
   , ISNULL(SUM(D.MONTO_DOC_MD), 0) AS CONSU
  FROM DOCUMENTO AS D
  WHERE D.ESTATUS_SAP = 'C'
  AND D.ESTATUS_WF = 'A'
  AND D.PAYER_ID IN (SELECT
      KUNNR
    FROM CLIENTE
    WHERE KUNNR = @CLIENTE)
	AND D.TSOL_ID IN(SELECT ID FROM TSOL WHERE REVERSO = 1)
  AND D.PERIODO = @PERIODO;

INSERT INTO @PRESUPUESTOS
  SELECT
    'PRESUPUESTO_CONTABLE_AR'
   ,ISNULL(SUM(D.MONTO_DOC_MD), 0) AS CONSU
  FROM DOCUMENTO AS D
  WHERE D.ESTATUS_SAP = 'P'
  AND D.ESTATUS_WF = 'A'
  AND D.PAYER_ID IN (SELECT
      KUNNR
    FROM CLIENTE
    WHERE KUNNR = @CLIENTE)
	AND D.TSOL_ID IN(SELECT 
	ID 
	FROM TSOL WHERE REVERSO = 1)
  AND D.PERIODO = @PERIODO;
INSERT INTO @PRESUPUESTOS
  SELECT
    'PRESUPUESTO_CONTABLE_PR'
   ,ISNULL(SUM(D.MONTO_DOC_MD), 0) AS CONSU
  FROM DOCUMENTO AS D
  WHERE D.ESTATUS_WF = 'P'
  AND D.PAYER_ID IN (SELECT
      KUNNR
    FROM CLIENTE
    WHERE KUNNR = @CLIENTE)
	AND D.TSOL_ID IN(SELECT ID 
	FROM TSOL 
	WHERE REVERSO = 1)
  AND D.PERIODO = @PERIODO;
  --<reversa

SELECT
  *
FROM @PRESUPUESTOS
END
DROP TABLE #tbBannersG

GO
/****** Object:  StoredProcedure [dbo].[CSP_PRESUPUESTO_ADD]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[CSP_PRESUPUESTO_ADD]
	@anio int,
	@sociedad nvarchar(max),
	@periodo nvarchar(max),
	@usuario_id nvarchar(16),
	@auto nchar(1),
	@caso int	
AS
DECLARE
	@id int

BEGIN TRY
	BEGIN
BEGIN TRAN
IF (@caso = 1)
BEGIN
 IF @auto = '1'
  BEGIN
   DELETE PRESUPUESTOP
    WHERE ANIO = @anio
  END
 ELSE
  BEGIN
   IF @sociedad != '' AND @periodo != ''
    BEGIN    
     DELETE PRESUPUESTOP
     WHERE ANIO = @anio
      AND REGION IN (SELECT REGION FROM REGION WHERE SOCIEDAD IN (SELECT val FROM dbo.split(@sociedad,',') WHERE val != ''))
      AND MES IN (SELECT val FROM dbo.split(@periodo,',') WHERE val != '')
    END
   ELSE IF @sociedad != ''
    BEGIN    
     DELETE PRESUPUESTOP
     WHERE ANIO = @anio
      AND REGION IN (SELECT REGION FROM REGION WHERE SOCIEDAD IN (SELECT val FROM dbo.split(@sociedad,',') WHERE val != ''))
    END
   END
  INSERT PRESUPUESTOH (ANIO, USUARIO_ID, FECHAC)
	VALUES (@anio, @usuario_id, GETDATE());
  SELECT
  @id = @@IDENTITY
  SELECT TOP 1
  @id
  COMMIT TRAN
 END
ELSE
 BEGIN
  IF @auto = '1'
   BEGIN
	DELETE PRESUPSAPP
    WHERE ANIO = @anio      
      AND PERIOD IN (SELECT TOP 1 val FROM dbo.split(@periodo,',') WHERE val != '')
   END
  ELSE IF @auto = '0'
   BEGIN
   IF @sociedad != '' AND @periodo != ''
    BEGIN    
     DELETE PRESUPSAPP
     WHERE ANIO = @anio
      AND BUKRS IN (SELECT TOP 1 val FROM dbo.split(@sociedad,',') WHERE val != '')
      AND PERIOD IN (SELECT TOP 1 val FROM dbo.split(@periodo,',') WHERE val != '')
    END
   ELSE IF @sociedad != ''
    BEGIN    
     DELETE PRESUPSAPP
     WHERE ANIO = @anio
      AND BUKRS IN (SELECT TOP 1 val FROM dbo.split(@sociedad,',') WHERE val != '')
    END
   END
  INSERT PRESUPSAPH (ANIO, USUARIO_ID, FECHAC)
   VALUES (@anio, @usuario_id, GETDATE())
  SELECT
   @id = @@IDENTITY
  SELECT TOP 1
   @id
  COMMIT TRAN
 END
END
END TRY
BEGIN CATCH
SELECT
  0 AS ErrorMensaje
ROLLBACK TRAN
END CATCH



GO
/****** Object:  StoredProcedure [dbo].[CSP_PRESUPUESTO_ADDP]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[CSP_PRESUPUESTO_ADDP]	 
	@path NVARCHAR(2000)
AS
DECLARE
	@sSql NVARCHAR(MAX)
BEGIN TRY
	BEGIN
		BEGIN TRAN
			SET @sSql = 'BULK INSERT PRESUPUESTOP
			FROM ''' + @path + '''
			WITH (FIELDTERMINATOR = '','')';
			PRINT @sSql
			EXEC (@sSql)
			select * from PRESUPUESTOP;						
			COMMIT TRAN			
		END
		END TRY
	BEGIN CATCH
		ROLLBACK TRAN
END CATCH



GO
/****** Object:  StoredProcedure [dbo].[CSP_PROVISIONES_ABIERTAS]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CSP_PROVISIONES_ABIERTAS]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

		-- Insert statements for procedure here
		SELECT 
	d.SOCIEDAD_ID,
	FORMAT(d.FECHAC,'MM/dd/yyyy')  AS FECHA_EMISION,
	CAST(d.NUM_DOC AS NVARCHAR(20))  AS NUM_DOC,
	(SELECT TXT50 FROM TALLT tt WHERE tt.TALL_ID=d.TALL_ID AND tt.SPRAS_ID='ES') AS TALL,
	(SELECT NAME1 FROM CLIENTE c WHERE c.KUNNR=d.PAYER_ID) AS CLIENTE_N,
	d.CONCEPTO,
	d.MONTO_DOC_MD  AS SALDO,
	CAST(DATEDIFF(DAY,d.FECHAC,GETDATE()) AS  NVARCHAR(20)) AS ANTIGUEDAD ,
	ISNULL(d.USUARIOD_ID,d.USUARIOC_ID) AS USUARIO_ID,
	(SELECT EMAIL FROM USUARIO WHERE ID=ISNULL(d.USUARIOD_ID,d.USUARIOC_ID))AS EMAIL_USUARIO,
	f.[STATUS] AS ESTATUS
	FROM DOCUMENTO d
	INNER JOIN  FLUJO f
	ON d.NUM_DOC=f.NUM_DOC AND f.POS=(SELECT MAX(POS) FROM FLUJO f1 WHERE d.NUM_DOC=f1.NUM_DOC )
	WHERE d.ESTATUS_WF ='A' AND d.ESTATUS='A'  AND d.ESTATUS_C IS NULL AND (d.ESTATUS_SAP ='X' OR d.ESTATUS_SAP IS NULL )  AND d.TSOL_ID LIKE 'PR%' 

END

GO
/****** Object:  StoredProcedure [dbo].[CSP_USUARIO]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<ROGELIO SÁNCHEZ>
-- Create date: <22-02-2018>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CSP_USUARIO]
	-- Add the parameters for the stored procedure here
	@ID		nvarchar(16),
	@PASS nvarchar(50),
	@NOMBRE NVARCHAR(50),
	@APELLIDO_P NVARCHAR(50),
	@APELLIDO_M NVARCHAR(50),
	@EMAIL NVARCHAR(255),
	@SPRAS_ID NCHAR(2),
	@ACTIVO BIT,
	@ACCION int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF @ACCION = 1
	BEGIN
		INSERT USUARIO( ID, PASS, NOMBRE, APELLIDO_P, APELLIDO_M, EMAIL, SPRAS_ID, ACTIVO)
		VALUES ( @ID, @PASS, @NOMBRE, @APELLIDO_P, @APELLIDO_M, @EMAIL, @SPRAS_ID,  'TRUE')
	END
	IF @ACCION = 2
	BEGIN 
		SELECT  USUARIO.ID, PASS, USUARIO.NOMBRE, APELLIDO_P, APELLIDO_M, EMAIL, SPRAS_ID, USUARIO.ACTIVO, ROL.ID AS ID_GR, ROL.NOMBRE AS NOMBRE_GR
		FROM USUARIO
		INNER JOIN MIEMBROS
		ON USUARIO.ID = MIEMBROS.USUARIO_ID
		INNER JOIN ROL
		ON MIEMBROS.ROL_ID = ROL.ID
		WHERE USUARIO.ID = @ID
	END
END



GO
/****** Object:  UserDefinedFunction [dbo].[split]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[split](
          @delimited NVARCHAR(MAX),
          @delimiter NVARCHAR(100)
        ) RETURNS @t TABLE (id INT IDENTITY(1,1), val NVARCHAR(MAX))
        AS
        BEGIN
          DECLARE @xml XML
          SET @xml = N'<t>' + REPLACE(@delimited,@delimiter,'</t><t>') + '</t>'

          INSERT INTO @t(val)
          SELECT  r.value('.','varchar(MAX)') as item
          FROM  @xml.nodes('/t') as records(r)
          RETURN
        END


GO
/****** Object:  Table [dbo].[ACCION]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ACCION](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DESCCRIPCION] [nvarchar](50) NULL,
	[TIPO] [nchar](1) NOT NULL,
 CONSTRAINT [PK_ACCION] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ACCIONT]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ACCIONT](
	[SPRAS_ID] [nchar](2) NOT NULL,
	[ACCION_ID] [int] NOT NULL,
	[TXT050] [nvarchar](50) NULL,
 CONSTRAINT [PK_ACCIONT] PRIMARY KEY CLUSTERED 
(
	[SPRAS_ID] ASC,
	[ACCION_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[APPSETTING]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[APPSETTING](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NOMBRE] [nvarchar](50) NULL,
	[VALUE] [nvarchar](255) NULL,
	[ACTIVO] [bit] NOT NULL,
 CONSTRAINT [PK_APPSETTING] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CALENDARIO_AC]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CALENDARIO_AC](
	[EJERCICIO] [smallint] NOT NULL,
	[PERIODO] [int] NOT NULL,
	[SOCIEDAD_ID] [nchar](4) NOT NULL,
	[TSOL_ID] [nvarchar](10) NOT NULL,
	[PRE_FROMF] [date] NOT NULL,
	[PRE_FROMH] [time](7) NOT NULL,
	[PRE_TOF] [date] NOT NULL,
	[PRE_TOH] [time](7) NOT NULL,
	[CIE_FROMF] [date] NOT NULL,
	[CIE_FROMH] [time](7) NOT NULL,
	[CIE_TOF] [date] NOT NULL,
	[CIE_TOH] [time](7) NOT NULL,
	[USUARIOC_ID] [nvarchar](16) NOT NULL,
	[FECHAC] [datetime] NOT NULL,
	[USUARIOM_ID] [nvarchar](16) NULL,
	[FECHAM] [datetime] NULL,
	[ACTIVO] [bit] NOT NULL,
 CONSTRAINT [PK_CALENDARIO_AC] PRIMARY KEY CLUSTERED 
(
	[EJERCICIO] ASC,
	[PERIODO] ASC,
	[SOCIEDAD_ID] ASC,
	[TSOL_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CALENDARIO_EX]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CALENDARIO_EX](
	[EJERCICIO] [smallint] NOT NULL,
	[PERIODO] [int] NOT NULL,
	[SOCIEDAD_ID] [nchar](4) NOT NULL,
	[TSOL_ID] [nvarchar](10) NOT NULL,
	[USUARIO_ID] [nvarchar](16) NOT NULL,
	[EX_FROMF] [date] NOT NULL,
	[EX_FROMH] [time](7) NOT NULL,
	[EX_TOF] [date] NOT NULL,
	[EX_TOH] [time](7) NOT NULL,
	[USUARIOC_ID] [nvarchar](16) NOT NULL,
	[FEHAC] [datetime] NOT NULL,
	[ACTIVO] [bit] NOT NULL,
 CONSTRAINT [PK_CALENDARIO_EX] PRIMARY KEY CLUSTERED 
(
	[EJERCICIO] ASC,
	[PERIODO] ASC,
	[SOCIEDAD_ID] ASC,
	[TSOL_ID] ASC,
	[USUARIO_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CAMPOS]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAMPOS](
	[PAGINA_ID] [int] NOT NULL,
	[ID] [nvarchar](25) NOT NULL,
	[DESCRIPCION] [nvarchar](50) NULL,
	[TIPO] [nchar](10) NULL,
 CONSTRAINT [PK_CAMPOS] PRIMARY KEY CLUSTERED 
(
	[PAGINA_ID] ASC,
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CAMPOZKE24]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAMPOZKE24](
	[CAMPO] [nvarchar](20) NOT NULL,
	[DESCRIPCION] [nvarchar](50) NULL,
	[ACTIVO] [bit] NULL,
 CONSTRAINT [PK_CAMPOZKE24] PRIMARY KEY CLUSTERED 
(
	[CAMPO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CAMPOZKE24T]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAMPOZKE24T](
	[SPRAS_ID] [nchar](2) NOT NULL,
	[CAMPO_ID] [nvarchar](20) NOT NULL,
	[TXT50] [nvarchar](50) NULL,
 CONSTRAINT [PK_CAMPOZKE24T] PRIMARY KEY CLUSTERED 
(
	[SPRAS_ID] ASC,
	[CAMPO_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CANAL]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CANAL](
	[CANAL] [nvarchar](10) NOT NULL,
	[CDESCRIPCION] [nvarchar](50) NULL,
 CONSTRAINT [PK_CANAL] PRIMARY KEY CLUSTERED 
(
	[CANAL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CARPETA]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CARPETA](
	[ID] [int] NOT NULL,
	[URL] [nvarchar](50) NULL,
	[TITULO] [nvarchar](50) NULL,
	[ICON] [nvarchar](20) NULL,
	[ACTIVO] [bit] NULL,
 CONSTRAINT [PK_CARPETA] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CARPETAT]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CARPETAT](
	[SPRAS_ID] [nchar](2) NOT NULL,
	[ID] [int] NOT NULL,
	[TXT50] [nvarchar](50) NULL,
 CONSTRAINT [PK_CARPETAT] PRIMARY KEY CLUSTERED 
(
	[SPRAS_ID] ASC,
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CARTA]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CARTA](
	[NUM_DOC] [numeric](10, 0) NOT NULL,
	[POS] [int] NOT NULL,
	[FECHAC] [datetime] NULL,
	[USUARIO_ID] [nvarchar](16) NULL,
	[TIPO] [nchar](1) NULL,
	[COMPANY] [nvarchar](100) NULL,
	[COMPANYX] [bit] NULL,
	[TAXID] [nvarchar](100) NULL,
	[TAXIDX] [bit] NULL,
	[CONCEPTO] [nvarchar](100) NULL,
	[CONCEPTOX] [bit] NULL,
	[CLIENTE] [nvarchar](100) NULL,
	[CLIENTEX] [bit] NULL,
	[PUESTO] [nvarchar](100) NULL,
	[PUESTOX] [bit] NULL,
	[DIRECCION] [nvarchar](100) NULL,
	[DIRECCIONX] [bit] NULL,
	[FOLIO] [nvarchar](10) NULL,
	[FOLIOX] [bit] NULL,
	[LUGAR] [nvarchar](100) NULL,
	[LUGARX] [bit] NULL,
	[PAYER] [nvarchar](100) NULL,
	[PAYERX] [bit] NULL,
	[ESTIMADO] [nvarchar](100) NULL,
	[ESTIMADOX] [bit] NULL,
	[MECANICA] [nvarchar](max) NULL,
	[MECANICAX] [bit] NULL,
	[NOMBREE] [nvarchar](100) NULL,
	[NOMBREEX] [bit] NULL,
	[PUESTOE] [nvarchar](100) NULL,
	[PUESTOEX] [bit] NULL,
	[COMPANYC] [nvarchar](100) NULL,
	[COMPANYCX] [bit] NULL,
	[NOMBREC] [nvarchar](100) NULL,
	[NOMBRECX] [bit] NULL,
	[PUESTOC] [nvarchar](100) NULL,
	[PUESTOCX] [bit] NULL,
	[COMPANYCC] [nvarchar](100) NULL,
	[COMPANYCCX] [bit] NULL,
	[LEGAL] [nvarchar](500) NULL,
	[LEGALX] [bit] NULL,
	[MAIL] [nvarchar](200) NULL,
	[MAILX] [bit] NULL,
	[LUGARFECH] [nvarchar](100) NULL,
	[LUGARFECHX] [bit] NULL,
	[MONTO] [nvarchar](100) NULL,
	[MONEDA] [nchar](10) NULL,
	[PAYERNOM] [nvarchar](100) NULL,
	[PAYERNOMX] [bit] NULL,
	[COMENTARIO] [nvarchar](255) NULL,
	[COMENTARIOX] [bit] NULL,
	[COMPROMISOK] [nvarchar](250) NULL,
	[COMPROMISOKX] [bit] NULL,
	[COMPROMISOC] [nvarchar](250) NULL,
	[COMPROMISOCX] [bit] NULL,
	[SECOND_TABX] [bit] NULL,
	[MATERIALX] [bit] NULL,
	[COSTO_UNX] [bit] NULL,
	[APOYOX] [bit] NULL,
	[APOYOPX] [bit] NULL,
	[COSTOAPX] [bit] NULL,
	[PRECIOX] [bit] NULL,
	[APOYO_ESTX] [bit] NULL,
	[APOYO_REAX] [bit] NULL,
	[VOLUMEN_ESTX] [bit] NULL,
	[VOLUMEN_REAX] [bit] NULL,
	[STATUS] [bit] NOT NULL,
 CONSTRAINT [PK_CARTA] PRIMARY KEY CLUSTERED 
(
	[NUM_DOC] ASC,
	[POS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CARTAP]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CARTAP](
	[NUM_DOC] [numeric](10, 0) NOT NULL,
	[POS_ID] [int] NOT NULL,
	[POS] [int] NOT NULL,
	[MATNR] [nvarchar](18) NOT NULL,
	[MATKL] [nvarchar](50) NULL,
	[CANTIDAD] [decimal](13, 5) NOT NULL,
	[MONTO] [decimal](13, 5) NOT NULL,
	[PORC_APOYO] [decimal](13, 5) NOT NULL,
	[MONTO_APOYO] [decimal](13, 5) NOT NULL,
	[PRECIO_SUG] [decimal](13, 5) NOT NULL,
	[VOLUMEN_EST] [decimal](13, 5) NOT NULL,
	[VOLUMEN_REAL] [decimal](13, 5) NULL,
	[APOYO_REAL] [decimal](13, 5) NULL,
	[VIGENCIA_DE] [date] NULL,
	[VIGENCIA_AL] [date] NULL,
	[APOYO_EST] [decimal](13, 5) NULL,
 CONSTRAINT [PK_CARTAP] PRIMARY KEY CLUSTERED 
(
	[NUM_DOC] ASC,
	[POS_ID] ASC,
	[POS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CATEGORIA]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CATEGORIA](
	[ID] [nvarchar](9) NOT NULL,
	[DESCRIPCION] [nvarchar](50) NULL,
	[ACTIVO] [bit] NULL,
 CONSTRAINT [PK_CATEGORIA] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CATEGORIAT]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CATEGORIAT](
	[SPRAS_ID] [nchar](2) NOT NULL,
	[CATEGORIA_ID] [nvarchar](9) NOT NULL,
	[TXT50] [nvarchar](50) NULL,
 CONSTRAINT [PK_CATEGORIAT] PRIMARY KEY CLUSTERED 
(
	[SPRAS_ID] ASC,
	[CATEGORIA_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CITIES]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CITIES](
	[ID] [bigint] NOT NULL,
	[NAME] [varchar](30) NOT NULL,
	[STATE_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CLIENTE]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CLIENTE](
	[VKORG] [nvarchar](4) NOT NULL,
	[VTWEG] [nchar](2) NOT NULL,
	[SPART] [nchar](2) NOT NULL,
	[KUNNR] [nchar](10) NOT NULL,
	[NAME1] [nvarchar](100) NULL,
	[STCD1] [nvarchar](20) NULL,
	[STCD2] [nvarchar](20) NULL,
	[LAND] [nchar](2) NULL,
	[REGION] [nvarchar](50) NULL,
	[SUBREGION] [nvarchar](50) NULL,
	[REGIO] [nvarchar](50) NULL,
	[ORT01] [nvarchar](50) NULL,
	[STRAS_GP] [nvarchar](100) NULL,
	[PSTLZ] [nvarchar](15) NULL,
	[CONTAC] [nvarchar](100) NULL,
	[CONT_EMAIL] [nvarchar](100) NULL,
	[PARVW] [nvarchar](2) NULL,
	[PAYER] [nchar](10) NULL,
	[GRUPO] [nchar](10) NULL,
	[SPRAS] [nchar](2) NULL,
	[ACTIVO] [bit] NOT NULL,
	[BDESCRIPCION] [nchar](50) NULL,
	[BANNER] [nchar](10) NULL,
	[CANAL] [nchar](10) NULL,
	[BZIRK] [nchar](6) NULL,
	[KONDA] [nchar](2) NULL,
	[VKGRP] [nchar](3) NULL,
	[VKBUR] [nchar](4) NULL,
	[BANNERG] [nchar](50) NULL,
	[PROVEEDOR_ID] [nchar](10) NULL,
	[USUARIO_ID] [nvarchar](16) NULL,
	[EXPORTACION] [nchar](1) NULL,
 CONSTRAINT [PK_CLIENTE] PRIMARY KEY CLUSTERED 
(
	[VKORG] ASC,
	[VTWEG] ASC,
	[SPART] ASC,
	[KUNNR] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CLIENTEF]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CLIENTEF](
	[VKORG] [nvarchar](4) NOT NULL,
	[VTWEG] [nchar](2) NOT NULL,
	[SPART] [nchar](2) NOT NULL,
	[KUNNR] [nchar](10) NOT NULL,
	[VERSION] [int] NOT NULL,
	[USUARIO0_ID] [nvarchar](16) NULL,
	[USUARIO1_ID] [nvarchar](16) NULL,
	[USUARIO2_ID] [nvarchar](16) NULL,
	[USUARIO3_ID] [nvarchar](16) NULL,
	[USUARIO4_ID] [nvarchar](16) NULL,
	[USUARIO5_ID] [nvarchar](16) NULL,
	[USUARIO6_ID] [nvarchar](16) NULL,
	[ACTIVO] [bit] NOT NULL,
	[FECHAC] [datetime] NULL,
	[FECHAM] [datetime] NULL,
	[USUARIO7_ID] [nvarchar](16) NULL,
 CONSTRAINT [PK_CLIENTEF] PRIMARY KEY CLUSTERED 
(
	[VKORG] ASC,
	[VTWEG] ASC,
	[SPART] ASC,
	[KUNNR] ASC,
	[VERSION] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CLIENTEI]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CLIENTEI](
	[VKORG] [nvarchar](4) NOT NULL,
	[VTWEG] [nchar](2) NOT NULL,
	[SPART] [nchar](2) NOT NULL,
	[KUNNR] [nchar](10) NOT NULL,
	[MWSKZ] [nchar](2) NOT NULL,
	[ACTIVO] [bit] NOT NULL,
 CONSTRAINT [PK_CLIENTEI] PRIMARY KEY CLUSTERED 
(
	[VKORG] ASC,
	[VTWEG] ASC,
	[SPART] ASC,
	[KUNNR] ASC,
	[MWSKZ] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CONDICION]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CONDICION](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DESCR] [nvarchar](50) NULL,
	[COND] [nvarchar](4) NULL,
	[ACTIVO] [bit] NULL,
 CONSTRAINT [PK_CONDICION] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CONDICIONT]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CONDICIONT](
	[SPRAS_ID] [nchar](2) NOT NULL,
	[CONDICION_ID] [int] NOT NULL,
	[TXT050] [nvarchar](50) NULL,
 CONSTRAINT [PK_CONDICIONT] PRIMARY KEY CLUSTERED 
(
	[SPRAS_ID] ASC,
	[CONDICION_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CONFDIST_CAT]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CONFDIST_CAT](
	[SOCIEDAD_ID] [nchar](4) NOT NULL,
	[CAMPO] [nvarchar](20) NULL,
	[PORC_AD] [bit] NULL,
	[PERIODOS] [int] NULL,
	[ACTIVO] [bit] NULL,
 CONSTRAINT [PK_CONFDIST_CAT] PRIMARY KEY CLUSTERED 
(
	[SOCIEDAD_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CONMAIL]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CONMAIL](
	[ID] [nchar](2) NOT NULL,
	[HOST] [nvarchar](100) NULL,
	[PORT] [int] NULL,
	[SSL] [bit] NOT NULL,
	[MAIL] [nvarchar](100) NULL,
	[PASS] [nvarchar](100) NULL,
	[ACTIVO] [bit] NOT NULL,
 CONSTRAINT [PK_CONMAIL] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CONPOSAPH]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CONPOSAPH](
	[CONSECUTIVO] [decimal](10, 0) NOT NULL,
	[TIPO_SOL] [nvarchar](10) NOT NULL,
	[TIPO_DOC] [nchar](2) NOT NULL,
	[SOCIEDAD] [nchar](4) NOT NULL,
	[FECHA_CONTAB] [nchar](1) NULL,
	[FECHA_DOCU] [nchar](1) NULL,
	[MONEDA] [nchar](1) NULL,
	[HEADER_TEXT] [nvarchar](50) NULL,
	[FECHA_INIVIG] [date] NULL,
	[FECHA_FINVIG] [date] NULL,
	[REFERENCIA] [nchar](16) NULL,
	[PAIS] [nchar](2) NULL,
	[NOTA] [nchar](50) NULL,
	[CORRESPONDENCIA] [nchar](50) NULL,
	[CALC_TAXT] [bit] NULL,
	[RELACION] [int] NULL,
	[RETENCION] [nchar](1) NULL,
	[DESCRI_CONFIG] [nvarchar](30) NULL,
 CONSTRAINT [PK__CONPOSAP__E3D44DA377AE5E94] PRIMARY KEY CLUSTERED 
(
	[CONSECUTIVO] ASC,
	[TIPO_SOL] ASC,
	[TIPO_DOC] ASC,
	[SOCIEDAD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CONPOSAPP]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CONPOSAPP](
	[CONSECUTIVO] [decimal](10, 0) NOT NULL,
	[POSICION] [int] NOT NULL,
	[RELACION] [int] NULL,
	[KEY] [nchar](1) NULL,
	[BUS_AREA] [nchar](3) NULL,
	[POSTING_KEY] [nchar](2) NULL,
	[NUM_PROV] [bit] NULL,
	[NUM_CTE] [bit] NULL,
	[CTA_MAYOR] [bit] NULL,
	[CTA_MAYOR_FV] [bit] NULL,
	[CECO] [bit] NULL,
	[TEXTO] [nchar](50) NULL,
	[SALES_ORG] [bit] NULL,
	[CAN_DIST] [bit] NULL,
	[DIVISION] [bit] NULL,
	[SALES_OFF] [bit] NULL,
	[SALES_GRP] [bit] NULL,
	[PRICE_GRP] [bit] NULL,
	[CORP_CAT] [bit] NULL,
	[CORP_BRAND] [bit] NULL,
	[INV_REF] [bit] NULL,
	[PAYM_TERM] [bit] NULL,
	[JURIS_CODE] [bit] NULL,
	[SALES_DIST] [bit] NULL,
	[PRODUCT] [bit] NULL,
	[TAX_CODE] [nchar](2) NULL,
	[PLANT] [bit] NULL,
	[REF_KEY1] [nchar](12) NULL,
	[REF_KEY3] [nchar](12) NULL,
	[ASSIGNACION] [bit] NULL,
	[QUANTITY] [int] NULL,
	[BASE_UNIT] [nchar](3) NULL,
	[MATERIALGP] [nvarchar](30) NULL,
	[TAXCODEGP] [nchar](2) NULL,
	[ASIGNACIONTXT] [nvarchar](50) NULL,
 CONSTRAINT [PK__CONPOSAP__FF4EFA58EED3199C] PRIMARY KEY CLUSTERED 
(
	[CONSECUTIVO] ASC,
	[POSICION] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CONSOPORTE]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CONSOPORTE](
	[TSOL_ID] [nvarchar](10) NOT NULL,
	[TSOPORTE_ID] [nchar](3) NOT NULL,
	[OBLIGATORIO] [bit] NOT NULL,
	[ACTIVO] [bit] NOT NULL,
 CONSTRAINT [PK_CONSOPORTE_1] PRIMARY KEY CLUSTERED 
(
	[TSOL_ID] ASC,
	[TSOPORTE_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CONTACTOC]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CONTACTOC](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NOMBRE] [nvarchar](150) NULL,
	[PHONE] [nvarchar](15) NULL,
	[EMAIL] [nvarchar](250) NULL,
	[VKORG] [nvarchar](4) NULL,
	[VTWEG] [nchar](2) NULL,
	[SPART] [nchar](2) NULL,
	[KUNNR] [nchar](10) NULL,
	[ACTIVO] [bit] NULL,
	[DEFECTO] [bit] NULL,
	[CARTA] [bit] NULL,
 CONSTRAINT [PK_CONTACTOC] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[COUNTRIES]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[COUNTRIES](
	[ID] [int] NOT NULL,
	[SORTNAME] [varchar](3) NOT NULL,
	[NAME] [varchar](150) NOT NULL,
	[PHONECODE] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CUENTA]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CUENTA](
	[SOCIEDAD_ID] [nchar](4) NOT NULL,
	[PAIS_ID] [nchar](2) NOT NULL,
	[TALL_ID] [nvarchar](10) NOT NULL,
	[EJERCICIO] [numeric](4, 0) NOT NULL,
	[ABONO] [numeric](10, 0) NULL,
	[CARGO] [numeric](10, 0) NULL,
	[CLEARING] [numeric](10, 0) NULL,
	[LIMITE] [decimal](13, 5) NULL,
	[IMPUESTO] [nchar](2) NULL,
 CONSTRAINT [PK_CUENTA] PRIMARY KEY CLUSTERED 
(
	[SOCIEDAD_ID] ASC,
	[PAIS_ID] ASC,
	[TALL_ID] ASC,
	[EJERCICIO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CUENTAGL]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CUENTAGL](
	[ID] [numeric](10, 0) NOT NULL,
	[NOMBRE] [nvarchar](50) NULL,
	[ACTIVO] [bit] NULL,
 CONSTRAINT [PK_CUENTAGL] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DELEGAR]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DELEGAR](
	[USUARIO_ID] [nvarchar](16) NOT NULL,
	[USUARIOD_ID] [nvarchar](16) NOT NULL,
	[FECHAI] [date] NOT NULL,
	[FECHAF] [date] NOT NULL,
	[ACTIVO] [bit] NOT NULL,
 CONSTRAINT [PK_DELEGAR] PRIMARY KEY CLUSTERED 
(
	[USUARIO_ID] ASC,
	[USUARIOD_ID] ASC,
	[FECHAI] ASC,
	[FECHAF] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DET_AGENTE]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DET_AGENTE](
	[PUESTOC_ID] [int] NOT NULL,
	[POS] [int] NOT NULL,
	[PUESTOA_ID] [int] NOT NULL,
	[AGROUP_ID] [bigint] NOT NULL,
	[MONTO] [decimal](13, 2) NULL,
	[PRESUPUESTO] [bit] NULL,
	[ACTIVO] [bit] NULL,
	[USUARIOA] [nvarchar](16) NULL,
	[USUARIOC] [nvarchar](16) NULL,
 CONSTRAINT [PK_DET_AGENTE] PRIMARY KEY CLUSTERED 
(
	[PUESTOC_ID] ASC,
	[POS] ASC,
	[PUESTOA_ID] ASC,
	[AGROUP_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DET_AGENTEC]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DET_AGENTEC](
	[USUARIOC_ID] [nvarchar](16) NOT NULL,
	[PAIS_ID] [nchar](2) NOT NULL,
	[VKORG] [nvarchar](4) NOT NULL,
	[VTWEG] [nchar](2) NOT NULL,
	[SPART] [nchar](2) NOT NULL,
	[KUNNR] [nchar](10) NOT NULL,
	[VERSION] [int] NOT NULL,
	[POS] [int] NOT NULL,
	[USUARIOA_ID] [nvarchar](16) NOT NULL,
	[MONTO] [decimal](13, 2) NULL,
	[PRESUPUESTO] [bit] NOT NULL,
	[MAIL] [bit] NOT NULL,
	[ACTIVO] [bit] NOT NULL,
 CONSTRAINT [PK_DET_AGENTEC] PRIMARY KEY CLUSTERED 
(
	[USUARIOC_ID] ASC,
	[PAIS_ID] ASC,
	[VKORG] ASC,
	[VTWEG] ASC,
	[SPART] ASC,
	[KUNNR] ASC,
	[VERSION] ASC,
	[POS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DET_AGENTEH]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DET_AGENTEH](
	[SOCIEDAD_ID] [nchar](4) NOT NULL,
	[PUESTOC_ID] [int] NOT NULL,
	[VERSION] [int] NOT NULL,
	[AGROUP_ID] [bigint] NOT NULL,
	[USUARIOC_ID] [nvarchar](16) NULL,
	[ACTIVO] [bit] NOT NULL,
 CONSTRAINT [PK_DET_AGENTEH] PRIMARY KEY CLUSTERED 
(
	[SOCIEDAD_ID] ASC,
	[PUESTOC_ID] ASC,
	[VERSION] ASC,
	[AGROUP_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DET_AGENTEP]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DET_AGENTEP](
	[SOCIEDAD_ID] [nchar](4) NOT NULL,
	[PUESTOC_ID] [int] NOT NULL,
	[VERSION] [int] NOT NULL,
	[AGROUP_ID] [bigint] NOT NULL,
	[POS] [int] NOT NULL,
	[PUESTOA_ID] [int] NULL,
	[USUARIOA_ID] [nvarchar](16) NULL,
	[MONTO] [decimal](13, 2) NULL,
	[PRESUPUESTO] [bit] NULL,
	[ACTIVO] [bit] NOT NULL,
 CONSTRAINT [PK_DET_AGENTEP] PRIMARY KEY CLUSTERED 
(
	[SOCIEDAD_ID] ASC,
	[PUESTOC_ID] ASC,
	[VERSION] ASC,
	[AGROUP_ID] ASC,
	[POS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DET_APROB]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DET_APROB](
	[PUESTOC_ID] [int] NOT NULL,
	[POS] [int] NOT NULL,
	[PUESTOA_ID] [int] NOT NULL,
	[BUKRS] [nchar](4) NOT NULL,
	[MONTO] [decimal](13, 2) NULL,
	[PRESUPUESTO] [bit] NULL,
	[ACTIVO] [bit] NULL,
 CONSTRAINT [PK_DET_APROB] PRIMARY KEY CLUSTERED 
(
	[PUESTOC_ID] ASC,
	[POS] ASC,
	[PUESTOA_ID] ASC,
	[BUKRS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DET_APROBH]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DET_APROBH](
	[SOCIEDAD_ID] [nchar](4) NOT NULL,
	[PUESTOC_ID] [int] NOT NULL,
	[VERSION] [int] NOT NULL,
	[ACTIVO] [bit] NOT NULL,
 CONSTRAINT [PK_DET_APROBH] PRIMARY KEY CLUSTERED 
(
	[SOCIEDAD_ID] ASC,
	[PUESTOC_ID] ASC,
	[VERSION] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DET_APROBP]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DET_APROBP](
	[SOCIEDAD_ID] [nchar](4) NOT NULL,
	[PUESTOC_ID] [int] NOT NULL,
	[VERSION] [int] NOT NULL,
	[POS] [int] NOT NULL,
	[PUESTOA_ID] [int] NULL,
	[MONTO] [decimal](13, 2) NULL,
	[PRESUPUESTO] [bit] NULL,
	[ACTIVO] [bit] NOT NULL,
	[N_MONTO] [int] NULL,
	[N_PRESUP] [int] NULL,
 CONSTRAINT [PK_DET_APROBP] PRIMARY KEY CLUSTERED 
(
	[SOCIEDAD_ID] ASC,
	[PUESTOC_ID] ASC,
	[VERSION] ASC,
	[POS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DET_TAX]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DET_TAX](
	[SOCIEDAD_ID] [nchar](4) NOT NULL,
	[PUESTOC_ID] [int] NOT NULL,
	[VERSION] [int] NOT NULL,
	[PAIS_ID] [nchar](2) NOT NULL,
	[PUESTOA_ID] [int] NULL,
	[USUARIOA_ID] [nvarchar](16) NULL,
	[ACTIVO] [bit] NOT NULL,
 CONSTRAINT [PK_DET_TAX_1] PRIMARY KEY CLUSTERED 
(
	[SOCIEDAD_ID] ASC,
	[PUESTOC_ID] ASC,
	[VERSION] ASC,
	[PAIS_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DET_TAXEO]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DET_TAXEO](
	[SOCIEDAD_ID] [nchar](4) NOT NULL,
	[PAIS_ID] [nchar](2) NOT NULL,
	[PUESTOC_ID] [int] NOT NULL,
	[USUARIOC_ID] [nvarchar](16) NOT NULL,
	[VERSION] [int] NOT NULL,
	[PUESTOA_ID] [int] NULL,
	[USUARIOA_ID] [nvarchar](16) NULL,
	[ACTIVO] [bit] NOT NULL,
 CONSTRAINT [PK_DET_TAXEO_1] PRIMARY KEY CLUSTERED 
(
	[SOCIEDAD_ID] ASC,
	[PAIS_ID] ASC,
	[PUESTOC_ID] ASC,
	[USUARIOC_ID] ASC,
	[VERSION] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DET_TAXEOC]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DET_TAXEOC](
	[USUARIOC_ID] [nvarchar](16) NOT NULL,
	[PAIS_ID] [nchar](2) NOT NULL,
	[VKORG] [nvarchar](4) NOT NULL,
	[VTWEG] [nchar](2) NOT NULL,
	[SPART] [nchar](2) NOT NULL,
	[KUNNR] [nchar](10) NOT NULL,
	[VERSION] [int] NOT NULL,
	[USUARIOA_ID] [nvarchar](16) NOT NULL,
	[ACTIVO] [bit] NOT NULL,
 CONSTRAINT [PK_DET_TAXEOC] PRIMARY KEY CLUSTERED 
(
	[USUARIOC_ID] ASC,
	[PAIS_ID] ASC,
	[VKORG] ASC,
	[VTWEG] ASC,
	[SPART] ASC,
	[KUNNR] ASC,
	[VERSION] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DOCTOAYUDA]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DOCTOAYUDA](
	[ID_DOCUMENTO] [nvarchar](10) NOT NULL,
	[NOMBRE] [nvarchar](50) NOT NULL,
	[ID_CLASIFICACION] [int] NOT NULL,
	[ACTIVO] [bit] NOT NULL,
	[RUTA_DOCUMENTO] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_DOCTOAYUDA] PRIMARY KEY CLUSTERED 
(
	[ID_DOCUMENTO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DOCTOCLASIF]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DOCTOCLASIF](
	[ID_CLASIFICACION] [int] NOT NULL,
	[CLASIFICACION_DSC] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_DOCTOCLASIF] PRIMARY KEY CLUSTERED 
(
	[ID_CLASIFICACION] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DOCTOCLASIFT]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DOCTOCLASIFT](
	[ID_CLASIFICACION] [int] NOT NULL,
	[SPRAS_ID] [nvarchar](2) NOT NULL,
	[TEXTO] [nvarchar](50) NULL,
 CONSTRAINT [PK_DOCTOCLASIFT] PRIMARY KEY CLUSTERED 
(
	[ID_CLASIFICACION] ASC,
	[SPRAS_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DOCUMENTBORR]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DOCUMENTBORR](
	[USUARIOC_ID] [nvarchar](16) NOT NULL,
	[TSOL_ID] [nvarchar](10) NULL,
	[TALL_ID] [nvarchar](10) NULL,
	[SOCIEDAD_ID] [nchar](4) NULL,
	[PAIS_ID] [nchar](2) NULL,
	[ESTADO] [nvarchar](50) NULL,
	[CIUDAD] [nvarchar](50) NULL,
	[PERIODO] [int] NULL,
	[EJERCICIO] [nchar](4) NULL,
	[TIPO_TECNICO] [nchar](1) NULL,
	[TIPO_RECURRENTE] [nchar](1) NULL,
	[CANTIDAD_EV] [decimal](3, 0) NULL,
	[FECHAD] [datetime] NULL,
	[FECHAC] [datetime] NULL,
	[HORAC] [time](7) NULL,
	[FECHAC_PLAN] [date] NULL,
	[FECHAC_USER] [date] NULL,
	[HORAC_USER] [time](7) NULL,
	[ESTATUS] [nchar](1) NULL,
	[ESTATUS_C] [nchar](1) NULL,
	[ESTATUS_SAP] [nchar](1) NULL,
	[ESTATUS_WF] [nchar](1) NULL,
	[DOCUMENTO_REF] [decimal](10, 0) NULL,
	[CONCEPTO] [nvarchar](100) NULL,
	[NOTAS] [nvarchar](255) NULL,
	[MONTO_DOC_MD] [decimal](13, 2) NULL,
	[MONTO_FIJO_MD] [decimal](13, 2) NULL,
	[MONTO_BASE_GS_PCT_MD] [decimal](13, 2) NULL,
	[MONTO_BASE_NS_PCT_MD] [decimal](13, 2) NULL,
	[MONTO_DOC_ML] [decimal](13, 2) NULL,
	[MONTO_FIJO_ML] [decimal](13, 2) NULL,
	[MONTO_BASE_GS_PCT_ML] [decimal](13, 2) NULL,
	[MONTO_BASE_NS_PCT_ML] [decimal](13, 2) NULL,
	[MONTO_DOC_ML2] [decimal](13, 2) NULL,
	[MONTO_FIJO_ML2] [decimal](13, 2) NULL,
	[MONTO_BASE_GS_PCT_ML2] [decimal](13, 2) NULL,
	[MONTO_BASE_NS_PCT_ML2] [decimal](13, 2) NULL,
	[PORC_ADICIONAL] [decimal](13, 2) NULL,
	[IMPUESTO] [nchar](2) NULL,
	[FECHAI_VIG] [date] NULL,
	[FECHAF_VIG] [date] NULL,
	[ESTATUS_EXT] [nchar](1) NULL,
	[SOLD_TO_ID] [nchar](10) NULL,
	[PAYER_ID] [nchar](10) NULL,
	[PAYER_NOMBRE] [nvarchar](50) NULL,
	[PAYER_EMAIL] [nvarchar](255) NULL,
	[GRUPO_CTE_ID] [nchar](10) NULL,
	[CANAL_ID] [nchar](2) NULL,
	[MONEDA_ID] [nchar](3) NULL,
	[MONEDAL_ID] [nchar](10) NULL,
	[MONEDAL2_ID] [nchar](10) NULL,
	[TIPO_CAMBIO] [decimal](10, 6) NULL,
	[TIPO_CAMBIOL] [decimal](10, 6) NULL,
	[TIPO_CAMBIOL2] [decimal](10, 6) NULL,
	[NO_FACTURA] [nvarchar](30) NULL,
	[FECHAD_SOPORTE] [datetime] NULL,
	[METODO_PAGO] [nchar](10) NULL,
	[NO_PROVEEDOR] [nchar](10) NULL,
	[PASO_ACTUAL] [int] NULL,
	[AGENTE_ACTUAL] [nvarchar](16) NULL,
	[FECHA_PASO_ACTUAL] [datetime] NULL,
	[VKORG] [nvarchar](4) NULL,
	[VTWEG] [nchar](2) NULL,
	[SPART] [nchar](2) NULL,
	[TIPO_TECNICO2] [nchar](1) NULL,
	[MONEDA_DIS] [nchar](3) NULL,
	[PORC_APOYO] [decimal](13, 2) NULL,
	[LIGADA] [nchar](1) NULL,
 CONSTRAINT [PK_USUARIOC_ID] PRIMARY KEY CLUSTERED 
(
	[USUARIOC_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DOCUMENTO]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DOCUMENTO](
	[NUM_DOC] [numeric](10, 0) NOT NULL,
	[TSOL_ID] [nvarchar](10) NULL,
	[TALL_ID] [nvarchar](10) NULL,
	[SOCIEDAD_ID] [nchar](4) NULL,
	[PAIS_ID] [nchar](2) NULL,
	[ESTADO] [nvarchar](50) NULL,
	[CIUDAD] [nvarchar](50) NULL,
	[PERIODO] [int] NULL,
	[EJERCICIO] [nchar](4) NULL,
	[TIPO_TECNICO] [nchar](1) NULL,
	[TIPO_RECURRENTE] [nchar](1) NULL,
	[CANTIDAD_EV] [decimal](3, 0) NULL,
	[USUARIOC_ID] [nvarchar](16) NULL,
	[USUARIOD_ID] [nvarchar](16) NULL,
	[FECHAD] [datetime] NULL,
	[FECHAC] [datetime] NULL,
	[HORAC] [time](7) NULL,
	[FECHAC_PLAN] [date] NULL,
	[FECHAC_USER] [date] NULL,
	[HORAC_USER] [time](7) NULL,
	[ESTATUS] [nchar](1) NULL,
	[ESTATUS_C] [nchar](1) NULL,
	[ESTATUS_SAP] [nchar](1) NULL,
	[ESTATUS_WF] [nchar](1) NULL,
	[DOCUMENTO_REF] [decimal](10, 0) NULL,
	[CONCEPTO] [nvarchar](100) NULL,
	[NOTAS] [nvarchar](255) NULL,
	[MONTO_DOC_MD] [decimal](18, 5) NULL,
	[MONTO_FIJO_MD] [decimal](13, 2) NULL,
	[MONTO_BASE_GS_PCT_MD] [decimal](13, 2) NULL,
	[MONTO_BASE_NS_PCT_MD] [decimal](13, 2) NULL,
	[MONTO_DOC_ML] [decimal](18, 5) NULL,
	[MONTO_FIJO_ML] [decimal](13, 2) NULL,
	[MONTO_BASE_GS_PCT_ML] [decimal](13, 2) NULL,
	[MONTO_BASE_NS_PCT_ML] [decimal](13, 2) NULL,
	[MONTO_DOC_ML2] [decimal](18, 5) NULL,
	[MONTO_FIJO_ML2] [decimal](13, 2) NULL,
	[MONTO_BASE_GS_PCT_ML2] [decimal](13, 2) NULL,
	[MONTO_BASE_NS_PCT_ML2] [decimal](13, 2) NULL,
	[PORC_ADICIONAL] [decimal](13, 2) NULL,
	[IMPUESTO] [nchar](2) NULL,
	[FECHAI_VIG] [date] NULL,
	[FECHAF_VIG] [date] NULL,
	[ESTATUS_EXT] [nchar](1) NULL,
	[SOLD_TO_ID] [nchar](10) NULL,
	[PAYER_ID] [nchar](10) NULL,
	[PAYER_NOMBRE] [nvarchar](50) NULL,
	[PAYER_EMAIL] [nvarchar](255) NULL,
	[GRUPO_CTE_ID] [nchar](10) NULL,
	[CANAL_ID] [nchar](2) NULL,
	[MONEDA_ID] [nchar](3) NULL,
	[MONEDAL_ID] [nchar](10) NULL,
	[MONEDAL2_ID] [nchar](10) NULL,
	[TIPO_CAMBIO] [decimal](10, 6) NULL,
	[TIPO_CAMBIOL] [decimal](10, 6) NULL,
	[TIPO_CAMBIOL2] [decimal](10, 6) NULL,
	[NO_FACTURA] [nvarchar](30) NULL,
	[FECHAD_SOPORTE] [datetime] NULL,
	[METODO_PAGO] [nchar](10) NULL,
	[NO_PROVEEDOR] [nchar](10) NULL,
	[PASO_ACTUAL] [int] NULL,
	[AGENTE_ACTUAL] [nvarchar](16) NULL,
	[FECHA_PASO_ACTUAL] [datetime] NULL,
	[VKORG] [nvarchar](4) NULL,
	[VTWEG] [nchar](2) NULL,
	[SPART] [nchar](2) NULL,
	[PUESTO_ID] [int] NULL,
	[GALL_ID] [nvarchar](5) NULL,
	[CONCEPTO_ID] [int] NULL,
	[DOCUMENTO_SAP] [nvarchar](10) NULL,
	[PORC_APOYO] [decimal](13, 5) NULL,
	[LIGADA] [bit] NULL,
	[OBJETIVOQ] [bit] NULL,
	[FRECUENCIA_LIQ] [int] NULL,
	[OBJQ_PORC] [decimal](13, 5) NULL,
	[CUENTAP] [numeric](10, 0) NULL,
	[CUENTAPL] [numeric](10, 0) NULL,
	[EXCEDE_PRES] [char](1) NULL,
	[CUENTACL] [numeric](10, 0) NULL,
	[TSOL_LIG] [nvarchar](10) NULL,
 CONSTRAINT [PK_DOCUMENTO] PRIMARY KEY CLUSTERED 
(
	[NUM_DOC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DOCUMENTOA]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DOCUMENTOA](
	[NUM_DOC] [numeric](10, 0) NOT NULL,
	[POS] [int] NOT NULL,
	[TIPO] [nchar](4) NULL,
	[CLASE] [nchar](3) NULL,
	[STEP_WF] [int] NULL,
	[USUARIO_ID] [nvarchar](16) NULL,
	[PATH] [nvarchar](500) NULL,
	[ACTIVO] [bit] NOT NULL,
 CONSTRAINT [PK_DOCUMENTOA] PRIMARY KEY CLUSTERED 
(
	[NUM_DOC] ASC,
	[POS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DOCUMENTOBORRF]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DOCUMENTOBORRF](
	[USUARIOC_ID] [nvarchar](16) NOT NULL,
	[POS] [int] NOT NULL,
	[ACTIVO] [bit] NULL,
	[FACTURA] [nvarchar](50) NULL,
	[FECHA] [date] NULL,
	[PROVEEDOR] [nchar](10) NULL,
	[CONTROL] [nvarchar](50) NULL,
	[AUTORIZACION] [nvarchar](50) NULL,
	[VENCIMIENTO] [date] NULL,
	[FACTURAK] [nvarchar](50) NULL,
	[EJERCICIOK] [nchar](4) NULL,
	[BILL_DOC] [nchar](10) NULL,
	[BELNR] [nchar](10) NULL,
	[IMPORTE_FAC] [decimal](13, 5) NULL,
	[PAYER] [nvarchar](10) NULL,
	[NAME1] [nvarchar](50) NULL,
	[SOCIEDAD] [nchar](4) NULL,
 CONSTRAINT [PK_DOCUMENTOBORRF] PRIMARY KEY CLUSTERED 
(
	[USUARIOC_ID] ASC,
	[POS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DOCUMENTOBORRM]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DOCUMENTOBORRM](
	[USUARIOC_ID] [nvarchar](16) NOT NULL,
	[POS_ID] [numeric](5, 0) NOT NULL,
	[POS] [int] NOT NULL,
	[MATNR] [nvarchar](18) NULL,
	[PORC_APOYO] [decimal](13, 5) NULL,
	[APOYO_EST] [decimal](13, 5) NULL,
	[APOYO_REAL] [decimal](13, 5) NULL,
	[VIGENCIA_DE] [date] NULL,
	[VIGENCIA_AL] [date] NULL,
	[VALORH] [decimal](13, 5) NULL,
 CONSTRAINT [PK_DOCUMENTOBORRM] PRIMARY KEY CLUSTERED 
(
	[USUARIOC_ID] ASC,
	[POS_ID] ASC,
	[POS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DOCUMENTOBORRN]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DOCUMENTOBORRN](
	[USUARIOC_ID] [nvarchar](16) NOT NULL,
	[POS] [int] NOT NULL,
	[STEP] [int] NULL,
	[USUARIO_ID] [nvarchar](16) NULL,
	[TEXTO] [nvarchar](500) NULL,
 CONSTRAINT [PK_DOCUMENTOBORRN] PRIMARY KEY CLUSTERED 
(
	[USUARIOC_ID] ASC,
	[POS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DOCUMENTOBORRP]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DOCUMENTOBORRP](
	[USUARIOC_ID] [nvarchar](16) NOT NULL,
	[POS] [numeric](5, 0) NOT NULL,
	[MATNR] [nvarchar](18) NOT NULL,
	[MATKL] [nvarchar](9) NULL,
	[CANTIDAD] [decimal](13, 5) NULL,
	[MONTO] [decimal](13, 5) NULL,
	[PORC_APOYO] [decimal](13, 5) NULL,
	[MONTO_APOYO] [decimal](13, 5) NULL,
	[PRECIO_SUG] [decimal](13, 5) NULL,
	[VOLUMEN_EST] [decimal](13, 5) NULL,
	[VOLUMEN_REAL] [decimal](13, 5) NULL,
	[APOYO_REAL] [decimal](13, 5) NULL,
	[VIGENCIA_DE] [date] NULL,
	[VIGENCIA_AL] [date] NULL,
	[APOYO_EST] [decimal](13, 5) NULL,
 CONSTRAINT [PK_DOCUMENTOBORRP] PRIMARY KEY CLUSTERED 
(
	[USUARIOC_ID] ASC,
	[POS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DOCUMENTOBORRREC]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DOCUMENTOBORRREC](
	[USUARIOC_ID] [nvarchar](16) NOT NULL,
	[POS] [int] NOT NULL,
	[FECHAF] [date] NULL,
	[PERIODO] [int] NULL,
	[EJERCICIO] [int] NULL,
	[MONTO_BASE] [decimal](13, 5) NULL,
	[MONTO_FIJO] [decimal](13, 5) NULL,
	[MONTO_GRS] [decimal](13, 5) NULL,
	[MONTO_NET] [decimal](13, 5) NULL,
	[ESTATUS] [nchar](1) NULL,
	[PORC] [decimal](13, 5) NULL,
	[DOC_REF] [numeric](10, 0) NULL,
	[FECHAV] [date] NULL,
 CONSTRAINT [PK_DOCUMENTOBORRREC] PRIMARY KEY CLUSTERED 
(
	[USUARIOC_ID] ASC,
	[POS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DOCUMENTOF]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DOCUMENTOF](
	[NUM_DOC] [numeric](10, 0) NOT NULL,
	[POS] [int] NOT NULL,
	[FACTURA] [nvarchar](50) NULL,
	[FECHA] [date] NULL,
	[PROVEEDOR] [nchar](10) NULL,
	[CONTROL] [nvarchar](50) NULL,
	[AUTORIZACION] [nvarchar](50) NULL,
	[VENCIMIENTO] [date] NULL,
	[FACTURAK] [nvarchar](4000) NULL,
	[EJERCICIOK] [nchar](4) NULL,
	[BILL_DOC] [nchar](10) NULL,
	[BELNR] [nchar](10) NULL,
	[IMPORTE_FAC] [decimal](13, 5) NULL,
	[PAYER] [nvarchar](10) NULL,
 CONSTRAINT [PK_DOCUMENTOF] PRIMARY KEY CLUSTERED 
(
	[NUM_DOC] ASC,
	[POS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DOCUMENTOL]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DOCUMENTOL](
	[NUM_DOC] [numeric](10, 0) NOT NULL,
	[POS] [int] NOT NULL,
	[FECHAF] [date] NULL,
	[MONTO_VENTA] [decimal](18, 5) NULL,
	[ESTATUS] [bit] NULL,
	[BACKORDER] [decimal](18, 5) NULL,
 CONSTRAINT [PK_DOCUMENTOL] PRIMARY KEY CLUSTERED 
(
	[NUM_DOC] ASC,
	[POS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DOCUMENTOM]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DOCUMENTOM](
	[NUM_DOC] [numeric](10, 0) NOT NULL,
	[POS_ID] [numeric](5, 0) NOT NULL,
	[POS] [int] NOT NULL,
	[MATNR] [nvarchar](18) NULL,
	[PORC_APOYO] [decimal](18, 10) NULL,
	[APOYO_EST] [decimal](18, 5) NULL,
	[APOYO_REAL] [decimal](18, 5) NULL,
	[VIGENCIA_DE] [date] NULL,
	[VIGENCIA_A] [date] NULL,
	[VALORH] [decimal](18, 5) NULL,
 CONSTRAINT [PK_DOCUMENTOM] PRIMARY KEY CLUSTERED 
(
	[NUM_DOC] ASC,
	[POS_ID] ASC,
	[POS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DOCUMENTON]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DOCUMENTON](
	[NUM_DOC] [numeric](10, 0) NOT NULL,
	[POS] [int] NOT NULL,
	[STEP] [int] NULL,
	[USUARIO_ID] [nvarchar](16) NULL,
	[TEXTO] [nvarchar](500) NULL,
 CONSTRAINT [PK_DOCUMENTON] PRIMARY KEY CLUSTERED 
(
	[NUM_DOC] ASC,
	[POS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DOCUMENTOP]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DOCUMENTOP](
	[NUM_DOC] [numeric](10, 0) NOT NULL,
	[POS] [numeric](5, 0) NOT NULL,
	[MATNR] [nvarchar](18) NOT NULL,
	[MATKL] [nvarchar](9) NULL,
	[CANTIDAD] [decimal](13, 5) NOT NULL,
	[MONTO] [decimal](13, 5) NOT NULL,
	[PORC_APOYO] [decimal](13, 5) NOT NULL,
	[MONTO_APOYO] [decimal](13, 5) NOT NULL,
	[PRECIO_SUG] [decimal](13, 5) NOT NULL,
	[VOLUMEN_EST] [decimal](13, 5) NOT NULL,
	[VOLUMEN_REAL] [decimal](18, 5) NULL,
	[APOYO_REAL] [decimal](13, 5) NULL,
	[VIGENCIA_DE] [date] NULL,
	[VIGENCIA_AL] [date] NULL,
	[APOYO_EST] [decimal](18, 5) NULL,
 CONSTRAINT [PK_DOCUMENTOP] PRIMARY KEY CLUSTERED 
(
	[NUM_DOC] ASC,
	[POS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DOCUMENTOR]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DOCUMENTOR](
	[NUM_DOC] [numeric](10, 0) NOT NULL,
	[TREVERSA_ID] [int] NOT NULL,
	[USUARIOC_ID] [nvarchar](16) NULL,
	[FECHAC] [date] NULL,
	[COMENTARIO] [nvarchar](500) NULL,
 CONSTRAINT [PK_DOCUMENTOR_1] PRIMARY KEY CLUSTERED 
(
	[NUM_DOC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DOCUMENTORAN]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DOCUMENTORAN](
	[NUM_DOC] [numeric](10, 0) NOT NULL,
	[POS] [int] NOT NULL,
	[LIN] [int] NOT NULL,
	[OBJETIVOI] [decimal](18, 5) NULL,
	[PORCENTAJE] [decimal](18, 5) NULL,
 CONSTRAINT [PK_DOCUMENTORAN] PRIMARY KEY CLUSTERED 
(
	[NUM_DOC] ASC,
	[POS] ASC,
	[LIN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DOCUMENTOREC]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DOCUMENTOREC](
	[NUM_DOC] [numeric](10, 0) NOT NULL,
	[POS] [int] NOT NULL,
	[FECHAF] [date] NULL,
	[PERIODO] [int] NULL,
	[EJERCICIO] [int] NULL,
	[MONTO_BASE] [decimal](13, 5) NULL,
	[MONTO_FIJO] [decimal](13, 5) NULL,
	[MONTO_GRS] [decimal](13, 5) NULL,
	[MONTO_NET] [decimal](13, 5) NULL,
	[ESTATUS] [nchar](1) NULL,
	[PORC] [decimal](13, 5) NULL,
	[DOC_REF] [numeric](10, 0) NULL,
	[FECHAV] [date] NULL,
	[NUM_DOC_Q] [numeric](10, 0) NULL,
	[ESTATUS_Q] [nchar](1) NULL,
 CONSTRAINT [PK_DOCUMENTOREC] PRIMARY KEY CLUSTERED 
(
	[NUM_DOC] ASC,
	[POS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DOCUMENTOSAP]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DOCUMENTOSAP](
	[NUM_DOC] [numeric](10, 0) NOT NULL,
	[BUKRS] [nchar](4) NULL,
	[EJERCICIO] [int] NULL,
	[CUENTA_C] [nchar](10) NULL,
	[CUENTA_A] [nchar](10) NULL,
	[REGISTRO_PR] [nchar](10) NULL,
	[REGISTRO_NO] [nchar](10) NULL,
	[REGISTRO_RE] [nchar](10) NULL,
	[REGISTRO_AP] [nchar](10) NULL,
	[BLART] [nchar](2) NULL,
	[LIFNR] [nchar](10) NULL,
	[KUNNR] [nchar](10) NULL,
	[IMPORTE] [decimal](13, 5) NULL,
	[DESCR] [nvarchar](60) NULL,
	[FECHAC] [date] NULL,
 CONSTRAINT [PK_DOCUMENTOSAP] PRIMARY KEY CLUSTERED 
(
	[NUM_DOC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DOCUMENTOTS]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DOCUMENTOTS](
	[NUM_DOC] [numeric](10, 0) NOT NULL,
	[TSFORM_ID] [int] NOT NULL,
	[CHECKS] [bit] NULL,
 CONSTRAINT [PK_DOCUMENTOTS] PRIMARY KEY CLUSTERED 
(
	[NUM_DOC] ASC,
	[TSFORM_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ESTATUS]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ESTATUS](
	[ID] [int] NOT NULL,
	[DESCRIPCION] [nvarchar](50) NULL,
	[CLASS] [nvarchar](100) NULL,
	[ACTIVO] [bit] NOT NULL,
 CONSTRAINT [PK_ESTATUS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ESTATUSR]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ESTATUSR](
	[ESTATUS_ID] [int] NOT NULL,
	[POS] [int] NOT NULL,
	[REGEX] [nvarchar](100) NULL,
	[ACTIVO] [bit] NOT NULL,
 CONSTRAINT [PK_ESTATUSR] PRIMARY KEY CLUSTERED 
(
	[ESTATUS_ID] ASC,
	[POS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ESTATUST]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ESTATUST](
	[SPRAS_ID] [nchar](2) NOT NULL,
	[ESTATUS_ID] [int] NOT NULL,
	[TXT050] [nvarchar](50) NULL,
 CONSTRAINT [PK_ESTATUST] PRIMARY KEY CLUSTERED 
(
	[SPRAS_ID] ASC,
	[ESTATUS_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FACTURASCONF]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FACTURASCONF](
	[SOCIEDAD_ID] [nchar](4) NOT NULL,
	[PAIS_ID] [nchar](2) NOT NULL,
	[TSOL] [nvarchar](10) NOT NULL,
	[FACTURA] [bit] NOT NULL,
	[FECHA] [bit] NOT NULL,
	[PROVEEDOR] [bit] NOT NULL,
	[CONTROL] [bit] NOT NULL,
	[AUTORIZACION] [bit] NOT NULL,
	[VENCIMIENTO] [bit] NOT NULL,
	[FACTURAK] [bit] NOT NULL,
	[EJERCICIOK] [bit] NOT NULL,
	[BILL_DOC] [bit] NOT NULL,
	[BELNR] [bit] NOT NULL,
	[IMPORTE_FAC] [bit] NOT NULL,
	[PAYER] [bit] NOT NULL,
	[DESCRIPCION] [bit] NOT NULL,
	[SOCIEDAD] [bit] NOT NULL,
	[ACTIVO] [bit] NOT NULL,
 CONSTRAINT [PK_FACTURASCONF] PRIMARY KEY CLUSTERED 
(
	[SOCIEDAD_ID] ASC,
	[PAIS_ID] ASC,
	[TSOL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FLUJNEGO]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FLUJNEGO](
	[NUM_DOC] [numeric](10, 0) NOT NULL,
	[POS] [int] NOT NULL,
	[KUNNR] [nchar](10) NULL,
	[FECHAC] [datetime] NULL,
	[FECHAM] [datetime] NULL,
	[COMENTARIO] [nvarchar](255) NULL,
 CONSTRAINT [PK_FLUJNEGO_1] PRIMARY KEY CLUSTERED 
(
	[NUM_DOC] ASC,
	[POS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FLUJO]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FLUJO](
	[WORKF_ID] [nvarchar](10) NOT NULL,
	[WF_VERSION] [int] NOT NULL,
	[WF_POS] [int] NOT NULL,
	[NUM_DOC] [numeric](10, 0) NOT NULL,
	[POS] [int] NOT NULL,
	[DETPOS] [int] NOT NULL,
	[DETVER] [int] NULL,
	[LOOP] [int] NULL,
	[USUARIOA_ID] [nvarchar](16) NULL,
	[USUARIOD_ID] [nvarchar](16) NULL,
	[ESTATUS] [nchar](1) NULL,
	[FECHAC] [datetime] NULL,
	[FECHAM] [datetime] NULL,
	[COMENTARIO] [nvarchar](max) NULL,
	[STATUS] [nvarchar](20) NULL,
 CONSTRAINT [PK_FLUJO] PRIMARY KEY CLUSTERED 
(
	[WORKF_ID] ASC,
	[WF_VERSION] ASC,
	[WF_POS] ASC,
	[NUM_DOC] ASC,
	[POS] ASC,
	[DETPOS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GALL]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GALL](
	[ID] [nvarchar](5) NOT NULL,
	[DESCRIPCION] [nvarchar](60) NULL,
	[ACTIVO] [bit] NULL,
	[GRUPO_ALL] [varchar](5) NULL,
 CONSTRAINT [PK_GALL] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GALLT]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GALLT](
	[SPRAS_ID] [nchar](2) NOT NULL,
	[GALL_ID] [nvarchar](5) NOT NULL,
	[TXT50] [nvarchar](50) NULL,
 CONSTRAINT [PK_GALLT] PRIMARY KEY CLUSTERED 
(
	[SPRAS_ID] ASC,
	[GALL_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GAUTORIZACION]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GAUTORIZACION](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[CLAVE] [nchar](10) NULL,
	[NOMBRE] [nvarchar](50) NULL,
	[BUKRS] [nchar](4) NULL,
	[LAND] [nchar](2) NULL,
 CONSTRAINT [PK_GAUTORIZACION] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Grupo]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Grupo](
	[Grupo_ID] [int] NOT NULL,
	[SPRAS_ID] [nchar](2) NOT NULL,
	[Descripcion] [nvarchar](50) NULL,
 CONSTRAINT [PK_Grupo] PRIMARY KEY CLUSTERED 
(
	[Grupo_ID] ASC,
	[SPRAS_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GrupoCat]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrupoCat](
	[Grupo_ID] [int] NOT NULL,
	[SPRAS_ID] [nchar](2) NOT NULL,
	[Descripcion] [nvarchar](25) NULL,
	[Mantto] [int] NULL,
 CONSTRAINT [PK_GrupoCat] PRIMARY KEY CLUSTERED 
(
	[Grupo_ID] ASC,
	[SPRAS_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IIMPUESTO]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IIMPUESTO](
	[LAND] [nchar](2) NOT NULL,
	[MWSKZ] [nchar](2) NOT NULL,
	[KSCHL] [nchar](4) NULL,
	[KBETR] [decimal](11, 2) NULL,
	[ACTIVO] [bit] NOT NULL,
 CONSTRAINT [PK_IIMPUESTO] PRIMARY KEY CLUSTERED 
(
	[LAND] ASC,
	[MWSKZ] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IMPUESTO]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IMPUESTO](
	[MWSKZ] [nchar](2) NOT NULL,
	[ACTIVO] [bit] NOT NULL,
 CONSTRAINT [PK_IMPUESTO] PRIMARY KEY CLUSTERED 
(
	[MWSKZ] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LAYOUT_CARGA]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LAYOUT_CARGA](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LAND] [nchar](2) NOT NULL,
	[SOCIEDAD_ID] [nchar](4) NOT NULL,
	[RUTA] [nvarchar](max) NOT NULL,
	[FECHAC] [datetime] NOT NULL,
	[TIPO] [nchar](10) NOT NULL,
 CONSTRAINT [PK_LAYOUT_CARGA] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LEYENDA]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LEYENDA](
	[ID] [nchar](10) NOT NULL,
	[PAIS_ID] [nchar](2) NOT NULL,
	[LEYENDA] [nvarchar](500) NULL,
	[ACTIVO] [bit] NULL,
	[EDITABLE] [bit] NOT NULL,
	[OBLIGATORIA] [bit] NOT NULL,
 CONSTRAINT [PK_LEYENDA] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[PAIS_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MATERIAL]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MATERIAL](
	[ID] [nvarchar](18) NOT NULL,
	[MTART] [nchar](4) NULL,
	[MATKL_ID] [nvarchar](9) NULL,
	[MAKTX] [nvarchar](50) NULL,
	[MAKTG] [nvarchar](50) NULL,
	[MEINS] [nchar](3) NULL,
	[PUNIT] [decimal](13, 5) NULL,
	[ACTIVO] [bit] NULL,
	[CTGR] [nvarchar](5) NULL,
	[BRAND] [nvarchar](5) NULL,
	[MATERIALGP_ID] [nchar](3) NULL,
 CONSTRAINT [PK_MATERIAL] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MATERIALGP]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MATERIALGP](
	[ID] [nchar](3) NOT NULL,
	[DESCRIPCION] [nvarchar](50) NULL,
	[ACTIVO] [bit] NOT NULL,
	[EXCLUIR] [bit] NOT NULL,
	[UNICA] [bit] NOT NULL,
 CONSTRAINT [PK_MATERIALGP] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MATERIALGPT]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MATERIALGPT](
	[SPRAS_ID] [nchar](2) NOT NULL,
	[MATERIALGP_ID] [nchar](3) NOT NULL,
	[TXT50] [nvarchar](50) NULL,
 CONSTRAINT [PK_MATERIALGPT] PRIMARY KEY CLUSTERED 
(
	[SPRAS_ID] ASC,
	[MATERIALGP_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MATERIALT]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MATERIALT](
	[SPRAS] [nchar](2) NOT NULL,
	[MATERIAL_ID] [nvarchar](18) NOT NULL,
	[MAKTX] [nvarchar](50) NOT NULL,
	[MAKTG] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_MATERIALT] PRIMARY KEY CLUSTERED 
(
	[SPRAS] ASC,
	[MATERIAL_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MATERIALVKE]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MATERIALVKE](
	[MATERIAL_ID] [nvarchar](18) NOT NULL,
	[VKORG] [nvarchar](4) NOT NULL,
	[VTWEG] [nchar](4) NOT NULL,
	[ACTIVO] [bit] NOT NULL,
 CONSTRAINT [PK_MATERIALVKE] PRIMARY KEY CLUSTERED 
(
	[MATERIAL_ID] ASC,
	[VKORG] ASC,
	[VTWEG] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MENSAJES]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MENSAJES](
	[ID_MENSAJE] [int] NOT NULL,
	[SPRAS] [nvarchar](2) NOT NULL,
	[PAGINA_ID] [int] NOT NULL,
	[DESCRIPCION] [nvarchar](max) NULL,
 CONSTRAINT [PK__MENSAJES__22F75BB1ACEF7433] PRIMARY KEY CLUSTERED 
(
	[ID_MENSAJE] ASC,
	[SPRAS] ASC,
	[PAGINA_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MIEMBROS]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MIEMBROS](
	[USUARIO_ID] [nvarchar](16) NOT NULL,
	[ROL_ID] [int] NOT NULL,
	[ACTIVO] [bit] NULL,
 CONSTRAINT [PK_MIEMBROS] PRIMARY KEY CLUSTERED 
(
	[USUARIO_ID] ASC,
	[ROL_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MONEDA]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MONEDA](
	[WAERS] [nchar](3) NOT NULL,
	[ISOCD] [nchar](3) NULL,
	[ALTWR] [nchar](3) NULL,
	[LTEXT] [nvarchar](50) NULL,
	[KTEXT] [nvarchar](15) NULL,
	[ACTIVO] [bit] NOT NULL,
 CONSTRAINT [PK_MONEDA] PRIMARY KEY CLUSTERED 
(
	[WAERS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NEGOCIACION]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NEGOCIACION](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[FECHAI] [date] NOT NULL,
	[FECHAF] [date] NOT NULL,
	[FECHAN] [date] NOT NULL,
	[ACTIVO] [bit] NOT NULL,
 CONSTRAINT [PK_NEGOCIACION_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NEGOCIACION2]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NEGOCIACION2](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TITULO] [nvarchar](250) NOT NULL,
	[FINICIO] [date] NOT NULL,
	[FRECUENCIA] [nchar](1) NOT NULL,
	[FRECUENCIA_N] [int] NOT NULL,
	[DIA_SEMANA] [nvarchar](1) NULL,
	[DIA_MES] [int] NULL,
	[ORDINAL_MES] [int] NULL,
	[ORDINAL_DSEMANA] [nvarchar](1) NULL,
 CONSTRAINT [PK_NEGOCIACION2] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NOTICIA]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NOTICIA](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[FECHAI] [date] NOT NULL,
	[FECHAF] [date] NOT NULL,
	[PATH] [nvarchar](255) NOT NULL,
	[USUARIOC_ID] [nvarchar](16) NULL,
	[FECHAC] [datetime] NULL,
	[ACTIVO] [bit] NOT NULL,
 CONSTRAINT [PK_NOTICIA] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PAGINA]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PAGINA](
	[ID] [int] NOT NULL,
	[URL] [nvarchar](255) NULL,
	[TITULO] [nvarchar](50) NULL,
	[CARPETA_ID] [int] NULL,
	[ICON] [nvarchar](20) NULL,
	[MOSTRAR] [bit] NOT NULL,
	[ACTIVO] [bit] NOT NULL,
	[Mantenimiento] [int] NULL,
	[Grupo] [int] NULL,
	[Orden] [int] NULL,
 CONSTRAINT [PK_PAGINA] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PAGINAT]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PAGINAT](
	[SPRAS_ID] [nchar](2) NOT NULL,
	[ID] [int] NOT NULL,
	[TXT50] [nvarchar](50) NULL,
 CONSTRAINT [PK_PAGINAT] PRIMARY KEY CLUSTERED 
(
	[SPRAS_ID] ASC,
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PAIS]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PAIS](
	[LAND] [nchar](2) NOT NULL,
	[SPRAS] [nchar](2) NULL,
	[LANDX] [nvarchar](50) NULL,
	[IMAGE] [nchar](6) NULL,
	[ACTIVO] [bit] NOT NULL,
	[SOCIEDAD_ID] [nchar](4) NULL,
	[DECIMAL] [nchar](1) NOT NULL,
	[MILES] [nchar](1) NOT NULL,
	[BACKORDER] [bit] NOT NULL,
 CONSTRAINT [PK_PAIS] PRIMARY KEY CLUSTERED 
(
	[LAND] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PERIODO]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PERIODO](
	[ID] [int] NOT NULL,
	[DESCRIPCION] [nchar](10) NULL,
	[ACTIVO] [nchar](10) NULL,
 CONSTRAINT [PK_PERIODO] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PERIODO445]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PERIODO445](
	[EJERCICIO] [int] NOT NULL,
	[MES_NATURAL] [int] NOT NULL,
	[DIA_NATURAL] [int] NOT NULL,
	[PERIODO] [int] NOT NULL,
	[ACTIVO] [bit] NOT NULL,
	[SUMA] [int] NOT NULL,
 CONSTRAINT [PK_PERIODO445] PRIMARY KEY CLUSTERED 
(
	[EJERCICIO] ASC,
	[MES_NATURAL] ASC,
	[DIA_NATURAL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PERIODOT]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PERIODOT](
	[SPRAS_ID] [nchar](2) NOT NULL,
	[PERIODO_ID] [int] NOT NULL,
	[TXT50] [nvarchar](50) NULL,
	[TXT03] [nvarchar](3) NULL,
	[TXT01] [nchar](1) NULL,
 CONSTRAINT [PK_PERIODOT] PRIMARY KEY CLUSTERED 
(
	[SPRAS_ID] ASC,
	[PERIODO_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PERMISO_PAGINA]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PERMISO_PAGINA](
	[PAGINA_ID] [int] NOT NULL,
	[ROL_ID] [int] NOT NULL,
	[PERMISO] [bit] NULL,
 CONSTRAINT [PK_PERMISO_PAGINA] PRIMARY KEY CLUSTERED 
(
	[PAGINA_ID] ASC,
	[ROL_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[POSICION]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[POSICION](
	[ID] [nvarchar](10) NOT NULL,
	[DESCRIPCION] [nvarchar](10) NULL,
 CONSTRAINT [PK_POSICION] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PRESUPSAPH]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRESUPSAPH](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ANIO] [int] NOT NULL,
	[USUARIO_ID] [nvarchar](16) NULL,
	[FECHAC] [datetime] NULL,
 CONSTRAINT [PK_PRESUPSAPH] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[ANIO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PRESUPSAPP]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRESUPSAPP](
	[ID] [int] NOT NULL,
	[ANIO] [int] NOT NULL,
	[POS] [int] NOT NULL,
	[PERIOD] [int] NULL,
	[TYPE] [nchar](1) NULL,
	[BUKRS] [nchar](4) NULL,
	[VKORG] [nvarchar](4) NULL,
	[VTWEG] [nchar](2) NULL,
	[SPART] [nchar](2) NULL,
	[VKBUR] [nchar](4) NULL,
	[VKGRP] [nchar](3) NULL,
	[BZIRK] [nchar](4) NULL,
	[MATNR] [nvarchar](18) NULL,
	[PRDHA] [nvarchar](18) NULL,
	[KUNNR] [nvarchar](10) NULL,
	[KUNNR_P] [nchar](10) NULL,
	[BANNER] [nchar](10) NULL,
	[BANNER_CALC] [nchar](10) NULL,
	[KUNNR_PAY] [nchar](10) NULL,
	[FECHAP] [nchar](10) NULL,
	[UNAME] [nvarchar](16) NULL,
	[XBLNR] [nvarchar](16) NULL,
	[VVX17] [decimal](13, 2) NULL,
	[CSHDC] [decimal](13, 2) NULL,
	[RECUN] [decimal](13, 2) NULL,
	[DSTRB] [decimal](13, 2) NULL,
	[OTHTA] [decimal](13, 2) NULL,
	[ADVER] [decimal](13, 2) NULL,
	[CORPM] [decimal](13, 2) NULL,
	[POP] [decimal](13, 2) NULL,
	[OTHER] [decimal](13, 2) NULL,
	[CONPR] [decimal](13, 2) NULL,
	[OHV] [decimal](13, 2) NULL,
	[FREEG] [decimal](13, 2) NULL,
	[RSRDV] [decimal](13, 2) NULL,
	[SPA] [decimal](13, 2) NULL,
	[PMVAR] [decimal](13, 2) NULL,
	[GRSLS] [decimal](13, 2) NULL,
	[NETLB] [decimal](13, 2) NULL,
 CONSTRAINT [PK_PRESUPSAPP] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[ANIO] ASC,
	[POS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PRESUPUESTOH]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRESUPUESTOH](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ANIO] [nchar](4) NOT NULL,
	[USUARIO_ID] [nvarchar](16) NULL,
	[FECHAC] [datetime] NULL,
 CONSTRAINT [PK_PRESUPUESTOH] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[ANIO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PRESUPUESTOP]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRESUPUESTOP](
	[ID] [int] NOT NULL,
	[ANIO] [nchar](4) NOT NULL,
	[POS] [int] NOT NULL,
	[MES] [nchar](3) NULL,
	[VERSION] [nvarchar](50) NULL,
	[PAIS] [nvarchar](15) NULL,
	[REGION] [nvarchar](15) NULL,
	[MONEDA] [nchar](2) NULL,
	[MATERIAL] [nvarchar](18) NULL,
	[BANNER] [nvarchar](10) NULL,
	[ADVER] [float] NULL,
	[CONPR] [float] NULL,
	[CSHDC] [float] NULL,
	[DIRLB] [float] NULL,
	[DSTRB] [float] NULL,
	[FREEG] [float] NULL,
	[GRSLS] [float] NULL,
	[NETLB] [float] NULL,
	[OVHDF] [float] NULL,
	[OVHDV] [float] NULL,
	[PKGMT] [float] NULL,
	[PMVAR] [float] NULL,
	[POP] [float] NULL,
	[PURCH] [float] NULL,
	[RAWMT] [float] NULL,
	[RECUN] [float] NULL,
	[RSRDV] [float] NULL,
	[TOTCS] [float] NULL,
	[VVX17] [float] NULL,
	[OTHTA] [float] NULL,
	[CORPM] [float] NULL,
	[SPA] [float] NULL,
 CONSTRAINT [PK_PRESUPUESTOP] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[ANIO] ASC,
	[POS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PROVEEDOR]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PROVEEDOR](
	[ID] [nchar](10) NOT NULL,
	[NOMBRE] [nvarchar](250) NULL,
	[SOCIEDAD_ID] [nchar](4) NULL,
	[PAIS_ID] [nchar](2) NULL,
	[ACTIVO] [bit] NOT NULL,
 CONSTRAINT [PK_PROVEEDOR] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PUESTO]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PUESTO](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ACTIVO] [bit] NULL,
 CONSTRAINT [PK_PUESTO] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PUESTOT]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PUESTOT](
	[SPRAS_ID] [nchar](2) NOT NULL,
	[PUESTO_ID] [int] NOT NULL,
	[TXT50] [nvarchar](50) NULL,
 CONSTRAINT [PK_PUESTOT] PRIMARY KEY CLUSTERED 
(
	[SPRAS_ID] ASC,
	[PUESTO_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RANGO]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RANGO](
	[ID] [nchar](2) NOT NULL,
	[INICIO] [numeric](18, 0) NULL,
	[FIN] [numeric](18, 0) NULL,
	[ACTUAL] [numeric](18, 0) NULL,
	[ACTIVO] [bit] NOT NULL,
 CONSTRAINT [PK_RANGO] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[REGION]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REGION](
	[REGION] [nchar](6) NOT NULL,
	[SOCIEDAD] [nchar](4) NULL,
 CONSTRAINT [PK_REGION] PRIMARY KEY CLUSTERED 
(
	[REGION] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RETENCION]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RETENCION](
	[ID] [int] NOT NULL,
	[DESCRIPCIÓN] [nvarchar](50) NULL,
	[PORC] [decimal](13, 2) NULL,
	[ACTIVO] [bit] NOT NULL,
 CONSTRAINT [PK_RETENCION] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RETENCIONT]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RETENCIONT](
	[SPRAS_ID] [nchar](2) NOT NULL,
	[RETENCION_ID] [int] NOT NULL,
	[TXT50] [nvarchar](50) NULL,
 CONSTRAINT [PK_RETENCIONT] PRIMARY KEY CLUSTERED 
(
	[SPRAS_ID] ASC,
	[RETENCION_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ROL]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ROL](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CLAVE] [nvarchar](10) NULL,
	[NOMBRE] [nvarchar](50) NULL,
	[ACTIVO] [bit] NULL,
 CONSTRAINT [PK_ROL] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ROLT]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ROLT](
	[SPRAS_ID] [nchar](2) NOT NULL,
	[ROL_ID] [int] NOT NULL,
	[TXT50] [nvarchar](50) NULL,
 CONSTRAINT [PK_ROLT] PRIMARY KEY CLUSTERED 
(
	[SPRAS_ID] ASC,
	[ROL_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SOCIEDAD]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SOCIEDAD](
	[BUKRS] [nchar](4) NOT NULL,
	[BUTXT] [nvarchar](50) NULL,
	[ORT01] [nvarchar](50) NULL,
	[LAND] [nchar](2) NULL,
	[SUBREGIO] [nchar](6) NULL,
	[WAERS] [nchar](3) NULL,
	[SPRAS] [nchar](2) NULL,
	[NAME1] [nvarchar](50) NULL,
	[KTOPL] [nchar](10) NULL,
	[ACTIVO] [bit] NOT NULL,
	[REGION] [nchar](10) NULL,
 CONSTRAINT [PK_SOCIEDAD] PRIMARY KEY CLUSTERED 
(
	[BUKRS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SPRAS]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SPRAS](
	[ID] [nchar](2) NOT NULL,
	[DESCRIPCION] [nvarchar](20) NULL,
 CONSTRAINT [PK_SPRAS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[STATES]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[STATES](
	[ID] [int] NOT NULL,
	[NAME] [varchar](30) NOT NULL,
	[COUNTRY_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TAB]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TAB](
	[ID] [nvarchar](50) NOT NULL,
	[CONTAINER] [nvarchar](50) NOT NULL,
	[DESCR] [nvarchar](50) NULL,
	[ACTIVO] [bit] NOT NULL,
 CONSTRAINT [PK_TAB] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TAB_CAMPO]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TAB_CAMPO](
	[TAB_ID] [nvarchar](50) NOT NULL,
	[CAMPO_ID] [nvarchar](25) NOT NULL,
	[ACTIVO] [bit] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TALL]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TALL](
	[ID] [nvarchar](10) NOT NULL,
	[DESCRIPCION] [nvarchar](62) NULL,
	[FECHAI] [date] NULL,
	[FECHAF] [date] NULL,
	[GALL_ID] [nvarchar](5) NULL,
	[ACTIVO] [bit] NULL,
 CONSTRAINT [PK_TALL] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TALLT]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TALLT](
	[SPRAS_ID] [nchar](2) NOT NULL,
	[TALL_ID] [nvarchar](10) NOT NULL,
	[TXT50] [nvarchar](62) NULL,
 CONSTRAINT [PK_TALLT] PRIMARY KEY CLUSTERED 
(
	[SPRAS_ID] ASC,
	[TALL_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TAX_LAND]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TAX_LAND](
	[SOCIEDAD_ID] [nchar](4) NOT NULL,
	[PAIS_ID] [nchar](2) NOT NULL,
	[ACTIVO] [bit] NULL,
 CONSTRAINT [PK_TAX_LAND] PRIMARY KEY CLUSTERED 
(
	[SOCIEDAD_ID] ASC,
	[PAIS_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TAXEOH]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TAXEOH](
	[SOCIEDAD_ID] [nchar](4) NOT NULL,
	[PAIS_ID] [nchar](2) NOT NULL,
	[VKORG] [nvarchar](4) NOT NULL,
	[VTWEG] [nchar](2) NOT NULL,
	[SPART] [nchar](2) NOT NULL,
	[KUNNR] [nchar](10) NOT NULL,
	[CONCEPTO_ID] [int] NOT NULL,
	[TNOTA_ID] [int] NULL,
	[FECHAC] [date] NULL,
	[USUARIOC_ID] [nvarchar](16) NULL,
	[FECHAM] [date] NULL,
	[USUARIOM_ID] [nvarchar](16) NULL,
	[IMPUESTO_ID] [nchar](2) NULL,
	[PORC] [decimal](13, 2) NULL,
	[PAY_T] [nchar](4) NULL,
	[ACTIVO] [bit] NOT NULL,
 CONSTRAINT [PK_TAXEOH] PRIMARY KEY CLUSTERED 
(
	[SOCIEDAD_ID] ASC,
	[PAIS_ID] ASC,
	[VKORG] ASC,
	[VTWEG] ASC,
	[SPART] ASC,
	[KUNNR] ASC,
	[CONCEPTO_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TAXEOP]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TAXEOP](
	[SOCIEDAD_ID] [nchar](4) NOT NULL,
	[PAIS_ID] [nchar](2) NOT NULL,
	[VKORG] [nvarchar](4) NOT NULL,
	[VTWEG] [nchar](2) NOT NULL,
	[SPART] [nchar](2) NOT NULL,
	[KUNNR] [nchar](10) NOT NULL,
	[CONCEPTO_ID] [int] NOT NULL,
	[POS] [int] NOT NULL,
	[RETENCION_ID] [int] NULL,
	[PORC] [decimal](13, 2) NULL,
	[ACTIVO] [bit] NOT NULL,
	[TRETENCION_ID] [nchar](2) NULL,
 CONSTRAINT [PK_TAXEOP] PRIMARY KEY CLUSTERED 
(
	[SOCIEDAD_ID] ASC,
	[PAIS_ID] ASC,
	[VKORG] ASC,
	[VTWEG] ASC,
	[SPART] ASC,
	[KUNNR] ASC,
	[CONCEPTO_ID] ASC,
	[POS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TCAMBIO]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TCAMBIO](
	[KURST] [nchar](4) NOT NULL,
	[FCURR] [nchar](3) NOT NULL,
	[TCURR] [nchar](3) NOT NULL,
	[GDATU] [date] NOT NULL,
	[UKURS] [decimal](9, 5) NULL,
 CONSTRAINT [PK_TCAMBIO] PRIMARY KEY CLUSTERED 
(
	[KURST] ASC,
	[FCURR] ASC,
	[TCURR] ASC,
	[GDATU] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TCLIENTE]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TCLIENTE](
	[ID] [nvarchar](2) NOT NULL,
	[ACTIVO] [bit] NOT NULL,
 CONSTRAINT [PK_TCLIENTE] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TCLIENTET]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TCLIENTET](
	[SPRAS] [nchar](2) NOT NULL,
	[PARVW_ID] [nvarchar](2) NOT NULL,
	[TXT50] [nvarchar](50) NULL,
 CONSTRAINT [PK_TCLIENTET] PRIMARY KEY CLUSTERED 
(
	[SPRAS] ASC,
	[PARVW_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TEXTO]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEXTO](
	[PAGINA_ID] [int] NOT NULL,
	[CAMPO_ID] [nvarchar](25) NOT NULL,
	[SPRAS_ID] [nchar](2) NOT NULL,
	[TEXTOS] [nvarchar](555) NULL,
	[ACTIVO] [bit] NULL,
 CONSTRAINT [PK_TEXTO] PRIMARY KEY CLUSTERED 
(
	[PAGINA_ID] ASC,
	[CAMPO_ID] ASC,
	[SPRAS_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TEXTOCV]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TEXTOCV](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SPRAS_ID] [nchar](2) NOT NULL,
	[CAMPO] [nvarchar](15) NOT NULL,
	[TEXTO] [varchar](250) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TRETENCION]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TRETENCION](
	[ID] [nchar](2) NOT NULL,
	[DESCRIPCION] [nvarchar](50) NULL,
	[ACTIVO] [bit] NULL,
 CONSTRAINT [PK_TRETENCION] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TRETENCIONT]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TRETENCIONT](
	[SPRAS_ID] [nchar](2) NOT NULL,
	[TRETENCION_ID] [nchar](2) NOT NULL,
	[TXT50] [nvarchar](50) NULL,
 CONSTRAINT [PK_TRETENCIONT] PRIMARY KEY CLUSTERED 
(
	[SPRAS_ID] ASC,
	[TRETENCION_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TREVERSA]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TREVERSA](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DESCRIPCION] [nvarchar](50) NULL,
	[USUARIOC_ID] [nvarchar](16) NULL,
	[FECHAC] [date] NULL,
	[ACTIVO] [bit] NULL,
 CONSTRAINT [PK_TREVERSA] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TREVERSAT]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TREVERSAT](
	[SPRAS_ID] [nchar](2) NOT NULL,
	[TREVERSA_ID] [int] NOT NULL,
	[TXT100] [nvarchar](100) NULL,
 CONSTRAINT [PK_TREVERSAT] PRIMARY KEY CLUSTERED 
(
	[SPRAS_ID] ASC,
	[TREVERSA_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TS_CAMPO]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TS_CAMPO](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ACTIVO] [bit] NOT NULL,
 CONSTRAINT [PK_TS_CAMPO] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TS_FORM]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TS_FORM](
	[ID] [int] NOT NULL,
	[BUKRS_ID] [nchar](4) NOT NULL,
	[LAND_ID] [nchar](2) NOT NULL,
	[POS] [int] NOT NULL,
	[CAMPO] [nvarchar](50) NULL,
 CONSTRAINT [PK_TS_FORM] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TS_FORMT]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TS_FORMT](
	[SPRAS_ID] [nchar](2) NOT NULL,
	[TSFORM_ID] [int] NOT NULL,
	[TXT100] [nvarchar](100) NULL,
 CONSTRAINT [PK_TS_FORMT] PRIMARY KEY CLUSTERED 
(
	[SPRAS_ID] ASC,
	[TSFORM_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TSOL]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TSOL](
	[ID] [nvarchar](10) NOT NULL,
	[DESCRIPCION] [nvarchar](60) NULL,
	[TSOLR] [nvarchar](10) NULL,
	[RANGO_ID] [nchar](2) NOT NULL,
	[ESTATUS] [nchar](1) NOT NULL,
	[FACTURA] [bit] NOT NULL,
	[PADRE] [bit] NOT NULL,
	[ADICIONA] [bit] NOT NULL,
	[TSOLM] [nvarchar](10) NULL,
	[TSOLC] [nvarchar](10) NULL,
	[TRECU] [nchar](1) NULL,
	[NEGO] [bit] NOT NULL,
	[CARTA] [bit] NOT NULL,
	[REVERSO] [bit] NOT NULL,
	[ACTIVO] [bit] NULL,
 CONSTRAINT [PK_TSOL] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TSOL_GROUP]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TSOL_GROUP](
	[ID] [nvarchar](10) NOT NULL,
	[TIPO] [nvarchar](5) NOT NULL,
	[DESCRIPCION] [nvarchar](60) NOT NULL,
	[ID_PADRE] [nvarchar](10) NULL,
	[TIPO_PADRE] [nvarchar](5) NULL,
 CONSTRAINT [PK_TSOL_GROUP] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[TIPO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TSOL_GROUPT]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TSOL_GROUPT](
	[SPRAS_ID] [nchar](2) NOT NULL,
	[TSOL_GROUP_ID] [nvarchar](10) NOT NULL,
	[TSOL_GROUP_TIPO] [nvarchar](5) NOT NULL,
	[TXT50] [nvarchar](50) NULL,
 CONSTRAINT [PK_TSOL_GROUPT] PRIMARY KEY CLUSTERED 
(
	[SPRAS_ID] ASC,
	[TSOL_GROUP_ID] ASC,
	[TSOL_GROUP_TIPO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TSOL_TREE]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TSOL_TREE](
	[TSOL_GROUP_ID] [nvarchar](10) NOT NULL,
	[TSOL_GROUP_TIPO] [nvarchar](5) NOT NULL,
	[TSOL_ID] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_TSOL_TREE_1] PRIMARY KEY CLUSTERED 
(
	[TSOL_ID] ASC,
	[TSOL_GROUP_ID] ASC,
	[TSOL_GROUP_TIPO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TSOLT]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TSOLT](
	[SPRAS_ID] [nchar](2) NOT NULL,
	[TSOL_ID] [nvarchar](10) NOT NULL,
	[TXT020] [nvarchar](20) NULL,
	[TXT50] [nvarchar](50) NULL,
	[TXT010] [nvarchar](10) NULL,
 CONSTRAINT [PK_TSOLT] PRIMARY KEY CLUSTERED 
(
	[SPRAS_ID] ASC,
	[TSOL_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TSOPORTE]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TSOPORTE](
	[ID] [nchar](3) NOT NULL,
	[DESCRIPCION] [nvarchar](50) NULL,
	[ACTIVO] [bit] NULL,
 CONSTRAINT [PK_TSOPORTE] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TSOPORTET]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TSOPORTET](
	[SPRAS_ID] [nchar](2) NOT NULL,
	[TSOPORTE_ID] [nchar](3) NOT NULL,
	[TXT50] [nvarchar](50) NULL,
 CONSTRAINT [PK_TSOPORTET] PRIMARY KEY CLUSTERED 
(
	[SPRAS_ID] ASC,
	[TSOPORTE_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TX_CONCEPTO]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TX_CONCEPTO](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DESCRIPCION] [nvarchar](50) NULL,
	[ACTIVO] [bit] NULL,
 CONSTRAINT [PK_TX_CONCEPTO] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TX_CONCEPTOT]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TX_CONCEPTOT](
	[SPRAS_ID] [nchar](2) NOT NULL,
	[CONCEPTO_ID] [int] NOT NULL,
	[TXT50] [nvarchar](50) NULL,
 CONSTRAINT [PK_TX_CONCEPTOT] PRIMARY KEY CLUSTERED 
(
	[SPRAS_ID] ASC,
	[CONCEPTO_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TX_NOTAT]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TX_NOTAT](
	[SPRAS_ID] [nchar](2) NOT NULL,
	[TNOTA_ID] [int] NOT NULL,
	[TXT50] [nvarchar](50) NULL,
 CONSTRAINT [PK_TX_NOTAT] PRIMARY KEY CLUSTERED 
(
	[SPRAS_ID] ASC,
	[TNOTA_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TX_TNOTA]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TX_TNOTA](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DESCRIPCION] [nvarchar](50) NULL,
	[ACTIVO] [bit] NULL,
 CONSTRAINT [PK_TX_TNOTA] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UMEDIDA]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UMEDIDA](
	[MSEHI] [nchar](3) NOT NULL,
	[ACTIVO] [bit] NULL,
 CONSTRAINT [PK_UMEDIDA] PRIMARY KEY CLUSTERED 
(
	[MSEHI] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UMEDIDAT]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UMEDIDAT](
	[SPRAS_ID] [nchar](2) NOT NULL,
	[MSEHI] [nchar](3) NOT NULL,
	[MSEH3] [nchar](3) NULL,
	[MSEH6] [nchar](6) NULL,
	[MSEHT] [nvarchar](10) NULL,
	[MSEHL] [nvarchar](30) NULL,
 CONSTRAINT [PK_UMEDIDAT] PRIMARY KEY CLUSTERED 
(
	[SPRAS_ID] ASC,
	[MSEHI] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[USUARIO]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USUARIO](
	[ID] [nvarchar](16) NOT NULL,
	[PASS] [nvarchar](50) NOT NULL,
	[NOMBRE] [nvarchar](50) NOT NULL,
	[APELLIDO_P] [nvarchar](50) NOT NULL,
	[APELLIDO_M] [nvarchar](50) NULL,
	[EMAIL] [nvarchar](256) NOT NULL,
	[SPRAS_ID] [nchar](2) NOT NULL,
	[ACTIVO] [bit] NULL,
	[PUESTO_ID] [int] NULL,
	[MANAGER] [nvarchar](16) NULL,
	[BACKUP_ID] [nvarchar](16) NULL,
	[BUNIT] [nchar](4) NOT NULL,
 CONSTRAINT [PK_USUARIO] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[USUARIOF]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USUARIOF](
	[USUARIO_ID] [nvarchar](16) NOT NULL,
	[VKORG] [nvarchar](4) NOT NULL,
	[VTWEG] [nchar](2) NOT NULL,
	[SPART] [nchar](2) NOT NULL,
	[KUNNR] [nchar](10) NOT NULL,
	[ACTIVO] [bit] NULL,
	[USUARIOC_ID] [nvarchar](16) NULL,
	[FECHAC] [datetime] NULL,
	[USUARIOM_ID] [nvarchar](16) NULL,
	[FECHAM] [datetime] NULL,
 CONSTRAINT [PK_USUARIOF] PRIMARY KEY CLUSTERED 
(
	[USUARIO_ID] ASC,
	[VKORG] ASC,
	[VTWEG] ASC,
	[SPART] ASC,
	[KUNNR] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[USUARIOGA]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USUARIOGA](
	[USUARIO_ID] [nvarchar](16) NOT NULL,
	[AGROUP_ID] [bigint] NOT NULL,
 CONSTRAINT [PK_USUARIOGA] PRIMARY KEY CLUSTERED 
(
	[USUARIO_ID] ASC,
	[AGROUP_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[USUARIOLOG]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USUARIOLOG](
	[USUARIO_ID] [nvarchar](16) NOT NULL,
	[POS] [int] NULL,
	[SESION] [nvarchar](max) NULL,
	[NAVEGADOR] [nvarchar](max) NULL,
	[UBICACION] [nvarchar](max) NULL,
	[FECHA] [datetime] NULL,
	[LOGIN] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[USUARIO_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[USUARIOSAP]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USUARIOSAP](
	[IDUSUARIO] [nvarchar](50) NOT NULL,
	[AUTOMATICO] [bit] NULL,
 CONSTRAINT [PK_USUARIOSAP] PRIMARY KEY CLUSTERED 
(
	[IDUSUARIO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[USUARIOSOC]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USUARIOSOC](
	[USUARIO_ID] [nvarchar](16) NOT NULL,
	[SOCIEDAD_ID] [nchar](4) NOT NULL,
 CONSTRAINT [PK_USUARIOSOC] PRIMARY KEY CLUSTERED 
(
	[USUARIO_ID] ASC,
	[SOCIEDAD_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WARNING]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WARNING](
	[PAGINA_ID] [int] NOT NULL,
	[CAMPO_ID] [nvarchar](25) NOT NULL,
	[SPRAS_ID] [nchar](2) NOT NULL,
	[WARNING] [nvarchar](255) NULL,
	[POSICION] [nvarchar](10) NULL,
	[ACTIVO] [bit] NULL,
 CONSTRAINT [PK_WARNING] PRIMARY KEY CLUSTERED 
(
	[PAGINA_ID] ASC,
	[CAMPO_ID] ASC,
	[SPRAS_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WARNING_COND]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WARNING_COND](
	[TAB_ID] [nvarchar](50) NOT NULL,
	[WARNING_ID] [nvarchar](20) NOT NULL,
	[POS] [int] NOT NULL,
	[ANDOR] [nvarchar](50) NULL,
	[CONDICION_ID] [int] NULL,
	[VALOR_COMP] [nvarchar](250) NULL,
	[ORAND] [nvarchar](50) NULL,
	[ACTIVO] [bit] NOT NULL,
 CONSTRAINT [PK_WARNING_COND_1] PRIMARY KEY CLUSTERED 
(
	[TAB_ID] ASC,
	[WARNING_ID] ASC,
	[POS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WARNINGP]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WARNINGP](
	[TAB_ID] [nvarchar](50) NOT NULL,
	[ID] [nvarchar](20) NOT NULL,
	[DESCR] [nvarchar](50) NULL,
	[SOCIEDAD_ID] [nchar](4) NULL,
	[TSOL_ID] [nvarchar](10) NULL,
	[TIPO] [nchar](1) NULL,
	[CAMPOVAL_ID] [nvarchar](50) NULL,
	[PAGINA_ID] [int] NULL,
	[CAMPO_ID] [nvarchar](25) NULL,
	[ACCION] [nvarchar](20) NULL,
 CONSTRAINT [PK_WARNINGP_1] PRIMARY KEY CLUSTERED 
(
	[TAB_ID] ASC,
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WARNINGPT]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WARNINGPT](
	[SPRAS_ID] [nchar](2) NOT NULL,
	[TAB_ID] [nvarchar](50) NOT NULL,
	[WARNING_ID] [nvarchar](20) NOT NULL,
	[TXT100] [nvarchar](100) NULL,
 CONSTRAINT [PK_WARNINGPT_1] PRIMARY KEY CLUSTERED 
(
	[SPRAS_ID] ASC,
	[TAB_ID] ASC,
	[WARNING_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WORKFH]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WORKFH](
	[ID] [nvarchar](10) NOT NULL,
	[DESCRIPCION] [nvarchar](60) NULL,
	[TSOL_ID] [nvarchar](10) NULL,
	[ESTATUS] [bit] NULL,
	[USUARIO_ID] [nvarchar](16) NULL,
	[FECHAC] [datetime] NULL,
	[BUKRS] [nchar](4) NULL,
	[ROL_ID] [int] NULL,
 CONSTRAINT [PK_WORKFH] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WORKFP]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WORKFP](
	[ID] [nvarchar](10) NOT NULL,
	[VERSION] [int] NOT NULL,
	[POS] [int] NOT NULL,
	[AGENTE_ID] [int] NULL,
	[ACCION_ID] [int] NULL,
	[NEXT_STEP] [int] NULL,
	[NS_ACCEPT] [int] NULL,
	[NS_REJECT] [int] NULL,
	[LOOPS] [int] NULL,
	[CONDICION_ID] [int] NULL,
	[NS_CN_ACCEPT] [int] NULL,
	[NS_CN_REJECT] [int] NULL,
	[EMAIL] [nchar](1) NULL,
	[EMAIL_TXT_ID] [int] NULL,
	[EMAIL_INN_ID] [int] NULL,
 CONSTRAINT [PK_WORKFP] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[VERSION] ASC,
	[POS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WORKFT]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WORKFT](
	[SPRAS_ID] [nchar](2) NOT NULL,
	[WF_ID] [nvarchar](10) NOT NULL,
	[WF_VERSION] [int] NOT NULL,
	[TXT20] [nvarchar](20) NULL,
	[TXT50] [nvarchar](50) NULL,
 CONSTRAINT [PK_WORKFT] PRIMARY KEY CLUSTERED 
(
	[SPRAS_ID] ASC,
	[WF_ID] ASC,
	[WF_VERSION] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WORKFV]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WORKFV](
	[ID] [nvarchar](10) NOT NULL,
	[VERSION] [int] NOT NULL,
	[DESCRIPCION] [nvarchar](60) NULL,
	[ESTATUS] [bit] NULL,
	[FECHAI] [date] NULL,
	[FECHAF] [date] NULL,
	[USUARIO_ID] [nvarchar](16) NULL,
	[FECHAC] [datetime] NULL,
 CONSTRAINT [PK_WORKV] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[VERSION] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ZBRAND]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ZBRAND](
	[ID_ZB] [nvarchar](5) NOT NULL,
	[Descripcion] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_ZB] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ZCTGR]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ZCTGR](
	[ID_ZC] [nvarchar](5) NOT NULL,
	[DESCRIPCION] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_ZC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[CARPETAV]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[CARPETAV]
AS
SELECT        dbo.USUARIO.ID AS USUARIO_ID, dbo.CARPETA.ID, dbo.CARPETA.URL, dbo.CARPETAT.TXT50, dbo.CARPETA.ICON
FROM            dbo.PAGINA INNER JOIN
                         dbo.PERMISO_PAGINA ON dbo.PAGINA.ID = dbo.PERMISO_PAGINA.PAGINA_ID INNER JOIN
                         dbo.USUARIO ON dbo.PERMISO_PAGINA.ROL_ID = dbo.USUARIO.PUESTO_ID INNER JOIN
                         dbo.CARPETA ON dbo.PAGINA.CARPETA_ID = dbo.CARPETA.ID INNER JOIN
                         dbo.CARPETAT ON dbo.CARPETA.ID = dbo.CARPETAT.ID AND dbo.CARPETAT.SPRAS_ID = dbo.USUARIO.SPRAS_ID
WHERE        (dbo.PERMISO_PAGINA.PERMISO = 1)
GROUP BY dbo.CARPETA.ID, dbo.CARPETA.URL, dbo.CARPETAT.TXT50, dbo.CARPETA.ICON, dbo.USUARIO.ID




GO
/****** Object:  View [dbo].[CREADOR]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[CREADOR]
AS
SELECT        u.ID, d.PUESTOC_ID, d.POS, d.AGROUP_ID, d.ACTIVO, ga.BUKRS, ga.LAND
FROM            dbo.USUARIO AS u INNER JOIN
                         dbo.USUARIOGA AS g ON u.ID = g.USUARIO_ID INNER JOIN
                         dbo.DET_AGENTE AS d ON g.AGROUP_ID = d.AGROUP_ID AND u.PUESTO_ID = d.PUESTOC_ID INNER JOIN
                         dbo.GAUTORIZACION AS ga ON d.AGROUP_ID = ga.ID
WHERE        (d.POS = 1)



GO
/****** Object:  View [dbo].[CREADOR2]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[CREADOR2]
AS
SELECT        u.ID, d.PUESTOC_ID, d.POS, d.AGROUP_ID, d.ACTIVO, ga.BUKRS, ga.LAND
FROM            dbo.USUARIO AS u INNER JOIN
                         dbo.USUARIOGA AS g ON u.ID = g.USUARIO_ID INNER JOIN
                         dbo.DET_AGENTEP AS d ON g.AGROUP_ID = d.AGROUP_ID AND u.PUESTO_ID = d.PUESTOC_ID INNER JOIN
                         dbo.GAUTORIZACION AS ga ON d.AGROUP_ID = ga.ID
WHERE        (d.POS = 1)



GO
/****** Object:  View [dbo].[DET_APROBV]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[DET_APROBV]
AS
SELECT DISTINCT dbo.DET_APROB.BUKRS, dbo.DET_APROB.PUESTOC_ID, dbo.PUESTOT.TXT50, dbo.PUESTOT.SPRAS_ID
FROM            dbo.DET_APROB INNER JOIN
                         dbo.PUESTOT ON dbo.DET_APROB.PUESTOC_ID = dbo.PUESTOT.PUESTO_ID



GO
/****** Object:  View [dbo].[DOCUMENTOV]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[DOCUMENTOV]
AS
SELECT        f.POS, d.NUM_DOC, d.TSOL_ID, d.TALL_ID, d.SOCIEDAD_ID, d.PAIS_ID, d.PERIODO, d.EJERCICIO, d.TIPO_TECNICO, d.TIPO_RECURRENTE, d.CANTIDAD_EV, 
                         d.USUARIOC_ID, d.FECHAD, d.FECHAC, d.HORAC, d.FECHAC_PLAN, d.FECHAC_USER, d.HORAC_USER, d.ESTATUS, d.ESTATUS_C, d.ESTATUS_SAP, 
                         d.ESTATUS_WF, d.DOCUMENTO_REF, d.CONCEPTO, d.NOTAS, d.MONTO_DOC_MD, d.MONTO_FIJO_MD, d.MONTO_BASE_GS_PCT_MD, 
                         d.MONTO_BASE_NS_PCT_MD, d.MONTO_DOC_ML, d.MONTO_FIJO_ML, d.MONTO_BASE_GS_PCT_ML, d.MONTO_BASE_NS_PCT_ML, d.MONTO_DOC_ML2, 
                         d.MONTO_FIJO_ML2, d.MONTO_BASE_GS_PCT_ML2, d.MONTO_BASE_NS_PCT_ML2, d.PORC_ADICIONAL, d.IMPUESTO, d.FECHAI_VIG, d.FECHAF_VIG, 
                         d.ESTATUS_EXT, d.SOLD_TO_ID, d.PAYER_ID, d.PAYER_NOMBRE, d.PAYER_EMAIL, d.GRUPO_CTE_ID, d.CANAL_ID, d.MONEDA_ID, d.MONEDAL_ID, 
                         d.MONEDAL2_ID, d.TIPO_CAMBIO, d.TIPO_CAMBIOL, d.TIPO_CAMBIOL2, d.NO_FACTURA, d.FECHAD_SOPORTE, d.METODO_PAGO, d.NO_PROVEEDOR, 
                         d.PASO_ACTUAL, d.AGENTE_ACTUAL, d.FECHA_PASO_ACTUAL, d.VKORG, d.VTWEG, d.SPART, f.USUARIOA_ID, f.ESTATUS AS EXPR2, d.CIUDAD, d.ESTADO
FROM            dbo.FLUJO AS f INNER JOIN
                         dbo.DOCUMENTO AS d ON f.NUM_DOC = d.NUM_DOC



GO
/****** Object:  View [dbo].[PAGINAV]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[PAGINAV]
AS
SELECT        dbo.USUARIO.ID, dbo.PAGINA.ID AS PAGINA_ID, dbo.PAGINA.URL AS PAGINA_URL, dbo.PAGINAT.TXT50, dbo.CARPETA.ID AS CARPETA_ID, dbo.CARPETA.URL AS CARPETA_URL, dbo.PAGINA.ICON, 
                         dbo.PERMISO_PAGINA.PERMISO, dbo.PAGINAT.SPRAS_ID, dbo.USUARIO.SPRAS_ID AS USUARIO_SPRAS, dbo.PAGINA.Mantenimiento, dbo.Grupo.Descripcion AS ManttoText, dbo.PAGINA.Grupo, 
                         dbo.GrupoCat.Descripcion AS GrupoText, dbo.PAGINA.Orden
FROM            dbo.USUARIO INNER JOIN
                         dbo.PERMISO_PAGINA ON dbo.USUARIO.PUESTO_ID = dbo.PERMISO_PAGINA.ROL_ID INNER JOIN
                         dbo.PAGINA ON dbo.PERMISO_PAGINA.PAGINA_ID = dbo.PAGINA.ID INNER JOIN
                         dbo.CARPETA ON dbo.PAGINA.CARPETA_ID = dbo.CARPETA.ID INNER JOIN
                         dbo.PAGINAT ON dbo.PAGINA.ID = dbo.PAGINAT.ID AND dbo.USUARIO.SPRAS_ID = dbo.PAGINAT.SPRAS_ID LEFT OUTER JOIN
                         dbo.GrupoCat ON dbo.PAGINA.Grupo = dbo.GrupoCat.Grupo_ID AND dbo.USUARIO.SPRAS_ID = dbo.GrupoCat.SPRAS_ID LEFT OUTER JOIN
                         dbo.Grupo ON dbo.PAGINA.Mantenimiento = dbo.Grupo.Grupo_ID AND dbo.USUARIO.SPRAS_ID = dbo.Grupo.SPRAS_ID
WHERE        (dbo.PERMISO_PAGINA.PERMISO = 1) AND (dbo.PAGINA.MOSTRAR = 1)

GO
/****** Object:  View [dbo].[WARNINGV]    Script Date: 28/12/2018 06:29:39 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[WARNINGV]
AS
SELECT        dbo.CAMPOS.PAGINA_ID, dbo.CAMPOS.ID, dbo.CAMPOS.DESCRIPCION, dbo.CAMPOS.TIPO, dbo.WARNING.SPRAS_ID, dbo.WARNING.WARNING, 
                         dbo.WARNING.POSICION, dbo.WARNING.ACTIVO
FROM            dbo.CAMPOS INNER JOIN
                         dbo.WARNING ON dbo.CAMPOS.PAGINA_ID = dbo.WARNING.PAGINA_ID AND dbo.CAMPOS.ID = dbo.WARNING.CAMPO_ID
WHERE        (dbo.WARNING.ACTIVO = 1)



GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_FK_DOCUMENTO_TALL]    Script Date: 28/12/2018 06:29:39 p.m. ******/
CREATE NONCLUSTERED INDEX [IX_FK_DOCUMENTO_TALL] ON [dbo].[DOCUMENTO]
(
	[TALL_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_FK_DOCUMENTO_TSOL]    Script Date: 28/12/2018 06:29:39 p.m. ******/
CREATE NONCLUSTERED INDEX [IX_FK_DOCUMENTO_TSOL] ON [dbo].[DOCUMENTO]
(
	[TSOL_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_FK_DOCUMENTO_USUARIO]    Script Date: 28/12/2018 06:29:39 p.m. ******/
CREATE NONCLUSTERED INDEX [IX_FK_DOCUMENTO_USUARIO] ON [dbo].[DOCUMENTO]
(
	[USUARIOC_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PRESUPSAPP] ADD  CONSTRAINT [DF_PRESUPSAPP_VVX17]  DEFAULT ((0)) FOR [VVX17]
GO
ALTER TABLE [dbo].[PRESUPSAPP] ADD  CONSTRAINT [DF_PRESUPSAPP_CSHDC]  DEFAULT ((0)) FOR [CSHDC]
GO
ALTER TABLE [dbo].[PRESUPSAPP] ADD  CONSTRAINT [DF_PRESUPSAPP_RECUN]  DEFAULT ((0)) FOR [RECUN]
GO
ALTER TABLE [dbo].[PRESUPSAPP] ADD  CONSTRAINT [DF_PRESUPSAPP_DSTRB]  DEFAULT ((0)) FOR [DSTRB]
GO
ALTER TABLE [dbo].[PRESUPSAPP] ADD  CONSTRAINT [DF_PRESUPSAPP_OTHTA]  DEFAULT ((0)) FOR [OTHTA]
GO
ALTER TABLE [dbo].[PRESUPSAPP] ADD  CONSTRAINT [DF_PRESUPSAPP_ADVER]  DEFAULT ((0)) FOR [ADVER]
GO
ALTER TABLE [dbo].[PRESUPSAPP] ADD  CONSTRAINT [DF_PRESUPSAPP_CORPM]  DEFAULT ((0)) FOR [CORPM]
GO
ALTER TABLE [dbo].[PRESUPSAPP] ADD  CONSTRAINT [DF_PRESUPSAPP_POP]  DEFAULT ((0)) FOR [POP]
GO
ALTER TABLE [dbo].[PRESUPSAPP] ADD  CONSTRAINT [DF_PRESUPSAPP_OTHER]  DEFAULT ((0)) FOR [OTHER]
GO
ALTER TABLE [dbo].[PRESUPSAPP] ADD  CONSTRAINT [DF_PRESUPSAPP_CONPR]  DEFAULT ((0)) FOR [CONPR]
GO
ALTER TABLE [dbo].[PRESUPSAPP] ADD  CONSTRAINT [DF_PRESUPSAPP_OHV]  DEFAULT ((0)) FOR [OHV]
GO
ALTER TABLE [dbo].[PRESUPSAPP] ADD  CONSTRAINT [DF_PRESUPSAPP_FREEG]  DEFAULT ((0)) FOR [FREEG]
GO
ALTER TABLE [dbo].[PRESUPSAPP] ADD  CONSTRAINT [DF_PRESUPSAPP_RSRDV]  DEFAULT ((0)) FOR [RSRDV]
GO
ALTER TABLE [dbo].[PRESUPSAPP] ADD  CONSTRAINT [DF_PRESUPSAPP_SPA]  DEFAULT ((0)) FOR [SPA]
GO
ALTER TABLE [dbo].[PRESUPSAPP] ADD  CONSTRAINT [DF_PRESUPSAPP_PMVAR]  DEFAULT ((0)) FOR [PMVAR]
GO
ALTER TABLE [dbo].[PRESUPUESTOP] ADD  CONSTRAINT [DF_PRESUPUESTOP_ADVER]  DEFAULT ((0)) FOR [ADVER]
GO
ALTER TABLE [dbo].[PRESUPUESTOP] ADD  CONSTRAINT [DF_PRESUPUESTOP_CONPR]  DEFAULT ((0)) FOR [CONPR]
GO
ALTER TABLE [dbo].[PRESUPUESTOP] ADD  CONSTRAINT [DF_PRESUPUESTOP_CSHDC]  DEFAULT ((0)) FOR [CSHDC]
GO
ALTER TABLE [dbo].[PRESUPUESTOP] ADD  CONSTRAINT [DF_PRESUPUESTOP_DIRLB]  DEFAULT ((0)) FOR [DIRLB]
GO
ALTER TABLE [dbo].[PRESUPUESTOP] ADD  CONSTRAINT [DF_PRESUPUESTOP_DSTRB]  DEFAULT ((0)) FOR [DSTRB]
GO
ALTER TABLE [dbo].[PRESUPUESTOP] ADD  CONSTRAINT [DF_PRESUPUESTOP_FREEG]  DEFAULT ((0)) FOR [FREEG]
GO
ALTER TABLE [dbo].[PRESUPUESTOP] ADD  CONSTRAINT [DF_PRESUPUESTOP_GRSLS]  DEFAULT ((0)) FOR [GRSLS]
GO
ALTER TABLE [dbo].[PRESUPUESTOP] ADD  CONSTRAINT [DF_PRESUPUESTOP_NETLB]  DEFAULT ((0)) FOR [NETLB]
GO
ALTER TABLE [dbo].[PRESUPUESTOP] ADD  CONSTRAINT [DF_PRESUPUESTOP_OVHDF]  DEFAULT ((0)) FOR [OVHDF]
GO
ALTER TABLE [dbo].[PRESUPUESTOP] ADD  CONSTRAINT [DF_PRESUPUESTOP_OVHDV]  DEFAULT ((0)) FOR [OVHDV]
GO
ALTER TABLE [dbo].[PRESUPUESTOP] ADD  CONSTRAINT [DF_PRESUPUESTOP_PKGMT]  DEFAULT ((0)) FOR [PKGMT]
GO
ALTER TABLE [dbo].[PRESUPUESTOP] ADD  CONSTRAINT [DF_PRESUPUESTOP_PMVAR]  DEFAULT ((0)) FOR [PMVAR]
GO
ALTER TABLE [dbo].[PRESUPUESTOP] ADD  CONSTRAINT [DF_PRESUPUESTOP_POP]  DEFAULT ((0)) FOR [POP]
GO
ALTER TABLE [dbo].[PRESUPUESTOP] ADD  CONSTRAINT [DF_PRESUPUESTOP_PURCH]  DEFAULT ((0)) FOR [PURCH]
GO
ALTER TABLE [dbo].[PRESUPUESTOP] ADD  CONSTRAINT [DF_PRESUPUESTOP_RAWMT]  DEFAULT ((0)) FOR [RAWMT]
GO
ALTER TABLE [dbo].[PRESUPUESTOP] ADD  CONSTRAINT [DF_PRESUPUESTOP_RECUN]  DEFAULT ((0)) FOR [RECUN]
GO
ALTER TABLE [dbo].[PRESUPUESTOP] ADD  CONSTRAINT [DF_PRESUPUESTOP_RSRDV]  DEFAULT ((0)) FOR [RSRDV]
GO
ALTER TABLE [dbo].[PRESUPUESTOP] ADD  CONSTRAINT [DF_PRESUPUESTOP_TOTCS]  DEFAULT ((0)) FOR [TOTCS]
GO
ALTER TABLE [dbo].[PRESUPUESTOP] ADD  CONSTRAINT [DF__PRESUPUES__VVX17__09746778]  DEFAULT ((0)) FOR [VVX17]
GO
ALTER TABLE [dbo].[PRESUPUESTOP] ADD  CONSTRAINT [DF__PRESUPUES__OTHTA__0A688BB1]  DEFAULT ((0)) FOR [OTHTA]
GO
ALTER TABLE [dbo].[PRESUPUESTOP] ADD  CONSTRAINT [DF__PRESUPUES__CORPM__0B5CAFEA]  DEFAULT ((0)) FOR [CORPM]
GO
ALTER TABLE [dbo].[PRESUPUESTOP] ADD  CONSTRAINT [DF__PRESUPUESTO__SPA__0C50D423]  DEFAULT ((0)) FOR [SPA]
GO
ALTER TABLE [dbo].[STATES] ADD  DEFAULT ('1') FOR [COUNTRY_ID]
GO
ALTER TABLE [dbo].[ACCIONT]  WITH CHECK ADD  CONSTRAINT [FK_ACCIONT_ACCION] FOREIGN KEY([ACCION_ID])
REFERENCES [dbo].[ACCION] ([ID])
GO
ALTER TABLE [dbo].[ACCIONT] CHECK CONSTRAINT [FK_ACCIONT_ACCION]
GO
ALTER TABLE [dbo].[ACCIONT]  WITH CHECK ADD  CONSTRAINT [FK_ACCIONT_SPRAS] FOREIGN KEY([SPRAS_ID])
REFERENCES [dbo].[SPRAS] ([ID])
GO
ALTER TABLE [dbo].[ACCIONT] CHECK CONSTRAINT [FK_ACCIONT_SPRAS]
GO
ALTER TABLE [dbo].[CALENDARIO_AC]  WITH CHECK ADD  CONSTRAINT [FK_CALENDARIO_AC_PERIODO] FOREIGN KEY([PERIODO])
REFERENCES [dbo].[PERIODO] ([ID])
GO
ALTER TABLE [dbo].[CALENDARIO_AC] CHECK CONSTRAINT [FK_CALENDARIO_AC_PERIODO]
GO
ALTER TABLE [dbo].[CALENDARIO_AC]  WITH CHECK ADD  CONSTRAINT [FK_CALENDARIO_AC_SOCIEDAD] FOREIGN KEY([SOCIEDAD_ID])
REFERENCES [dbo].[SOCIEDAD] ([BUKRS])
GO
ALTER TABLE [dbo].[CALENDARIO_AC] CHECK CONSTRAINT [FK_CALENDARIO_AC_SOCIEDAD]
GO
ALTER TABLE [dbo].[CALENDARIO_AC]  WITH CHECK ADD  CONSTRAINT [FK_CALENDARIO_AC_TSOL] FOREIGN KEY([TSOL_ID])
REFERENCES [dbo].[TSOL] ([ID])
GO
ALTER TABLE [dbo].[CALENDARIO_AC] CHECK CONSTRAINT [FK_CALENDARIO_AC_TSOL]
GO
ALTER TABLE [dbo].[CALENDARIO_EX]  WITH CHECK ADD  CONSTRAINT [FK_CALENDARIO_EX_CALENDARIO_AC] FOREIGN KEY([EJERCICIO], [PERIODO], [SOCIEDAD_ID], [TSOL_ID])
REFERENCES [dbo].[CALENDARIO_AC] ([EJERCICIO], [PERIODO], [SOCIEDAD_ID], [TSOL_ID])
GO
ALTER TABLE [dbo].[CALENDARIO_EX] CHECK CONSTRAINT [FK_CALENDARIO_EX_CALENDARIO_AC]
GO
ALTER TABLE [dbo].[CALENDARIO_EX]  WITH CHECK ADD  CONSTRAINT [FK_CALENDARIO_EX_USUARIO] FOREIGN KEY([USUARIO_ID])
REFERENCES [dbo].[USUARIO] ([ID])
GO
ALTER TABLE [dbo].[CALENDARIO_EX] CHECK CONSTRAINT [FK_CALENDARIO_EX_USUARIO]
GO
ALTER TABLE [dbo].[CAMPOS]  WITH CHECK ADD  CONSTRAINT [FK_CAMPOS_PAGINA] FOREIGN KEY([PAGINA_ID])
REFERENCES [dbo].[PAGINA] ([ID])
GO
ALTER TABLE [dbo].[CAMPOS] CHECK CONSTRAINT [FK_CAMPOS_PAGINA]
GO
ALTER TABLE [dbo].[CAMPOZKE24T]  WITH CHECK ADD  CONSTRAINT [FK_CAMPOZKE24T_CAMPOZKE24] FOREIGN KEY([CAMPO_ID])
REFERENCES [dbo].[CAMPOZKE24] ([CAMPO])
GO
ALTER TABLE [dbo].[CAMPOZKE24T] CHECK CONSTRAINT [FK_CAMPOZKE24T_CAMPOZKE24]
GO
ALTER TABLE [dbo].[CAMPOZKE24T]  WITH CHECK ADD  CONSTRAINT [FK_CAMPOZKE24T_SPRAS] FOREIGN KEY([SPRAS_ID])
REFERENCES [dbo].[SPRAS] ([ID])
GO
ALTER TABLE [dbo].[CAMPOZKE24T] CHECK CONSTRAINT [FK_CAMPOZKE24T_SPRAS]
GO
ALTER TABLE [dbo].[CARPETAT]  WITH CHECK ADD  CONSTRAINT [FK_CARPETAT_CARPETA] FOREIGN KEY([ID])
REFERENCES [dbo].[CARPETA] ([ID])
GO
ALTER TABLE [dbo].[CARPETAT] CHECK CONSTRAINT [FK_CARPETAT_CARPETA]
GO
ALTER TABLE [dbo].[CARPETAT]  WITH CHECK ADD  CONSTRAINT [FK_CARPETAT_SPRAS] FOREIGN KEY([SPRAS_ID])
REFERENCES [dbo].[SPRAS] ([ID])
GO
ALTER TABLE [dbo].[CARPETAT] CHECK CONSTRAINT [FK_CARPETAT_SPRAS]
GO
ALTER TABLE [dbo].[CARTA]  WITH CHECK ADD  CONSTRAINT [FK_CARTA_DOCUMENTO] FOREIGN KEY([NUM_DOC])
REFERENCES [dbo].[DOCUMENTO] ([NUM_DOC])
GO
ALTER TABLE [dbo].[CARTA] CHECK CONSTRAINT [FK_CARTA_DOCUMENTO]
GO
ALTER TABLE [dbo].[CARTA]  WITH CHECK ADD  CONSTRAINT [FK_CARTA_USUARIO] FOREIGN KEY([USUARIO_ID])
REFERENCES [dbo].[USUARIO] ([ID])
GO
ALTER TABLE [dbo].[CARTA] CHECK CONSTRAINT [FK_CARTA_USUARIO]
GO
ALTER TABLE [dbo].[CARTAP]  WITH CHECK ADD  CONSTRAINT [FK_CARTAP_CARTA] FOREIGN KEY([NUM_DOC], [POS_ID])
REFERENCES [dbo].[CARTA] ([NUM_DOC], [POS])
GO
ALTER TABLE [dbo].[CARTAP] CHECK CONSTRAINT [FK_CARTAP_CARTA]
GO
ALTER TABLE [dbo].[CATEGORIAT]  WITH CHECK ADD  CONSTRAINT [FK_CATEGORIAT_CATEGORIA] FOREIGN KEY([CATEGORIA_ID])
REFERENCES [dbo].[CATEGORIA] ([ID])
GO
ALTER TABLE [dbo].[CATEGORIAT] CHECK CONSTRAINT [FK_CATEGORIAT_CATEGORIA]
GO
ALTER TABLE [dbo].[CATEGORIAT]  WITH CHECK ADD  CONSTRAINT [FK_CATEGORIAT_SPRAS] FOREIGN KEY([SPRAS_ID])
REFERENCES [dbo].[SPRAS] ([ID])
GO
ALTER TABLE [dbo].[CATEGORIAT] CHECK CONSTRAINT [FK_CATEGORIAT_SPRAS]
GO
ALTER TABLE [dbo].[CITIES]  WITH NOCHECK ADD  CONSTRAINT [FK_CITIES_STATES] FOREIGN KEY([STATE_ID])
REFERENCES [dbo].[STATES] ([ID])
GO
ALTER TABLE [dbo].[CITIES] CHECK CONSTRAINT [FK_CITIES_STATES]
GO
ALTER TABLE [dbo].[CLIENTE]  WITH NOCHECK ADD  CONSTRAINT [FK_CLIENTE_PAIS] FOREIGN KEY([LAND])
REFERENCES [dbo].[PAIS] ([LAND])
GO
ALTER TABLE [dbo].[CLIENTE] CHECK CONSTRAINT [FK_CLIENTE_PAIS]
GO
ALTER TABLE [dbo].[CLIENTE]  WITH NOCHECK ADD  CONSTRAINT [FK_CLIENTE_PROVEEDOR] FOREIGN KEY([PROVEEDOR_ID])
REFERENCES [dbo].[PROVEEDOR] ([ID])
GO
ALTER TABLE [dbo].[CLIENTE] CHECK CONSTRAINT [FK_CLIENTE_PROVEEDOR]
GO
ALTER TABLE [dbo].[CLIENTE]  WITH NOCHECK ADD  CONSTRAINT [FK_CLIENTE_TCLIENTE] FOREIGN KEY([PARVW])
REFERENCES [dbo].[TCLIENTE] ([ID])
GO
ALTER TABLE [dbo].[CLIENTE] CHECK CONSTRAINT [FK_CLIENTE_TCLIENTE]
GO
ALTER TABLE [dbo].[CLIENTEF]  WITH CHECK ADD  CONSTRAINT [FK_CLIENTEF_CLIENTE] FOREIGN KEY([VKORG], [VTWEG], [SPART], [KUNNR])
REFERENCES [dbo].[CLIENTE] ([VKORG], [VTWEG], [SPART], [KUNNR])
GO
ALTER TABLE [dbo].[CLIENTEF] CHECK CONSTRAINT [FK_CLIENTEF_CLIENTE]
GO
ALTER TABLE [dbo].[CLIENTEI]  WITH CHECK ADD  CONSTRAINT [FK_CLIENTEI_CLIENTE] FOREIGN KEY([VKORG], [VTWEG], [SPART], [KUNNR])
REFERENCES [dbo].[CLIENTE] ([VKORG], [VTWEG], [SPART], [KUNNR])
GO
ALTER TABLE [dbo].[CLIENTEI] CHECK CONSTRAINT [FK_CLIENTEI_CLIENTE]
GO
ALTER TABLE [dbo].[CLIENTEI]  WITH NOCHECK ADD  CONSTRAINT [FK_CLIENTEI_IMPUESTO] FOREIGN KEY([MWSKZ])
REFERENCES [dbo].[IMPUESTO] ([MWSKZ])
GO
ALTER TABLE [dbo].[CLIENTEI] CHECK CONSTRAINT [FK_CLIENTEI_IMPUESTO]
GO
ALTER TABLE [dbo].[CONDICIONT]  WITH CHECK ADD  CONSTRAINT [FK_CONDICIONT_CONDICION] FOREIGN KEY([CONDICION_ID])
REFERENCES [dbo].[CONDICION] ([ID])
GO
ALTER TABLE [dbo].[CONDICIONT] CHECK CONSTRAINT [FK_CONDICIONT_CONDICION]
GO
ALTER TABLE [dbo].[CONDICIONT]  WITH CHECK ADD  CONSTRAINT [FK_CONDICIONT_SPRAS] FOREIGN KEY([SPRAS_ID])
REFERENCES [dbo].[SPRAS] ([ID])
GO
ALTER TABLE [dbo].[CONDICIONT] CHECK CONSTRAINT [FK_CONDICIONT_SPRAS]
GO
ALTER TABLE [dbo].[CONFDIST_CAT]  WITH CHECK ADD  CONSTRAINT [FK_CONFDIST_CAT_CAMPOZKE24] FOREIGN KEY([CAMPO])
REFERENCES [dbo].[CAMPOZKE24] ([CAMPO])
GO
ALTER TABLE [dbo].[CONFDIST_CAT] CHECK CONSTRAINT [FK_CONFDIST_CAT_CAMPOZKE24]
GO
ALTER TABLE [dbo].[CONFDIST_CAT]  WITH CHECK ADD  CONSTRAINT [FK_CONFDIST_CAT_SOCIEDAD] FOREIGN KEY([SOCIEDAD_ID])
REFERENCES [dbo].[SOCIEDAD] ([BUKRS])
GO
ALTER TABLE [dbo].[CONFDIST_CAT] CHECK CONSTRAINT [FK_CONFDIST_CAT_SOCIEDAD]
GO
ALTER TABLE [dbo].[CONPOSAPH]  WITH NOCHECK ADD  CONSTRAINT [FK_CONPOSAPH_SOCIEDAD] FOREIGN KEY([SOCIEDAD])
REFERENCES [dbo].[SOCIEDAD] ([BUKRS])
GO
ALTER TABLE [dbo].[CONPOSAPH] CHECK CONSTRAINT [FK_CONPOSAPH_SOCIEDAD]
GO
ALTER TABLE [dbo].[CONSOPORTE]  WITH CHECK ADD  CONSTRAINT [FK_CONSOPORTE_TSOL] FOREIGN KEY([TSOL_ID])
REFERENCES [dbo].[TSOL] ([ID])
GO
ALTER TABLE [dbo].[CONSOPORTE] CHECK CONSTRAINT [FK_CONSOPORTE_TSOL]
GO
ALTER TABLE [dbo].[CONSOPORTE]  WITH CHECK ADD  CONSTRAINT [FK_CONSOPORTE_TSOPORTE] FOREIGN KEY([TSOPORTE_ID])
REFERENCES [dbo].[TSOPORTE] ([ID])
GO
ALTER TABLE [dbo].[CONSOPORTE] CHECK CONSTRAINT [FK_CONSOPORTE_TSOPORTE]
GO
ALTER TABLE [dbo].[CONTACTOC]  WITH CHECK ADD  CONSTRAINT [FK_CONTACTOC_CLIENTE] FOREIGN KEY([VKORG], [VTWEG], [SPART], [KUNNR])
REFERENCES [dbo].[CLIENTE] ([VKORG], [VTWEG], [SPART], [KUNNR])
GO
ALTER TABLE [dbo].[CONTACTOC] CHECK CONSTRAINT [FK_CONTACTOC_CLIENTE]
GO
ALTER TABLE [dbo].[CUENTA]  WITH NOCHECK ADD  CONSTRAINT [FK_CUENTA_CUENTAGL] FOREIGN KEY([ABONO])
REFERENCES [dbo].[CUENTAGL] ([ID])
GO
ALTER TABLE [dbo].[CUENTA] CHECK CONSTRAINT [FK_CUENTA_CUENTAGL]
GO
ALTER TABLE [dbo].[CUENTA]  WITH NOCHECK ADD  CONSTRAINT [FK_CUENTA_CUENTAGL1] FOREIGN KEY([CARGO])
REFERENCES [dbo].[CUENTAGL] ([ID])
GO
ALTER TABLE [dbo].[CUENTA] CHECK CONSTRAINT [FK_CUENTA_CUENTAGL1]
GO
ALTER TABLE [dbo].[CUENTA]  WITH NOCHECK ADD  CONSTRAINT [FK_CUENTA_CUENTAGL2] FOREIGN KEY([CLEARING])
REFERENCES [dbo].[CUENTAGL] ([ID])
GO
ALTER TABLE [dbo].[CUENTA] CHECK CONSTRAINT [FK_CUENTA_CUENTAGL2]
GO
ALTER TABLE [dbo].[CUENTA]  WITH NOCHECK ADD  CONSTRAINT [FK_CUENTA_PAIS] FOREIGN KEY([PAIS_ID])
REFERENCES [dbo].[PAIS] ([LAND])
GO
ALTER TABLE [dbo].[CUENTA] CHECK CONSTRAINT [FK_CUENTA_PAIS]
GO
ALTER TABLE [dbo].[CUENTA]  WITH NOCHECK ADD  CONSTRAINT [FK_CUENTA_SOCIEDAD] FOREIGN KEY([SOCIEDAD_ID])
REFERENCES [dbo].[SOCIEDAD] ([BUKRS])
GO
ALTER TABLE [dbo].[CUENTA] CHECK CONSTRAINT [FK_CUENTA_SOCIEDAD]
GO
ALTER TABLE [dbo].[CUENTA]  WITH NOCHECK ADD  CONSTRAINT [FK_CUENTA_TALL] FOREIGN KEY([TALL_ID])
REFERENCES [dbo].[TALL] ([ID])
GO
ALTER TABLE [dbo].[CUENTA] CHECK CONSTRAINT [FK_CUENTA_TALL]
GO
ALTER TABLE [dbo].[DELEGAR]  WITH CHECK ADD  CONSTRAINT [FK_DELEGAR_USUARIO] FOREIGN KEY([USUARIO_ID])
REFERENCES [dbo].[USUARIO] ([ID])
GO
ALTER TABLE [dbo].[DELEGAR] CHECK CONSTRAINT [FK_DELEGAR_USUARIO]
GO
ALTER TABLE [dbo].[DELEGAR]  WITH CHECK ADD  CONSTRAINT [FK_DELEGAR_USUARIO1] FOREIGN KEY([USUARIOD_ID])
REFERENCES [dbo].[USUARIO] ([ID])
GO
ALTER TABLE [dbo].[DELEGAR] CHECK CONSTRAINT [FK_DELEGAR_USUARIO1]
GO
ALTER TABLE [dbo].[DET_AGENTE]  WITH CHECK ADD  CONSTRAINT [FK_DET_AGENTE_GAUTORIZACION] FOREIGN KEY([AGROUP_ID])
REFERENCES [dbo].[GAUTORIZACION] ([ID])
GO
ALTER TABLE [dbo].[DET_AGENTE] CHECK CONSTRAINT [FK_DET_AGENTE_GAUTORIZACION]
GO
ALTER TABLE [dbo].[DET_AGENTE]  WITH CHECK ADD  CONSTRAINT [FK_DET_AGENTE_PUESTO] FOREIGN KEY([PUESTOC_ID])
REFERENCES [dbo].[PUESTO] ([ID])
GO
ALTER TABLE [dbo].[DET_AGENTE] CHECK CONSTRAINT [FK_DET_AGENTE_PUESTO]
GO
ALTER TABLE [dbo].[DET_AGENTE]  WITH CHECK ADD  CONSTRAINT [FK_DET_AGENTE_PUESTO1] FOREIGN KEY([PUESTOA_ID])
REFERENCES [dbo].[PUESTO] ([ID])
GO
ALTER TABLE [dbo].[DET_AGENTE] CHECK CONSTRAINT [FK_DET_AGENTE_PUESTO1]
GO
ALTER TABLE [dbo].[DET_AGENTEC]  WITH CHECK ADD  CONSTRAINT [FK_DET_AGENTEC_CLIENTE] FOREIGN KEY([VKORG], [VTWEG], [SPART], [KUNNR])
REFERENCES [dbo].[CLIENTE] ([VKORG], [VTWEG], [SPART], [KUNNR])
GO
ALTER TABLE [dbo].[DET_AGENTEC] CHECK CONSTRAINT [FK_DET_AGENTEC_CLIENTE]
GO
ALTER TABLE [dbo].[DET_AGENTEC]  WITH CHECK ADD  CONSTRAINT [FK_DET_AGENTEC_PAIS] FOREIGN KEY([PAIS_ID])
REFERENCES [dbo].[PAIS] ([LAND])
GO
ALTER TABLE [dbo].[DET_AGENTEC] CHECK CONSTRAINT [FK_DET_AGENTEC_PAIS]
GO
ALTER TABLE [dbo].[DET_AGENTEC]  WITH CHECK ADD  CONSTRAINT [FK_DET_AGENTEC_USUARIO] FOREIGN KEY([USUARIOC_ID])
REFERENCES [dbo].[USUARIO] ([ID])
GO
ALTER TABLE [dbo].[DET_AGENTEC] CHECK CONSTRAINT [FK_DET_AGENTEC_USUARIO]
GO
ALTER TABLE [dbo].[DET_AGENTEC]  WITH CHECK ADD  CONSTRAINT [FK_DET_AGENTEC_USUARIO1] FOREIGN KEY([USUARIOA_ID])
REFERENCES [dbo].[USUARIO] ([ID])
GO
ALTER TABLE [dbo].[DET_AGENTEC] CHECK CONSTRAINT [FK_DET_AGENTEC_USUARIO1]
GO
ALTER TABLE [dbo].[DET_AGENTEH]  WITH CHECK ADD  CONSTRAINT [FK_DET_AGENTEH_GAUTORIZACION] FOREIGN KEY([AGROUP_ID])
REFERENCES [dbo].[GAUTORIZACION] ([ID])
GO
ALTER TABLE [dbo].[DET_AGENTEH] CHECK CONSTRAINT [FK_DET_AGENTEH_GAUTORIZACION]
GO
ALTER TABLE [dbo].[DET_AGENTEH]  WITH CHECK ADD  CONSTRAINT [FK_DET_AGENTEH_PUESTO] FOREIGN KEY([PUESTOC_ID])
REFERENCES [dbo].[PUESTO] ([ID])
GO
ALTER TABLE [dbo].[DET_AGENTEH] CHECK CONSTRAINT [FK_DET_AGENTEH_PUESTO]
GO
ALTER TABLE [dbo].[DET_AGENTEH]  WITH CHECK ADD  CONSTRAINT [FK_DET_AGENTEH_SOCIEDAD] FOREIGN KEY([SOCIEDAD_ID])
REFERENCES [dbo].[SOCIEDAD] ([BUKRS])
GO
ALTER TABLE [dbo].[DET_AGENTEH] CHECK CONSTRAINT [FK_DET_AGENTEH_SOCIEDAD]
GO
ALTER TABLE [dbo].[DET_AGENTEH]  WITH CHECK ADD  CONSTRAINT [FK_DET_AGENTEH_USUARIO] FOREIGN KEY([USUARIOC_ID])
REFERENCES [dbo].[USUARIO] ([ID])
GO
ALTER TABLE [dbo].[DET_AGENTEH] CHECK CONSTRAINT [FK_DET_AGENTEH_USUARIO]
GO
ALTER TABLE [dbo].[DET_AGENTEP]  WITH CHECK ADD  CONSTRAINT [FK_DET_AGENTEP_DET_AGENTEH] FOREIGN KEY([SOCIEDAD_ID], [PUESTOC_ID], [VERSION], [AGROUP_ID])
REFERENCES [dbo].[DET_AGENTEH] ([SOCIEDAD_ID], [PUESTOC_ID], [VERSION], [AGROUP_ID])
GO
ALTER TABLE [dbo].[DET_AGENTEP] CHECK CONSTRAINT [FK_DET_AGENTEP_DET_AGENTEH]
GO
ALTER TABLE [dbo].[DET_AGENTEP]  WITH CHECK ADD  CONSTRAINT [FK_DET_AGENTEP_PUESTO] FOREIGN KEY([PUESTOA_ID])
REFERENCES [dbo].[PUESTO] ([ID])
GO
ALTER TABLE [dbo].[DET_AGENTEP] CHECK CONSTRAINT [FK_DET_AGENTEP_PUESTO]
GO
ALTER TABLE [dbo].[DET_AGENTEP]  WITH CHECK ADD  CONSTRAINT [FK_DET_AGENTEP_USUARIO] FOREIGN KEY([USUARIOA_ID])
REFERENCES [dbo].[USUARIO] ([ID])
GO
ALTER TABLE [dbo].[DET_AGENTEP] CHECK CONSTRAINT [FK_DET_AGENTEP_USUARIO]
GO
ALTER TABLE [dbo].[DET_APROB]  WITH CHECK ADD  CONSTRAINT [FK_DET_APROB_PUESTO] FOREIGN KEY([PUESTOC_ID])
REFERENCES [dbo].[PUESTO] ([ID])
GO
ALTER TABLE [dbo].[DET_APROB] CHECK CONSTRAINT [FK_DET_APROB_PUESTO]
GO
ALTER TABLE [dbo].[DET_APROB]  WITH CHECK ADD  CONSTRAINT [FK_DET_APROB_PUESTO1] FOREIGN KEY([PUESTOA_ID])
REFERENCES [dbo].[PUESTO] ([ID])
GO
ALTER TABLE [dbo].[DET_APROB] CHECK CONSTRAINT [FK_DET_APROB_PUESTO1]
GO
ALTER TABLE [dbo].[DET_APROB]  WITH CHECK ADD  CONSTRAINT [FK_DET_APROB_SOCIEDAD] FOREIGN KEY([BUKRS])
REFERENCES [dbo].[SOCIEDAD] ([BUKRS])
GO
ALTER TABLE [dbo].[DET_APROB] CHECK CONSTRAINT [FK_DET_APROB_SOCIEDAD]
GO
ALTER TABLE [dbo].[DET_APROBH]  WITH CHECK ADD  CONSTRAINT [FK_DET_APROBH_PUESTO1] FOREIGN KEY([PUESTOC_ID])
REFERENCES [dbo].[PUESTO] ([ID])
GO
ALTER TABLE [dbo].[DET_APROBH] CHECK CONSTRAINT [FK_DET_APROBH_PUESTO1]
GO
ALTER TABLE [dbo].[DET_APROBH]  WITH CHECK ADD  CONSTRAINT [FK_DET_APROBH_SOCIEDAD1] FOREIGN KEY([SOCIEDAD_ID])
REFERENCES [dbo].[SOCIEDAD] ([BUKRS])
GO
ALTER TABLE [dbo].[DET_APROBH] CHECK CONSTRAINT [FK_DET_APROBH_SOCIEDAD1]
GO
ALTER TABLE [dbo].[DET_APROBP]  WITH CHECK ADD  CONSTRAINT [FK_DET_APROBP_DET_APROBH] FOREIGN KEY([SOCIEDAD_ID], [PUESTOC_ID], [VERSION])
REFERENCES [dbo].[DET_APROBH] ([SOCIEDAD_ID], [PUESTOC_ID], [VERSION])
GO
ALTER TABLE [dbo].[DET_APROBP] CHECK CONSTRAINT [FK_DET_APROBP_DET_APROBH]
GO
ALTER TABLE [dbo].[DET_TAXEO]  WITH CHECK ADD  CONSTRAINT [FK_DET_TAXEO_PUESTO] FOREIGN KEY([PUESTOA_ID])
REFERENCES [dbo].[PUESTO] ([ID])
GO
ALTER TABLE [dbo].[DET_TAXEO] CHECK CONSTRAINT [FK_DET_TAXEO_PUESTO]
GO
ALTER TABLE [dbo].[DET_TAXEO]  WITH CHECK ADD  CONSTRAINT [FK_DET_TAXEO_USUARIO] FOREIGN KEY([USUARIOC_ID])
REFERENCES [dbo].[USUARIO] ([ID])
GO
ALTER TABLE [dbo].[DET_TAXEO] CHECK CONSTRAINT [FK_DET_TAXEO_USUARIO]
GO
ALTER TABLE [dbo].[DET_TAXEOC]  WITH CHECK ADD  CONSTRAINT [FK_DET_TAXEOC_PAIS] FOREIGN KEY([PAIS_ID])
REFERENCES [dbo].[PAIS] ([LAND])
GO
ALTER TABLE [dbo].[DET_TAXEOC] CHECK CONSTRAINT [FK_DET_TAXEOC_PAIS]
GO
ALTER TABLE [dbo].[DOCTOAYUDA]  WITH CHECK ADD  CONSTRAINT [FK_DOCTOAYUDA_DOCTOCLASIF] FOREIGN KEY([ID_CLASIFICACION])
REFERENCES [dbo].[DOCTOCLASIF] ([ID_CLASIFICACION])
GO
ALTER TABLE [dbo].[DOCTOAYUDA] CHECK CONSTRAINT [FK_DOCTOAYUDA_DOCTOCLASIF]
GO
ALTER TABLE [dbo].[DOCTOCLASIFT]  WITH CHECK ADD  CONSTRAINT [FK_DOCTOCLASIFT_DOCTOCLASIF] FOREIGN KEY([ID_CLASIFICACION])
REFERENCES [dbo].[DOCTOCLASIF] ([ID_CLASIFICACION])
GO
ALTER TABLE [dbo].[DOCTOCLASIFT] CHECK CONSTRAINT [FK_DOCTOCLASIFT_DOCTOCLASIF]
GO
ALTER TABLE [dbo].[DOCUMENTO]  WITH CHECK ADD  CONSTRAINT [FK_DOCUMENTO_CLIENTE] FOREIGN KEY([VKORG], [VTWEG], [SPART], [PAYER_ID])
REFERENCES [dbo].[CLIENTE] ([VKORG], [VTWEG], [SPART], [KUNNR])
GO
ALTER TABLE [dbo].[DOCUMENTO] CHECK CONSTRAINT [FK_DOCUMENTO_CLIENTE]
GO
ALTER TABLE [dbo].[DOCUMENTO]  WITH NOCHECK ADD  CONSTRAINT [FK_DOCUMENTO_CUENTAGL] FOREIGN KEY([CUENTAP])
REFERENCES [dbo].[CUENTAGL] ([ID])
GO
ALTER TABLE [dbo].[DOCUMENTO] CHECK CONSTRAINT [FK_DOCUMENTO_CUENTAGL]
GO
ALTER TABLE [dbo].[DOCUMENTO]  WITH NOCHECK ADD  CONSTRAINT [FK_DOCUMENTO_CUENTAGL1] FOREIGN KEY([CUENTAPL])
REFERENCES [dbo].[CUENTAGL] ([ID])
GO
ALTER TABLE [dbo].[DOCUMENTO] CHECK CONSTRAINT [FK_DOCUMENTO_CUENTAGL1]
GO
ALTER TABLE [dbo].[DOCUMENTO]  WITH CHECK ADD  CONSTRAINT [FK_DOCUMENTO_GALL] FOREIGN KEY([GALL_ID])
REFERENCES [dbo].[GALL] ([ID])
GO
ALTER TABLE [dbo].[DOCUMENTO] CHECK CONSTRAINT [FK_DOCUMENTO_GALL]
GO
ALTER TABLE [dbo].[DOCUMENTO]  WITH CHECK ADD  CONSTRAINT [FK_DOCUMENTO_PAIS] FOREIGN KEY([PAIS_ID])
REFERENCES [dbo].[PAIS] ([LAND])
GO
ALTER TABLE [dbo].[DOCUMENTO] CHECK CONSTRAINT [FK_DOCUMENTO_PAIS]
GO
ALTER TABLE [dbo].[DOCUMENTO]  WITH CHECK ADD  CONSTRAINT [FK_DOCUMENTO_SOCIEDAD] FOREIGN KEY([SOCIEDAD_ID])
REFERENCES [dbo].[SOCIEDAD] ([BUKRS])
GO
ALTER TABLE [dbo].[DOCUMENTO] CHECK CONSTRAINT [FK_DOCUMENTO_SOCIEDAD]
GO
ALTER TABLE [dbo].[DOCUMENTO]  WITH CHECK ADD  CONSTRAINT [FK_DOCUMENTO_TALL] FOREIGN KEY([TALL_ID])
REFERENCES [dbo].[TALL] ([ID])
GO
ALTER TABLE [dbo].[DOCUMENTO] CHECK CONSTRAINT [FK_DOCUMENTO_TALL]
GO
ALTER TABLE [dbo].[DOCUMENTO]  WITH CHECK ADD  CONSTRAINT [FK_DOCUMENTO_TSOL] FOREIGN KEY([TSOL_ID])
REFERENCES [dbo].[TSOL] ([ID])
GO
ALTER TABLE [dbo].[DOCUMENTO] CHECK CONSTRAINT [FK_DOCUMENTO_TSOL]
GO
ALTER TABLE [dbo].[DOCUMENTO]  WITH CHECK ADD  CONSTRAINT [FK_DOCUMENTO_USUARIO] FOREIGN KEY([USUARIOC_ID])
REFERENCES [dbo].[USUARIO] ([ID])
GO
ALTER TABLE [dbo].[DOCUMENTO] CHECK CONSTRAINT [FK_DOCUMENTO_USUARIO]
GO
ALTER TABLE [dbo].[DOCUMENTOA]  WITH CHECK ADD  CONSTRAINT [FK_DOCUMENTOA_DOCUMENTO] FOREIGN KEY([NUM_DOC])
REFERENCES [dbo].[DOCUMENTO] ([NUM_DOC])
GO
ALTER TABLE [dbo].[DOCUMENTOA] CHECK CONSTRAINT [FK_DOCUMENTOA_DOCUMENTO]
GO
ALTER TABLE [dbo].[DOCUMENTOA]  WITH CHECK ADD  CONSTRAINT [FK_DOCUMENTOA_TSOPORTE] FOREIGN KEY([CLASE])
REFERENCES [dbo].[TSOPORTE] ([ID])
GO
ALTER TABLE [dbo].[DOCUMENTOA] CHECK CONSTRAINT [FK_DOCUMENTOA_TSOPORTE]
GO
ALTER TABLE [dbo].[DOCUMENTOBORRF]  WITH CHECK ADD  CONSTRAINT [FK_DOCUMENTOBORRF_DOCUMENTBORR] FOREIGN KEY([USUARIOC_ID])
REFERENCES [dbo].[DOCUMENTBORR] ([USUARIOC_ID])
GO
ALTER TABLE [dbo].[DOCUMENTOBORRF] CHECK CONSTRAINT [FK_DOCUMENTOBORRF_DOCUMENTBORR]
GO
ALTER TABLE [dbo].[DOCUMENTOBORRM]  WITH CHECK ADD  CONSTRAINT [FK_DOCUMENTOBORRM_DOCUMENTOBORRP] FOREIGN KEY([USUARIOC_ID], [POS_ID])
REFERENCES [dbo].[DOCUMENTOBORRP] ([USUARIOC_ID], [POS])
GO
ALTER TABLE [dbo].[DOCUMENTOBORRM] CHECK CONSTRAINT [FK_DOCUMENTOBORRM_DOCUMENTOBORRP]
GO
ALTER TABLE [dbo].[DOCUMENTOBORRN]  WITH CHECK ADD  CONSTRAINT [FK_DOCUMENTOBORRN_DOCUMENTBORR] FOREIGN KEY([USUARIOC_ID])
REFERENCES [dbo].[DOCUMENTBORR] ([USUARIOC_ID])
GO
ALTER TABLE [dbo].[DOCUMENTOBORRN] CHECK CONSTRAINT [FK_DOCUMENTOBORRN_DOCUMENTBORR]
GO
ALTER TABLE [dbo].[DOCUMENTOBORRP]  WITH CHECK ADD  CONSTRAINT [FK_DOCUMENTOBORRP_DOCUMENTBORR] FOREIGN KEY([USUARIOC_ID])
REFERENCES [dbo].[DOCUMENTBORR] ([USUARIOC_ID])
GO
ALTER TABLE [dbo].[DOCUMENTOBORRP] CHECK CONSTRAINT [FK_DOCUMENTOBORRP_DOCUMENTBORR]
GO
ALTER TABLE [dbo].[DOCUMENTOBORRREC]  WITH CHECK ADD  CONSTRAINT [FK_DOCUMENTOBORRREC_DOCUMENTBORR] FOREIGN KEY([USUARIOC_ID])
REFERENCES [dbo].[DOCUMENTBORR] ([USUARIOC_ID])
GO
ALTER TABLE [dbo].[DOCUMENTOBORRREC] CHECK CONSTRAINT [FK_DOCUMENTOBORRREC_DOCUMENTBORR]
GO
ALTER TABLE [dbo].[DOCUMENTOF]  WITH NOCHECK ADD  CONSTRAINT [FK_DOCUMENTOF_DOCUMENTO] FOREIGN KEY([NUM_DOC])
REFERENCES [dbo].[DOCUMENTO] ([NUM_DOC])
GO
ALTER TABLE [dbo].[DOCUMENTOF] CHECK CONSTRAINT [FK_DOCUMENTOF_DOCUMENTO]
GO
ALTER TABLE [dbo].[DOCUMENTOL]  WITH CHECK ADD  CONSTRAINT [FK_DOCUMENTOL_DOCUMENTO] FOREIGN KEY([NUM_DOC])
REFERENCES [dbo].[DOCUMENTO] ([NUM_DOC])
GO
ALTER TABLE [dbo].[DOCUMENTOL] CHECK CONSTRAINT [FK_DOCUMENTOL_DOCUMENTO]
GO
ALTER TABLE [dbo].[DOCUMENTOM]  WITH CHECK ADD  CONSTRAINT [FK_DOCUMENTOM_DOCUMENTOP] FOREIGN KEY([NUM_DOC], [POS_ID])
REFERENCES [dbo].[DOCUMENTOP] ([NUM_DOC], [POS])
GO
ALTER TABLE [dbo].[DOCUMENTOM] CHECK CONSTRAINT [FK_DOCUMENTOM_DOCUMENTOP]
GO
ALTER TABLE [dbo].[DOCUMENTON]  WITH CHECK ADD  CONSTRAINT [FK_DOCUMENTON_DOCUMENTO] FOREIGN KEY([NUM_DOC])
REFERENCES [dbo].[DOCUMENTO] ([NUM_DOC])
GO
ALTER TABLE [dbo].[DOCUMENTON] CHECK CONSTRAINT [FK_DOCUMENTON_DOCUMENTO]
GO
ALTER TABLE [dbo].[DOCUMENTOP]  WITH CHECK ADD  CONSTRAINT [FK_DOCUMENTOP_DOCUMENTO] FOREIGN KEY([NUM_DOC])
REFERENCES [dbo].[DOCUMENTO] ([NUM_DOC])
GO
ALTER TABLE [dbo].[DOCUMENTOP] CHECK CONSTRAINT [FK_DOCUMENTOP_DOCUMENTO]
GO
ALTER TABLE [dbo].[DOCUMENTOR]  WITH CHECK ADD  CONSTRAINT [FK_DOCUMENTOR_DOCUMENTO] FOREIGN KEY([NUM_DOC])
REFERENCES [dbo].[DOCUMENTO] ([NUM_DOC])
GO
ALTER TABLE [dbo].[DOCUMENTOR] CHECK CONSTRAINT [FK_DOCUMENTOR_DOCUMENTO]
GO
ALTER TABLE [dbo].[DOCUMENTOR]  WITH CHECK ADD  CONSTRAINT [FK_DOCUMENTOR_TREVERSA] FOREIGN KEY([TREVERSA_ID])
REFERENCES [dbo].[TREVERSA] ([ID])
GO
ALTER TABLE [dbo].[DOCUMENTOR] CHECK CONSTRAINT [FK_DOCUMENTOR_TREVERSA]
GO
ALTER TABLE [dbo].[DOCUMENTORAN]  WITH CHECK ADD  CONSTRAINT [FK_DOCUMENTORAN_DOCUMENTOREC] FOREIGN KEY([NUM_DOC], [POS])
REFERENCES [dbo].[DOCUMENTOREC] ([NUM_DOC], [POS])
GO
ALTER TABLE [dbo].[DOCUMENTORAN] CHECK CONSTRAINT [FK_DOCUMENTORAN_DOCUMENTOREC]
GO
ALTER TABLE [dbo].[DOCUMENTOREC]  WITH CHECK ADD  CONSTRAINT [FK_DOCUMENTOREC_DOCUMENTO] FOREIGN KEY([NUM_DOC])
REFERENCES [dbo].[DOCUMENTO] ([NUM_DOC])
GO
ALTER TABLE [dbo].[DOCUMENTOREC] CHECK CONSTRAINT [FK_DOCUMENTOREC_DOCUMENTO]
GO
ALTER TABLE [dbo].[DOCUMENTOSAP]  WITH CHECK ADD  CONSTRAINT [FK_DOCUMENTOSAP_DOCUMENTO] FOREIGN KEY([NUM_DOC])
REFERENCES [dbo].[DOCUMENTO] ([NUM_DOC])
GO
ALTER TABLE [dbo].[DOCUMENTOSAP] CHECK CONSTRAINT [FK_DOCUMENTOSAP_DOCUMENTO]
GO
ALTER TABLE [dbo].[DOCUMENTOTS]  WITH CHECK ADD  CONSTRAINT [FK_DOCUMENTOTS_DOCUMENTO] FOREIGN KEY([NUM_DOC])
REFERENCES [dbo].[DOCUMENTO] ([NUM_DOC])
GO
ALTER TABLE [dbo].[DOCUMENTOTS] CHECK CONSTRAINT [FK_DOCUMENTOTS_DOCUMENTO]
GO
ALTER TABLE [dbo].[DOCUMENTOTS]  WITH CHECK ADD  CONSTRAINT [FK_DOCUMENTOTS_TS_CAMPO] FOREIGN KEY([TSFORM_ID])
REFERENCES [dbo].[TS_CAMPO] ([ID])
GO
ALTER TABLE [dbo].[DOCUMENTOTS] CHECK CONSTRAINT [FK_DOCUMENTOTS_TS_CAMPO]
GO
ALTER TABLE [dbo].[ESTATUSR]  WITH CHECK ADD  CONSTRAINT [FK_ESTATUSR_ESTATUS] FOREIGN KEY([ESTATUS_ID])
REFERENCES [dbo].[ESTATUS] ([ID])
GO
ALTER TABLE [dbo].[ESTATUSR] CHECK CONSTRAINT [FK_ESTATUSR_ESTATUS]
GO
ALTER TABLE [dbo].[ESTATUST]  WITH CHECK ADD  CONSTRAINT [FK_ESTATUST_ESTATUS] FOREIGN KEY([ESTATUS_ID])
REFERENCES [dbo].[ESTATUS] ([ID])
GO
ALTER TABLE [dbo].[ESTATUST] CHECK CONSTRAINT [FK_ESTATUST_ESTATUS]
GO
ALTER TABLE [dbo].[ESTATUST]  WITH CHECK ADD  CONSTRAINT [FK_ESTATUST_SPRAS] FOREIGN KEY([SPRAS_ID])
REFERENCES [dbo].[SPRAS] ([ID])
GO
ALTER TABLE [dbo].[ESTATUST] CHECK CONSTRAINT [FK_ESTATUST_SPRAS]
GO
ALTER TABLE [dbo].[FACTURASCONF]  WITH CHECK ADD  CONSTRAINT [FK_FACTURASCONF_SOCIEDAD] FOREIGN KEY([SOCIEDAD_ID])
REFERENCES [dbo].[SOCIEDAD] ([BUKRS])
GO
ALTER TABLE [dbo].[FACTURASCONF] CHECK CONSTRAINT [FK_FACTURASCONF_SOCIEDAD]
GO
ALTER TABLE [dbo].[FLUJO]  WITH CHECK ADD  CONSTRAINT [FK_FLUJO_DOCUMENTO] FOREIGN KEY([NUM_DOC])
REFERENCES [dbo].[DOCUMENTO] ([NUM_DOC])
GO
ALTER TABLE [dbo].[FLUJO] CHECK CONSTRAINT [FK_FLUJO_DOCUMENTO]
GO
ALTER TABLE [dbo].[FLUJO]  WITH CHECK ADD  CONSTRAINT [FK_FLUJO_USUARIO] FOREIGN KEY([USUARIOA_ID])
REFERENCES [dbo].[USUARIO] ([ID])
GO
ALTER TABLE [dbo].[FLUJO] CHECK CONSTRAINT [FK_FLUJO_USUARIO]
GO
ALTER TABLE [dbo].[FLUJO]  WITH CHECK ADD  CONSTRAINT [FK_FLUJO_USUARIO1] FOREIGN KEY([USUARIOD_ID])
REFERENCES [dbo].[USUARIO] ([ID])
GO
ALTER TABLE [dbo].[FLUJO] CHECK CONSTRAINT [FK_FLUJO_USUARIO1]
GO
ALTER TABLE [dbo].[FLUJO]  WITH CHECK ADD  CONSTRAINT [FK_FLUJO_WORKFP] FOREIGN KEY([WORKF_ID], [WF_VERSION], [WF_POS])
REFERENCES [dbo].[WORKFP] ([ID], [VERSION], [POS])
GO
ALTER TABLE [dbo].[FLUJO] CHECK CONSTRAINT [FK_FLUJO_WORKFP]
GO
ALTER TABLE [dbo].[GALLT]  WITH CHECK ADD  CONSTRAINT [FK_GALLT_GALL] FOREIGN KEY([GALL_ID])
REFERENCES [dbo].[GALL] ([ID])
GO
ALTER TABLE [dbo].[GALLT] CHECK CONSTRAINT [FK_GALLT_GALL]
GO
ALTER TABLE [dbo].[GALLT]  WITH CHECK ADD  CONSTRAINT [FK_GALLT_SPRAS] FOREIGN KEY([SPRAS_ID])
REFERENCES [dbo].[SPRAS] ([ID])
GO
ALTER TABLE [dbo].[GALLT] CHECK CONSTRAINT [FK_GALLT_SPRAS]
GO
ALTER TABLE [dbo].[GAUTORIZACION]  WITH CHECK ADD  CONSTRAINT [FK_GAUTORIZACION_SOCIEDAD] FOREIGN KEY([BUKRS])
REFERENCES [dbo].[SOCIEDAD] ([BUKRS])
GO
ALTER TABLE [dbo].[GAUTORIZACION] CHECK CONSTRAINT [FK_GAUTORIZACION_SOCIEDAD]
GO
ALTER TABLE [dbo].[IIMPUESTO]  WITH NOCHECK ADD  CONSTRAINT [FK_IIMPUESTO_IMPUESTO] FOREIGN KEY([MWSKZ])
REFERENCES [dbo].[IMPUESTO] ([MWSKZ])
GO
ALTER TABLE [dbo].[IIMPUESTO] CHECK CONSTRAINT [FK_IIMPUESTO_IMPUESTO]
GO
ALTER TABLE [dbo].[IIMPUESTO]  WITH NOCHECK ADD  CONSTRAINT [FK_IIMPUESTO_PAIS] FOREIGN KEY([LAND])
REFERENCES [dbo].[PAIS] ([LAND])
GO
ALTER TABLE [dbo].[IIMPUESTO] CHECK CONSTRAINT [FK_IIMPUESTO_PAIS]
GO
ALTER TABLE [dbo].[LAYOUT_CARGA]  WITH CHECK ADD  CONSTRAINT [FK_LAYOUT_CARGA_PAIS] FOREIGN KEY([LAND])
REFERENCES [dbo].[PAIS] ([LAND])
GO
ALTER TABLE [dbo].[LAYOUT_CARGA] CHECK CONSTRAINT [FK_LAYOUT_CARGA_PAIS]
GO
ALTER TABLE [dbo].[LAYOUT_CARGA]  WITH CHECK ADD  CONSTRAINT [FK_LAYOUT_CARGA_SOCIEDAD] FOREIGN KEY([SOCIEDAD_ID])
REFERENCES [dbo].[SOCIEDAD] ([BUKRS])
GO
ALTER TABLE [dbo].[LAYOUT_CARGA] CHECK CONSTRAINT [FK_LAYOUT_CARGA_SOCIEDAD]
GO
ALTER TABLE [dbo].[LEYENDA]  WITH CHECK ADD  CONSTRAINT [FK_LEYENDA_PAIS] FOREIGN KEY([PAIS_ID])
REFERENCES [dbo].[PAIS] ([LAND])
GO
ALTER TABLE [dbo].[LEYENDA] CHECK CONSTRAINT [FK_LEYENDA_PAIS]
GO
ALTER TABLE [dbo].[MATERIAL]  WITH NOCHECK ADD  CONSTRAINT [FK_MATERIAL_CTGR] FOREIGN KEY([CTGR])
REFERENCES [dbo].[ZCTGR] ([ID_ZC])
GO
ALTER TABLE [dbo].[MATERIAL] CHECK CONSTRAINT [FK_MATERIAL_CTGR]
GO
ALTER TABLE [dbo].[MATERIAL]  WITH NOCHECK ADD  CONSTRAINT [FK_MATERIAL_MARCA] FOREIGN KEY([BRAND])
REFERENCES [dbo].[ZBRAND] ([ID_ZB])
GO
ALTER TABLE [dbo].[MATERIAL] CHECK CONSTRAINT [FK_MATERIAL_MARCA]
GO
ALTER TABLE [dbo].[MATERIAL]  WITH CHECK ADD  CONSTRAINT [FK_MATERIAL_MATERIALGP] FOREIGN KEY([MATERIALGP_ID])
REFERENCES [dbo].[MATERIALGP] ([ID])
GO
ALTER TABLE [dbo].[MATERIAL] CHECK CONSTRAINT [FK_MATERIAL_MATERIALGP]
GO
ALTER TABLE [dbo].[MATERIAL]  WITH NOCHECK ADD  CONSTRAINT [FK_MATERIAL_UMEDIDA] FOREIGN KEY([MEINS])
REFERENCES [dbo].[UMEDIDA] ([MSEHI])
GO
ALTER TABLE [dbo].[MATERIAL] CHECK CONSTRAINT [FK_MATERIAL_UMEDIDA]
GO
ALTER TABLE [dbo].[MATERIALGPT]  WITH NOCHECK ADD  CONSTRAINT [FK_MATERIALGPT_MATERIALGP] FOREIGN KEY([MATERIALGP_ID])
REFERENCES [dbo].[MATERIALGP] ([ID])
GO
ALTER TABLE [dbo].[MATERIALGPT] CHECK CONSTRAINT [FK_MATERIALGPT_MATERIALGP]
GO
ALTER TABLE [dbo].[MATERIALGPT]  WITH NOCHECK ADD  CONSTRAINT [FK_MATERIALGPT_SPRAS] FOREIGN KEY([SPRAS_ID])
REFERENCES [dbo].[SPRAS] ([ID])
GO
ALTER TABLE [dbo].[MATERIALGPT] CHECK CONSTRAINT [FK_MATERIALGPT_SPRAS]
GO
ALTER TABLE [dbo].[MATERIALT]  WITH NOCHECK ADD  CONSTRAINT [FK_MATERIALT_MATERIAL] FOREIGN KEY([MATERIAL_ID])
REFERENCES [dbo].[MATERIAL] ([ID])
GO
ALTER TABLE [dbo].[MATERIALT] CHECK CONSTRAINT [FK_MATERIALT_MATERIAL]
GO
ALTER TABLE [dbo].[MATERIALT]  WITH NOCHECK ADD  CONSTRAINT [FK_MATERIALT_SPRAS] FOREIGN KEY([SPRAS])
REFERENCES [dbo].[SPRAS] ([ID])
GO
ALTER TABLE [dbo].[MATERIALT] CHECK CONSTRAINT [FK_MATERIALT_SPRAS]
GO
ALTER TABLE [dbo].[MATERIALVKE]  WITH NOCHECK ADD  CONSTRAINT [FK_MATERIALVKE_MATERIAL] FOREIGN KEY([MATERIAL_ID])
REFERENCES [dbo].[MATERIAL] ([ID])
GO
ALTER TABLE [dbo].[MATERIALVKE] CHECK CONSTRAINT [FK_MATERIALVKE_MATERIAL]
GO
ALTER TABLE [dbo].[MIEMBROS]  WITH CHECK ADD  CONSTRAINT [FK_MIEMBROS_ROL] FOREIGN KEY([ROL_ID])
REFERENCES [dbo].[ROL] ([ID])
GO
ALTER TABLE [dbo].[MIEMBROS] CHECK CONSTRAINT [FK_MIEMBROS_ROL]
GO
ALTER TABLE [dbo].[MIEMBROS]  WITH CHECK ADD  CONSTRAINT [FK_MIEMBROS_USUARIO] FOREIGN KEY([USUARIO_ID])
REFERENCES [dbo].[USUARIO] ([ID])
GO
ALTER TABLE [dbo].[MIEMBROS] CHECK CONSTRAINT [FK_MIEMBROS_USUARIO]
GO
ALTER TABLE [dbo].[NOTICIA]  WITH CHECK ADD  CONSTRAINT [FK_NOTICIA_USUARIO] FOREIGN KEY([USUARIOC_ID])
REFERENCES [dbo].[USUARIO] ([ID])
GO
ALTER TABLE [dbo].[NOTICIA] CHECK CONSTRAINT [FK_NOTICIA_USUARIO]
GO
ALTER TABLE [dbo].[PAGINA]  WITH CHECK ADD  CONSTRAINT [FK_PAGINA_CARPETA] FOREIGN KEY([CARPETA_ID])
REFERENCES [dbo].[CARPETA] ([ID])
GO
ALTER TABLE [dbo].[PAGINA] CHECK CONSTRAINT [FK_PAGINA_CARPETA]
GO
ALTER TABLE [dbo].[PAGINAT]  WITH CHECK ADD  CONSTRAINT [FK_PAGINAT_PAGINA] FOREIGN KEY([ID])
REFERENCES [dbo].[PAGINA] ([ID])
GO
ALTER TABLE [dbo].[PAGINAT] CHECK CONSTRAINT [FK_PAGINAT_PAGINA]
GO
ALTER TABLE [dbo].[PAGINAT]  WITH CHECK ADD  CONSTRAINT [FK_PAGINAT_SPRAS] FOREIGN KEY([SPRAS_ID])
REFERENCES [dbo].[SPRAS] ([ID])
GO
ALTER TABLE [dbo].[PAGINAT] CHECK CONSTRAINT [FK_PAGINAT_SPRAS]
GO
ALTER TABLE [dbo].[PAIS]  WITH NOCHECK ADD  CONSTRAINT [FK_PAIS_SOCIEDAD] FOREIGN KEY([SOCIEDAD_ID])
REFERENCES [dbo].[SOCIEDAD] ([BUKRS])
GO
ALTER TABLE [dbo].[PAIS] CHECK CONSTRAINT [FK_PAIS_SOCIEDAD]
GO
ALTER TABLE [dbo].[PERIODOT]  WITH CHECK ADD  CONSTRAINT [FK_PERIODOT_PERIODO] FOREIGN KEY([PERIODO_ID])
REFERENCES [dbo].[PERIODO] ([ID])
GO
ALTER TABLE [dbo].[PERIODOT] CHECK CONSTRAINT [FK_PERIODOT_PERIODO]
GO
ALTER TABLE [dbo].[PERIODOT]  WITH CHECK ADD  CONSTRAINT [FK_PERIODOT_SPRAS] FOREIGN KEY([SPRAS_ID])
REFERENCES [dbo].[SPRAS] ([ID])
GO
ALTER TABLE [dbo].[PERIODOT] CHECK CONSTRAINT [FK_PERIODOT_SPRAS]
GO
ALTER TABLE [dbo].[PERMISO_PAGINA]  WITH CHECK ADD  CONSTRAINT [FK_PERMISO_PAGINA_PAGINA] FOREIGN KEY([PAGINA_ID])
REFERENCES [dbo].[PAGINA] ([ID])
GO
ALTER TABLE [dbo].[PERMISO_PAGINA] CHECK CONSTRAINT [FK_PERMISO_PAGINA_PAGINA]
GO
ALTER TABLE [dbo].[PERMISO_PAGINA]  WITH CHECK ADD  CONSTRAINT [FK_PERMISO_PAGINA_PUESTO] FOREIGN KEY([ROL_ID])
REFERENCES [dbo].[PUESTO] ([ID])
GO
ALTER TABLE [dbo].[PERMISO_PAGINA] CHECK CONSTRAINT [FK_PERMISO_PAGINA_PUESTO]
GO
ALTER TABLE [dbo].[PRESUPSAPH]  WITH CHECK ADD  CONSTRAINT [FK_PRESUPSAPH_USUARIO] FOREIGN KEY([USUARIO_ID])
REFERENCES [dbo].[USUARIO] ([ID])
GO
ALTER TABLE [dbo].[PRESUPSAPH] CHECK CONSTRAINT [FK_PRESUPSAPH_USUARIO]
GO
ALTER TABLE [dbo].[PRESUPSAPP]  WITH NOCHECK ADD  CONSTRAINT [FK_PRESUPSAPP_PRESUPSAPH] FOREIGN KEY([ID], [ANIO])
REFERENCES [dbo].[PRESUPSAPH] ([ID], [ANIO])
GO
ALTER TABLE [dbo].[PRESUPSAPP] CHECK CONSTRAINT [FK_PRESUPSAPP_PRESUPSAPH]
GO
ALTER TABLE [dbo].[PRESUPUESTOH]  WITH CHECK ADD  CONSTRAINT [FK_PRESUPUESTOH_USUARIO] FOREIGN KEY([USUARIO_ID])
REFERENCES [dbo].[USUARIO] ([ID])
GO
ALTER TABLE [dbo].[PRESUPUESTOH] CHECK CONSTRAINT [FK_PRESUPUESTOH_USUARIO]
GO
ALTER TABLE [dbo].[PRESUPUESTOP]  WITH NOCHECK ADD  CONSTRAINT [FK_PRESUPUESTOP_PRESUPUESTOH] FOREIGN KEY([ID], [ANIO])
REFERENCES [dbo].[PRESUPUESTOH] ([ID], [ANIO])
GO
ALTER TABLE [dbo].[PRESUPUESTOP] CHECK CONSTRAINT [FK_PRESUPUESTOP_PRESUPUESTOH]
GO
ALTER TABLE [dbo].[PUESTOT]  WITH CHECK ADD  CONSTRAINT [FK_PUESTOT_PUESTO] FOREIGN KEY([PUESTO_ID])
REFERENCES [dbo].[PUESTO] ([ID])
GO
ALTER TABLE [dbo].[PUESTOT] CHECK CONSTRAINT [FK_PUESTOT_PUESTO]
GO
ALTER TABLE [dbo].[PUESTOT]  WITH CHECK ADD  CONSTRAINT [FK_PUESTOT_SPRAS] FOREIGN KEY([SPRAS_ID])
REFERENCES [dbo].[SPRAS] ([ID])
GO
ALTER TABLE [dbo].[PUESTOT] CHECK CONSTRAINT [FK_PUESTOT_SPRAS]
GO
ALTER TABLE [dbo].[RETENCIONT]  WITH CHECK ADD  CONSTRAINT [FK_RETENCIONT_RETENCION] FOREIGN KEY([RETENCION_ID])
REFERENCES [dbo].[RETENCION] ([ID])
GO
ALTER TABLE [dbo].[RETENCIONT] CHECK CONSTRAINT [FK_RETENCIONT_RETENCION]
GO
ALTER TABLE [dbo].[RETENCIONT]  WITH CHECK ADD  CONSTRAINT [FK_RETENCIONT_SPRAS] FOREIGN KEY([SPRAS_ID])
REFERENCES [dbo].[SPRAS] ([ID])
GO
ALTER TABLE [dbo].[RETENCIONT] CHECK CONSTRAINT [FK_RETENCIONT_SPRAS]
GO
ALTER TABLE [dbo].[ROLT]  WITH CHECK ADD  CONSTRAINT [FK_ROLT_ROL] FOREIGN KEY([ROL_ID])
REFERENCES [dbo].[ROL] ([ID])
GO
ALTER TABLE [dbo].[ROLT] CHECK CONSTRAINT [FK_ROLT_ROL]
GO
ALTER TABLE [dbo].[ROLT]  WITH CHECK ADD  CONSTRAINT [FK_ROLT_SPRAS] FOREIGN KEY([SPRAS_ID])
REFERENCES [dbo].[SPRAS] ([ID])
GO
ALTER TABLE [dbo].[ROLT] CHECK CONSTRAINT [FK_ROLT_SPRAS]
GO
ALTER TABLE [dbo].[SOCIEDAD]  WITH NOCHECK ADD  CONSTRAINT [FK_SOCIEDAD_MONEDA] FOREIGN KEY([WAERS])
REFERENCES [dbo].[MONEDA] ([WAERS])
GO
ALTER TABLE [dbo].[SOCIEDAD] CHECK CONSTRAINT [FK_SOCIEDAD_MONEDA]
GO
ALTER TABLE [dbo].[STATES]  WITH CHECK ADD  CONSTRAINT [FK_STATES_COUNTRIES] FOREIGN KEY([COUNTRY_ID])
REFERENCES [dbo].[COUNTRIES] ([ID])
GO
ALTER TABLE [dbo].[STATES] CHECK CONSTRAINT [FK_STATES_COUNTRIES]
GO
ALTER TABLE [dbo].[TAB_CAMPO]  WITH CHECK ADD  CONSTRAINT [FK_TAB_CAMPO_TAB] FOREIGN KEY([TAB_ID])
REFERENCES [dbo].[TAB] ([ID])
GO
ALTER TABLE [dbo].[TAB_CAMPO] CHECK CONSTRAINT [FK_TAB_CAMPO_TAB]
GO
ALTER TABLE [dbo].[TALL]  WITH NOCHECK ADD  CONSTRAINT [FK_TALL_GALL] FOREIGN KEY([GALL_ID])
REFERENCES [dbo].[GALL] ([ID])
GO
ALTER TABLE [dbo].[TALL] CHECK CONSTRAINT [FK_TALL_GALL]
GO
ALTER TABLE [dbo].[TALLT]  WITH NOCHECK ADD  CONSTRAINT [FK_TALLT_SPRAS] FOREIGN KEY([SPRAS_ID])
REFERENCES [dbo].[SPRAS] ([ID])
GO
ALTER TABLE [dbo].[TALLT] CHECK CONSTRAINT [FK_TALLT_SPRAS]
GO
ALTER TABLE [dbo].[TALLT]  WITH NOCHECK ADD  CONSTRAINT [FK_TALLT_TALL] FOREIGN KEY([TALL_ID])
REFERENCES [dbo].[TALL] ([ID])
GO
ALTER TABLE [dbo].[TALLT] CHECK CONSTRAINT [FK_TALLT_TALL]
GO
ALTER TABLE [dbo].[TAX_LAND]  WITH CHECK ADD  CONSTRAINT [FK_TAX_LAND_PAIS] FOREIGN KEY([PAIS_ID])
REFERENCES [dbo].[PAIS] ([LAND])
GO
ALTER TABLE [dbo].[TAX_LAND] CHECK CONSTRAINT [FK_TAX_LAND_PAIS]
GO
ALTER TABLE [dbo].[TAXEOH]  WITH CHECK ADD  CONSTRAINT [FK_TAXEOH_CLIENTE] FOREIGN KEY([VKORG], [VTWEG], [SPART], [KUNNR])
REFERENCES [dbo].[CLIENTE] ([VKORG], [VTWEG], [SPART], [KUNNR])
GO
ALTER TABLE [dbo].[TAXEOH] CHECK CONSTRAINT [FK_TAXEOH_CLIENTE]
GO
ALTER TABLE [dbo].[TAXEOH]  WITH CHECK ADD  CONSTRAINT [FK_TAXEOH_IMPUESTO] FOREIGN KEY([IMPUESTO_ID])
REFERENCES [dbo].[IMPUESTO] ([MWSKZ])
GO
ALTER TABLE [dbo].[TAXEOH] CHECK CONSTRAINT [FK_TAXEOH_IMPUESTO]
GO
ALTER TABLE [dbo].[TAXEOH]  WITH CHECK ADD  CONSTRAINT [FK_TAXEOH_PAIS] FOREIGN KEY([PAIS_ID])
REFERENCES [dbo].[PAIS] ([LAND])
GO
ALTER TABLE [dbo].[TAXEOH] CHECK CONSTRAINT [FK_TAXEOH_PAIS]
GO
ALTER TABLE [dbo].[TAXEOH]  WITH CHECK ADD  CONSTRAINT [FK_TAXEOH_SOCIEDAD] FOREIGN KEY([SOCIEDAD_ID])
REFERENCES [dbo].[SOCIEDAD] ([BUKRS])
GO
ALTER TABLE [dbo].[TAXEOH] CHECK CONSTRAINT [FK_TAXEOH_SOCIEDAD]
GO
ALTER TABLE [dbo].[TAXEOH]  WITH CHECK ADD  CONSTRAINT [FK_TAXEOH_TX_CONCEPTO] FOREIGN KEY([CONCEPTO_ID])
REFERENCES [dbo].[TX_CONCEPTO] ([ID])
GO
ALTER TABLE [dbo].[TAXEOH] CHECK CONSTRAINT [FK_TAXEOH_TX_CONCEPTO]
GO
ALTER TABLE [dbo].[TAXEOH]  WITH CHECK ADD  CONSTRAINT [FK_TAXEOH_TX_TNOTA] FOREIGN KEY([TNOTA_ID])
REFERENCES [dbo].[TX_TNOTA] ([ID])
GO
ALTER TABLE [dbo].[TAXEOH] CHECK CONSTRAINT [FK_TAXEOH_TX_TNOTA]
GO
ALTER TABLE [dbo].[TAXEOP]  WITH CHECK ADD  CONSTRAINT [FK_TAXEOP_RETENCION] FOREIGN KEY([RETENCION_ID])
REFERENCES [dbo].[RETENCION] ([ID])
GO
ALTER TABLE [dbo].[TAXEOP] CHECK CONSTRAINT [FK_TAXEOP_RETENCION]
GO
ALTER TABLE [dbo].[TAXEOP]  WITH CHECK ADD  CONSTRAINT [FK_TAXEOP_TAXEOH] FOREIGN KEY([SOCIEDAD_ID], [PAIS_ID], [VKORG], [VTWEG], [SPART], [KUNNR], [CONCEPTO_ID])
REFERENCES [dbo].[TAXEOH] ([SOCIEDAD_ID], [PAIS_ID], [VKORG], [VTWEG], [SPART], [KUNNR], [CONCEPTO_ID])
GO
ALTER TABLE [dbo].[TAXEOP] CHECK CONSTRAINT [FK_TAXEOP_TAXEOH]
GO
ALTER TABLE [dbo].[TAXEOP]  WITH CHECK ADD  CONSTRAINT [FK_TAXEOP_TRETENCION] FOREIGN KEY([TRETENCION_ID])
REFERENCES [dbo].[TRETENCION] ([ID])
GO
ALTER TABLE [dbo].[TAXEOP] CHECK CONSTRAINT [FK_TAXEOP_TRETENCION]
GO
ALTER TABLE [dbo].[TCAMBIO]  WITH NOCHECK ADD  CONSTRAINT [FK_TCAMBIO_MONEDA] FOREIGN KEY([TCURR])
REFERENCES [dbo].[MONEDA] ([WAERS])
GO
ALTER TABLE [dbo].[TCAMBIO] CHECK CONSTRAINT [FK_TCAMBIO_MONEDA]
GO
ALTER TABLE [dbo].[TCAMBIO]  WITH NOCHECK ADD  CONSTRAINT [FK_TCAMBIO_MONEDA1] FOREIGN KEY([FCURR])
REFERENCES [dbo].[MONEDA] ([WAERS])
GO
ALTER TABLE [dbo].[TCAMBIO] CHECK CONSTRAINT [FK_TCAMBIO_MONEDA1]
GO
ALTER TABLE [dbo].[TCLIENTET]  WITH CHECK ADD  CONSTRAINT [FK_TCLIENTET_SPRAS] FOREIGN KEY([SPRAS])
REFERENCES [dbo].[SPRAS] ([ID])
GO
ALTER TABLE [dbo].[TCLIENTET] CHECK CONSTRAINT [FK_TCLIENTET_SPRAS]
GO
ALTER TABLE [dbo].[TCLIENTET]  WITH CHECK ADD  CONSTRAINT [FK_TCLIENTET_TCLIENTE] FOREIGN KEY([PARVW_ID])
REFERENCES [dbo].[TCLIENTE] ([ID])
GO
ALTER TABLE [dbo].[TCLIENTET] CHECK CONSTRAINT [FK_TCLIENTET_TCLIENTE]
GO
ALTER TABLE [dbo].[TEXTO]  WITH CHECK ADD  CONSTRAINT [FK_TEXTO_CAMPOS] FOREIGN KEY([PAGINA_ID], [CAMPO_ID])
REFERENCES [dbo].[CAMPOS] ([PAGINA_ID], [ID])
GO
ALTER TABLE [dbo].[TEXTO] CHECK CONSTRAINT [FK_TEXTO_CAMPOS]
GO
ALTER TABLE [dbo].[TEXTO]  WITH CHECK ADD  CONSTRAINT [FK_TEXTO_SPRAS] FOREIGN KEY([SPRAS_ID])
REFERENCES [dbo].[SPRAS] ([ID])
GO
ALTER TABLE [dbo].[TEXTO] CHECK CONSTRAINT [FK_TEXTO_SPRAS]
GO
ALTER TABLE [dbo].[TEXTOCV]  WITH CHECK ADD  CONSTRAINT [FK_SPRAS_SPRAS_ID] FOREIGN KEY([SPRAS_ID])
REFERENCES [dbo].[SPRAS] ([ID])
GO
ALTER TABLE [dbo].[TEXTOCV] CHECK CONSTRAINT [FK_SPRAS_SPRAS_ID]
GO
ALTER TABLE [dbo].[TRETENCIONT]  WITH CHECK ADD  CONSTRAINT [FK_TRETENCIONT_SPRAS] FOREIGN KEY([SPRAS_ID])
REFERENCES [dbo].[SPRAS] ([ID])
GO
ALTER TABLE [dbo].[TRETENCIONT] CHECK CONSTRAINT [FK_TRETENCIONT_SPRAS]
GO
ALTER TABLE [dbo].[TRETENCIONT]  WITH CHECK ADD  CONSTRAINT [FK_TRETENCIONT_TRETENCION] FOREIGN KEY([TRETENCION_ID])
REFERENCES [dbo].[TRETENCION] ([ID])
GO
ALTER TABLE [dbo].[TRETENCIONT] CHECK CONSTRAINT [FK_TRETENCIONT_TRETENCION]
GO
ALTER TABLE [dbo].[TREVERSAT]  WITH CHECK ADD  CONSTRAINT [FK_TREVERSAT_SPRAS] FOREIGN KEY([SPRAS_ID])
REFERENCES [dbo].[SPRAS] ([ID])
GO
ALTER TABLE [dbo].[TREVERSAT] CHECK CONSTRAINT [FK_TREVERSAT_SPRAS]
GO
ALTER TABLE [dbo].[TREVERSAT]  WITH CHECK ADD  CONSTRAINT [FK_TREVERSAT_TREVERSA] FOREIGN KEY([TREVERSA_ID])
REFERENCES [dbo].[TREVERSA] ([ID])
GO
ALTER TABLE [dbo].[TREVERSAT] CHECK CONSTRAINT [FK_TREVERSAT_TREVERSA]
GO
ALTER TABLE [dbo].[TS_FORM]  WITH CHECK ADD  CONSTRAINT [FK_TS_FORM_PAIS] FOREIGN KEY([LAND_ID])
REFERENCES [dbo].[PAIS] ([LAND])
GO
ALTER TABLE [dbo].[TS_FORM] CHECK CONSTRAINT [FK_TS_FORM_PAIS]
GO
ALTER TABLE [dbo].[TS_FORM]  WITH CHECK ADD  CONSTRAINT [FK_TS_FORM_SOCIEDAD] FOREIGN KEY([BUKRS_ID])
REFERENCES [dbo].[SOCIEDAD] ([BUKRS])
GO
ALTER TABLE [dbo].[TS_FORM] CHECK CONSTRAINT [FK_TS_FORM_SOCIEDAD]
GO
ALTER TABLE [dbo].[TS_FORM]  WITH CHECK ADD  CONSTRAINT [FK_TS_FORM_TS_CAMPO] FOREIGN KEY([POS])
REFERENCES [dbo].[TS_CAMPO] ([ID])
GO
ALTER TABLE [dbo].[TS_FORM] CHECK CONSTRAINT [FK_TS_FORM_TS_CAMPO]
GO
ALTER TABLE [dbo].[TS_FORMT]  WITH CHECK ADD  CONSTRAINT [FK_TS_FORMT_SPRAS] FOREIGN KEY([SPRAS_ID])
REFERENCES [dbo].[SPRAS] ([ID])
GO
ALTER TABLE [dbo].[TS_FORMT] CHECK CONSTRAINT [FK_TS_FORMT_SPRAS]
GO
ALTER TABLE [dbo].[TS_FORMT]  WITH CHECK ADD  CONSTRAINT [FK_TS_FORMT_TS_CAMPO] FOREIGN KEY([TSFORM_ID])
REFERENCES [dbo].[TS_CAMPO] ([ID])
GO
ALTER TABLE [dbo].[TS_FORMT] CHECK CONSTRAINT [FK_TS_FORMT_TS_CAMPO]
GO
ALTER TABLE [dbo].[TSOL]  WITH CHECK ADD  CONSTRAINT [FK_TSOL_RANGO] FOREIGN KEY([RANGO_ID])
REFERENCES [dbo].[RANGO] ([ID])
GO
ALTER TABLE [dbo].[TSOL] CHECK CONSTRAINT [FK_TSOL_RANGO]
GO
ALTER TABLE [dbo].[TSOL]  WITH CHECK ADD  CONSTRAINT [FK_TSOL_TSOL] FOREIGN KEY([TSOLR])
REFERENCES [dbo].[TSOL] ([ID])
GO
ALTER TABLE [dbo].[TSOL] CHECK CONSTRAINT [FK_TSOL_TSOL]
GO
ALTER TABLE [dbo].[TSOLT]  WITH CHECK ADD  CONSTRAINT [FK_TSOLT_SPRAS] FOREIGN KEY([SPRAS_ID])
REFERENCES [dbo].[SPRAS] ([ID])
GO
ALTER TABLE [dbo].[TSOLT] CHECK CONSTRAINT [FK_TSOLT_SPRAS]
GO
ALTER TABLE [dbo].[TSOLT]  WITH CHECK ADD  CONSTRAINT [FK_TSOLT_TSOL] FOREIGN KEY([TSOL_ID])
REFERENCES [dbo].[TSOL] ([ID])
GO
ALTER TABLE [dbo].[TSOLT] CHECK CONSTRAINT [FK_TSOLT_TSOL]
GO
ALTER TABLE [dbo].[TSOPORTET]  WITH CHECK ADD  CONSTRAINT [FK_TSOPORTET_SPRAS] FOREIGN KEY([SPRAS_ID])
REFERENCES [dbo].[SPRAS] ([ID])
GO
ALTER TABLE [dbo].[TSOPORTET] CHECK CONSTRAINT [FK_TSOPORTET_SPRAS]
GO
ALTER TABLE [dbo].[TSOPORTET]  WITH CHECK ADD  CONSTRAINT [FK_TSOPORTET_TSOPORTE] FOREIGN KEY([TSOPORTE_ID])
REFERENCES [dbo].[TSOPORTE] ([ID])
GO
ALTER TABLE [dbo].[TSOPORTET] CHECK CONSTRAINT [FK_TSOPORTET_TSOPORTE]
GO
ALTER TABLE [dbo].[TX_CONCEPTOT]  WITH CHECK ADD  CONSTRAINT [FK_TX_CONCEPTOT_SPRAS] FOREIGN KEY([SPRAS_ID])
REFERENCES [dbo].[SPRAS] ([ID])
GO
ALTER TABLE [dbo].[TX_CONCEPTOT] CHECK CONSTRAINT [FK_TX_CONCEPTOT_SPRAS]
GO
ALTER TABLE [dbo].[TX_CONCEPTOT]  WITH CHECK ADD  CONSTRAINT [FK_TX_CONCEPTOT_TX_CONCEPTO] FOREIGN KEY([CONCEPTO_ID])
REFERENCES [dbo].[TX_CONCEPTO] ([ID])
GO
ALTER TABLE [dbo].[TX_CONCEPTOT] CHECK CONSTRAINT [FK_TX_CONCEPTOT_TX_CONCEPTO]
GO
ALTER TABLE [dbo].[TX_NOTAT]  WITH CHECK ADD  CONSTRAINT [FK_TX_NOTAT_SPRAS] FOREIGN KEY([SPRAS_ID])
REFERENCES [dbo].[SPRAS] ([ID])
GO
ALTER TABLE [dbo].[TX_NOTAT] CHECK CONSTRAINT [FK_TX_NOTAT_SPRAS]
GO
ALTER TABLE [dbo].[TX_NOTAT]  WITH CHECK ADD  CONSTRAINT [FK_TX_NOTAT_TX_TNOTA] FOREIGN KEY([TNOTA_ID])
REFERENCES [dbo].[TX_TNOTA] ([ID])
GO
ALTER TABLE [dbo].[TX_NOTAT] CHECK CONSTRAINT [FK_TX_NOTAT_TX_TNOTA]
GO
ALTER TABLE [dbo].[UMEDIDAT]  WITH NOCHECK ADD  CONSTRAINT [FK_UMEDIDAT_SPRAS] FOREIGN KEY([SPRAS_ID])
REFERENCES [dbo].[SPRAS] ([ID])
GO
ALTER TABLE [dbo].[UMEDIDAT] CHECK CONSTRAINT [FK_UMEDIDAT_SPRAS]
GO
ALTER TABLE [dbo].[UMEDIDAT]  WITH NOCHECK ADD  CONSTRAINT [FK_UMEDIDAT_UMEDIDA] FOREIGN KEY([MSEHI])
REFERENCES [dbo].[UMEDIDA] ([MSEHI])
GO
ALTER TABLE [dbo].[UMEDIDAT] CHECK CONSTRAINT [FK_UMEDIDAT_UMEDIDA]
GO
ALTER TABLE [dbo].[USUARIO]  WITH CHECK ADD  CONSTRAINT [FK_USUARIO_PUESTO] FOREIGN KEY([PUESTO_ID])
REFERENCES [dbo].[PUESTO] ([ID])
GO
ALTER TABLE [dbo].[USUARIO] CHECK CONSTRAINT [FK_USUARIO_PUESTO]
GO
ALTER TABLE [dbo].[USUARIO]  WITH CHECK ADD  CONSTRAINT [FK_USUARIO_SOCIEDAD] FOREIGN KEY([BUNIT])
REFERENCES [dbo].[SOCIEDAD] ([BUKRS])
GO
ALTER TABLE [dbo].[USUARIO] CHECK CONSTRAINT [FK_USUARIO_SOCIEDAD]
GO
ALTER TABLE [dbo].[USUARIO]  WITH CHECK ADD  CONSTRAINT [FK_USUARIO_SPRAS] FOREIGN KEY([SPRAS_ID])
REFERENCES [dbo].[SPRAS] ([ID])
GO
ALTER TABLE [dbo].[USUARIO] CHECK CONSTRAINT [FK_USUARIO_SPRAS]
GO
ALTER TABLE [dbo].[USUARIOF]  WITH CHECK ADD  CONSTRAINT [FK_USUARIOF_CLIENTE] FOREIGN KEY([VKORG], [VTWEG], [SPART], [KUNNR])
REFERENCES [dbo].[CLIENTE] ([VKORG], [VTWEG], [SPART], [KUNNR])
GO
ALTER TABLE [dbo].[USUARIOF] CHECK CONSTRAINT [FK_USUARIOF_CLIENTE]
GO
ALTER TABLE [dbo].[USUARIOF]  WITH CHECK ADD  CONSTRAINT [FK_USUARIOF_USUARIO] FOREIGN KEY([USUARIO_ID])
REFERENCES [dbo].[USUARIO] ([ID])
GO
ALTER TABLE [dbo].[USUARIOF] CHECK CONSTRAINT [FK_USUARIOF_USUARIO]
GO
ALTER TABLE [dbo].[USUARIOGA]  WITH CHECK ADD  CONSTRAINT [FK_USUARIOGA_GAUTORIZACION] FOREIGN KEY([AGROUP_ID])
REFERENCES [dbo].[GAUTORIZACION] ([ID])
GO
ALTER TABLE [dbo].[USUARIOGA] CHECK CONSTRAINT [FK_USUARIOGA_GAUTORIZACION]
GO
ALTER TABLE [dbo].[USUARIOGA]  WITH CHECK ADD  CONSTRAINT [FK_USUARIOGA_USUARIO] FOREIGN KEY([USUARIO_ID])
REFERENCES [dbo].[USUARIO] ([ID])
GO
ALTER TABLE [dbo].[USUARIOGA] CHECK CONSTRAINT [FK_USUARIOGA_USUARIO]
GO
ALTER TABLE [dbo].[USUARIOLOG]  WITH CHECK ADD  CONSTRAINT [FK_USUARIOLOG_USUARIO] FOREIGN KEY([USUARIO_ID])
REFERENCES [dbo].[USUARIO] ([ID])
GO
ALTER TABLE [dbo].[USUARIOLOG] CHECK CONSTRAINT [FK_USUARIOLOG_USUARIO]
GO
ALTER TABLE [dbo].[USUARIOSOC]  WITH CHECK ADD  CONSTRAINT [FK_USUARIOSOC_SOCIEDAD] FOREIGN KEY([SOCIEDAD_ID])
REFERENCES [dbo].[SOCIEDAD] ([BUKRS])
GO
ALTER TABLE [dbo].[USUARIOSOC] CHECK CONSTRAINT [FK_USUARIOSOC_SOCIEDAD]
GO
ALTER TABLE [dbo].[USUARIOSOC]  WITH CHECK ADD  CONSTRAINT [FK_USUARIOSOC_USUARIO] FOREIGN KEY([USUARIO_ID])
REFERENCES [dbo].[USUARIO] ([ID])
GO
ALTER TABLE [dbo].[USUARIOSOC] CHECK CONSTRAINT [FK_USUARIOSOC_USUARIO]
GO
ALTER TABLE [dbo].[WARNING]  WITH CHECK ADD  CONSTRAINT [FK_WARNING_CAMPOS] FOREIGN KEY([PAGINA_ID], [CAMPO_ID])
REFERENCES [dbo].[CAMPOS] ([PAGINA_ID], [ID])
GO
ALTER TABLE [dbo].[WARNING] CHECK CONSTRAINT [FK_WARNING_CAMPOS]
GO
ALTER TABLE [dbo].[WARNING]  WITH CHECK ADD  CONSTRAINT [FK_WARNING_POSICION] FOREIGN KEY([POSICION])
REFERENCES [dbo].[POSICION] ([ID])
GO
ALTER TABLE [dbo].[WARNING] CHECK CONSTRAINT [FK_WARNING_POSICION]
GO
ALTER TABLE [dbo].[WARNING]  WITH CHECK ADD  CONSTRAINT [FK_WARNING_SPRAS] FOREIGN KEY([SPRAS_ID])
REFERENCES [dbo].[SPRAS] ([ID])
GO
ALTER TABLE [dbo].[WARNING] CHECK CONSTRAINT [FK_WARNING_SPRAS]
GO
ALTER TABLE [dbo].[WARNING_COND]  WITH CHECK ADD  CONSTRAINT [FK_WARNING_COND_CONDICION] FOREIGN KEY([CONDICION_ID])
REFERENCES [dbo].[CONDICION] ([ID])
GO
ALTER TABLE [dbo].[WARNING_COND] CHECK CONSTRAINT [FK_WARNING_COND_CONDICION]
GO
ALTER TABLE [dbo].[WARNINGP]  WITH CHECK ADD  CONSTRAINT [FK_WARNINGP_TAB] FOREIGN KEY([TAB_ID])
REFERENCES [dbo].[TAB] ([ID])
GO
ALTER TABLE [dbo].[WARNINGP] CHECK CONSTRAINT [FK_WARNINGP_TAB]
GO
ALTER TABLE [dbo].[WARNINGPT]  WITH CHECK ADD  CONSTRAINT [FK_WARNINGPT_SPRAS] FOREIGN KEY([SPRAS_ID])
REFERENCES [dbo].[SPRAS] ([ID])
GO
ALTER TABLE [dbo].[WARNINGPT] CHECK CONSTRAINT [FK_WARNINGPT_SPRAS]
GO
ALTER TABLE [dbo].[WORKFP]  WITH CHECK ADD  CONSTRAINT [FK_WORKFP_ACCION] FOREIGN KEY([ACCION_ID])
REFERENCES [dbo].[ACCION] ([ID])
GO
ALTER TABLE [dbo].[WORKFP] CHECK CONSTRAINT [FK_WORKFP_ACCION]
GO
ALTER TABLE [dbo].[WORKFP]  WITH CHECK ADD  CONSTRAINT [FK_WORKFP_WORKFV] FOREIGN KEY([ID], [VERSION])
REFERENCES [dbo].[WORKFV] ([ID], [VERSION])
GO
ALTER TABLE [dbo].[WORKFP] CHECK CONSTRAINT [FK_WORKFP_WORKFV]
GO
ALTER TABLE [dbo].[WORKFT]  WITH CHECK ADD  CONSTRAINT [FK_WORKFT_SPRAS] FOREIGN KEY([SPRAS_ID])
REFERENCES [dbo].[SPRAS] ([ID])
GO
ALTER TABLE [dbo].[WORKFT] CHECK CONSTRAINT [FK_WORKFT_SPRAS]
GO
ALTER TABLE [dbo].[WORKFT]  WITH CHECK ADD  CONSTRAINT [FK_WORKFT_WORKFV] FOREIGN KEY([WF_ID], [WF_VERSION])
REFERENCES [dbo].[WORKFV] ([ID], [VERSION])
GO
ALTER TABLE [dbo].[WORKFT] CHECK CONSTRAINT [FK_WORKFT_WORKFV]
GO
ALTER TABLE [dbo].[WORKFV]  WITH CHECK ADD  CONSTRAINT [FK_WORKV_WORKFH] FOREIGN KEY([ID])
REFERENCES [dbo].[WORKFH] ([ID])
GO
ALTER TABLE [dbo].[WORKFV] CHECK CONSTRAINT [FK_WORKV_WORKFH]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[35] 4[28] 2[21] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = -96
         Left = 0
      End
      Begin Tables = 
         Begin Table = "USUARIO"
            Begin Extent = 
               Top = 6
               Left = 93
               Bottom = 135
               Right = 263
            End
            DisplayFlags = 280
            TopColumn = 8
         End
         Begin Table = "PERMISO_PAGINA"
            Begin Extent = 
               Top = 6
               Left = 557
               Bottom = 118
               Right = 727
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PAGINA"
            Begin Extent = 
               Top = 120
               Left = 246
               Bottom = 249
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CARPETA"
            Begin Extent = 
               Top = 120
               Left = 454
               Bottom = 249
               Right = 624
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CARPETAT"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 250
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CARPETAV'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CARPETAV'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CARPETAV'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "u"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "g"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 101
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "d"
            Begin Extent = 
               Top = 6
               Left = 454
               Bottom = 135
               Right = 624
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ga"
            Begin Extent = 
               Top = 6
               Left = 662
               Bottom = 135
               Right = 832
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CREADOR'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CREADOR'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[21] 2[19] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = -96
         Left = 0
      End
      Begin Tables = 
         Begin Table = "u"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "g"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 101
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "d"
            Begin Extent = 
               Top = 6
               Left = 454
               Bottom = 135
               Right = 624
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ga"
            Begin Extent = 
               Top = 6
               Left = 662
               Bottom = 135
               Right = 832
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CREADOR2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CREADOR2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[29] 4[25] 2[21] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "DET_APROB"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PUESTOT"
            Begin Extent = 
               Top = 15
               Left = 543
               Bottom = 127
               Right = 713
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'DET_APROBV'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'DET_APROBV'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[29] 4[34] 2[14] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "f"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 224
            End
            DisplayFlags = 280
            TopColumn = 5
         End
         Begin Table = "d"
            Begin Extent = 
               Top = 6
               Left = 262
               Bottom = 135
               Right = 519
            End
            DisplayFlags = 280
            TopColumn = 3
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 71
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'DOCUMENTOV'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'DOCUMENTOV'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'DOCUMENTOV'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[30] 4[36] 2[19] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "USUARIO"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "PERMISO_PAGINA"
            Begin Extent = 
               Top = 23
               Left = 525
               Bottom = 152
               Right = 695
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PAGINA"
            Begin Extent = 
               Top = 138
               Left = 235
               Bottom = 267
               Right = 405
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CARPETA"
            Begin Extent = 
               Top = 151
               Left = 514
               Bottom = 305
               Right = 684
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "PAGINAT"
            Begin Extent = 
               Top = 147
               Left = 34
               Bottom = 303
               Right = 204
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         O' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PAGINAV'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'r = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PAGINAV'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PAGINAV'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[28] 4[33] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "CAMPOS"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "WARNING"
            Begin Extent = 
               Top = 4
               Left = 312
               Bottom = 133
               Right = 482
            End
            DisplayFlags = 280
            TopColumn = 1
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'WARNINGV'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'WARNINGV'
GO
USE [master]
GO
ALTER DATABASE [TAT004] SET  READ_WRITE 
GO
