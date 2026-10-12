-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Criação do banco de dados `spotter` (caso não exista) e seleção
--
CREATE DATABASE IF NOT EXISTS `spotter` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `spotter`;

-- --------------------------------------------------------

--
-- Estrutura para tabela `cadastros`
--

CREATE TABLE IF NOT EXISTS `cadastros` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(50) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `Senha` varchar(100) NOT NULL,
  `CEP` char(9) NOT NULL,
  `RG` varchar(20) NOT NULL,
  `DataAbertura` datetime NOT NULL DEFAULT current_timestamp(),
  `User` varchar(20) NOT NULL DEFAULT 'Usuário',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `cadastros`
--

INSERT INTO `cadastros` (`Id`, `Nome`, `Email`, `Senha`, `CEP`, `RG`, `DataAbertura`, `User`) VALUES
(1, 'Teste 1', 'teste@teste', 'abc1234567890', '12345-67', '12.435.788-90', '2026-09-02 21:49:36', 'Admin'),
(2, 'Teste 2', 'teste@teste.com', 'abcd1234567890', '13221-21', '21.312.312-3211', '2026-09-10 18:04:33', 'Admin'),
(3, 'Teste 3', 'teste@testeuser.com', '1234567890', '13221-41', '21.218.312-3211', '2026-02-10 18:05:33', 'Usuário');

-- --------------------------------------------------------

--
-- Estrutura para tabela `maquinas`
--

CREATE TABLE IF NOT EXISTS `maquinas` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Nome` varchar(100) NOT NULL,
  `Marca` varchar(50) NOT NULL,
  `NumeroSerie` longtext DEFAULT NULL,
  `Status` int NOT NULL DEFAULT 1,
  `UltimaManutencao` datetime(6) DEFAULT NULL,
  `Observacoes` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `maquinas`
--

INSERT INTO `maquinas` (`Nome`, `Marca`, `NumeroSerie`, `Status`, `UltimaManutencao`, `Observacoes`) VALUES
('Escavadeira Hidráulica 320', 'Caterpillar', 'CAT320X98231', 1, '2026-04-10 08:30:00.000000', 'Revisão geral realizada com troca de filtros.'),
('Escavadeira PC200', 'Komatsu', 'KOM200Y12345', 1, '2026-03-15 14:00:00.000000', 'Funcionamento normal.'),
('Escavadeira EC220D', 'Volvo', 'VOL220Z55443', 2, '2026-05-01 10:15:00.000000', 'Aguardando peças para manutenção hidráulica.'),
('Motoniveladora 140K', 'Caterpillar', 'CAT140K77889', 1, '2026-02-20 09:00:00.000000', NULL),
('Carregadeira WA380', 'Komatsu', 'KOM380A99887', 1, '2026-01-10 16:45:00.000000', 'Ótimo estado de conservação.'),
('Carregadeira L120H', 'Volvo', 'VOL120B11223', 1, '2026-04-25 11:30:00.000000', NULL),
('Trator de Esteira D6T', 'Caterpillar', 'CATD6T33445', 1, '2026-04-05 07:00:00.000000', 'Troca de óleo recente.'),
('Trator D65EX', 'Komatsu', 'KOM65EX66778', 2, '2026-02-14 13:20:00.000000', 'Manutenção corretiva no sistema de direção.'),
('Caminhão Articulado A40G', 'Volvo', 'VOLA40G99001', 1, '2026-03-30 15:10:00.000000', NULL),
('Rolo Compactador CS56B', 'Caterpillar', 'CATCS56B22334', 1, '2026-04-18 10:00:00.000000', 'Verificação de vibração concluída.'),
('Trator Agrícola 8R 370', 'John Deere', 'JD8R37000111', 1, '2026-04-12 08:00:00.000000', 'Uso principal no plantio.'),
('Colheitadeira S790', 'John Deere', 'JDS79000222', 1, '2026-01-05 14:00:00.000000', 'Revisada antes da safra.'),
('Pulverizador 4730', 'John Deere', 'JD473000333', 1, '2026-03-22 09:30:00.000000', NULL),
('Retroescavadeira B730', 'Bobcat', 'BOB730X44556', 1, '2026-04-02 11:00:00.000000', 'Equipamento versátil em obras urbanas.'),
('Minicarregadeira S650', 'Bobcat', 'BOBS650Y77889', 1, '2026-03-18 16:00:00.000000', 'Pneus trocados recentemente.'),
('Manipulador Telescópico T40.180', 'Bobcat', 'BOBT4018099', 2, '2026-02-28 10:45:00.000000', 'Em manutenção preventiva.'),
('Miniescavadeira PC35MR', 'Komatsu', 'KOM35MR1122', 1, '2026-04-20 13:00:00.000000', NULL),
('Escavadeira EC140', 'Volvo', 'VOLEC1405566', 1, '2026-03-10 11:15:00.000000', 'Operando em terreno rochoso.'),
('Plantadeira DB40', 'John Deere', 'JDDB4088990', 1, '2026-02-10 08:20:00.000000', 'Discos de corte alinhados.'),
('Compactador de Solo SP56', 'Caterpillar', 'CATSP5600112', 1, '2026-04-15 15:00:00.000000', NULL);

-- --------------------------------------------------------

--
-- Estrutura para tabela `chamados`
--

CREATE TABLE IF NOT EXISTS `chamados` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(50) NOT NULL,
  `Descricao` varchar(100) NOT NULL,
  `DataAbertura` datetime NOT NULL DEFAULT current_timestamp(),
  `DataFechamento` datetime DEFAULT NULL,
  `Status` varchar(20) NOT NULL DEFAULT 'Aberto',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `chamados`
--

INSERT INTO `chamados` (`Id`, `Nome`, `Descricao`, `DataAbertura`, `DataFechamento`, `Status`) VALUES
(1, 'Teste', 'Descrição de teste 1', '2026-09-02 21:49:36', NULL, 'Aberto'),
(2, 'Teste 2', 'Descrição de teste 2', '2026-09-10 17:35:56', NULL, 'Aberto');

COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;