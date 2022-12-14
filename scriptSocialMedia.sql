USE [master]
GO
/****** Object:  Database [SocialMedia]    Script Date: 13/11/2022 19:00:36 ******/
CREATE DATABASE [SocialMedia]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SocialMedia', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\SocialMedia.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SocialMedia_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\SocialMedia_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [SocialMedia] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SocialMedia].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SocialMedia] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SocialMedia] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SocialMedia] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SocialMedia] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SocialMedia] SET ARITHABORT OFF 
GO
ALTER DATABASE [SocialMedia] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SocialMedia] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SocialMedia] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SocialMedia] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SocialMedia] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SocialMedia] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SocialMedia] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SocialMedia] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SocialMedia] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SocialMedia] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SocialMedia] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SocialMedia] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SocialMedia] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SocialMedia] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SocialMedia] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SocialMedia] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SocialMedia] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SocialMedia] SET RECOVERY FULL 
GO
ALTER DATABASE [SocialMedia] SET  MULTI_USER 
GO
ALTER DATABASE [SocialMedia] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SocialMedia] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SocialMedia] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SocialMedia] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SocialMedia] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SocialMedia] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'SocialMedia', N'ON'
GO
ALTER DATABASE [SocialMedia] SET QUERY_STORE = OFF
GO
USE [SocialMedia]
GO
/****** Object:  User [matias123]    Script Date: 13/11/2022 19:00:36 ******/
CREATE USER [matias123] FOR LOGIN [matias123] WITH DEFAULT_SCHEMA=[matias123]
GO
/****** Object:  User [mat]    Script Date: 13/11/2022 19:00:36 ******/
CREATE USER [mat] FOR LOGIN [mat] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [matias123]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [matias123]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [matias123]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [matias123]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [matias123]
GO
ALTER ROLE [db_datareader] ADD MEMBER [matias123]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [matias123]
GO
ALTER ROLE [db_denydatareader] ADD MEMBER [matias123]
GO
ALTER ROLE [db_denydatawriter] ADD MEMBER [matias123]
GO
ALTER ROLE [db_owner] ADD MEMBER [mat]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [mat]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [mat]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [mat]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [mat]
GO
ALTER ROLE [db_datareader] ADD MEMBER [mat]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [mat]
GO
ALTER ROLE [db_denydatareader] ADD MEMBER [mat]
GO
ALTER ROLE [db_denydatawriter] ADD MEMBER [mat]
GO
/****** Object:  Schema [matias123]    Script Date: 13/11/2022 19:00:36 ******/
CREATE SCHEMA [matias123]
GO
/****** Object:  Table [dbo].[Comentario]    Script Date: 13/11/2022 19:00:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comentario](
	[IdComentario] [int] NOT NULL,
	[IdPublicacion] [int] NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[Descripcion] [varchar](500) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Comentario] PRIMARY KEY CLUSTERED 
(
	[IdComentario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LlaveApi]    Script Date: 13/11/2022 19:00:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LlaveApi](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Llave] [varchar](50) NOT NULL,
	[TipoLLave] [int] NOT NULL,
	[Activo] [int] NOT NULL,
	[Usuario] [varchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Publicacion]    Script Date: 13/11/2022 19:00:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Publicacion](
	[IdPublicacion] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Descripcion] [varchar](1000) NOT NULL,
	[Imagen] [varchar](500) NULL,
 CONSTRAINT [PK_Publicacion] PRIMARY KEY CLUSTERED 
(
	[IdPublicacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Seguridad]    Script Date: 13/11/2022 19:00:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Seguridad](
	[idSeguridad] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[NombreUsuario] [varchar](100) NOT NULL,
	[Contraseña] [varchar](200) NOT NULL,
	[Rol] [varchar](15) NOT NULL,
 CONSTRAINT [PK__Segurida__217BFD3DB1D16210] PRIMARY KEY CLUSTERED 
(
	[idSeguridad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 13/11/2022 19:00:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombres] [varchar](50) NOT NULL,
	[Apellidos] [varchar](50) NOT NULL,
	[Email] [varchar](30) NOT NULL,
	[FechaNacimiento] [date] NOT NULL,
	[Telefono] [varchar](10) NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Comentario]  WITH NOCHECK ADD  CONSTRAINT [FK_Comentario_Publicacion] FOREIGN KEY([IdPublicacion])
REFERENCES [dbo].[Publicacion] ([IdPublicacion])
GO
ALTER TABLE [dbo].[Comentario] CHECK CONSTRAINT [FK_Comentario_Publicacion]
GO
ALTER TABLE [dbo].[Comentario]  WITH NOCHECK ADD  CONSTRAINT [FK_Comentario_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[Comentario] CHECK CONSTRAINT [FK_Comentario_Usuario]
GO
ALTER TABLE [dbo].[Publicacion]  WITH NOCHECK ADD  CONSTRAINT [FK_Publicacion_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[Publicacion] CHECK CONSTRAINT [FK_Publicacion_Usuario]
GO
/****** Object:  StoredProcedure [dbo].[spPaginacion]    Script Date: 13/11/2022 19:00:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spPaginacion](
@pagina int,
@CantidadReg int)
as

begin
DECLARE @SkipReG int = @pagina*@CantidadReg

SELECT * FROM Publicacion
ORDER BY IdPublicacion
OFFSET @SkipReG ROW
FETCH NEXT @CantidadReg ROWS ONLY

end
GO
USE [master]
GO
ALTER DATABASE [SocialMedia] SET  READ_WRITE 
GO
