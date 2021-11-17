USE [master]
GO
/****** Object:  Database [cadalu]    Script Date: 17/11/2021 18:33:22 ******/
CREATE DATABASE [cadalu]

GO
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [cadalu].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [cadalu] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [cadalu] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [cadalu] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [cadalu] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [cadalu] SET ARITHABORT OFF 
GO
ALTER DATABASE [cadalu] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [cadalu] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [cadalu] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [cadalu] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [cadalu] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [cadalu] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [cadalu] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [cadalu] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [cadalu] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [cadalu] SET  DISABLE_BROKER 
GO
ALTER DATABASE [cadalu] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [cadalu] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [cadalu] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [cadalu] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [cadalu] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [cadalu] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [cadalu] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [cadalu] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [cadalu] SET  MULTI_USER 
GO
ALTER DATABASE [cadalu] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [cadalu] SET DB_CHAINING OFF 
GO
ALTER DATABASE [cadalu] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [cadalu] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [cadalu] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [cadalu] SET QUERY_STORE = OFF
GO
USE [cadalu]
GO
/****** Object:  Table [dbo].[agrupamentos]    Script Date: 17/11/2021 18:33:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[agrupamentos](
	[id] [smallint] NOT NULL,
	[nome] [varchar](50) NOT NULL,
 CONSTRAINT [PK_agrupamentos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[alunos]    Script Date: 17/11/2021 18:33:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[alunos](
	[id] [int] NOT NULL,
	[nome] [varchar](max) NOT NULL,
	[turma] [int] NOT NULL,
	[pai1] [int] NOT NULL,
	[pai2] [int] NULL,
 CONSTRAINT [PK_alunos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[avaliacoes]    Script Date: 17/11/2021 18:33:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[avaliacoes](
	[id] [int] NOT NULL,
	[aluno] [int] NOT NULL,
	[avaliador] [int] NOT NULL,
	[avaliacao] [varchar](50) NOT NULL,
	[tipo] [varchar](max) NOT NULL,
 CONSTRAINT [PK_avaliacoes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[disciplinas]    Script Date: 17/11/2021 18:33:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[disciplinas](
	[turma] [int] NOT NULL,
	[professor] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[turma] ASC,
	[professor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[escolas]    Script Date: 17/11/2021 18:33:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[escolas](
	[id] [int] NOT NULL,
	[nome] [varchar](250) NOT NULL,
	[agrup] [smallint] NOT NULL,
 CONSTRAINT [PK_escolas] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mensagens]    Script Date: 17/11/2021 18:33:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mensagens](
	[id] [int] NOT NULL,
	[aluno] [int] NOT NULL,
	[mensagem] [varchar](max) NOT NULL,
	[professor] [int] NOT NULL,
 CONSTRAINT [PK_mensagens] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[pais]    Script Date: 17/11/2021 18:33:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pais](
	[id] [int] NOT NULL,
	[nome] [varchar](max) NOT NULL,
	[email] [varchar](max) NOT NULL,
	[telefone] [int] NOT NULL,
	[password] [varchar](max) NOT NULL,
 CONSTRAINT [PK_pais] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[professores]    Script Date: 17/11/2021 18:33:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[professores](
	[id] [int] NOT NULL,
	[nome] [varchar](max) NOT NULL,
	[email] [varchar](max) NOT NULL,
	[telefone] [int] NOT NULL,
	[password] [varchar](max) NOT NULL,
	[escola] [int] NOT NULL,
	[disciplina] [varchar](50) NOT NULL,
 CONSTRAINT [PK_professores] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sumario]    Script Date: 17/11/2021 18:33:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sumario](
	[id] [int] NOT NULL,
	[professor] [int] NOT NULL,
	[turma] [int] NOT NULL,
	[sumario] [varchar](250) NOT NULL,
 CONSTRAINT [PK_sumario] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[turmas]    Script Date: 17/11/2021 18:33:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[turmas](
	[id] [int] NOT NULL,
	[nome] [varchar](50) NOT NULL,
	[escola] [int] NOT NULL,
 CONSTRAINT [PK_turmas] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[alunos]  WITH CHECK ADD  CONSTRAINT [FK_alunos_pais] FOREIGN KEY([pai1])
REFERENCES [dbo].[pais] ([id])
GO
ALTER TABLE [dbo].[alunos] CHECK CONSTRAINT [FK_alunos_pais]
GO
ALTER TABLE [dbo].[alunos]  WITH CHECK ADD  CONSTRAINT [FK_alunos_pais1] FOREIGN KEY([pai2])
REFERENCES [dbo].[pais] ([id])
GO
ALTER TABLE [dbo].[alunos] CHECK CONSTRAINT [FK_alunos_pais1]
GO
ALTER TABLE [dbo].[alunos]  WITH CHECK ADD  CONSTRAINT [FK_alunos_turmas] FOREIGN KEY([turma])
REFERENCES [dbo].[turmas] ([id])
GO
ALTER TABLE [dbo].[alunos] CHECK CONSTRAINT [FK_alunos_turmas]
GO
ALTER TABLE [dbo].[avaliacoes]  WITH CHECK ADD  CONSTRAINT [FK_avaliacoes_alunos] FOREIGN KEY([aluno])
REFERENCES [dbo].[alunos] ([id])
GO
ALTER TABLE [dbo].[avaliacoes] CHECK CONSTRAINT [FK_avaliacoes_alunos]
GO
ALTER TABLE [dbo].[avaliacoes]  WITH CHECK ADD  CONSTRAINT [FK_avaliacoes_professores] FOREIGN KEY([avaliador])
REFERENCES [dbo].[professores] ([id])
GO
ALTER TABLE [dbo].[avaliacoes] CHECK CONSTRAINT [FK_avaliacoes_professores]
GO
ALTER TABLE [dbo].[disciplinas]  WITH CHECK ADD  CONSTRAINT [FK_disciplinas_professores] FOREIGN KEY([professor])
REFERENCES [dbo].[professores] ([id])
GO
ALTER TABLE [dbo].[disciplinas] CHECK CONSTRAINT [FK_disciplinas_professores]
GO
ALTER TABLE [dbo].[disciplinas]  WITH CHECK ADD  CONSTRAINT [FK_disciplinas_turmas] FOREIGN KEY([turma])
REFERENCES [dbo].[turmas] ([id])
GO
ALTER TABLE [dbo].[disciplinas] CHECK CONSTRAINT [FK_disciplinas_turmas]
GO
ALTER TABLE [dbo].[escolas]  WITH CHECK ADD  CONSTRAINT [FK_escolas_agrupamentos] FOREIGN KEY([agrup])
REFERENCES [dbo].[agrupamentos] ([id])
GO
ALTER TABLE [dbo].[escolas] CHECK CONSTRAINT [FK_escolas_agrupamentos]
GO
ALTER TABLE [dbo].[mensagens]  WITH CHECK ADD  CONSTRAINT [FK_mensagens_alunos] FOREIGN KEY([aluno])
REFERENCES [dbo].[alunos] ([id])
GO
ALTER TABLE [dbo].[mensagens] CHECK CONSTRAINT [FK_mensagens_alunos]
GO
ALTER TABLE [dbo].[mensagens]  WITH CHECK ADD  CONSTRAINT [FK_mensagens_professores] FOREIGN KEY([professor])
REFERENCES [dbo].[professores] ([id])
GO
ALTER TABLE [dbo].[mensagens] CHECK CONSTRAINT [FK_mensagens_professores]
GO
ALTER TABLE [dbo].[professores]  WITH CHECK ADD  CONSTRAINT [FK_professores_escolas] FOREIGN KEY([escola])
REFERENCES [dbo].[escolas] ([id])
GO
ALTER TABLE [dbo].[professores] CHECK CONSTRAINT [FK_professores_escolas]
GO
ALTER TABLE [dbo].[sumario]  WITH CHECK ADD  CONSTRAINT [FK_sumario_professores] FOREIGN KEY([professor])
REFERENCES [dbo].[professores] ([id])
GO
ALTER TABLE [dbo].[sumario] CHECK CONSTRAINT [FK_sumario_professores]
GO
ALTER TABLE [dbo].[sumario]  WITH CHECK ADD  CONSTRAINT [FK_sumario_turmas] FOREIGN KEY([turma])
REFERENCES [dbo].[turmas] ([id])
GO
ALTER TABLE [dbo].[sumario] CHECK CONSTRAINT [FK_sumario_turmas]
GO
ALTER TABLE [dbo].[turmas]  WITH CHECK ADD  CONSTRAINT [FK_turmas_escolas] FOREIGN KEY([escola])
REFERENCES [dbo].[escolas] ([id])
GO
ALTER TABLE [dbo].[turmas] CHECK CONSTRAINT [FK_turmas_escolas]
GO
USE [master]
GO
ALTER DATABASE [cadalu] SET  READ_WRITE 
GO
