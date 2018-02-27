-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: Feb 27, 2018 at 10:33 PM
-- Server version: 5.6.35
-- PHP Version: 7.0.15

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `josh_rochon`
--
CREATE DATABASE IF NOT EXISTS `josh_rochon` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `josh_rochon`;

-- --------------------------------------------------------

--
-- Table structure for table `client`
--

CREATE TABLE `client` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `firstappt` varchar(255) NOT NULL,
  `stylist_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `client`
--

INSERT INTO `client` (`id`, `name`, `email`, `firstappt`, `stylist_id`) VALUES
(25, 'Jennifer', 'jenn@gmail.com', '2018-02-12', 48),
(26, 'James Bond', 'jenn@gmail.com', '2018-02-05', 49),
(27, 'Jennifer', 'Chew@gmail.com', '2018-02-05', 48),
(28, 'Jennifer', 'jenn@gmail.com', '2018-02-05', 50);

-- --------------------------------------------------------

--
-- Table structure for table `stylist`
--

CREATE TABLE `stylist` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `startdate` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `stylist`
--

INSERT INTO `stylist` (`id`, `name`, `email`, `startdate`) VALUES
(48, 'Josh Rochon', 'joshdrochon@gmail.com', '2018-02-12'),
(49, 'Samwise the Brave', 'Sam@gmail.com', '2018-02-05'),
(50, 'Luke Skywalker', 'Luke@gmail.com', '2018-02-06');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `client`
--
ALTER TABLE `client`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `stylist`
--
ALTER TABLE `stylist`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `client`
--
ALTER TABLE `client`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=29;
--
-- AUTO_INCREMENT for table `stylist`
--
ALTER TABLE `stylist`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=51;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
