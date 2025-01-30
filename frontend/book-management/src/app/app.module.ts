import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';  // Import HttpClientModule
import { FormsModule } from '@angular/forms';  // Import FormsModule
import { AppComponent } from './app.component';
import { BooksComponent } from './books/books.component';  // Import BooksComponent
import { RouterModule } from '@angular/router';  // Import RouterModule for routing
import { AppRoutingModule } from './app-routing.module';  // Import AppRoutingModule

@NgModule({
  declarations: [
    AppComponent,
    BooksComponent  // Declare the BooksComponent here
  ],
  imports: [
    BrowserModule,
    HttpClientModule,  // Add HttpClientModule here to make HTTP requests
    FormsModule,  // Add FormsModule here to enable ngModel
    AppRoutingModule,  // Add AppRoutingModule here for routing
    RouterModule  // Add RouterModule to enable routing
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
