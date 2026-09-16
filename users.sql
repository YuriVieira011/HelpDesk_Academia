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
-- Criação do banco de dados `users` (caso não exista) e seleção
--
CREATE DATABASE IF NOT EXISTS `users` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `users`;

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
  `DataFechamento` datetime DEFAULT NULL,
  `Status` varchar(20) NOT NULL DEFAULT 'Aberto',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `cadastros`
--

INSERT INTO `cadastros` (`Id`, `Nome`, `Email`, `Senha`, `CEP`, `RG`, `DataAbertura`, `DataFechamento`, `Status`) VALUES
(1, 'Teste', 'teste@teste', '236718923', '12345-67', '12.435.788-90', '2026-09-02 21:49:36', NULL, 'Aberto'),
(2, 'Teste 2', 'teste@teste.com', '1223121212121', '13221-21', '21.312.312-3211', '2026-09-10 18:04:33', NULL, 'Aberto');

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