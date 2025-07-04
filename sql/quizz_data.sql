PRAGMA foreign_keys=OFF;
BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "__EFMigrationsLock" (

    "Id" INTEGER NOT NULL CONSTRAINT "PK___EFMigrationsLock" PRIMARY KEY,

    "Timestamp" TEXT NOT NULL

);
CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (

    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,

    "ProductVersion" TEXT NOT NULL

);
INSERT INTO __EFMigrationsHistory VALUES('20250528100545_InitialCreate','9.0.5');
CREATE TABLE IF NOT EXISTS "Quizzes" (

    "Id" INTEGER NOT NULL CONSTRAINT "PK_Quizzes" PRIMARY KEY AUTOINCREMENT,

    "Title" TEXT NOT NULL,

    "PassingRate" REAL NOT NULL

);
INSERT INTO Quizzes VALUES(1,'HTML/CSS/JS/C# Quiz',0.5999999999999999778);
CREATE TABLE IF NOT EXISTS "Questions" (

    "Id" INTEGER NOT NULL CONSTRAINT "PK_Questions" PRIMARY KEY AUTOINCREMENT,

    "Text" TEXT NOT NULL,

    "PassingRate" REAL NOT NULL,

    "QuizId" INTEGER NOT NULL,

    CONSTRAINT "FK_Questions_Quizzes_QuizId" FOREIGN KEY ("QuizId") REFERENCES "Quizzes" ("Id") ON DELETE CASCADE

);
INSERT INTO Questions VALUES(1,'What does HTML stand for?',1.0,1);
INSERT INTO Questions VALUES(2,'Which HTML element is used to define internal CSS?',1.0,1);
INSERT INTO Questions VALUES(3,'Which CSS property controls the text size?',1.0,1);
INSERT INTO Questions VALUES(4,'How do you select an element with id ''demo'' in CSS?',1.0,1);
INSERT INTO Questions VALUES(5,'Which event occurs when the user clicks on an HTML element in JS?',1.0,1);
INSERT INTO Questions VALUES(6,'Which keyword is used to declare a variable in JavaScript?',1.0,1);
INSERT INTO Questions VALUES(7,'Which symbol is used for comments in C#?',1.0,1);
INSERT INTO Questions VALUES(8,'What does ''async'' mean in C#?',1.0,1);
INSERT INTO Questions VALUES(9,'Which method is used to query a table in EF Core?',1.0,1);
INSERT INTO Questions VALUES(10,'What is the default HTTP method for form submission in HTML?',1.0,1);
CREATE TABLE IF NOT EXISTS "QuizResults" (

    "Id" INTEGER NOT NULL CONSTRAINT "PK_QuizResults" PRIMARY KEY AUTOINCREMENT,

    "Score" INTEGER NOT NULL,

    "Passed" INTEGER NOT NULL,

    "StartTime" TEXT NOT NULL,

    "EndTime" TEXT NOT NULL,

    "QuizId" INTEGER NOT NULL,

    CONSTRAINT "FK_QuizResults_Quizzes_QuizId" FOREIGN KEY ("QuizId") REFERENCES "Quizzes" ("Id") ON DELETE CASCADE

);
INSERT INTO QuizResults VALUES(1,0,0,'2025-05-28 12:16:22.4697964','2025-05-28 20:18:44.6356117',1);
INSERT INTO QuizResults VALUES(2,0,0,'2025-05-28 12:16:33.1529213','2025-05-28 20:18:15.6266609',1);
INSERT INTO QuizResults VALUES(3,0,0,'2025-05-28 12:19:30.0940297','0001-01-01 00:00:00',1);
INSERT INTO QuizResults VALUES(4,0,0,'2025-05-28 12:21:06.8574398','0001-01-01 00:00:00',1);
INSERT INTO QuizResults VALUES(5,0,0,'2025-05-28 19:19:12.9146415','0001-01-01 00:00:00',1);
INSERT INTO QuizResults VALUES(6,0,0,'2025-05-28 19:19:48.7159102','0001-01-01 00:00:00',1);
INSERT INTO QuizResults VALUES(7,0,0,'2025-05-28 19:19:56.3372021','0001-01-01 00:00:00',1);
INSERT INTO QuizResults VALUES(8,0,0,'2025-05-28 19:21:03.1869539','0001-01-01 00:00:00',1);
INSERT INTO QuizResults VALUES(9,0,0,'2025-05-28 19:27:56.8827392','0001-01-01 00:00:00',1);
INSERT INTO QuizResults VALUES(10,0,0,'2025-05-28 19:28:41.6711397','0001-01-01 00:00:00',1);
CREATE TABLE IF NOT EXISTS "Answers" (

    "Id" INTEGER NOT NULL CONSTRAINT "PK_Answers" PRIMARY KEY AUTOINCREMENT,

    "Text" TEXT NOT NULL,

    "IsCorrect" INTEGER NOT NULL,

    "QuestionId" INTEGER NOT NULL,

    CONSTRAINT "FK_Answers_Questions_QuestionId" FOREIGN KEY ("QuestionId") REFERENCES "Questions" ("Id") ON DELETE CASCADE

);
INSERT INTO Answers VALUES(1,'Hyper Text Markup Language',1,1);
INSERT INTO Answers VALUES(2,'Home Tool Markup Language',0,1);
INSERT INTO Answers VALUES(3,'Hyperlinks and Text Markup Language',0,1);
INSERT INTO Answers VALUES(4,'Hyper Tool Multi Language',0,1);
INSERT INTO Answers VALUES(5,'<style>',1,2);
INSERT INTO Answers VALUES(6,'<css>',0,2);
INSERT INTO Answers VALUES(7,'<script>',0,2);
INSERT INTO Answers VALUES(8,'<link>',0,2);
INSERT INTO Answers VALUES(9,'font-size',1,3);
INSERT INTO Answers VALUES(10,'text-size',0,3);
INSERT INTO Answers VALUES(11,'font-style',0,3);
INSERT INTO Answers VALUES(12,'text-style',0,3);
INSERT INTO Answers VALUES(13,'#demo',1,4);
INSERT INTO Answers VALUES(14,'.demo',0,4);
INSERT INTO Answers VALUES(15,'demo',0,4);
INSERT INTO Answers VALUES(16,'*demo',0,4);
INSERT INTO Answers VALUES(17,'onclick',1,5);
INSERT INTO Answers VALUES(18,'onchange',0,5);
INSERT INTO Answers VALUES(19,'onmouseclick',0,5);
INSERT INTO Answers VALUES(20,'onmouseover',0,5);
INSERT INTO Answers VALUES(21,'let',1,6);
INSERT INTO Answers VALUES(22,'int',0,6);
INSERT INTO Answers VALUES(23,'var',0,6);
INSERT INTO Answers VALUES(24,'String',0,6);
INSERT INTO Answers VALUES(25,'//',1,7);
INSERT INTO Answers VALUES(26,'<!-- -->',0,7);
INSERT INTO Answers VALUES(27,'#',0,7);
INSERT INTO Answers VALUES(28,'/* */',0,7);
INSERT INTO Answers VALUES(29,'Allows a method to be awaited',1,8);
INSERT INTO Answers VALUES(30,'Defines a static method',0,8);
INSERT INTO Answers VALUES(31,'Makes a method synchronous',0,8);
INSERT INTO Answers VALUES(32,'Marks method for encryption',0,8);
INSERT INTO Answers VALUES(33,'DbSet.ToList()',1,9);
INSERT INTO Answers VALUES(34,'DbSet.Read()',0,9);
INSERT INTO Answers VALUES(35,'DbSet.FindAll()',0,9);
INSERT INTO Answers VALUES(36,'DbSet.SelectAll()',0,9);
INSERT INTO Answers VALUES(37,'GET',1,10);
INSERT INTO Answers VALUES(38,'POST',0,10);
INSERT INTO Answers VALUES(39,'PUT',0,10);
INSERT INTO Answers VALUES(40,'DELETE',0,10);
CREATE TABLE IF NOT EXISTS "AnswerUsers" (

    "Id" INTEGER NOT NULL CONSTRAINT "PK_AnswerUsers" PRIMARY KEY AUTOINCREMENT,

    "QuestionId" INTEGER NOT NULL,

    "AnswerSelectedId" INTEGER NOT NULL,

    "IsCorrect" INTEGER NOT NULL,

    "QuizResultId" INTEGER NOT NULL,

    CONSTRAINT "FK_AnswerUsers_Answers_AnswerSelectedId" FOREIGN KEY ("AnswerSelectedId") REFERENCES "Answers" ("Id") ON DELETE RESTRICT,

    CONSTRAINT "FK_AnswerUsers_Questions_QuestionId" FOREIGN KEY ("QuestionId") REFERENCES "Questions" ("Id") ON DELETE RESTRICT,

    CONSTRAINT "FK_AnswerUsers_QuizResults_QuizResultId" FOREIGN KEY ("QuizResultId") REFERENCES "QuizResults" ("Id") ON DELETE CASCADE

);
INSERT INTO AnswerUsers VALUES(1,1,1,1,1);
INSERT INTO AnswerUsers VALUES(2,1,1,1,1);
INSERT INTO sqlite_sequence VALUES('Quizzes',1);
INSERT INTO sqlite_sequence VALUES('Questions',10);
INSERT INTO sqlite_sequence VALUES('Answers',40);
INSERT INTO sqlite_sequence VALUES('QuizResults',10);
INSERT INTO sqlite_sequence VALUES('AnswerUsers',2);
CREATE INDEX "IX_Answers_QuestionId" ON "Answers" ("QuestionId");
CREATE INDEX "IX_AnswerUsers_AnswerSelectedId" ON "AnswerUsers" ("AnswerSelectedId");
CREATE INDEX "IX_AnswerUsers_QuestionId" ON "AnswerUsers" ("QuestionId");
CREATE INDEX "IX_AnswerUsers_QuizResultId" ON "AnswerUsers" ("QuizResultId");
CREATE INDEX "IX_Questions_QuizId" ON "Questions" ("QuizId");
CREATE INDEX "IX_QuizResults_QuizId" ON "QuizResults" ("QuizId");
COMMIT;
