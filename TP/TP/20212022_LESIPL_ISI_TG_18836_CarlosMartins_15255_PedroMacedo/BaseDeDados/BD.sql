USE [master]
GO
/****** Object:  Database [TP_ISI]    Script Date: 30/11/2021 23:21:58 ******/
CREATE DATABASE [TP_ISI]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TP_ISI', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\TP_ISI.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TP_ISI_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\TP_ISI_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [TP_ISI] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TP_ISI].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TP_ISI] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TP_ISI] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TP_ISI] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TP_ISI] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TP_ISI] SET ARITHABORT OFF 
GO
ALTER DATABASE [TP_ISI] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TP_ISI] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TP_ISI] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TP_ISI] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TP_ISI] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TP_ISI] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TP_ISI] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TP_ISI] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TP_ISI] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TP_ISI] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TP_ISI] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TP_ISI] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TP_ISI] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TP_ISI] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TP_ISI] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TP_ISI] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TP_ISI] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TP_ISI] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TP_ISI] SET  MULTI_USER 
GO
ALTER DATABASE [TP_ISI] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TP_ISI] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TP_ISI] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TP_ISI] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TP_ISI] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TP_ISI] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [TP_ISI] SET QUERY_STORE = OFF
GO
USE [TP_ISI]
GO
/****** Object:  Table [dbo].[Encomenda]    Script Date: 30/11/2021 23:21:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Encomenda](
	[ID_Encomenda] [int] IDENTITY(1,1) NOT NULL,
	[ID_EquipaFK] [int] NOT NULL,
	[Data] [date] NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_Encomenda] PRIMARY KEY CLUSTERED 
(
	[ID_Encomenda] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EncomendaProduto]    Script Date: 30/11/2021 23:21:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EncomendaProduto](
	[ID_EncomendaProduto] [int] IDENTITY(1,1) NOT NULL,
	[ID_ProdutoFK] [int] NOT NULL,
	[ID_EncomendaFK] [int] NOT NULL,
	[Quantidade] [int] NOT NULL,
 CONSTRAINT [PK_EncomendaProduto] PRIMARY KEY CLUSTERED 
(
	[ID_EncomendaProduto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Equipa]    Script Date: 30/11/2021 23:21:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Equipa](
	[ID_Equipa] [int] NOT NULL,
	[Nome] [nchar](100) NOT NULL,
 CONSTRAINT [PK_Equipas] PRIMARY KEY CLUSTERED 
(
	[ID_Equipa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Funcionario]    Script Date: 30/11/2021 23:21:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Funcionario](
	[ID_Func] [int] NOT NULL,
	[Nome] [nchar](100) NOT NULL,
	[NIF] [nchar](10) NOT NULL,
	[ID_EquipaFK] [int] NOT NULL,
 CONSTRAINT [PK_Funcionarios] PRIMARY KEY CLUSTERED 
(
	[ID_Func] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pessoa]    Script Date: 30/11/2021 23:21:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pessoa](
	[ID_Pessoa] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nchar](100) NOT NULL,
	[DataNascimento] [date] NOT NULL,
	[Contacto] [nchar](10) NOT NULL,
	[DataTeste] [date] NULL,
	[Testado] [bit] NOT NULL,
	[Infetado] [bit] NOT NULL,
	[Isolamento] [bit] NOT NULL,
	[NIF] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Idosos_1] PRIMARY KEY CLUSTERED 
(
	[ID_Pessoa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Produto]    Script Date: 30/11/2021 23:21:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Produto](
	[ID_Produto] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nchar](100) NOT NULL,
	[Preco] [float] NOT NULL,
	[SKU] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Produto] PRIMARY KEY CLUSTERED 
(
	[ID_Produto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Requisicao]    Script Date: 30/11/2021 23:21:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Requisicao](
	[ID_Requisicao] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Visita]    Script Date: 30/11/2021 23:21:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Visita](
	[ID_Visita] [int] IDENTITY(1,1) NOT NULL,
	[Data] [datetime] NOT NULL,
	[ID_PessoaFK] [int] NOT NULL,
	[Infetado] [int] NOT NULL,
	[ID_EquipaFK] [int] NOT NULL,
 CONSTRAINT [PK_Intervencao] PRIMARY KEY CLUSTERED 
(
	[ID_Visita] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Encomenda] ON 

INSERT [dbo].[Encomenda] ([ID_Encomenda], [ID_EquipaFK], [Data], [Estado]) VALUES (19, 2, CAST(N'2021-11-28' AS Date), 0)
INSERT [dbo].[Encomenda] ([ID_Encomenda], [ID_EquipaFK], [Data], [Estado]) VALUES (20, 1, CAST(N'2021-11-28' AS Date), 0)
INSERT [dbo].[Encomenda] ([ID_Encomenda], [ID_EquipaFK], [Data], [Estado]) VALUES (21, 1, CAST(N'2021-11-29' AS Date), 0)
INSERT [dbo].[Encomenda] ([ID_Encomenda], [ID_EquipaFK], [Data], [Estado]) VALUES (22, 2, CAST(N'2021-11-29' AS Date), 0)
SET IDENTITY_INSERT [dbo].[Encomenda] OFF
GO
SET IDENTITY_INSERT [dbo].[EncomendaProduto] ON 

INSERT [dbo].[EncomendaProduto] ([ID_EncomendaProduto], [ID_ProdutoFK], [ID_EncomendaFK], [Quantidade]) VALUES (14, 1015, 19, 4)
INSERT [dbo].[EncomendaProduto] ([ID_EncomendaProduto], [ID_ProdutoFK], [ID_EncomendaFK], [Quantidade]) VALUES (15, 1017, 19, 20)
INSERT [dbo].[EncomendaProduto] ([ID_EncomendaProduto], [ID_ProdutoFK], [ID_EncomendaFK], [Quantidade]) VALUES (16, 1016, 19, 10)
INSERT [dbo].[EncomendaProduto] ([ID_EncomendaProduto], [ID_ProdutoFK], [ID_EncomendaFK], [Quantidade]) VALUES (17, 1018, 19, 50)
INSERT [dbo].[EncomendaProduto] ([ID_EncomendaProduto], [ID_ProdutoFK], [ID_EncomendaFK], [Quantidade]) VALUES (18, 1020, 19, 1)
INSERT [dbo].[EncomendaProduto] ([ID_EncomendaProduto], [ID_ProdutoFK], [ID_EncomendaFK], [Quantidade]) VALUES (19, 1019, 20, 5)
INSERT [dbo].[EncomendaProduto] ([ID_EncomendaProduto], [ID_ProdutoFK], [ID_EncomendaFK], [Quantidade]) VALUES (20, 1021, 20, 7)
SET IDENTITY_INSERT [dbo].[EncomendaProduto] OFF
GO
INSERT [dbo].[Equipa] ([ID_Equipa], [Nome]) VALUES (1, N'PSP                                                                                                 ')
INSERT [dbo].[Equipa] ([ID_Equipa], [Nome]) VALUES (2, N'GNR                                                                                                 ')
GO
INSERT [dbo].[Funcionario] ([ID_Func], [Nome], [NIF], [ID_EquipaFK]) VALUES (1, N'Carlos                                                                                              ', N'100000000 ', 1)
INSERT [dbo].[Funcionario] ([ID_Func], [Nome], [NIF], [ID_EquipaFK]) VALUES (2, N'Bruno                                                                                               ', N'100000001 ', 1)
INSERT [dbo].[Funcionario] ([ID_Func], [Nome], [NIF], [ID_EquipaFK]) VALUES (3, N'Ze                                                                                                  ', N'100000002 ', 2)
INSERT [dbo].[Funcionario] ([ID_Func], [Nome], [NIF], [ID_EquipaFK]) VALUES (4, N'Manuel                                                                                              ', N'100000003 ', 2)
GO
SET IDENTITY_INSERT [dbo].[Pessoa] ON 

INSERT [dbo].[Pessoa] ([ID_Pessoa], [Nome], [DataNascimento], [Contacto], [DataTeste], [Testado], [Infetado], [Isolamento], [NIF]) VALUES (1, N'Manel                                                                                               ', CAST(N'1940-01-01' AS Date), N'910000001 ', CAST(N'2021-10-18' AS Date), 1, 1, 1, N'810000001 ')
INSERT [dbo].[Pessoa] ([ID_Pessoa], [Nome], [DataNascimento], [Contacto], [DataTeste], [Testado], [Infetado], [Isolamento], [NIF]) VALUES (2, N'Manuela                                                                                             ', CAST(N'1943-02-02' AS Date), N'910000002 ', CAST(N'2021-10-18' AS Date), 1, 0, 0, N'810000002 ')
INSERT [dbo].[Pessoa] ([ID_Pessoa], [Nome], [DataNascimento], [Contacto], [DataTeste], [Testado], [Infetado], [Isolamento], [NIF]) VALUES (3, N'Tone                                                                                                ', CAST(N'1950-03-03' AS Date), N'910000003 ', CAST(N'2021-10-18' AS Date), 0, 0, 0, N'810000003 ')
INSERT [dbo].[Pessoa] ([ID_Pessoa], [Nome], [DataNascimento], [Contacto], [DataTeste], [Testado], [Infetado], [Isolamento], [NIF]) VALUES (4, N'Joaquim                                                                                             ', CAST(N'1955-04-23' AS Date), N'910000004 ', CAST(N'2021-09-11' AS Date), 1, 0, 0, N'810000004 ')
INSERT [dbo].[Pessoa] ([ID_Pessoa], [Nome], [DataNascimento], [Contacto], [DataTeste], [Testado], [Infetado], [Isolamento], [NIF]) VALUES (5, N'Manuelinha                                                                                          ', CAST(N'1945-06-05' AS Date), N'910000005 ', CAST(N'2021-07-21' AS Date), 1, 1, 1, N'810000005 ')
INSERT [dbo].[Pessoa] ([ID_Pessoa], [Nome], [DataNascimento], [Contacto], [DataTeste], [Testado], [Infetado], [Isolamento], [NIF]) VALUES (6, N'Patricio                                                                                            ', CAST(N'1951-02-07' AS Date), N'910000006 ', NULL, 1, 0, 0, N'810000006 ')
INSERT [dbo].[Pessoa] ([ID_Pessoa], [Nome], [DataNascimento], [Contacto], [DataTeste], [Testado], [Infetado], [Isolamento], [NIF]) VALUES (7, N'Patricia                                                                                            ', CAST(N'1959-08-08' AS Date), N'910000007 ', NULL, 0, 0, 0, N'810000007 ')
INSERT [dbo].[Pessoa] ([ID_Pessoa], [Nome], [DataNascimento], [Contacto], [DataTeste], [Testado], [Infetado], [Isolamento], [NIF]) VALUES (8, N'Ana                                                                                                 ', CAST(N'1960-07-07' AS Date), N'910000008 ', NULL, 0, 0, 0, N'810000008 ')
INSERT [dbo].[Pessoa] ([ID_Pessoa], [Nome], [DataNascimento], [Contacto], [DataTeste], [Testado], [Infetado], [Isolamento], [NIF]) VALUES (9, N'Ano                                                                                                 ', CAST(N'1961-09-03' AS Date), N'910000009 ', NULL, 0, 0, 0, N'810000009 ')
INSERT [dbo].[Pessoa] ([ID_Pessoa], [Nome], [DataNascimento], [Contacto], [DataTeste], [Testado], [Infetado], [Isolamento], [NIF]) VALUES (10, N'Pedro                                                                                               ', CAST(N'1940-08-31' AS Date), N'910000010 ', CAST(N'2021-11-01' AS Date), 1, 1, 1, N'810000010 ')
INSERT [dbo].[Pessoa] ([ID_Pessoa], [Nome], [DataNascimento], [Contacto], [DataTeste], [Testado], [Infetado], [Isolamento], [NIF]) VALUES (11, N'Pedra                                                                                               ', CAST(N'1942-03-31' AS Date), N'910000011 ', CAST(N'2021-11-01' AS Date), 1, 1, 1, N'810000011 ')
INSERT [dbo].[Pessoa] ([ID_Pessoa], [Nome], [DataNascimento], [Contacto], [DataTeste], [Testado], [Infetado], [Isolamento], [NIF]) VALUES (12, N'Antonieta                                                                                           ', CAST(N'1947-02-12' AS Date), N'910000012 ', CAST(N'2021-11-01' AS Date), 1, 1, 1, N'810000012 ')
INSERT [dbo].[Pessoa] ([ID_Pessoa], [Nome], [DataNascimento], [Contacto], [DataTeste], [Testado], [Infetado], [Isolamento], [NIF]) VALUES (13, N'Carlos                                                                                              ', CAST(N'2000-09-11' AS Date), N'910000013 ', NULL, 0, 0, 0, N'810000013 ')
INSERT [dbo].[Pessoa] ([ID_Pessoa], [Nome], [DataNascimento], [Contacto], [DataTeste], [Testado], [Infetado], [Isolamento], [NIF]) VALUES (14, N'Raquel                                                                                              ', CAST(N'2000-06-26' AS Date), N'910000014 ', NULL, 0, 0, 0, N'810000014 ')
SET IDENTITY_INSERT [dbo].[Pessoa] OFF
GO
SET IDENTITY_INSERT [dbo].[Produto] ON 

INSERT [dbo].[Produto] ([ID_Produto], [Nome], [Preco], [SKU]) VALUES (1015, N'Desinfetante                                                                                        ', 1.49, N'SKU9438   ')
INSERT [dbo].[Produto] ([ID_Produto], [Nome], [Preco], [SKU]) VALUES (1016, N'Luvas                                                                                               ', 2.1, N'SKU7302   ')
INSERT [dbo].[Produto] ([ID_Produto], [Nome], [Preco], [SKU]) VALUES (1017, N'Agua                                                                                                ', 0.1, N'SKU6096   ')
INSERT [dbo].[Produto] ([ID_Produto], [Nome], [Preco], [SKU]) VALUES (1018, N'Mascaras                                                                                            ', 1.49, N'SKU2992   ')
INSERT [dbo].[Produto] ([ID_Produto], [Nome], [Preco], [SKU]) VALUES (1019, N'Batas                                                                                               ', 5, N'SKU4914   ')
INSERT [dbo].[Produto] ([ID_Produto], [Nome], [Preco], [SKU]) VALUES (1020, N'Aspirador Portatil                                                                                  ', 15, N'SKU6201   ')
INSERT [dbo].[Produto] ([ID_Produto], [Nome], [Preco], [SKU]) VALUES (1021, N'Toalhitas anti-covid                                                                                ', 6, N'SKU8641   ')
SET IDENTITY_INSERT [dbo].[Produto] OFF
GO
SET IDENTITY_INSERT [dbo].[Visita] ON 

INSERT [dbo].[Visita] ([ID_Visita], [Data], [ID_PessoaFK], [Infetado], [ID_EquipaFK]) VALUES (5, CAST(N'2021-03-18T00:00:00.000' AS DateTime), 1, 1, 1)
INSERT [dbo].[Visita] ([ID_Visita], [Data], [ID_PessoaFK], [Infetado], [ID_EquipaFK]) VALUES (6, CAST(N'2021-11-01T00:00:00.000' AS DateTime), 2, 0, 1)
INSERT [dbo].[Visita] ([ID_Visita], [Data], [ID_PessoaFK], [Infetado], [ID_EquipaFK]) VALUES (7, CAST(N'2021-09-11T00:00:00.000' AS DateTime), 5, 1, 2)
INSERT [dbo].[Visita] ([ID_Visita], [Data], [ID_PessoaFK], [Infetado], [ID_EquipaFK]) VALUES (8, CAST(N'2021-07-21T00:00:00.000' AS DateTime), 6, 0, 2)
INSERT [dbo].[Visita] ([ID_Visita], [Data], [ID_PessoaFK], [Infetado], [ID_EquipaFK]) VALUES (1005, CAST(N'2021-10-18T00:00:00.000' AS DateTime), 1, 1, 1)
INSERT [dbo].[Visita] ([ID_Visita], [Data], [ID_PessoaFK], [Infetado], [ID_EquipaFK]) VALUES (1006, CAST(N'2020-11-14T00:00:00.000' AS DateTime), 2, 0, 1)
INSERT [dbo].[Visita] ([ID_Visita], [Data], [ID_PessoaFK], [Infetado], [ID_EquipaFK]) VALUES (1007, CAST(N'2021-09-11T00:00:00.000' AS DateTime), 5, 1, 2)
INSERT [dbo].[Visita] ([ID_Visita], [Data], [ID_PessoaFK], [Infetado], [ID_EquipaFK]) VALUES (1008, CAST(N'2021-07-21T00:00:00.000' AS DateTime), 6, 0, 2)
INSERT [dbo].[Visita] ([ID_Visita], [Data], [ID_PessoaFK], [Infetado], [ID_EquipaFK]) VALUES (1009, CAST(N'2021-10-18T00:00:00.000' AS DateTime), 1, 1, 1)
INSERT [dbo].[Visita] ([ID_Visita], [Data], [ID_PessoaFK], [Infetado], [ID_EquipaFK]) VALUES (1010, CAST(N'2021-03-22T00:00:00.000' AS DateTime), 2, 0, 1)
INSERT [dbo].[Visita] ([ID_Visita], [Data], [ID_PessoaFK], [Infetado], [ID_EquipaFK]) VALUES (1011, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 1, 1, 1)
INSERT [dbo].[Visita] ([ID_Visita], [Data], [ID_PessoaFK], [Infetado], [ID_EquipaFK]) VALUES (1012, CAST(N'2021-06-21T00:00:00.000' AS DateTime), 2, 0, 1)
INSERT [dbo].[Visita] ([ID_Visita], [Data], [ID_PessoaFK], [Infetado], [ID_EquipaFK]) VALUES (1013, CAST(N'2021-10-18T00:00:00.000' AS DateTime), 1, 1, 1)
INSERT [dbo].[Visita] ([ID_Visita], [Data], [ID_PessoaFK], [Infetado], [ID_EquipaFK]) VALUES (1014, CAST(N'2021-11-01T00:00:00.000' AS DateTime), 2, 0, 1)
SET IDENTITY_INSERT [dbo].[Visita] OFF
GO
ALTER TABLE [dbo].[Encomenda]  WITH CHECK ADD  CONSTRAINT [FK_Encomenda_Equipa] FOREIGN KEY([ID_EquipaFK])
REFERENCES [dbo].[Equipa] ([ID_Equipa])
GO
ALTER TABLE [dbo].[Encomenda] CHECK CONSTRAINT [FK_Encomenda_Equipa]
GO
ALTER TABLE [dbo].[EncomendaProduto]  WITH CHECK ADD  CONSTRAINT [FK_EncomendaProduto_Encomenda] FOREIGN KEY([ID_EncomendaFK])
REFERENCES [dbo].[Encomenda] ([ID_Encomenda])
GO
ALTER TABLE [dbo].[EncomendaProduto] CHECK CONSTRAINT [FK_EncomendaProduto_Encomenda]
GO
ALTER TABLE [dbo].[EncomendaProduto]  WITH CHECK ADD  CONSTRAINT [FK_EncomendaProduto_Produto] FOREIGN KEY([ID_ProdutoFK])
REFERENCES [dbo].[Produto] ([ID_Produto])
GO
ALTER TABLE [dbo].[EncomendaProduto] CHECK CONSTRAINT [FK_EncomendaProduto_Produto]
GO
ALTER TABLE [dbo].[Funcionario]  WITH CHECK ADD  CONSTRAINT [FK_Funcionario_Equipa] FOREIGN KEY([ID_EquipaFK])
REFERENCES [dbo].[Equipa] ([ID_Equipa])
GO
ALTER TABLE [dbo].[Funcionario] CHECK CONSTRAINT [FK_Funcionario_Equipa]
GO
ALTER TABLE [dbo].[Visita]  WITH CHECK ADD  CONSTRAINT [FK_Visita_Equipa] FOREIGN KEY([ID_EquipaFK])
REFERENCES [dbo].[Equipa] ([ID_Equipa])
GO
ALTER TABLE [dbo].[Visita] CHECK CONSTRAINT [FK_Visita_Equipa]
GO
ALTER TABLE [dbo].[Visita]  WITH CHECK ADD  CONSTRAINT [FK_Visita_Pessoa] FOREIGN KEY([ID_PessoaFK])
REFERENCES [dbo].[Pessoa] ([ID_Pessoa])
GO
ALTER TABLE [dbo].[Visita] CHECK CONSTRAINT [FK_Visita_Pessoa]
GO
USE [master]
GO
ALTER DATABASE [TP_ISI] SET  READ_WRITE 
GO
