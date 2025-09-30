-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Sep 30, 2025 at 09:14 AM
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
-- Database: `e-library`
--

-- --------------------------------------------------------

--
-- Table structure for table `authors`
--

CREATE TABLE `authors` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `bio` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `authors`
--

INSERT INTO `authors` (`id`, `name`, `bio`) VALUES
(1, 'William Shakespeare', 'English playwright, poet, and actor, widely regarded as the greatest writer in the English language.'),
(2, 'Jane Austen', 'English novelist known for works such as Pride and Prejudice and Sense and Sensibility.'),
(3, 'Charles Dickens', 'English writer and social critic, author of Oliver Twist, A Tale of Two Cities, and Great Expectations.'),
(4, 'Leo Tolstoy', 'Russian author of War and Peace and Anna Karenina, considered among the greatest novelists of all time.'),
(5, 'Fyodor Dostoevsky', 'Russian novelist and philosopher, known for Crime and Punishment and The Brothers Karamazov.'),
(6, 'Mark Twain', 'American writer and humorist, best known for The Adventures of Tom Sawyer and Adventures of Huckleberry Finn.'),
(8, 'Ernest Hemingway', 'American novelist and short-story writer, famous for The Old Man and the Sea and A Farewell to Arms.'),
(9, 'J.K. Rowling', 'British author best known for writing the Harry Potter fantasy series.'),
(10, 'Agatha Christie', 'English writer, known as the “Queen of Crime,” author of 66 detective novels including Murder on the Orient Express.'),
(11, 'Gabriel García Márquez', 'Colombian novelist and Nobel Prize winner, known for One Hundred Years of Solitude and Love in the Time of Cholera.'),
(12, 'Paulo Coelho', 'Brazilian author of The Alchemist, one of the best-selling books in history.'),
(13, 'Homer', 'Ancient Greek poet traditionally said to be the author of the epic poems The Iliad and The Odyssey.');

-- --------------------------------------------------------

--
-- Table structure for table `books`
--

CREATE TABLE `books` (
  `id` int(11) NOT NULL,
  `title` varchar(255) NOT NULL,
  `description` text DEFAULT NULL,
  `genre_id` int(11) NOT NULL,
  `author_id` int(11) NOT NULL,
  `number_of_copies` int(11) NOT NULL DEFAULT 1,
  `available_copies` int(11) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `books`
--

INSERT INTO `books` (`id`, `title`, `description`, `genre_id`, `author_id`, `number_of_copies`, `available_copies`) VALUES
(2, 'Pride and Prejudice', 'A romantic novel exploring manners, upbringing, and marriage in 19th-century England.', 7, 2, 4, 0),
(3, 'Great Expectations', 'The story of an orphan named Pip and his growth through struggles and aspirations.', 1, 3, 6, 3),
(4, 'War and Peace', 'Tolstoy’s epic about Russian society during the Napoleonic Wars.', 8, 4, 3, 1),
(5, 'Crime and Punishment', 'A psychological novel about crime, guilt, and redemption.', 5, 5, 5, 2),
(6, 'Adventures of Huckleberry Finn', 'Mark Twain’s classic tale of freedom and race in 19th-century America.', 14, 6, 3, 2),
(8, 'The Old Man and the Sea', 'The struggle of an old fisherman with a giant marlin in the Gulf Stream.', 14, 8, 2, 1),
(9, 'Harry Potter and the Sorcerer’s Stone', 'The first book in the Harry Potter series.', 4, 9, 10, 8),
(10, 'Murder on the Orient Express', 'A Hercule Poirot mystery on a stranded train.', 5, 10, 6, 4),
(11, 'One Hundred Years of Solitude', 'A multigenerational story of the Buendía family.', 1, 11, 7, 4),
(12, 'The Alchemist', 'A philosophical novel about pursuing one’s dreams.', 14, 12, 8, 5),
(13, 'The Odyssey', 'Homer’s epic of Odysseus’s journey home after the Trojan War.', 14, 13, 5, 2),
(15, 'Emma', 'A Jane Austen classic about matchmaking and self-discovery.', 7, 2, 3, 0),
(16, 'Oliver Twist', 'The story of a young orphan in London’s underworld.', 8, 3, 5, 0),
(17, 'Anna Karenina', 'Tolstoy’s tragic romance exploring love, betrayal, and society.', 7, 4, 3, 0),
(18, 'The Brothers Karamazov', 'A philosophical novel exploring morality, faith, and free will.', 5, 5, 4, 0),
(20, 'And Then There Were None', 'A mystery novel where ten strangers are trapped and killed one by one.', 5, 10, 5, 0);

-- --------------------------------------------------------

--
-- Table structure for table `borrowings`
--

CREATE TABLE `borrowings` (
  `id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `book_id` int(11) NOT NULL,
  `borrowed_at` datetime NOT NULL DEFAULT current_timestamp(),
  `due_date` datetime NOT NULL,
  `returned_at` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `borrowings`
--

INSERT INTO `borrowings` (`id`, `user_id`, `book_id`, `borrowed_at`, `due_date`, `returned_at`) VALUES
(29, 1, 5, '2025-09-30 06:43:42', '2025-10-07 06:43:42', NULL),
(30, 1, 4, '2025-09-30 06:44:12', '2025-10-02 06:44:12', NULL),
(31, 1, 11, '2025-09-30 06:44:30', '2025-09-30 06:44:30', NULL),
(37, 1, 2, '2025-09-01 10:00:00', '2025-09-08 10:00:00', '2025-09-07 16:30:00'),
(38, 1, 6, '2025-08-20 09:15:00', '2025-09-03 09:15:00', '2025-09-02 14:00:00'),
(39, 1, 13, '2025-07-10 11:45:00', '2025-07-24 11:45:00', '2025-07-23 18:20:00'),
(40, 1, 18, '2025-09-05 08:30:00', '2025-09-19 08:30:00', '2025-09-18 12:00:00'),
(41, 1, 9, '2025-08-01 14:00:00', '2025-08-15 14:00:00', '2025-08-14 17:45:00'),
(42, 2, 13, '2025-09-30 06:48:50', '2025-10-07 06:48:50', NULL),
(43, 2, 12, '2025-09-30 06:49:01', '2025-10-01 06:49:01', NULL),
(44, 2, 2, '2025-09-30 06:49:18', '2025-10-04 06:49:18', NULL),
(45, 3, 2, '2025-09-30 06:49:48', '2025-10-01 06:49:48', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `genres`
--

CREATE TABLE `genres` (
  `id` int(11) NOT NULL,
  `name` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `genres`
--

INSERT INTO `genres` (`id`, `name`) VALUES
(14, 'Adventure'),
(10, 'Biography'),
(4, 'Fantasy'),
(1, 'Fiction'),
(8, 'Historical Fiction'),
(9, 'Horror'),
(5, 'Mystery'),
(2, 'Non-Fiction'),
(11, 'Poetry'),
(7, 'Romance'),
(3, 'Science Fiction'),
(15, 'Self-Help'),
(6, 'Thriller'),
(13, 'Young Adult');

-- --------------------------------------------------------

--
-- Table structure for table `notifications`
--

CREATE TABLE `notifications` (
  `id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `message` text NOT NULL,
  `is_read` tinyint(1) NOT NULL DEFAULT 0,
  `type` varchar(50) DEFAULT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `notifications`
--

INSERT INTO `notifications` (`id`, `user_id`, `message`, `is_read`, `type`, `created_at`) VALUES
(1, 1, 'The book \'War and Peace\' is due in 2 day(s). Kindly ensure it is returned on time.', 0, 'reminder', '2025-09-30 06:56:24'),
(2, 1, 'The book \'One Hundred Years of Solitude\' is overdue. Please return it as soon as possible.', 1, 'alert', '2025-09-30 06:56:24'),
(3, 2, 'The book \'The Alchemist\' is due in 1 day(s). Kindly ensure it is returned on time.', 0, 'reminder', '2025-09-30 06:56:24'),
(4, 3, 'The book \'Pride and Prejudice\' is due in 1 day(s). Kindly ensure it is returned on time.', 0, 'reminder', '2025-09-30 06:56:24');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `full_name` varchar(255) NOT NULL,
  `username` varchar(100) NOT NULL,
  `password` varchar(255) NOT NULL,
  `phone_number` bigint(20) DEFAULT NULL,
  `role` enum('admin','user') NOT NULL DEFAULT 'user'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`id`, `full_name`, `username`, `password`, `phone_number`, `role`) VALUES
(1, 'Hady Safa', 'hadysafa_', '$2a$11$AhncY1zSQbdCXv92eE8QI.Icp8aIQSOnm4qP3rmsYXaQcq2i70D8y', 70860816, 'user'),
(2, 'Ahmad Khalil', 'ahmadkhalil_', '$2a$11$ZDm0HUmwFo3wt0L.zMpVA.s4O.Mg9IT4wQ5hs2tG6GEZW5nASgOdm', 70895547, 'user'),
(3, 'Sara Amin', 'saraamin_', '$2a$11$YZBtWbmhCIrv7417s36vg.WtCNIxXjiGCDKtCHkdRlxGBIfAi.Fde', 78554226, 'user'),
(4, 'Admin', 'admin', '$2a$11$LFwHN11j8QeCcmIbV3AW9eyVqhjTCV/QlqxQlDds.AW59eAq1Qy5q', 0, 'admin');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `authors`
--
ALTER TABLE `authors`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `books`
--
ALTER TABLE `books`
  ADD PRIMARY KEY (`id`),
  ADD KEY `genre_id` (`genre_id`),
  ADD KEY `author_id` (`author_id`);

--
-- Indexes for table `borrowings`
--
ALTER TABLE `borrowings`
  ADD PRIMARY KEY (`id`),
  ADD KEY `user_id` (`user_id`),
  ADD KEY `book_id` (`book_id`);

--
-- Indexes for table `genres`
--
ALTER TABLE `genres`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `name` (`name`);

--
-- Indexes for table `notifications`
--
ALTER TABLE `notifications`
  ADD PRIMARY KEY (`id`),
  ADD KEY `user_id` (`user_id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `username` (`username`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `authors`
--
ALTER TABLE `authors`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT for table `books`
--
ALTER TABLE `books`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT for table `borrowings`
--
ALTER TABLE `borrowings`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=46;

--
-- AUTO_INCREMENT for table `genres`
--
ALTER TABLE `genres`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT for table `notifications`
--
ALTER TABLE `notifications`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `books`
--
ALTER TABLE `books`
  ADD CONSTRAINT `books_ibfk_1` FOREIGN KEY (`genre_id`) REFERENCES `genres` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `books_ibfk_2` FOREIGN KEY (`author_id`) REFERENCES `authors` (`id`) ON DELETE CASCADE;

--
-- Constraints for table `borrowings`
--
ALTER TABLE `borrowings`
  ADD CONSTRAINT `borrowings_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `borrowings_ibfk_2` FOREIGN KEY (`book_id`) REFERENCES `books` (`id`) ON DELETE CASCADE;

--
-- Constraints for table `notifications`
--
ALTER TABLE `notifications`
  ADD CONSTRAINT `notifications_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
