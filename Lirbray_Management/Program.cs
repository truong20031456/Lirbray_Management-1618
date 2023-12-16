using System;
using System.Collections.Generic;

namespace LibraryManager
{
    public class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();
            Library borrowedBooks = new Library();
            Library returnedBooks = new Library();

            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Thêm sách vào thư viện");
                Console.WriteLine("2. Hiển thị danh sách sách trong thư viện");
                Console.WriteLine("3. Cho mượn sách");
                Console.WriteLine("4. Hiển thị danh sách sách đã cho mượn");
                Console.WriteLine("5. Thu hồi sách");
                Console.WriteLine("6. Hiển thị danh sách sách đã thu hồi");
                Console.WriteLine("7. Xóa sách khỏi thư viện");
                Console.WriteLine("8. Thoát");

                Console.Write("Nhập lựa chọn của bạn: ");
                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Vui lòng nhập một số nguyên.");
                    Console.Write("Nhập lựa chọn của bạn: ");
                }

                switch (choice)
                {
                    case 1:
                        AddBookToLibrary(library);
                        break;
                    case 2:
                        Console.WriteLine("\nDanh sách sách trong thư viện:");
                        library.DisplayBooks();
                        break;
                    case 3:
                        BorrowBook(library, borrowedBooks);
                        break;
                    case 4:
                        Console.WriteLine("\nDanh sách sách đã cho mượn:");
                        borrowedBooks.DisplayBooks();
                        break;
                    case 5:
                        ReturnBook(borrowedBooks, returnedBooks);
                        break;
                    case 6:
                        Console.WriteLine("\nDanh sách sách đã thu hồi:");
                        returnedBooks.DisplayBooks();
                        break;
                    case 7:
                        RemoveBookFromLibrary(library);
                        break;
                    case 8:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn lại.");
                        break;
                }
            }
        }

        // Thêm sách vào thư viện
        static void AddBookToLibrary(Library library)
        {
            Console.WriteLine("\nNhập thông tin sách:");
            Console.Write("Tên sách: ");
            string bookName = Console.ReadLine();

            Console.Write("Mô tả: ");
            string bookDescription = Console.ReadLine();

            Console.Write("ID: ");
            int bookId;
            while (!int.TryParse(Console.ReadLine(), out bookId))
            {
                Console.WriteLine("Vui lòng nhập một số nguyên cho ID.");
                Console.Write("ID: ");
            }

            Console.Write("ISBN: ");
            string bookISBN = Console.ReadLine();

            Book newBook = new Book(bookName, bookDescription, bookId, bookISBN);
            library.AddBook(newBook);

            Console.WriteLine("Sách đã được thêm vào thư viện.");
        }

        // Cho mượn sách từ thư viện
        static void BorrowBook(Library library, Library borrowedBooks)
        {
            Console.Write("\nNhập ID của sách cần mượn: ");
            int bookIdToBorrow;
            while (!int.TryParse(Console.ReadLine(), out bookIdToBorrow))
            {
                Console.WriteLine("Vui lòng nhập một số nguyên cho ID.");
                Console.Write("Nhập ID của sách cần mượn: ");
            }

            Book bookToBorrow = library.FindBookById(bookIdToBorrow);
            if (bookToBorrow != null)
            {
                borrowedBooks.AddBook(bookToBorrow);
                library.RemoveBook(bookToBorrow);
                Console.WriteLine("Sách đã được cho mượn.");
            }
            else
            {
                Console.WriteLine("Không tìm thấy sách trong thư viện.");
            }
        }

        // Thu hồi sách đã cho mượn
        static void ReturnBook(Library borrowedBooks, Library returnedBooks)
        {
            Console.Write("\nNhập ID của sách cần thu hồi: ");
            int bookIdToReturn;
            while (!int.TryParse(Console.ReadLine(), out bookIdToReturn))
            {
                Console.WriteLine("Vui lòng nhập một số nguyên cho ID.");
                Console.Write("Nhập ID của sách cần thu hồi: ");
            }

            Book bookToReturn = borrowedBooks.FindBookById(bookIdToReturn);
            if (bookToReturn != null)
            {
                returnedBooks.AddBook(bookToReturn);
                borrowedBooks.RemoveBook(bookToReturn);
                Console.WriteLine("Sách đã được thu hồi.");
            }
            else
            {
                Console.WriteLine("Không tìm thấy sách đã cho mượn.");
            }
        }

        // Xóa sách khỏi thư viện
        static void RemoveBookFromLibrary(Library library)
        {
            Console.Write("\nNhập ID của sách cần xóa khỏi thư viện: ");
            int bookIdToRemove;
            while (!int.TryParse(Console.ReadLine(), out bookIdToRemove))
            {
                Console.WriteLine("Vui lòng nhập một số nguyên cho ID.");
                Console.Write("Nhập ID của sách cần xóa khỏi thư viện: ");
            }

            Book bookToRemove = library.FindBookById(bookIdToRemove);
            if (bookToRemove != null)
            {
                library.RemoveBook(bookToRemove);
                Console.WriteLine("Sách đã được xóa khỏi thư viện.");
            }
            else
            {
                Console.WriteLine("Không tìm thấy sách trong thư viện.");
            }
        }
    }

    public class Book
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public string ISBN { get; set; }

        public Book(string name, string description, int id, string isbn)
        {
            Name = name;
            Description = description;
            Id = id;
            ISBN = isbn;
        }
    }

    public class Library
    {
        private List<Book> books;

        public Library()
        {
            books = new List<Book>();
        }

        public void AddBook(Book book)
        {
            books.Add(book);
        }

        public void RemoveBook(Book book)
        {
            books.Remove(book);
        }

        public void DisplayBooks()
        {
            foreach (Book book in books)
            {
                Console.WriteLine($"ID: {book.Id}, Tên: {book.Name}, Mô tả: {book.Description}, ISBN: {book.ISBN}");
            }
        }

        public Book FindBookById(int id)
        {
            return books.Find(book => book.Id == id);
        }
    }
}
