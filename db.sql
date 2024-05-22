use master
DROP DATABASE IF EXISTS BookingMovieApp;
CREATE DATABASE BookingMovieApp;
GO
use BookingMovieApp

GO
CREATE TABLE Users (
    id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    username VARCHAR(50) NOT NULL,
    email VARCHAR(100),
    password VARCHAR(100) NOT NULL,
    full_name VARCHAR(100),
    birth_date DATE,
	is_admin int, 
    gender VARCHAR(10) CHECK (gender IN ('Male', 'Female', 'Other')),
);

INSERT INTO User(username,password, is_admin)
VALUES(un1, 'A665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3', 0),
VALUES(ad1, 'A665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3', 1);


CREATE TABLE Genres (
    genre_id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    name VARCHAR(100) NOT NULL
);

CREATE TABLE Directors (
    director_id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    name VARCHAR(100) NOT NULL
);

CREATE TABLE Stars (
    star_id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    name VARCHAR(100) NOT NULL
);

CREATE TABLE Movies (
    movie_id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    title NVARCHAR(255) NOT NULL,
    genre_id INT,
    duration_minutes INT,
    release_date DATE,
    rating DECIMAL(3, 1),
	Certification int, 
    plot_summary TEXT,
    poster_url VARCHAR(255),
         VARCHAR(255),
    trailer_url VARCHAR(255), 
	FOREIGN KEY (genre_id) REFERENCES Genres(genre_id)
);

CREATE TABLE MovieStars (
    movie_id INT,
    star_id INT,
    PRIMARY KEY (movie_id, star_id),
    FOREIGN KEY (movie_id) REFERENCES Movies(movie_id),
    FOREIGN KEY (star_id) REFERENCES Stars(star_id)
);

CREATE TABLE MovieDirectors (
    movie_id INT,
    director_id INT,
    PRIMARY KEY (movie_id, director_id),
    FOREIGN KEY (movie_id) REFERENCES Movies(movie_id),
    FOREIGN KEY (director_id) REFERENCES Directors(director_id)
);


CREATE TABLE Cinema(
	cinema_id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	name varchar(32), 
)

-- to upgrade: cinema fields, custom chairs for each show time
-- PRIMARY KEY(movie_id,cinema_id, showtime_datetime )
CREATE TABLE Showtimes (
    showtime_id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    movie_id INT,
	cinema_id INT, 
    showtime_datetime DATETIME,
    FOREIGN KEY (movie_id) REFERENCES Movies(movie_id),
    FOREIGN KEY (cinema_id) REFERENCES Cinema(cinema_id)
);

CREATE TABLE Seats(
    seat_id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	row char(1), 
	number int, 
	showtime_id int, 
	status VARCHAR(12) CHECK (status IN ('Available', 'Sold')), 
	price int, -- decimal to upgrade
    FOREIGN KEY (showtime_id) REFERENCES Showtimes(showtime_id)
)

CREATE TABLE Bookings (
    booking_id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    user_id INT,    
    showtime_id INT,
    booking_datetime DATETIME,
    original_price int,
	total_price int, 
    FOREIGN KEY (user_id) REFERENCES Users(id),
    FOREIGN KEY (showtime_id) REFERENCES Showtimes(showtime_id)
);
 

CREATE TABLE BookingSeats( 
    booking_id INT,
    seat_id INT,
    PRIMARY KEY (booking_id, seat_id),
    FOREIGN KEY (booking_id) REFERENCES Bookings(booking_id),
    FOREIGN KEY (seat_id) REFERENCES Seats(seat_id)
)

CREATE TABLE Vouchers (
    VoucherID INT PRIMARY KEY IDENTITY(1, 1),
    VoucherCode VARCHAR(10) UNIQUE,
    VoucherType VARCHAR(20) CHECK (VoucherType IN ('Percentage', 'Fixed Amount')), 
    DiscountValue DECIMAL(10, 2),
    ValidFrom DATE,
    ValidUntil DATE,
    UsageLimit INT,
    RemainingUsage INT,
);


INSERT INTO Users (username, email, password, full_name, birth_date, gender)
VALUES
('huypeter', 'john@example.com', '123', 'Huy Pham', '2003-01-01', 'Male'),
('txb', 'jane@example.com', '123', 'Bach Xuan', '1985-08-2', 'Female'),
('alex_jones', 'alex@example.com', '789', 'Alex Jones', '1978-12-10', 'Other');

-- Genres
INSERT INTO Genres (name)
VALUES
('Action'),
('Comedy'),
('Tragedy'),
('Drama');

-- Directors
INSERT INTO Directors (name)
VALUES
('Director 1'),
('Director 2'),
('Director 3');

-- Stars
INSERT INTO Stars (name)
VALUES
('Cillyan Murphy'),
('Christopho Nolan'),
('Tom Haland');

-- Movies
-- Movies
INSERT INTO Movies (title, duration_minutes, release_date, rating, Certification, plot_summary, poster_url,poster_vertical_url, trailer_url, genre_id)
VALUES ('Mai', 120, '2023-01-01', 8.5, 18, 'Plot summary for Mai', '/Images/slider-mai.jpg','/Images/mv-mai.jpg', 'trailer1.mp4',1),
('Fast and Furious', 130, '2022-01-01', 8, 16, 'Plot summary for Fast and Furious', '/Images/slider-faf.jpg','/Images/mv-faf.jpg', 'trailer1.mp4',2),
('Aquaman', 144, '2023-02-01', 8, 18, 'Plot summary for Aquaman', '/Images/slider-aquaman.jpg','/Images/mv-aquaman.jpg', 'trailer1.mp4',3),
('Avatar', 130, '2021-01-01', 9, 16, 'Plot summary for Avatar', '/Images/slider-avatar.jpg','/Images/mv-avatar.jpg', 'trailer1.mp4',3),
('Quỷ Thay Đầu', 130, '2024-04-01', 9, 18, 'Plot summary for Quỷ Thay Đầu', '/Images/mv-bhead.jpg','/Images/mv-bhead.jpg', 'trailer1.mp4',4),
('Dune - Hành tinh cát', 180, '2024-05-03', 8.5, 18, 'Plot summary for Dune 2', '/Images/slider-dune.jpg','/Images/mv-dune.jpg', 'trailer1.mp4',1),
('KungFu Panda 4', 120, '2024-03-08', 6.5, 12, 'Plot summary for KungFuPanda', '/Images/slider-kungfu.jpg','/Images/mv-panda.jpg', 'trailer1.mp4',1),
('Đào Phở và Piano', 120, '2018-12-13', 7.0, 12, 'Plot summary for Đào Phở và Piano', '/Images/slider-dao-pho-piano.jpg','/Images/mv-dao-pho-va-piano.jpg', 'trailer1.mp4',1),
('Demon Slayer', 120, '2024-02-23', 8.0, 12, 'Plot summary for Demon Slayer', '/Images/slider-demonslayer.jpg','/Images/mv-demonslayer.jpg', 'trailer1.mp4',2),
('Doctor Strange', 120, '2022-02-23', 8.0, 12, 'Plot summary for Doctor Strange', '/Images/slider-drstrange.jpg','/Images/mv-drstrange.jpg', 'trailer1.mp4',3),
('Marvel Endgame', 120, '2021-02-09', 8.0, 12, 'Plot summary for Marvel Endgame', '/Images/slider-endgame.jpg','/Images/mv-endgame.jpg', 'trailer1.mp4',1),
('Mission Impossible', 120, '2020-05-10', 7.5, 12, 'Plot summary for Mission Impossible', '/Images/slider-impossible.jpg','/Images/mv-impossible.jpg', 'trailer1.mp4',2),
('Madam Web', 120, '2020-05-10', 7.5, 12, 'Plot summary for Madam Web', '/Images/slider-impossible.jpg','/Images/mv-madamweb.jpg', 'trailer1.mp4',3),
('The Super Mario Bros', 120, '2020-05-10', 7.5, 12, 'Plot summary for The Super Mario Bros', '/Images/slider-mario.jpg','/Images/mv-mario.jpg', 'trailer1.mp4',4),
('Mật vụ Ong', 120, '2024-01-10', 7.5, 12, 'Plot summary for Mật vụ Ong', '/Images/slider-matvuong.jpg','/Images/mv-matvuong.jpg', 'trailer1.mp4',4),
('Nhà Bà Nữ', 120, '2022-01-21', 7.5, 12, 'Plot summary for Nhà Bà Nữ', '/Images/slider-nhabanu.jpg','/Images/mv-nhabanu.jpg', 'trailer1.mp4',3),
('Night Swim', 120, '2021-06-12', 7.5, 12, 'Plot summary for Night Swim', '/Images/slider-boidem.jpg','/Images/mv-nightswim.jpg', 'trailer1.mp4',4),
('Oppenheimer', 120, '2023-05-03', 7.5, 12, 'Plot summary for Oppenheimer', '/Images/slider-oppenheimer.jpg','/Images/mv-openheimer.jpg', 'trailer1.mp4',2),
('Shazam', 120, '2020-05-10', 7.5, 12, 'Plot summary for Shazam', '/Images/slider-shazam.jpg','/Images/mv-shazam.jpg', 'trailer1.mp4',4),
('Argylle - Siêu điệp viên', 120, '2020-05-10', 7.5, 12, 'Plot summary for Argylle - Siêu điệp viên', '/Images/slider-sieudiepvien.jpg','/Images/mv-sieudiepvien.jpg', 'trailer1.mp4',3),
('Spiderman', 120, '2020-05-10', 7.5, 12, 'Plot summary for Spiderman', '/Images/slider-spiderman.jpg','/Images/mv-spiderman.jpg', 'trailer1.mp4',2),
('Spy x Family', 120, '2019-03-19', 7.5, 12, 'Plot summary for Spy x Family', '/Images/slider-spy.jpg','/Images/mv-spy.jpg', 'trailer1.mp4',1),
('Titanic', 120, '2012-12-12', 9.0, 12, 'Plot summary for Titanic', '/Images/slider-titanic.jpg','/Images/mv-titanic.jpg', 'trailer1.mp4',1),
('Transformer', 120, '2022-05-30', 7.5, 12, 'Plot summary for Mission Impossible', '/Images/slider-transformer.jpg','/Images/mv-transformer.jpg', 'trailer1.mp4',2),
('Kong x Godzilla', 120, '2020-08-21', 7.5, 12, 'Plot summary for Kong x Godzilla', '/Images/slider-kong.jpg','/Images/mv-kong.jpg', 'trailer1.mp4',3);




-- MovieStars
INSERT INTO MovieStars (movie_id, star_id)
VALUES
(1, 1),
(1, 2),
(2, 2),
(3, 3);

-- MovieDirectors
INSERT INTO MovieDirectors (movie_id, director_id)
VALUES
(1, 1),
(1, 2),
(2, 2),
(3, 3);

-- Cinema
INSERT INTO Cinema (name)
VALUES
('Cinema 1'),
('Cinema 2'),
('Cinema 3');

-- Showtimes
INSERT INTO Showtimes (movie_id, cinema_id, showtime_datetime)
VALUES
(1, 1, '2024-04-24 15:00:00'),
(2, 2, '2024-04-25 16:00:00'),
(3, 3, '2024-04-04 17:00:00'), 
(4, 1, '2024-04-01 15:00:00'),
(5, 2, '2024-04-01 16:00:00'),
(6, 3, '2024-04-01 17:00:00'),
(7, 3, '2024-04-01 17:00:00'),
(25, 3, '2024-03-27 17:00:00'),
(25, 3, '2024-03-28 17:00:00'),
(25, 3, '2024-03-29 17:00:00'),
(25, 3, '2024-03-27 12:00:00'),
(25, 3, '2024-03-28 12:00:00'),
(25, 3, '2024-03-28 13:00:00'),
(25, 3, '2024-03-28 14:00:00'),
(25, 3, '2024-03-28 15:00:00'),
(25, 3, '2024-03-15 15:00:00'),
(25, 3, '2024-03-16 15:00:00'),


-- Add dummy for BookingSeats 
CREATE TABLE #Letters (
    letter CHAR(1) PRIMARY KEY
);

-- Insert letters A-Z into the temporary table
INSERT INTO #Letters (letter)
VALUES ('A'), ('B'), ('C'), ('D'), ('E'), ('F'), ('G'), ('H'), ('I'), ('J'),
       ('K'), ('L');

-- Insert 50 dummy records into Seats table
INSERT INTO Seats (row, number, showtime_id, status, price)
SELECT
    l.letter,
    s.number,
    2 AS showtime_id, -- Change this to your desired showtime_id
    'Available' AS status,
    100 AS price -- Change this to your desired price
FROM
    (SELECT number FROM master..spt_values WHERE type='P' AND number BETWEEN 1 AND 10) AS s
CROSS JOIN
    #Letters AS l;

-- Generate dummy data for Vouchers table
INSERT INTO Vouchers (VoucherCode, VoucherType, DiscountValue, ValidFrom, ValidUntil, UsageLimit, RemainingUsage)
VALUES
('ABC123', 'Percentage', 10, '2024-03-01', '2024-04-01', 100, 100),
('DEF456', 'Fixed Amount', 5.00, '2024-03-15', '2024-04-15', 50, 50),
('GHI789', 'Percentage', 20, '2024-03-01', '2024-05-01', 200, 200),
('JKL012', 'Fixed Amount', 10.00, '2024-03-10', '2024-04-10', 20, 20);


Insert into Bookings(user_id, showtime_id, booking_datetime, original_price, total_price)
Values(4, 2, '2024-04-24 15:00:00.000', 100,100),
(4, 2, '2024-04-24 15:00:00.000', 100,100),
(4, 2, '2024-04-25 15:00:00.000', 200,100),
(4, 2, '2024-04-26 15:00:00.000', 400,100),
(4, 2, '2024-04-27 15:00:00.000', 130,100),
(4, 2, '2024-03-4 15:00:00.000', 240,100),
(4, 2, '2024-03-4 16:00:00.000', 240,100),
(4, 2, '2024-03-4 17:00:00.000', 240,100),
(4, 2, '2024-03-11 15:00:00.000', 240,100),
(4, 2, '2024-03-11 16:00:00.000', 760,540),
(4, 2, '2024-03-11 17:00:00.000', 870,500),
(4, 2, '2024-03-18 12:00:00.000', 310,100),
(4, 2, '2024-03-18 13:00:00.000', 210,100),
(4, 2, '2024-03-18 14:00:00.000', 240,100),
(4, 2, '2024-03-18 15:00:00.000', 230,120),
(4, 2, '2024-03-18 16:00:00.000', 860,320),
(4, 2, '2024-03-18 17:00:00.000', 653,350),
--
(4, 3, '2024-03-4 15:00:00.000', 340,200),
(4, 3, '2024-03-4 16:00:00.000', 640,500),
(4, 3, '2024-03-4 17:00:00.000', 540,120),
(4, 3, '2024-03-11 15:00:00.000', 240,100),
(4, 3, '2024-03-11 16:00:00.000', 760,540),
(4, 3, '2024-03-11 17:00:00.000', 870,500),
(4, 3, '2024-03-18 12:00:00.000', 310,100),
(4, 3, '2024-03-18 13:00:00.000', 210,100),
(4, 3, '2024-03-18 14:00:00.000', 240,100),
(4, 3, '2024-03-18 15:00:00.000', 230,120),
(4, 3, '2024-03-18 15:00:00.000', 860,320),
(4, 3, '2024-03-18 17:00:00.000', 653,350); 
