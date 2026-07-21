# Tài liệu Đặc tả Giao diện Người dùng (UI Screens Specification)

Dưới đây là danh sách toàn bộ các màn hình trong phần mềm quản lý thư viện của chúng ta, kèm theo bảng mô tả chi tiết các trường dữ liệu (Field Name, Field Type, Description) theo đúng chuẩn như ảnh mẫu của bạn.

---

### 1. Màn hình Đăng nhập (LoginWindow)
Màn hình đầu tiên người dùng nhìn thấy khi mở ứng dụng.

| Field Name | Field Type | Description |
| :--- | :--- | :--- |
| **Username** | Text Box | This is for user to input user name for logging in |
| **Password** | Password Box | This is for user to input password for logging in |
| **Login** | Button | User clicks to authenticate him/herself into the system with provided email/user name & password |
| **Register** | Button | User clicks to navigate to the account creation screen |

---

### 2. Màn hình Đăng ký (RegisterWindow)
Dành cho độc giả mới tạo tài khoản.

| Field Name | Field Type | Description |
| :--- | :--- | :--- |
| **Full Name** | Text Box | This is for user to input their full legal name |
| **Username** | Text Box | This is for user to input their desired login username |
| **Password** | Password Box | This is for user to input their desired secure password |
| **Register** | Button | User clicks to create a new account with the provided details |
| **Back** | Button | User clicks to return to the Login screen |

---

### 3. Màn hình Chính / Trang chủ (MainWindow)
Bảng điều khiển (Dashboard) chính hiển thị danh sách sách và các menu điều hướng.

| Field Name | Field Type | Description |
| :--- | :--- | :--- |
| **Search** | Text Box | This is for user to input text to search books by title or author |
| **Readers** | Button | Admin clicks to open the Reader Management screen |
| **Categories** | Button | Admin clicks to open the Category Management screen |
| **Add Book** | Button | Admin clicks to open the Add Book screen |
| **Fines** | Button | User clicks to open the Fines Management screen |
| **Settings** | Button | Admin clicks to open the System Settings configuration screen |
| **My Borrows** | Button | User clicks to open their personal borrowed books screen |
| **Logout** | Button | User clicks to log out of the system |
| **Book List** | Items Control | Displays the grid of available books in the library catalog |
| **Info** | Button | User clicks to view detailed book information and read/write reviews |
| **Borrow** | Button | User clicks to borrow the selected book |
| **Update** | Button | Admin clicks to edit the selected book's details |
| **Delete** | Button | Admin clicks to permanently delete the selected book from the catalog |

---

### 4. Màn hình Thêm sách mới (AddBook)
Dành cho Admin để nhập kho một cuốn sách mới.

| Field Name | Field Type | Description |
| :--- | :--- | :--- |
| **Title** | Text Box | This is for admin to input the book's title |
| **Author** | Text Box | This is for admin to input the book's author name |
| **Year Published** | Text Box | This is for admin to input the publication year |
| **Publisher** | Text Box | This is for admin to input the publisher's name |
| **Description** | Text Box | This is for admin to input a summary or description of the book |
| **Category** | Combo Box | Admin selects the appropriate genre/category for the book |
| **Quantity** | Text Box | This is for admin to input the number of available copies |
| **Image URL** | Text Box | This is for admin to input the web link to the book's cover image |
| **Save** | Button | Admin clicks to insert the new book into the database |
| **Close** | Button | Admin clicks to cancel and close the window |

---

### 5. Màn hình Cập nhật sách (UpdateBook)
Dành cho Admin để chỉnh sửa thông tin sách đã có.

| Field Name | Field Type | Description |
| :--- | :--- | :--- |
| **Title** | Text Box | This is for admin to modify the book's title |
| **Author** | Text Box | This is for admin to modify the book's author name |
| **Year Published** | Text Box | This is for admin to modify the publication year |
| **Publisher** | Text Box | This is for admin to modify the publisher's name |
| **Description** | Text Box | This is for admin to modify the summary or description of the book |
| **Category** | Combo Box | Admin changes the appropriate genre/category for the book |
| **Quantity** | Text Box | This is for admin to modify the number of available copies |
| **Image URL** | Text Box | This is for admin to modify the web link to the book's cover image |
| **Update Book** | Button | Admin clicks to save the modified information to the database |
| **Close** | Button | Admin clicks to cancel and close the window |

---

### 6. Màn hình Sách đang mượn (MyBorrow)
Dành cho Độc giả quản lý và trả những cuốn sách đang mượn.

| Field Name | Field Type | Description |
| :--- | :--- | :--- |
| **Search** | Text Box | This is for user to input text to search their borrowed books |
| **Borrowed Books List**| Data Grid | Displays the user's active borrow records and return status |
| **Return Book** | Button | User clicks to return the selected book back to the library |
| **Back** | Button | User clicks to return to the Dashboard |

---

### 7. Màn hình Quản lý Độc giả (ReaderManagement)
Dành cho Admin để kiểm soát thành viên.

| Field Name | Field Type | Description |
| :--- | :--- | :--- |
| **Search** | Text Box | This is for admin to search readers by username or name |
| **Readers List** | Data Grid | Displays all registered readers in the system |
| **Delete** | Button | Admin clicks to delete a reader's account from the system |
| **Back** | Button | Admin clicks to return to the Dashboard |

---

### 8. Màn hình Quản lý Thể loại (CategoryManagement)
Dành cho Admin thêm/sửa/xóa các danh mục sách.

| Field Name | Field Type | Description |
| :--- | :--- | :--- |
| **Category Name** | Text Box | This is for admin to input a new or existing category name |
| **Categories List** | Data Grid | Displays all available book categories |
| **Add** | Button | Admin clicks to insert a new category |
| **Update** | Button | Admin clicks to save changes to the selected category |
| **Delete** | Button | Admin clicks to remove a category |
| **Clear** | Button | Admin clicks to clear the input text field |
| **Back** | Button | Admin clicks to return to the Dashboard |

---

### 9. Màn hình Chi tiết Sách & Đánh giá (BookDetails)
Cho phép người dùng xem review và chấm điểm sách.

| Field Name | Field Type | Description |
| :--- | :--- | :--- |
| **Rating (1-5)** | Combo Box | User selects a star rating (1 to 5) for the book |
| **Comment** | Text Box | This is for user to input their text review/feedback for the book |
| **Submit Review** | Button | User clicks to save their rating and comment |
| **Reviews List** | List View | Displays all reviews submitted by users for this book |
| **Back** | Button | User clicks to close the details window |

---

### 10. Màn hình Cài đặt Hệ thống (SystemSettings)
Dành cho Admin thay đổi quy tắc thư viện.

| Field Name | Field Type | Description |
| :--- | :--- | :--- |
| **Config Value** | Text Box | Dynamic input fields for changing rules (e.g., Max Borrow Days, Late Fees) |
| **Save Changes** | Button | Admin clicks to apply all modified settings across the system |
| **Back to Dashboard** | Button | Admin clicks to return to the Dashboard |

---

### 11. Màn hình Quản lý Tiền phạt (FinesManagement)
Hiển thị danh sách phạt tự động và thủ công.

| Field Name | Field Type | Description |
| :--- | :--- | :--- |
| **Select Borrow Record**| Combo Box | Admin selects an active borrow record to issue a manual fine |
| **Amount** | Text Box | This is for admin to input the monetary fine amount |
| **Reason** | Text Box | This is for admin to input the reason for the fine (e.g., Damaged Book) |
| **Create Fine** | Button | Admin clicks to generate and save the manual fine |
| **Fines List** | Data Grid | Displays all automatic and manual fines (Reader only sees their own) |
| **Mark as Paid** | Button | Admin clicks to update a fine's status from Unpaid to Paid |
| **Back to Dashboard** | Button | User clicks to return to the Dashboard |
