import { Component, OnInit } from '@angular/core';
import { BookService } from '../book.service'; 

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.scss']
})
export class BooksComponent implements OnInit {
  books: any[] = [];
  newBook = { id: '', title: '', author: '', ISBN: '', publicationDate: '' };

  constructor(private bookService: BookService) { }

  ngOnInit(): void {
    this.getBooks();  // Get all the Books when page loads
  }

  // Method to request to get all books from the backend
  getBooks(): void {
    this.bookService.getBooks().subscribe((data: any) => {
      this.books = data; 
    });
  }

  // Method to add a new book to the backend and reset the form
  addBook(): void {
    // If the publicationDate is provided, ensure it's in the correct format (ISO 8601)
    if (this.newBook.publicationDate) {
      this.newBook.publicationDate = new Date(this.newBook.publicationDate).toISOString().split('T')[0];
    }

    // Check if id exists, to either update or add a new book
    if (this.newBook.id) {
      // If book exists (update)
      this.bookService.updateBook(this.newBook).subscribe(() => {
        this.getBooks();  // Refreshing the books list
        this.resetForm();  // Resetting the form after the update
      });
    } else {
      // Otherwise, add a new book (POST request)
      this.bookService.addBook(this.newBook).subscribe(() => {
        this.getBooks();  // Refreshing the books list
        this.resetForm();  // Resetting the form after adding
      });
    }
  }

  // Method to delete a book by ID
  deleteBook(id: string): void {
    this.bookService.deleteBook(id).subscribe(() => {
      this.getBooks();  // Refreshing the books list after deletion
    });
  }

  // Method to pre-fill the form with the data of the selected book for editing
  editBook(book: any): void {
    this.newBook = { ...book }; 
  }

  // Reset the form after adding or updating a book
  resetForm(): void {
    this.newBook = { id: '', title: '', author: '', ISBN: '', publicationDate: '' };
  }
}
