# QuizzApp

## Hướng Dẫn Chạy Repository, Service, Controller và Swagger UI

### 1. Tầng Repository

Tầng **Repository** (thư mục `Repository/`) chịu trách nhiệm truy cập và thao tác dữ liệu bằng Entity Framework Core với SQLite.

- `IQuizRepository.cs`: Khai báo các hàm thao tác dữ liệu.
- `QuizRepository.cs`: Cài đặt các hàm CRUD (tạo, đọc, cập nhật, xóa) liên quan đến bài quiz, câu hỏi, câu trả lời, v.v.

### 2. Tầng Service

Tầng **Service** (thư mục `Service/`) chứa các logic nghiệp vụ và kết nối giữa Controller và Repository.

- `IQuizService.cs`: Khai báo các dịch vụ liên quan đến xử lý logic quiz.
- `QuizService.cs`: Cài đặt các chức năng nghiệp vụ, sử dụng Repository để thao tác với dữ liệu.

### 3. Tầng Controller

Tầng **Controller** (thư mục `Controllers/`) định nghĩa các API endpoint để frontend hoặc công cụ như Postman gọi.

- `QuizController.cs`: Nhận yêu cầu HTTP, gọi các phương thức từ Service và trả kết quả.

### 4. Swagger UI (Giao Diện Kiểm Thử API)

Dự án này đã tích hợp **Swagger UI** thông qua gói \[Swashbuckle.AspNetCore] giúp bạn kiểm thử API trực tiếp trên trình duyệt.

- Swagger được bật mặc định trong môi trường phát triển.
- Sau khi chạy ứng dụng, mở trình duyệt và truy cập:

```
https://localhost:7284/swagger/index.html
```

hoặc (nếu chạy bằng HTTP):

```
http://localhost:5003/swagger/index.html
```

Tại đây bạn sẽ thấy tất cả các endpoint (`GET`, `POST`, ...) với định dạng yêu cầu/trả về, và có thể thử trực tiếp.

---

## Công Nghệ và Thư Viện Sử Dụng

- **.NET 9 / ASP.NET Core** – Framework xây dựng Web API
- **Entity Framework Core** – ORM truy cập cơ sở dữ liệu
- **SQLite** – Cơ sở dữ liệu nhẹ, lưu trữ dạng file
- **Swashbuckle.AspNetCore (Swagger)** – Tài liệu & giao diện thử API
- **dotnet CLI** – Công cụ dòng lệnh để build, chạy, reload tự động

---

## Cấu Trúc Dự Án

```
QuizzApp/
├── Controllers/
│   └── QuizController.cs              # Các endpoint của API quiz
├── Data/
│   ├── AppDbContext.cs                # DbContext của EF Core
│   └── QuizData.cs                    # Dữ liệu seed cho quiz và câu hỏi
├── Models/
│   ├── Answer.cs                      # Entity Câu trả lời
│   ├── AnswerUser.cs                  # Entity Câu trả lời của người dùng
│   ├── Question.cs                    # Entity Câu hỏi
│   ├── Quiz.cs                        # Entity Quiz
│   ├── QuizResult.cs                  # Entity Kết quả quiz
│   └── DTOs/
│       └── SubmitAnswerDto.cs         # DTO dùng khi nộp câu trả lời
├── Repository/
│   ├── IQuizRepository.cs             # Interface cho repository
│   └── QuizRepository.cs              # Cài đặt repository
├── Service/
│   ├── IQuizService.cs                # Interface cho service
│   └── QuizService.cs                 # Cài đặt service
├── Migrations/
├── sql/
│   └── quizz_data.sql                 # Script SQL để seed dữ liệu
├── Program.cs                         # Điểm khởi chạy chương trình
├── appsettings.json                   # File cấu hình chung
├── appsettings.Development.json       # Cấu hình cho môi trường phát triển
├── quizz.db                           # File cơ sở dữ liệu SQLite
├── QuizzApp.csproj                    # File cấu hình dự án .NET
├── QuizzApp.sln                       # Solution file
└── README.md                          # Tài liệu dự án
```

---

## Các API Chính trong `QuizController`

- **\[GET] /api/quiz/{quizId}/questions**
  Trả về danh sách câu hỏi và đáp án theo mã bài quiz.

- **\[POST] /api/quiz/submit-answer**
  Gửi câu trả lời cho một câu hỏi. Body gồm `QuizResultId`, `QuestionId`, `AnswerSelectedId`.

- **\[GET] /api/quiz/result/{quizResultId}**
  Trả về kết quả của một lần làm quiz.

- **\[POST] /api/quiz/{quizId}/start**
  Bắt đầu làm một bài quiz mới và trả về `QuizResultId`.

- **\[POST] /api/quiz/result/{quizResultId}/end**
  Kết thúc bài quiz, lưu thời điểm hoàn thành.

Tất cả các endpoint đều có trong Swagger để bạn kiểm thử trực tiếp.

---

## Cách Chạy Dự Án (Hot Reload + Swagger)

```sh
dotnet build         # Biên dịch dự án
dotnet watch run     # Chạy và tự động reload khi có thay đổi
```

## Sơ đồ luồng người dùng

![User-flow](./asset/user-flow-diagram.jpg)

## Sơ đồ quan hệ thực thể

![ERD](./asset/entity-relationship-diagram.jpg)
