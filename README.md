# StudentRecordMS-Solution

SQL 
_____________________

CREATE DATABASE StudentRecordDB;
USE StudentRecordDB;

-- 2. Roles Table
CREATE TABLE TblRole (
    RoleId INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(20) NOT NULL,
    IsActive BIT DEFAULT 1
);

-- 3. Users Table
CREATE TABLE TblUser (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    UserName NVARCHAR(30) NOT NULL,
    UserPassword NVARCHAR(30) NOT NULL,
    RoleId INT NOT NULL,
    IsActive BIT DEFAULT 1,
    CONSTRAINT FK_TblUser_TblRole FOREIGN KEY (RoleId) REFERENCES TblRole(RoleId)
);

-- 4. Students Table
CREATE TABLE Student (
    Id INT PRIMARY KEY IDENTITY(1,1),
    RollNumber NVARCHAR(5) NOT NULL UNIQUE,
    Name NVARCHAR(30) NOT NULL,
    Maths INT NOT NULL CHECK (Maths BETWEEN 1 AND 100),
    Physics INT NOT NULL CHECK (Physics BETWEEN 1 AND 100),
    Chemistry INT NOT NULL CHECK (Chemistry BETWEEN 1 AND 100),
    English INT NOT NULL CHECK (English BETWEEN 1 AND 100),
    Programming INT NOT NULL CHECK (Programming BETWEEN 1 AND 100),
    IsActive BIT DEFAULT 1,
    UserId INT NULL,
    CONSTRAINT FK_Student_TblUser FOREIGN KEY (UserId) REFERENCES TblUser(UserId)
);

INSERT INTO TblRole (RoleName)
VALUES ('Invigilator'), ('Student');

-- Invigilator
INSERT INTO TblUser (UserName, UserPassword, RoleId)
VALUES ('admin', 'admin01', 1);

-- Students (Users)
INSERT INTO TblUser (UserName, UserPassword, RoleId)
VALUES ('neha', 'neha123', 2);

INSERT INTO TblUser (UserName, UserPassword, RoleId)
VALUES ('bibitha', 'bibitha123', 2);

-- Student Records
INSERT INTO Student (RollNumber, Name, Maths, Physics, Chemistry, English, Programming, IsActive, UserId)
VALUES ('10001', 'Neha S', 88, 91, 85, 79, 93, 1, 2);

INSERT INTO Student (RollNumber, Name, Maths, Physics, Chemistry, English, Programming, IsActive, UserId)
VALUES ('10002', 'Bibitha K', 76, 84, 80, 88, 90, 1, 3);


-- ===================== STORED PROCEDURES =====================

-- 9. Authenticate User
CREATE PROCEDURE sp_AuthenticateUser
    @UserName NVARCHAR(30),
    @UserPassword NVARCHAR(30)
AS
BEGIN
    SELECT u.UserId, u.UserName, u.UserPassword, u.RoleId, u.IsActive,
           r.RoleName
    FROM TblUser u
    INNER JOIN TblRole r ON u.RoleId = r.RoleId
    WHERE u.UserName = @UserName AND u.UserPassword = @UserPassword AND u.IsActive = 1;
END

-- 10. Get All Students
CREATE PROCEDURE sp_GetAllStudents
AS
BEGIN
    SELECT Id, RollNumber, Name, Maths, Physics, Chemistry, English, Programming, IsActive
    FROM Student
    WHERE IsActive = 1
    ORDER BY Id;
END

-- 11. Get Student By RollNumber
CREATE PROCEDURE sp_GetStudentByRollNumber
    @RollNumber NVARCHAR(5)
AS
BEGIN
    SELECT Id, RollNumber, Name, Maths, Physics, Chemistry, English, Programming, IsActive
    FROM Student
    WHERE RollNumber = @RollNumber;
END

-- 12. Get Student By UserId (for Student login)
CREATE PROCEDURE sp_GetStudentByUserId
    @UserId INT
AS
BEGIN
    SELECT Id, RollNumber, Name, Maths, Physics, Chemistry, English, Programming, IsActive
    FROM Student
    WHERE UserId = @UserId AND IsActive = 1;
END

-- 13. Add Student
CREATE PROCEDURE sp_AddStudent
    @RollNumber NVARCHAR(5),
    @Name NVARCHAR(30),
    @Maths INT,
    @Physics INT,
    @Chemistry INT,
    @English INT,
    @Programming INT,
    @UserId INT = NULL
AS
BEGIN
    INSERT INTO Student (RollNumber, Name, Maths, Physics, Chemistry, English, Programming, IsActive, UserId)
    VALUES (@RollNumber, @Name, @Maths, @Physics, @Chemistry, @English, @Programming, 1, @UserId);
END

-- 14. Update Student Marks
CREATE PROCEDURE sp_UpdateStudentMarks
    @RollNumber NVARCHAR(5),
    @Maths INT,
    @Physics INT,
    @Chemistry INT,
    @English INT,
    @Programming INT
AS
BEGIN
    UPDATE Student SET
        Maths = @Maths,
        Physics = @Physics,
        Chemistry = @Chemistry,
        English = @English,
        Programming = @Programming
    WHERE RollNumber = @RollNumber;
END

-- 15. Delete (Disable) Student
CREATE PROCEDURE sp_DeleteStudent
    @RollNumber NVARCHAR(5)
AS
BEGIN
    UPDATE Student SET IsActive = 0 WHERE RollNumber = @RollNumber;
END

-- 16. Get Next Roll Number
CREATE PROCEDURE sp_GetNextRollNumber
AS
BEGIN
    SELECT CAST(ISNULL(MAX(CAST(RollNumber AS INT)), 10000) + 1 AS NVARCHAR(5)) AS NextRollNumber
    FROM Student;
END

INSERT INTO TblRole (RoleName)
VALUES ('Invigilator'), ('Student');

-- Invigilator
INSERT INTO TblUser (UserName, UserPassword, RoleId)
VALUES ('admin', 'admin01', 1);

-- Students (Users)
INSERT INTO TblUser (UserName, UserPassword, RoleId)
VALUES ('neha', 'neha123', 2);

INSERT INTO TblUser (UserName, UserPassword, RoleId)
VALUES ('bibitha', 'bibitha123', 2);

-- Student Records
INSERT INTO Student (RollNumber, Name, Maths, Physics, Chemistry, English, Programming, IsActive, UserId)
VALUES ('10001', 'Neha S', 88, 91, 85, 79, 93, 1, 2);

INSERT INTO Student (RollNumber, Name, Maths, Physics, Chemistry, English, Programming, IsActive, UserId)
VALUES ('10002', 'Bibitha K', 76, 84, 80, 88, 90, 1, 3);
