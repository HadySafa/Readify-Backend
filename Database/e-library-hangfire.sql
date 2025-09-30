-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Sep 30, 2025 at 09:15 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `e-library-hangfire`
--

-- --------------------------------------------------------

--
-- Table structure for table `aggregatedcounter`
--

CREATE TABLE `aggregatedcounter` (
  `Id` int(11) NOT NULL,
  `Key` varchar(100) NOT NULL,
  `Value` int(11) NOT NULL,
  `ExpireAt` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Dumping data for table `aggregatedcounter`
--

INSERT INTO `aggregatedcounter` (`Id`, `Key`, `Value`, `ExpireAt`) VALUES
(1, 'stats:succeeded', 1, NULL),
(2, 'stats:succeeded:2025-09-30', 1, '2025-10-30 06:56:24'),
(3, 'stats:succeeded:2025-09-30-06', 1, '2025-10-01 06:56:24');

-- --------------------------------------------------------

--
-- Table structure for table `counter`
--

CREATE TABLE `counter` (
  `Id` int(11) NOT NULL,
  `Key` varchar(100) NOT NULL,
  `Value` int(11) NOT NULL,
  `ExpireAt` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `distributedlock`
--

CREATE TABLE `distributedlock` (
  `Resource` varchar(100) NOT NULL,
  `CreatedAt` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `hash`
--

CREATE TABLE `hash` (
  `Id` int(11) NOT NULL,
  `Key` varchar(100) NOT NULL,
  `Field` varchar(40) NOT NULL,
  `Value` longtext DEFAULT NULL,
  `ExpireAt` datetime(6) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Dumping data for table `hash`
--

INSERT INTO `hash` (`Id`, `Key`, `Field`, `Value`, `ExpireAt`) VALUES
(1, 'recurring-job:track-due-dates', 'Queue', 'default', NULL),
(2, 'recurring-job:track-due-dates', 'Cron', '0 55 6 * * *', NULL),
(3, 'recurring-job:track-due-dates', 'TimeZoneId', 'UTC', NULL),
(4, 'recurring-job:track-due-dates', 'Job', '{\"Type\":\"e_library.Jobs.BookBorrowingDueDateJob, e-library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\",\"Method\":\"TrackDueDates\",\"ParameterTypes\":\"[]\",\"Arguments\":\"[]\"}', NULL),
(5, 'recurring-job:track-due-dates', 'CreatedAt', '2025-09-29T19:09:12.4465511Z', NULL),
(6, 'recurring-job:track-due-dates', 'NextExecution', '2025-10-01T06:55:00.0000000Z', NULL),
(7, 'recurring-job:track-due-dates', 'V', '2', NULL),
(10, 'recurring-job:track-due-dates', 'LastExecution', '2025-09-30T06:56:09.1118111Z', NULL),
(12, 'recurring-job:track-due-dates', 'LastJobId', '1', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `job`
--

CREATE TABLE `job` (
  `Id` int(11) NOT NULL,
  `StateId` int(11) DEFAULT NULL,
  `StateName` varchar(20) DEFAULT NULL,
  `InvocationData` longtext NOT NULL,
  `Arguments` longtext NOT NULL,
  `CreatedAt` datetime(6) NOT NULL,
  `ExpireAt` datetime(6) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Dumping data for table `job`
--

INSERT INTO `job` (`Id`, `StateId`, `StateName`, `InvocationData`, `Arguments`, `CreatedAt`, `ExpireAt`) VALUES
(1, 3, 'Succeeded', '{\"Type\":\"e_library.Jobs.BookBorrowingDueDateJob, e-library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\",\"Method\":\"TrackDueDates\",\"ParameterTypes\":\"[]\",\"Arguments\":\"[]\"}', '[]', '2025-09-30 06:56:09.158930', '2025-10-01 06:56:24.275020');

-- --------------------------------------------------------

--
-- Table structure for table `jobparameter`
--

CREATE TABLE `jobparameter` (
  `Id` int(11) NOT NULL,
  `JobId` int(11) NOT NULL,
  `Name` varchar(40) NOT NULL,
  `Value` longtext DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Dumping data for table `jobparameter`
--

INSERT INTO `jobparameter` (`Id`, `JobId`, `Name`, `Value`) VALUES
(1, 1, 'RecurringJobId', '\"track-due-dates\"'),
(2, 1, 'Time', '1759215369'),
(3, 1, 'CurrentCulture', '\"en-US\"'),
(4, 1, 'CurrentUICulture', '\"en-US\"');

-- --------------------------------------------------------

--
-- Table structure for table `jobqueue`
--

CREATE TABLE `jobqueue` (
  `Id` int(11) NOT NULL,
  `JobId` int(11) NOT NULL,
  `FetchedAt` datetime(6) DEFAULT NULL,
  `Queue` varchar(50) NOT NULL,
  `FetchToken` varchar(36) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `jobstate`
--

CREATE TABLE `jobstate` (
  `Id` int(11) NOT NULL,
  `JobId` int(11) NOT NULL,
  `CreatedAt` datetime(6) NOT NULL,
  `Name` varchar(20) NOT NULL,
  `Reason` varchar(100) DEFAULT NULL,
  `Data` longtext DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `list`
--

CREATE TABLE `list` (
  `Id` int(11) NOT NULL,
  `Key` varchar(100) NOT NULL,
  `Value` longtext DEFAULT NULL,
  `ExpireAt` datetime(6) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `server`
--

CREATE TABLE `server` (
  `Id` varchar(100) NOT NULL,
  `Data` longtext NOT NULL,
  `LastHeartbeat` datetime(6) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Dumping data for table `server`
--

INSERT INTO `server` (`Id`, `Data`, `LastHeartbeat`) VALUES
('hady-2004:20688:4e84097a-f462-469d-a7cd-ff0f24580c59', '{\"WorkerCount\":20,\"Queues\":[\"default\"],\"StartedAt\":\"2025-09-30T06:51:38.7638732Z\"}', '2025-09-30 07:14:58.654535');

-- --------------------------------------------------------

--
-- Table structure for table `set`
--

CREATE TABLE `set` (
  `Id` int(11) NOT NULL,
  `Key` varchar(100) NOT NULL,
  `Value` varchar(256) NOT NULL,
  `Score` float NOT NULL,
  `ExpireAt` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Dumping data for table `set`
--

INSERT INTO `set` (`Id`, `Key`, `Value`, `Score`, `ExpireAt`) VALUES
(1, 'recurring-jobs', 'track-due-dates', 1759300000, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `state`
--

CREATE TABLE `state` (
  `Id` int(11) NOT NULL,
  `JobId` int(11) NOT NULL,
  `Name` varchar(20) NOT NULL,
  `Reason` varchar(100) DEFAULT NULL,
  `CreatedAt` datetime(6) NOT NULL,
  `Data` longtext DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Dumping data for table `state`
--

INSERT INTO `state` (`Id`, `JobId`, `Name`, `Reason`, `CreatedAt`, `Data`) VALUES
(1, 1, 'Enqueued', 'Triggered by recurring job scheduler', '2025-09-30 06:56:09.193273', '{\"EnqueuedAt\":\"2025-09-30T06:56:09.1875134Z\",\"Queue\":\"default\"}'),
(2, 1, 'Processing', NULL, '2025-09-30 06:56:23.997089', '{\"StartedAt\":\"2025-09-30T06:56:23.9832182Z\",\"ServerId\":\"hady-2004:20688:4e84097a-f462-469d-a7cd-ff0f24580c59\",\"WorkerId\":\"c6320100-3629-4afd-8424-1eb8107b0c22\"}'),
(3, 1, 'Succeeded', NULL, '2025-09-30 06:56:24.272396', '{\"SucceededAt\":\"2025-09-30T06:56:24.2593993Z\",\"PerformanceDuration\":\"243\",\"Latency\":\"14856\"}');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `aggregatedcounter`
--
ALTER TABLE `aggregatedcounter`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `IX_CounterAggregated_Key` (`Key`);

--
-- Indexes for table `counter`
--
ALTER TABLE `counter`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Counter_Key` (`Key`);

--
-- Indexes for table `hash`
--
ALTER TABLE `hash`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `IX_Hash_Key_Field` (`Key`,`Field`);

--
-- Indexes for table `job`
--
ALTER TABLE `job`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Job_StateName` (`StateName`);

--
-- Indexes for table `jobparameter`
--
ALTER TABLE `jobparameter`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `IX_JobParameter_JobId_Name` (`JobId`,`Name`),
  ADD KEY `FK_JobParameter_Job` (`JobId`);

--
-- Indexes for table `jobqueue`
--
ALTER TABLE `jobqueue`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_JobQueue_QueueAndFetchedAt` (`Queue`,`FetchedAt`);

--
-- Indexes for table `jobstate`
--
ALTER TABLE `jobstate`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `FK_JobState_Job` (`JobId`);

--
-- Indexes for table `list`
--
ALTER TABLE `list`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `server`
--
ALTER TABLE `server`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `set`
--
ALTER TABLE `set`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `IX_Set_Key_Value` (`Key`,`Value`);

--
-- Indexes for table `state`
--
ALTER TABLE `state`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `FK_HangFire_State_Job` (`JobId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `aggregatedcounter`
--
ALTER TABLE `aggregatedcounter`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `counter`
--
ALTER TABLE `counter`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `hash`
--
ALTER TABLE `hash`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT for table `job`
--
ALTER TABLE `job`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `jobparameter`
--
ALTER TABLE `jobparameter`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `jobqueue`
--
ALTER TABLE `jobqueue`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `jobstate`
--
ALTER TABLE `jobstate`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `list`
--
ALTER TABLE `list`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `set`
--
ALTER TABLE `set`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `state`
--
ALTER TABLE `state`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `jobparameter`
--
ALTER TABLE `jobparameter`
  ADD CONSTRAINT `FK_JobParameter_Job` FOREIGN KEY (`JobId`) REFERENCES `job` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `jobstate`
--
ALTER TABLE `jobstate`
  ADD CONSTRAINT `FK_JobState_Job` FOREIGN KEY (`JobId`) REFERENCES `job` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `state`
--
ALTER TABLE `state`
  ADD CONSTRAINT `FK_HangFire_State_Job` FOREIGN KEY (`JobId`) REFERENCES `job` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
