-- phpMyAdmin SQL Dump
-- version 5.2.3
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1:3306
-- Létrehozás ideje: 2026. Sze 23. 06:58
-- Kiszolgáló verziója: 8.4.7
-- PHP verzió: 8.3.28

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `sportolo13b`
--
CREATE DATABASE IF NOT EXISTS `sportolo13b` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE `sportolo13b`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `eredmeny`
--

DROP TABLE IF EXISTS `eredmeny`;
CREATE TABLE IF NOT EXISTS `eredmeny` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Competition` varchar(40) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Description` text COLLATE utf8mb4_unicode_ci,
  `ResultTime` datetime DEFAULT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  `SportoloId` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `fk_Sportolo` (`SportoloId`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `sportolo`
--

DROP TABLE IF EXISTS `sportolo`;
CREATE TABLE IF NOT EXISTS `sportolo` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `email` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `age` int NOT NULL,
  `password` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `registrationTime` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- A tábla adatainak kiíratása `sportolo`
--

INSERT INTO `sportolo` (`id`, `name`, `email`, `age`, `password`, `registrationTime`) VALUES
(1, 'Kovács Bence', 'kovacs.bence@example.com', 21, 'jelszo123', '2025-01-12 09:15:00'),
(2, 'Nagy Petra', 'nagy.petra@example.com', 19, 'titkosjelszo', '2025-02-03 14:30:00'),
(3, 'Szabó Márk', 'szabo.mark@example.com', 24, 'sport2025', '2025-02-20 08:00:00'),
(4, 'Tóth Zsófia', 'toth.zsofia@example.com', 22, 'futas!42', '2025-03-15 17:45:00'),
(5, 'Horváth Dávid', 'horvath.david@example.com', 20, 'uszas99', '2025-04-01 11:20:00'),
(6, 'Varga Lili', 'varga.lili@example.com', 23, 'atletika7', '2025-04-18 16:05:00');
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
