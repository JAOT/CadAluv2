-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Tempo de geração: 22-Fev-2022 às 19:57
-- Versão do servidor: 10.4.22-MariaDB
-- versão do PHP: 8.1.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Banco de dados: `cadalu`
--

-- --------------------------------------------------------

--
-- Estrutura da tabela `agrupamentos`
--

CREATE TABLE `agrupamentos` (
  `identidade` int(11) NOT NULL,
  `nome` varchar(50) NOT NULL,
  `Morada` varchar(100) NOT NULL,
  `Telefone` bigint(20) NOT NULL,
  `mapa` varchar(1000) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Extraindo dados da tabela `agrupamentos`
--

INSERT INTO `agrupamentos` (`identidade`, `nome`, `Morada`, `Telefone`, `mapa`) VALUES
(1, 'Agrupamento de Escolas Almeida Garrett', 'Rua Almeida Garrett', 224564744, 'https://www.google.com/maps/place/Escola+Secund%C3%A1ria+Ant%C3%B3nio+S%C3%A9rgio/@41.1221528,-8.6132264,242m/data=!3m1!1e3!4m5!3m4!1s0xd2464d451a09af9:0x5b4479f6fb3e3945!8m2!3d41.1221582!4d-8.612957'),
(2, 'Agrupamento de Escolas Carolina Michaelis', 'Rua Carolina Michaelis', 224568855, 'https://www.google.com/maps/place/Escola+Secund%C3%A1ria+Carolina+Micha%C3%ABlis/@41.1590312,-8.6237263,17z/data=!4m9!1m2!2m1!1scarolina+michaelis!3m5!1s0xd2464e2182e9551:0x81af00f15a018cc1!8m2!3d41.1594694!4d-8.6220052!15sChJjYXJvbGluYSBtaWNoYWVsaXOSAQtoaWdoX3NjaG9vbA');

-- --------------------------------------------------------

--
-- Estrutura da tabela `alunos`
--

CREATE TABLE `alunos` (
  `identidade` int(11) NOT NULL,
  `nome` varchar(50) NOT NULL DEFAULT '0',
  `turma` int(11) NOT NULL,
  `pai1` int(11) NOT NULL,
  `pai2` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Extraindo dados da tabela `alunos`
--

INSERT INTO `alunos` (`identidade`, `nome`, `turma`, `pai1`, `pai2`) VALUES
(1, 'Guido', 1, 1, 2),
(2, 'Antonieta', 2, 3, 0),
(3, 'Josué', 3, 2, 1);

-- --------------------------------------------------------

--
-- Estrutura da tabela `avaliacoes`
--

CREATE TABLE `avaliacoes` (
  `identidade` int(11) NOT NULL,
  `aval` varchar(50) NOT NULL,
  `tipo` varchar(250) NOT NULL DEFAULT '0',
  `aluno` int(11) NOT NULL DEFAULT 0,
  `avaliador` int(11) NOT NULL DEFAULT 0,
  `disciplina` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Extraindo dados da tabela `avaliacoes`
--

INSERT INTO `avaliacoes` (`identidade`, `aval`, `tipo`, `aluno`, `avaliador`, `disciplina`) VALUES
(1, 'Aprovado - Excelente', '1º Período', 1, 2, 1),
(2, 'Muito Bom', 'Teste', 3, 1, 1),
(3, '3', '1º Período', 3, 1, 2),
(4, 'Bom', 'Teste', 2, 1, 1),
(5, '4', '1º Período', 2, 1, 1);

-- --------------------------------------------------------

--
-- Estrutura da tabela `disciplinas`
--

CREATE TABLE `disciplinas` (
  `id` int(11) NOT NULL,
  `turma` int(11) NOT NULL,
  `professor` int(11) NOT NULL,
  `Nome` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Extraindo dados da tabela `disciplinas`
--

INSERT INTO `disciplinas` (`id`, `turma`, `professor`, `Nome`) VALUES
(1, 1, 2, 'Português'),
(2, 2, 1, 'Matemática'),
(3, 3, 1, 'Matemática'),
(4, 1, 1, 'Matemática');

-- --------------------------------------------------------

--
-- Estrutura da tabela `escolas`
--

CREATE TABLE `escolas` (
  `identidade` int(11) NOT NULL,
  `nome` varchar(50) NOT NULL DEFAULT 'Escola',
  `agrup` int(11) NOT NULL,
  `Morada` varchar(100) NOT NULL,
  `Telefone` bigint(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Extraindo dados da tabela `escolas`
--

INSERT INTO `escolas` (`identidade`, `nome`, `agrup`, `Morada`, `Telefone`) VALUES
(1, 'Escola das Pedras', 1, 'Rua das Pedras', 223125246),
(2, 'Carolina Michaelis', 2, 'Rua da Cruz Quebrada', 221258547);

-- --------------------------------------------------------

--
-- Estrutura da tabela `mensagens`
--

CREATE TABLE `mensagens` (
  `identidade` int(11) NOT NULL,
  `aluno` int(11) NOT NULL,
  `tema` varchar(50) NOT NULL,
  `texto` text NOT NULL DEFAULT '\'Sem texto...\'',
  `professor` int(11) DEFAULT NULL,
  `datahora` datetime NOT NULL DEFAULT current_timestamp(),
  `lida` int(11) NOT NULL DEFAULT 0,
  `pai` int(11) DEFAULT NULL,
  `documento` varchar(1000) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Extraindo dados da tabela `mensagens`
--

INSERT INTO `mensagens` (`identidade`, `aluno`, `tema`, `texto`, `professor`, `datahora`, `lida`, `pai`, `documento`) VALUES
(57, 1, 'Falta', 'Derivado a confinamento', 1, '2022-02-21 11:30:18', 0, 0, ''),
(58, 1, 'RE: Falta', 'A mensagem foi recebida e será processada assim que possível', 1, '2022-02-21 11:30:18', 1, 1, ''),
(59, 1, 'Sou enc', 'Sou enc\n', 2, '2022-02-21 11:41:51', 1, 1, ''),
(60, 1, 'As notas foram lançadas', 'Podem encontrá-las no link abaixo', 2, '2022-02-21 11:30:18', 0, 0, 'https://bit.ly/3BEGHDZ'),
(63, 1, 'Re: As notas foram lançadas', 'Discordo', 1, '2022-02-22 12:30:46', 1, 1, ''),
(64, 1, 'Aulas', 'Adoro!', 2, '2022-02-22 13:12:25', 1, 1, ''),
(65, 1, 'Crime', 'Dissertation EU!', 2, '2022-02-22 13:16:06', 1, 1, ''),
(66, 3, 'Mensagem', 'Mensagem para enviar', 1, '2022-02-22 13:57:27', 1, 1, '');

-- --------------------------------------------------------

--
-- Estrutura da tabela `pais`
--

CREATE TABLE `pais` (
  `identidade` int(11) NOT NULL,
  `nome` varchar(50) NOT NULL,
  `email` varchar(50) NOT NULL,
  `telefone` int(9) NOT NULL DEFAULT 13456789,
  `password` varchar(255) NOT NULL DEFAULT '123456',
  `pin` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Extraindo dados da tabela `pais`
--

INSERT INTO `pais` (`identidade`, `nome`, `email`, `telefone`, `password`, `pin`) VALUES
(0, 'Default', 'dummy@dummy.com', 0, '000000000', 0),
(1, 'Vitor Castro', 'v.castro@kamil.com', 912365987, '123456', NULL),
(2, 'Amelia Sousa', 'asousa@kamil.com', 923456123, '123456', NULL),
(3, 'Josefina Teixeira', 'j.teixeira@kamil.com', 921654987, '123456', NULL),
(4, 'Test User', 'tu@email.com', 939830088, '123456', 1221);

-- --------------------------------------------------------

--
-- Estrutura da tabela `professores`
--

CREATE TABLE `professores` (
  `identidade` int(11) NOT NULL,
  `nome` varchar(50) NOT NULL,
  `email` varchar(50) NOT NULL,
  `telefone` int(9) NOT NULL DEFAULT 123456789,
  `disciplina` varchar(50) NOT NULL DEFAULT 'Geral',
  `password` varchar(50) NOT NULL DEFAULT '123456',
  `escola` int(11) NOT NULL,
  `turma` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Extraindo dados da tabela `professores`
--

INSERT INTO `professores` (`identidade`, `nome`, `email`, `telefone`, `disciplina`, `password`, `escola`, `turma`) VALUES
(1, 'José Manel', 'j.manel@hmail.com', 965123456, '2', '123456', 2, 0),
(2, 'Maria Rita', 'm.rita@jmail.com', 945123456, '1', '123456', 1, 0);

-- --------------------------------------------------------

--
-- Estrutura da tabela `sumario`
--

CREATE TABLE `sumario` (
  `identidade` int(11) NOT NULL,
  `professor` int(11) NOT NULL,
  `turma` int(11) NOT NULL,
  `texto` varchar(255) NOT NULL,
  `datah` date NOT NULL DEFAULT curdate()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Extraindo dados da tabela `sumario`
--

INSERT INTO `sumario` (`identidade`, `professor`, `turma`, `texto`, `datah`) VALUES
(1, 2, 1, 'Inglês - Verbo \"To Be\"', '2021-12-23'),
(2, 1, 3, 'A reconquista', '2021-12-23'),
(3, 2, 1, 'Matemática - Inequações', '2021-12-23'),
(4, 2, 1, 'Português - \"Os Lusíadas\"', '2021-12-23'),
(5, 1, 2, 'A Batalha de Aljubarrota', '2021-12-23');

-- --------------------------------------------------------

--
-- Estrutura da tabela `turmas`
--

CREATE TABLE `turmas` (
  `identidade` int(11) NOT NULL,
  `nome` varchar(50) NOT NULL DEFAULT 'turma',
  `escola` int(11) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Extraindo dados da tabela `turmas`
--

INSERT INTO `turmas` (`identidade`, `nome`, `escola`) VALUES
(1, '2º Ano', 1),
(2, '6º B', 2),
(3, '5º A', 2);

--
-- Índices para tabelas despejadas
--

--
-- Índices para tabela `agrupamentos`
--
ALTER TABLE `agrupamentos`
  ADD PRIMARY KEY (`identidade`);

--
-- Índices para tabela `alunos`
--
ALTER TABLE `alunos`
  ADD PRIMARY KEY (`identidade`),
  ADD KEY `FK_alunos_turmas` (`turma`),
  ADD KEY `FK_alunos_pais` (`pai1`),
  ADD KEY `FK_alunos_pais_2` (`pai2`);

--
-- Índices para tabela `avaliacoes`
--
ALTER TABLE `avaliacoes`
  ADD PRIMARY KEY (`identidade`),
  ADD KEY `FK_avaliacoes_alunos` (`aluno`),
  ADD KEY `FK_avaliacoes_professores` (`avaliador`),
  ADD KEY `disciplina` (`disciplina`);

--
-- Índices para tabela `disciplinas`
--
ALTER TABLE `disciplinas`
  ADD PRIMARY KEY (`id`),
  ADD KEY `FK_disciplinas_professores` (`professor`),
  ADD KEY `turma` (`turma`,`professor`) USING BTREE;

--
-- Índices para tabela `escolas`
--
ALTER TABLE `escolas`
  ADD PRIMARY KEY (`identidade`),
  ADD KEY `FK_escolas_agrupamentos` (`agrup`);

--
-- Índices para tabela `mensagens`
--
ALTER TABLE `mensagens`
  ADD PRIMARY KEY (`identidade`),
  ADD KEY `FK_mensagens_professores` (`professor`),
  ADD KEY `FK_mensagens_alunos` (`aluno`),
  ADD KEY `FK_mensagens_pais` (`pai`);

--
-- Índices para tabela `pais`
--
ALTER TABLE `pais`
  ADD PRIMARY KEY (`identidade`),
  ADD KEY `identidade` (`identidade`);

--
-- Índices para tabela `professores`
--
ALTER TABLE `professores`
  ADD PRIMARY KEY (`identidade`),
  ADD KEY `FK_professores_escolas` (`escola`),
  ADD KEY `identidade` (`identidade`);

--
-- Índices para tabela `sumario`
--
ALTER TABLE `sumario`
  ADD PRIMARY KEY (`identidade`),
  ADD KEY `FK_sumario_professores` (`professor`),
  ADD KEY `FK_sumario_turmas` (`turma`);

--
-- Índices para tabela `turmas`
--
ALTER TABLE `turmas`
  ADD PRIMARY KEY (`identidade`),
  ADD KEY `FK_turmas_escolas` (`escola`);

--
-- AUTO_INCREMENT de tabelas despejadas
--

--
-- AUTO_INCREMENT de tabela `agrupamentos`
--
ALTER TABLE `agrupamentos`
  MODIFY `identidade` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de tabela `alunos`
--
ALTER TABLE `alunos`
  MODIFY `identidade` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de tabela `avaliacoes`
--
ALTER TABLE `avaliacoes`
  MODIFY `identidade` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de tabela `disciplinas`
--
ALTER TABLE `disciplinas`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de tabela `escolas`
--
ALTER TABLE `escolas`
  MODIFY `identidade` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de tabela `mensagens`
--
ALTER TABLE `mensagens`
  MODIFY `identidade` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=67;

--
-- AUTO_INCREMENT de tabela `pais`
--
ALTER TABLE `pais`
  MODIFY `identidade` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT de tabela `professores`
--
ALTER TABLE `professores`
  MODIFY `identidade` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de tabela `sumario`
--
ALTER TABLE `sumario`
  MODIFY `identidade` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de tabela `turmas`
--
ALTER TABLE `turmas`
  MODIFY `identidade` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- Restrições para despejos de tabelas
--

--
-- Limitadores para a tabela `alunos`
--
ALTER TABLE `alunos`
  ADD CONSTRAINT `FK_alunos_pais` FOREIGN KEY (`pai1`) REFERENCES `pais` (`identidade`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `FK_alunos_pais_2` FOREIGN KEY (`pai2`) REFERENCES `pais` (`identidade`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `FK_alunos_turmas` FOREIGN KEY (`turma`) REFERENCES `turmas` (`identidade`) ON DELETE NO ACTION ON UPDATE CASCADE;

--
-- Limitadores para a tabela `avaliacoes`
--
ALTER TABLE `avaliacoes`
  ADD CONSTRAINT `FK_avaliacoes_alunos` FOREIGN KEY (`aluno`) REFERENCES `alunos` (`identidade`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `FK_avaliacoes_disciplinas` FOREIGN KEY (`disciplina`) REFERENCES `disciplinas` (`id`),
  ADD CONSTRAINT `FK_avaliacoes_professores` FOREIGN KEY (`avaliador`) REFERENCES `professores` (`identidade`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Limitadores para a tabela `disciplinas`
--
ALTER TABLE `disciplinas`
  ADD CONSTRAINT `FK_disciplinas_professores` FOREIGN KEY (`professor`) REFERENCES `professores` (`identidade`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `FK_disciplinas_turmas` FOREIGN KEY (`turma`) REFERENCES `turmas` (`identidade`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Limitadores para a tabela `escolas`
--
ALTER TABLE `escolas`
  ADD CONSTRAINT `FK_escolas_agrupamentos` FOREIGN KEY (`agrup`) REFERENCES `agrupamentos` (`identidade`) ON DELETE NO ACTION ON UPDATE CASCADE;

--
-- Limitadores para a tabela `mensagens`
--
ALTER TABLE `mensagens`
  ADD CONSTRAINT `FK_mensagens_alunos` FOREIGN KEY (`aluno`) REFERENCES `alunos` (`identidade`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `FK_mensagens_pais` FOREIGN KEY (`pai`) REFERENCES `pais` (`identidade`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `FK_mensagens_professores` FOREIGN KEY (`professor`) REFERENCES `professores` (`identidade`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Limitadores para a tabela `professores`
--
ALTER TABLE `professores`
  ADD CONSTRAINT `FK_professores_escolas` FOREIGN KEY (`escola`) REFERENCES `escolas` (`identidade`) ON DELETE NO ACTION ON UPDATE CASCADE;

--
-- Limitadores para a tabela `sumario`
--
ALTER TABLE `sumario`
  ADD CONSTRAINT `FK_sumario_professores` FOREIGN KEY (`professor`) REFERENCES `professores` (`identidade`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `FK_sumario_turmas` FOREIGN KEY (`turma`) REFERENCES `turmas` (`identidade`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Limitadores para a tabela `turmas`
--
ALTER TABLE `turmas`
  ADD CONSTRAINT `FK_turmas_escolas` FOREIGN KEY (`escola`) REFERENCES `escolas` (`identidade`) ON DELETE NO ACTION ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
