USE LibraryDB;
GO

-- ======================
-- 1. SystemConfigs Table
-- ======================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SystemConfigs]') AND type in (N'U'))
BEGIN
    CREATE TABLE SystemConfigs
    (
        ConfigId INT IDENTITY(1,1) PRIMARY KEY,
        ConfigKey VARCHAR(50) NOT NULL UNIQUE,
        ConfigValue NVARCHAR(500) NOT NULL,
        Description NVARCHAR(500) NULL
    );

    -- Insert Default Configurations
    INSERT INTO SystemConfigs (ConfigKey, ConfigValue, Description)
    VALUES 
    ('MaxBorrowDays', '14', 'Maximum number of days a book can be borrowed'),
    ('LateFeePerDay', '5000', 'Penalty fee per day for late returns (VND)'),
    ('MaxBooksPerReader', '3', 'Maximum number of books a reader can borrow at the same time');
END
GO

-- ======================
-- 2. Fines Table
-- ======================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Fines]') AND type in (N'U'))
BEGIN
    CREATE TABLE Fines
    (
        FineId INT IDENTITY(1,1) PRIMARY KEY,
        BorrowId INT NOT NULL,
        Amount DECIMAL(18,2) NOT NULL,
        Reason NVARCHAR(255) NULL,
        Status NVARCHAR(50) NOT NULL, -- 'Unpaid' or 'Paid'
        CreatedDate DATE NOT NULL,
        PaidDate DATE NULL,

        CONSTRAINT FK_Fines_Borrow
            FOREIGN KEY(BorrowId)
            REFERENCES Borrow(BorrowId)
    );
END
GO

-- ======================
-- 3. BookReviews Table
-- ======================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BookReviews]') AND type in (N'U'))
BEGIN
    CREATE TABLE BookReviews
    (
        ReviewId INT IDENTITY(1,1) PRIMARY KEY,
        BookId INT NOT NULL,
        UserId INT NOT NULL,
        Rating INT NOT NULL, -- 1 to 5
        Comment NVARCHAR(1000) NULL,
        ReviewDate DATETIME NOT NULL DEFAULT GETDATE(),

        CONSTRAINT FK_BookReviews_Book
            FOREIGN KEY(BookId)
            REFERENCES Book(BookId),

        CONSTRAINT FK_BookReviews_Users
            FOREIGN KEY(UserId)
            REFERENCES Users(UserId)
    );
END
GO
