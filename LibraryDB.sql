CREATE DATABASE LibraryDB;
GO

USE LibraryDB;
GO

-- ======================
-- Category
-- ======================
CREATE TABLE Category
(
    CategoryId INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(100) NOT NULL
);

-- ======================
-- Book
-- ======================
CREATE TABLE Book
(
    BookId INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Author NVARCHAR(100),
    Publisher NVARCHAR(100),
    YearPublish INT,
    Quantity INT NOT NULL DEFAULT 0,
    CategoryId INT,
    Image NVARCHAR(500),

    CONSTRAINT FK_Book_Category
        FOREIGN KEY(CategoryId)
        REFERENCES Category(CategoryId)
);

-- ======================
-- Users
-- ======================
CREATE TABLE Users
(
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR(100) NOT NULL,
    Phone VARCHAR(20),
    Email VARCHAR(100),
    Password VARCHAR(255),
    Role VARCHAR(20) NOT NULL
);

-- ======================
-- Borrow
-- ======================
CREATE TABLE Borrow
(
    BorrowId INT IDENTITY(1,1) PRIMARY KEY,
    ReaderId INT NOT NULL,
    BookId INT NOT NULL,
    BorrowDate DATE NOT NULL,
    ReturnDate DATE NULL,
    Status NVARCHAR(30) NOT NULL,

    CONSTRAINT FK_Borrow_Reader
        FOREIGN KEY(ReaderId)
        REFERENCES Users(UserId),

    CONSTRAINT FK_Borrow_Book
        FOREIGN KEY(BookId)
        REFERENCES Book(BookId)
);

-- ======================
-- Sample Data
-- ======================

-- Category
INSERT INTO Category(CategoryName)
VALUES
('Novel'),
('IT'),
('English'),
('Science');

-- Book
INSERT INTO Book
(
    Title,
    Author,
    Publisher,
    YearPublish,
    Quantity,
    CategoryId,
    Image
)
VALUES
(
    'Clean Code',
    'Robert C. Martin',
    'Prentice Hall',
    2008,
    5,
    2,
    'https://m.media-amazon.com/images/I/71q5PkuN7XL._SL1500_.jpg'
),
(
    'Harry Potter',
    'J.K. Rowling',
    'Bloomsbury',
    1997,
    10,
    1,
    'https://tse3.mm.bing.net/th/id/OIP.rzOJSuuR3Ouj7mFASdVGhQHaLH?r=0&cb=thfc1falcon4&rs=1&pid=ImgDetMain&o=7&rm=3'
),
(
    'Learning SQL',
    'Alan Beaulieu',
    'OReilly',
    2020,
    3,
    2,
    'https://sanet.pics/storage-6/0320/WqXGzcn3ne2kWHE6t5VlQuaz2gURkLLh.jpg'
),
(
    'Physics 101',
    'John Smith',
    'Pearson',
    2018,
    6,
    4,
    'https://m.media-amazon.com/images/I/61HyUGR80VL._SL1433_.jpg'
);

-- Users
INSERT INTO Users
(
    UserName,
    Phone,
    Email,
    Password,
    Role
)
VALUES
(
    'Admin1',
    '0901111111',
    'a@gmail.com',
    'Pass@1234',
    'Admin'
),
(
    'Reader1',
    '0902222222',
    'b@gmail.com',
    'Pass@1234',
    'Reader'
),
(
    'Reader2',
    '0903333333',
    'c@gmail.com',
    'Pass@1234',
    'Reader'
);

-- Borrow
INSERT INTO Borrow
(
    ReaderId,
    BookId,
    BorrowDate,
    ReturnDate,
    Status
)
VALUES
(
    1,
    1,
    '2026-07-01',
    '2026-07-08',
    'Returned'
),
(
    2,
    2,
    '2026-07-05',
    NULL,
    'Borrowing'
),
(
    3,
    4,
    '2026-07-06',
    NULL,
    'Borrowing'
);
